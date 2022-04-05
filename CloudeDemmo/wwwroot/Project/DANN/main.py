import random
import os
import sys
import torch.backends.cudnn as cudnn
import torch.optim as optim
import torch.utils.data
import numpy as np
from torchvision import datasets
from torchvision import transforms
from model import CNNModel
import dataloader
from torch.autograd import Variable
import math
from thop import profile
from datetime import datetime
import matplotlib.pyplot as plt
from sklearn import manifold

cuda = torch.cuda.is_available()

model_root = 'models'

learning_rate = 1e-3
batch_size = 220
n_epoch = 200
classes = 10
x_num = 880
manual_seed = random.randint(1, 10000)
random.seed(manual_seed)
torch.manual_seed(manual_seed)


def step_decay(epoch, learning_rate):
    """
    learning rate step decay
    :param epoch: current training epoch
    :param learning_rate: initial learning rate
    :return: learning rate after step decay
    """
    initial_lrate = learning_rate
    drop = 0.8
    epochs_drop = 5.0
    lrate = initial_lrate * math.pow(drop, math.floor((1 + epoch) / epochs_drop)) #0.8 的 n 次方
    return lrate


SOURCE_NAME = 'CWRU_train_A.txt'
TARGET_NAME = 'CWRU_train_D.txt'

source_loader = dataloader.load_training_A(SOURCE_NAME, batch_size)
target_train_loader = dataloader.load_training_A(TARGET_NAME, batch_size)


# load model

my_net = CNNModel()

# setup optimizer

for p in my_net.parameters():
    p.requires_grad = True

# training
best_accu_t = 0.0

prev_time = datetime.now()
acc = []
ep = []
acc_c = []
ep_c = []
matrix = np.zeros((classes, classes))
out_s = np.zeros((x_num, classes))
out_s_l = np.zeros((x_num, 1))
out_t = np.zeros((x_num, classes))
out_t_l = np.zeros((x_num, 1))
sum = np.zeros((2*x_num, classes))
sum_l = np.zeros((2*x_num, 1))

for epoch in range(n_epoch):
    lr = step_decay(epoch, learning_rate)
    optimizer = optim.Adam(my_net.parameters(), lr=lr)

    loss_class = torch.nn.CrossEntropyLoss()
    loss_domain = torch.nn.CrossEntropyLoss()
    if cuda:
        my_net = my_net.cuda()
        loss_class = loss_class.cuda()
        loss_domain = loss_domain.cuda()

    correct = 0
    total_loss = 0
    len_dataloader = min(len(source_loader), len(target_train_loader))
    data_source_iter = iter(source_loader)
    data_target_iter = iter(target_train_loader)
    k = 0
    for i in range(len_dataloader):

        p = float(i + epoch * len_dataloader) / n_epoch / len_dataloader
        alpha = 2. / (1. + np.exp(-10 * p)) - 1
        # training model using source data
        data_source = data_source_iter.next()
        s_img, s_label = data_source
        optimizer.zero_grad()
        # my_net.zero_grad()
        batch_size = len(s_label)

        domain_label1 = torch.zeros(batch_size).long()

        if cuda:
            s_img = s_img.cuda()
            s_label = s_label.cuda()
            domain_label1 = domain_label1.cuda()


        class_output, domain_output = my_net(input_data=s_img, alpha=alpha)
        #flops and params
        if epoch == 0 and i == 0:
            flops, params = profile(my_net, (s_img,alpha,))
            print('flops: ', flops, 'params: ', params)

        preds = class_output.data.max(1, keepdim=True)[1]
        correct += preds.eq(s_label.data.view_as(preds)).sum()


        err_s_label = loss_class(class_output, s_label.long())
        err_s_domain = loss_domain(domain_output, domain_label1.long())
        total_loss += err_s_label
        # training model using target data
        data_target = data_target_iter.next()
        t_img, _ = data_target

        batch_size = len(t_img)

        domain_label = torch.ones(batch_size).long()

        if cuda:
            t_img = t_img.cuda()
            domain_label = domain_label.cuda()

        _, domain_output = my_net(input_data=t_img, alpha=alpha)
        err_t_domain = loss_domain(domain_output, domain_label)
        #开始对抗进行反向传播
        err = err_s_label + err_t_domain + err_s_domain
        err.backward()
        optimizer.step()

        if epoch == n_epoch - 1:
            out_source = class_output.detach().cpu().numpy()
            out_s[k * batch_size:(k + 1) * batch_size, :] = out_source
            out_label = s_label.cpu().reshape(batch_size, 1)
            out_s_l[k * batch_size:(k + 1) * batch_size, :] = out_label
            k += 1

        sys.stdout.write('\r epoch: %d, [iter: %d / all %d], err_s_label: %f, err_s_domain: %f, err_t_domain: %f' \
              % (epoch, i + 1, len_dataloader, err_s_label.data.cpu().numpy(),
                 err_s_domain.data.cpu().numpy(), err_t_domain.data.cpu().item()))
        sys.stdout.flush()
        torch.save(my_net, '{0}/mnist_mnistm_model_epoch_current.pth'.format(model_root))
