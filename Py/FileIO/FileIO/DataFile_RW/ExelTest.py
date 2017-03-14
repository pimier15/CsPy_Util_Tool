import xlrd

filepath = r'D:\03JobPro\2017\01_LG\Data\2017_03_Test3_SampleData\1si.xlsx'

workbook = xlrd.open_workbook(filepath)

sheetNames = workbook.sheet_names()
print sheetNames





