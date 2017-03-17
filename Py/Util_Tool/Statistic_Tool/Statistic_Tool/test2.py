from scipy import stats
import csv_Tool
import Tkinter as TK
from tkFileDialog import askopenfilename
import numpy as np  


data = [
    [1.0 , 1.0 , 4 , 10],
    [2.0 , 10.0, 4 , 10],
    [3.0 , 23.0, 4 , 10]
    ]

tempdata = [ item[2:] for item in data]
print tempdata


ndData = np.asarray(data)

temp1  =np.std(ndData , axis = 0)
temp2  =np.std(ndData , axis = 1)
temp3  =np.mean(ndData , axis = 0 )
temp4  =np.mean(ndData , axis = 1 )

z1 = stats.zscore(ndData, axis = 0 , ddof = 0)
z2 = stats.zscore(ndData, axis = 1 , ddof = 0)


print z1
print z2
print 'done'