#训练集准确率
    total_loss /= len(source_loader)
    acc_train = float(correct) * 100. / (len(source_loader) * batch_size)
    acc.append(acc_train)
    ep.append(epoch)

    print('{} set: Average classification loss: {:.4f}, Accuracy: {}/{} ({:.2f}%)'.format(
        SOURCE_NAME, total_loss.item(), correct, len(source_loader.dataset), acc_train))

#测试集预测
    with torch.no_grad():
        clf_criterion = torch.nn.CrossEntropyLoss()

        my_net.eval()
        test_loss = 0
        correct = 0
        len_dataloader2 = len(target_train_loader)

        q = 0
        data_target_test = iter(target_train_loader)
        for i in range(len_dataloader2):
            p = float(i + epoch * len_dataloader2) / n_epoch / len_dataloader2
            alpha = 2. / (1. + np.exp(-10 * p)) - 1
            # training model using source data
            test_target = data_target_test.next()

            t_img, t_label = test_target

        # for data, target in target_test_loader:  # 一次取所有数据
            if cuda:
                t_img, t_label = t_img.cuda(), t_label.cuda()
            t_img, t_label = Variable(t_img), Variable(t_label)
            target_preds, _ = my_net(input_data=t_img,alpha=alpha)


            if epoch == n_epoch - 1:
                out_t[q * batch_size:(q + 1) * batch_size, :] = target_preds.cpu()
                out_label = t_label.cpu().reshape(batch_size, 1)
                out_t_l[q * batch_size:(q + 1) * batch_size, :] = out_label
                q += 1


            test_loss += clf_criterion(target_preds, t_label.long())  # sum up batch loss
            pred = target_preds.data.max(1)[1]  # get the index of the max log-probability

            if epoch == n_epoch - 1:
                # n,m = target,pred
                for n, m in zip(t_label, pred):
                    matrix[int(n)][int(m)] += 1

            correct += pred.eq(t_label.data.view_as(pred)).cpu().sum()
        cor = float(correct) * 100. / len(target_train_loader.dataset)

        acc_c.append(cor)
        ep_c.append(epoch)

        test_loss /= len(target_train_loader)
        print('{} set: Average classification loss: {:.4f}, Accuracy: {}/{} ({:.2f}%)\n'.format(
            TARGET_NAME, test_loss.item(), correct, len(target_train_loader.dataset),
            100. * correct / len(target_train_loader.dataset)))

cur_time = datetime.now()
h, remainder = divmod((cur_time - prev_time).seconds, 3600)
m, s = divmod(remainder, 60)
time_str = "Time %02d:%02d:%02d" % (h, m, s)
print(time_str)
print('Finished')

# 混淆矩阵
labels = np.arange(0, 10, 1)
for i in range(classes):
    _num = 0
    for j in range(classes):
        _num += matrix[i][j]
    for k in range(classes):
        if _num != 0:
            matrix[i][k] = round(matrix[i][k] / _num, 2)
        else:
            matrix[i][k] = 0
