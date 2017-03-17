from matplotlib import pyplot as plt
import csv_Tool
import os 
import Tkinter as Tk
from tkFileDialog import askopenfilename 

root = Tk.Tk()
root.withdraw()
datapath = askopenfilename()
csv = csv_Tool.IO_csv()
data = csv.ReadCsv(datapath)

ok_data = [ item for item in data if item[2] == 'TURE'  ]
ng_data = [ item for item in data if item[2] == 'FALSE' ]

plt.scatter(





