import csv
import itertools
import Classifier_Perfomence

base = r'D:\03JobPro\2017\01_LG\Data\2017_03_Test3_SampleData\TestImg\compare'
folder = '100_50'
#outpath = base + '\\'+ folder +'\\' + 'output_1si' + folder+'.csv'
outpath = base + '\\'+ folder +'\\' + 'output_3si.csv'

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

ori = ReadCsv(outpath)
ori  = ConvertInt(ori)


TPcount=0
TNcount=0
FPcount=0
FNcount=0

for i in range(len(ori)):
    if ori[i][2] == 'OK' and ori[i][3] == 'OK':
        TPcount += 1
    if ori[i][2] == 'NG'and ori[i][3] == 'NG':
        TNcount += 1
    if ori[i][2] == 'OK' and ori[i][3] == 'NG':
        FNcount += 1
    if ori[i][2] == 'NG'and ori[i][3] == 'OK':
        FPcount += 1
    

target  = [ row[2]  for row in ori ]
predict = [ row[3]  for row in ori ]
labels = ['OK', 'NG']

NgCount_Target = 0
NgCount_Predict = 0


for label in target:
    if label == 'NG':
        NgCount_Target += 1

for label in predict:
    if label == 'NG':
        NgCount_Predict += 1

print 'Target Ng Count  :  ' + str(NgCount_Target)
print 'Predict Ng Count  :  ' + str(NgCount_Predict)

analysis = Classifier_Perfomence.Analysis(target,predict,labels)
analysis.CalcSpecificity(NgCount_Target)

print 'Confusion Metrix'
print  analysis.confusionMetrics
print 'accuracy :   '+str(analysis.accuracy)
print 'specificity :   '+str(analysis.specificity)
print 'TP       :   '+str(analysis.TP      )
print 'TN       :   '+str(analysis.TN      )
print 'FP       :   '+str(analysis.FP      )
print 'FN       :   '+str(analysis.FN      )
print 'done'
print 'done'
    



    


    






    
    