print(matrix)
plt.figure(1)
plt.imshow(matrix, cmap='coolwarm')
plt.xticks(range(classes), labels)
# 设置y轴坐标label
plt.yticks(range(classes), labels, rotation=45)
plt.colorbar()
plt.xlabel('Predicted Labels')
plt.ylabel('True Labels')
plt.title('Confusion matrix')
for x in range(classes):
    for y in range(classes):
        info = matrix[y][x]
        plt.text(x, y, info,
                 verticalalignment='center',
                 horizontalalignment='center',
                 color="white")
plt.tight_layout()
plt.show()

sum[:x_num, :] = out_s
sum[x_num:, :] = out_t
sum_l[:x_num, :] = out_s_l
sum_l[x_num:, :] = out_t_l


s_d = {}
t_d = {}
for i in range(classes):
    s_d[i] = []
    t_d[i] = []

tsne = manifold.TSNE(n_components=2, init='pca')
X_tsne = tsne.fit_transform(sum)

x_min, x_max = X_tsne.min(0), X_tsne.max(0)
X_norm = (X_tsne - x_min) / (x_max - x_min)
plt.figure(1)
color_source = (
'#FF0000', '#0000FF', '#FFFF00', '#008000', '#2F4F4F', '#FFC0CB', '#800080', '#A0522D', '#D2B48C', '#00BFFF')
color_target = (
'#A52A2A', '#191970', '#FFD700', '#556B2F', '#008080', '#DC143C', '#EE82EE', '#D2691E', '#FFDEAD', '#ADD8E6')
for i in range(len(X_norm)):
    if i < 880:
        plt.text(X_norm[i, 0], X_norm[i, 1], 'o', color=color_source[int(sum_l[i])],
                 fontdict={'weight': 'light', 'size': 15})
        s_d[int(sum_l[i])].append(X_norm[i])
        #s_d[int(sum_l[i])].append(X_norm[i])
    else:
        plt.text(X_norm[i, 0], X_norm[i, 1], '*', color=color_target[int(sum_l[i])],
                 fontdict={'weight': 'light', 'size': 15})
        t_d[int(sum_l[i])].append(X_norm[i])
        #t_d[int(sum_l[i])].append(X_norm[i])
plt.show()

# #目标域到中心的距离平均值
x_zhong = 0
for i in range(classes):
    num1 = 0
    num2 = 0
    num3 = 0
    num4 = 0
    num_x = 0
    num_y = 0
    x_result = 0
    s1 = s_d[i]
    t1 = t_d[i]
    lens = len(s1)
    s2 = []#源域x
    t2=[]#目标域x
    s3=[]#源域y
    t3 =[]#目标域y
    for j in range(lens):
        s2.append(s1[j][0])
        s3.append(s1[j][1])

        t2.append(t1[j][0])
        t3.append(t1[j][1])
    for j in range(lens):
        num1 += s2[j]
        num3 += s3[j]

        num2 += t2[j]
        num4 += t3[j]
    num1 = num1 / lens
    num2 = num2 / lens
    num3 = num3 / lens
    num4 = num4 / lens
    #源域中心 （num1,num3） 目标域中心(num2,num4)
    x_result = math.sqrt((num2-num1)*(num2-num1) + (num4-num3)*(num4-num3))
    x_zhong += x_result
x_zhong = x_zhong / classes
print(x_zhong)



# fig = plt.figure(1)
# ax1 = fig.add_subplot(211)
# plt.title("train_acc")
# plt.xlabel('epoch')
# plt.ylabel('acc Rate')
# plt.plot(ep, acc)
# ax2 = fig.add_subplot(212)
# plt.title('test_acc')
# plt.xlabel('epoch')
# plt.ylabel('acc Rate')
# plt.plot(ep_c, acc_c)
# plt.show()


#
#
#
#
#
#
#


