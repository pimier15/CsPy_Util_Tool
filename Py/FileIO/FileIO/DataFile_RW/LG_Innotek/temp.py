import csv


base = r'D:\03JobPro\2017\01_LG\Data\2017_03_Test3_SampleData\Data_Result_Sheet'
path = base + '\\reorder_1si.csv'
outpath = base +'\\' + 'output\\' + 'outfile_1si_2.csv'

def ReadCsv(path):
    output = []
    f = open(path,'r')
    rows = csv.reader(f)
    for row in rows:
        output.append(row)
    f.close
    return output


def changecolumn(data):
    for i in range(len(data)):
        col0 = data[i][0]
        col1 = data[i][1]
        data[i][0] = col1
        data[i][1] = col0

ori = ReadCsv(path)
changecolumn(ori)
#sortedori = sorted(ori, key = lambda x: (x[0],x[1]))

cw = csv.writer(file(outpath , 'wb'))
cw.writerows(ori)



