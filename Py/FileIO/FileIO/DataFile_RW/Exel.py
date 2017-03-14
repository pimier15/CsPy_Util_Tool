import xlrd
import xlwt


class Exel_RW():
    
    def __init__(self,filePath):
        self.WorkBook = xlrd.open_workbook(filePath)
        self.RowsNum = self.WorkBook
        self.RowsValueList = []
        self.ColumnsValueList = []
    
    def ExelRead(self):
        pass


    def FileOpen(self,filePath):
        return xlrd.open_workbook(filePath)

    def SetRowValuesList(self,workbook):







