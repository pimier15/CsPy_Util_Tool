import csv


base = r'D:\03JobPro\2017\01_LG\Data\2017_03_Test3_SampleData\Data_Result_Sheet'
path = base + '\\RefData_3si.csv'
outpath = base +'\\' + 'output\\' + 'outfile_3si.csv'

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
        output[idx][2] = float(data[idx][2])
        output[idx][3] = float(data[idx][3])
        output[idx][4] = float(data[idx][4])
        output[idx][5] = float(data[idx][5])
    return output

def changecolumn(data):
    for i in range(len(data)):
        col0 = data[i][0]
        col1 = data[i][1]
        data[i][0] = col1
        data[i][1] = col0



ori = ReadCsv(path)
ori  = ConvertInt(ori)

#col0 = ori[:][0]
#col1 = ori[:][1]
#
#ori[:][0] = col1 
#ori[:][1] = col0 
changecolumn(ori)
sortedori = sorted(ori, key = lambda x: (x[0],x[1]))

cw = csv.writer(file(outpath , 'wb'))
cw.writerows(sortedori)



