import csv
import os

class IO_csv():

    def ReadCsv(self,path):
        output = []
        with open(path,'rb') as f:
            reader = csv.reader(f)
            for row in reader:
                output.append(row.split(','))
        return output
    
    def WriteCsv(self,dataList,path):   
        with open(path, "wb") as f:    
            writer = csv.writer(f , delimiter = ',')
            for row in dataList:
                writer.writerow(row)



if __name__ == '__main__':
    IOcsv = IO_csv()
    dirpath = r'c:\test'
    filename= 'test.csv'
    filepath = os.path.join(dirpath,filename)
    if not os.path.exists(dirpath):
        os.makedirs(dirpath)

    dataList = [
        [1,2,3,4],
        [4,2,3,4],
        [3,2,3,4],
        [2,4,3,4],
        ]

    IOcsv.WriteCsv(dataList,filepath )
    print ' done '
    raw_input()

    result = IOcsv.ReadCsv(filepath)
    print result

