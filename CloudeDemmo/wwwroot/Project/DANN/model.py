import torch.nn as nn
from functions import ReverseLayerF


class CNNModel(nn.Module):

    def __init__(self):
        super(CNNModel, self).__init__()
        #特征提取器
        self.feature = nn.Sequential(
            nn.Conv2d(1, 64, kernel_size=(1,5)),
            nn.BatchNorm2d(64),
            nn.MaxPool2d(kernel_size=(1,2)),
            nn.ReLU(inplace=True),
            nn.Conv2d(64, 50, kernel_size=(1,5)),
            nn.BatchNorm2d(50),
            nn.Dropout2d(),
            nn.MaxPool2d(kernel_size=(1, 2)),
            nn.ReLU(inplace=True),
        )

        #类别预测器
        self.class_classifier = nn.Sequential(
            nn.Linear(50 * 1 * 253, 100),
            nn.BatchNorm1d(100),
            nn.ReLU(True),
            nn.Dropout(),
            nn.Linear(100, 100),
            nn.BatchNorm1d(100),
            nn.ReLU(True),
            nn.Linear(100, 10),
            nn.LogSoftmax(dim=1),
        )
        #域分类器
        self.domain_classifier = nn.Sequential(
            nn.Linear(50 * 1 * 253, 100),
            nn.BatchNorm1d(100),
            nn.ReLU(True),
            nn.Linear(100, 2),
            nn.LogSoftmax(dim=1),
        )

    def forward(self, input_data, alpha):
        feature = self.feature(input_data)
        feature = feature.view(-1, 50 * 1 * 253)
        reverse_feature = ReverseLayerF.apply(feature, alpha)
        class_output = self.class_classifier(feature)
        domain_output = self.domain_classifier(reverse_feature)

        return class_output, domain_output
