import os
import torch
import numpy as np
from torchvision import datasets, transforms
import torch.utils.data as Data
def load_training_A(ROOT_PATH,batch_size):

    string0 = np.loadtxt(ROOT_PATH, dtype=np.float32)
    pre_trainx = string0[:, 1:].reshape(880, -1)
    train_x = torch.from_numpy(pre_trainx)
    trainx = torch.reshape(train_x, [-1, 1, 1, 1024])
    pre_trainy = string0[:, 0].reshape(-1, 1)
    trainy = torch.from_numpy(pre_trainy.reshape(-1, ))
    trainset = Data.TensorDataset(trainx, trainy)

    train_loader = torch.utils.data.DataLoader(dataset=trainset,batch_size=batch_size,shuffle=True, drop_last=True)
    return train_loader

def load_training_C(ROOT_PATH, batch_size):

    string0 = np.loadtxt(ROOT_PATH, dtype=np.float32)
    pre_trainx = string0[:, 1:].reshape(220, -1)
    train_x = torch.from_numpy(pre_trainx)
    trainx = torch.reshape(train_x, [-1, 1, 1, 1024])
    pre_trainy = string0[:, 0].reshape(-1, 1)
    trainy = torch.from_numpy(pre_trainy.reshape(-1, ))
    trainset = Data.TensorDataset(trainx, trainy)

    train_loader = torch.utils.data.DataLoader(dataset=trainset,batch_size=batch_size,shuffle=True, drop_last=True)
    return train_loader

def load_testing(ROOT_PATH, batch_size):
    string1 = np.loadtxt(ROOT_PATH, dtype=np.float32)
    pre_testx = string1[:, 1:].reshape(880, -1)
    pre_testy = string1[:, 0].reshape(-1, 1)
    test_x = torch.from_numpy(pre_testx)
    testx = torch.reshape(test_x, [-1, 1, 1, 1024])
    testy = torch.from_numpy(pre_testy.reshape(-1, ))
    testset = Data.TensorDataset(testx,testy)
    test_loader = torch.utils.data.DataLoader(testset, batch_size=batch_size, shuffle=True)
    return test_loader