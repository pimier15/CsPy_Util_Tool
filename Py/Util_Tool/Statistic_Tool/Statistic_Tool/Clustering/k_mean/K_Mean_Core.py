import cv2
import Tkinter as Tk
from tkFileDialog import askdirectory


class kMean_Core():
    
    def __init__(self):
        self.SrcPath = ''
        self.SrcImg = None


    def SetImage(self,f):
        def wrap():
            root = Tk.Tk()
            root.withdraw()
            filepath = askdirectory()

            f(filepath)

            
        return temp


    @SetImage
    def Cluster(self):

        return a ,b ,c



    

         


