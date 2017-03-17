from scipy import stats
import csv_Tool
import Tkinter as TK
from tkFileDialog import askopenfilename
import numpy as np  
import itertools

csv = csv_Tool.IO_csv()
tk = TK.Tk()
tk.withdraw()
filePath = askopenfilename()
base_data = csv.ReadCsv(filePath)

initpath = r''

data = base_data[ : , 3: ]

ndData = np.asarray(data)
zscore = stats.zscore( ndData , axis = 0 , ddof = 1)

index_OKNG = []

for i in range(len(base_data)):
    if base_data[i][2] == 'TRUE':
        base_data[i][2] = 1
    else:
        base_daa[i][2] = 0
    index_OKNG.append( base_data[i][ : 2])



refPath = askopenfilename( initialdir = initpath)
ref_Data = csv.ReadCsv(refPath)


ref_defined = []
for i in range(len(ref_Data)):
    singleLine = [ item[2:6] for item in ref_Data[i] ]
    ref_defined.append(singleLine)


defined_data = list(itertools.chain.from_iterable([ ref_defined , index_OKNG , zscore , ])) 

tk.withdraw()
outputPath = askopenfilename( initialdir = initpath)
csv.WriteCsv(defined_data , outputPath)









