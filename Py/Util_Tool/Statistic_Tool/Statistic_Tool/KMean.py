import numpy as np
import cv2
import Tkinter as Tk
from tkFileDialog import askopenfilename

root  = Tk.Tk()
root.withdraw()
filepath = askopenfilename(initialdir = r'D:\02TestData\ImgData')

path = r'D:\1612vision\Lg\LGSample_blue\Mapping_Image\testImage\310x310_F7_center_13000x15800y_min2000max30000.bmp'
img = cv2.imread(filepath,0)

print np.shape(img)

#Z = img.reshape((-1,1)) # 
Z = img.ravel() # Flatten Array with sam n-dim 

print np.shape(img)
Z2 = img.ravel() # Convert to Flatten Array

# convert to np.float32
Z = np.float32(Z)

# define criteria, number of clusters(K) and apply kmeans()
criteria = (cv2.TERM_CRITERIA_EPS + cv2.TERM_CRITERIA_MAX_ITER, 10, 1.0)
K = 3
ret,label,center=cv2.kmeans(Z,K,None,criteria,10,cv2.KMEANS_RANDOM_CENTERS)

# Now convert back into uint8, and make original image
center = np.uint8(center)
res = center[label.flatten()]   # center position of each chips
res2 = res.reshape((img.shape))

outputpath = r'D:\02TestData\ImgData\output_kMean.png'
cv2.imwrite(outputpath,res2)



cv2.imshow('res2',res2)
cv2.waitKey(0)
cv2.destroyAllWindows()