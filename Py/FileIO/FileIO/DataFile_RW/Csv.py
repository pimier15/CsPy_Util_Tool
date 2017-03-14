import csv
import itertools
import os

base = r'D:\03JobPro\2017\01_LG\Data\2017_03_Test3_SampleData\TestImg\compare'
foldername = '100_50'

filepath1 = os.path.join(base,foldername,'3si.csv')
filepath2 = os.path.join(base,foldername,'predict_3si.csv') 
outpath = os.path.join(base,foldername,'output_3si' + foldername + '.csv') 

def ReadCsv(path):
    output = []
    f = open(path,'r')
    rows = csv.reader(f)
    for row in rows:
        output.append(row)
    f.close
    return output

def ConvertInt(data):
    output = data
    for idx in range(len(data)):
        output[idx][0] = int(data[idx][0])
        output[idx][1] = int(data[idx][1])
    return output

def Concatenate(list1 , list2):
    return list1 + list2


ori = ReadCsv(filepath1)
pred = ReadCsv(filepath2)

bck = []

bck[:] = pred[:]
pred[:][0] = bck[:][1] 
pred[:][1] = bck[:][0] 

pred = ConvertInt(pred)
ori  = ConvertInt(ori)




sortedori = sorted(ori, key = lambda x: (x[1],x[0]))
predlabel = [row[2] for row in pred  ]



[ sortedori[i].append(predlabel[i]) for i in range(len( sortedori )) ]

cw = csv.writer(file(outpath , 'wb'))
cw.writerows(sortedori)











    
    









