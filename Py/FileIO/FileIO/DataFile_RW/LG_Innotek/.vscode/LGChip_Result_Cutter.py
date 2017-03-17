import csv
#
base = r'D:\03JobPro\2017\01_LG\Data\2017_03_Test3_SampleData\TestImg\compare\100_55_3si'
filepath = base + '\\' + 'T100_55.csv'
outpath = base + '\\' + 'predict_3si.csv'

def ReadCsv(path):
    output = []
    f = open(path,'r')
    rows = csv.reader(f)
    for row in rows:
        output.append(row)
    f.close
    return output

data = ReadCsv(filepath)

last = len(data)

sliced = [ row[0 : 3] for row in data[2::] ]

cw = csv.writer(file(outpath , 'wb'))
cw.writerows(sliced)


