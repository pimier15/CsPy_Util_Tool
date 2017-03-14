import TestFuncLib as funclib
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
import mpl_toolkits.mplot3d.axes3d as p3
import numpy as np
from matplotlib import cm

FuncData = []
XData = []
YData = []
ZData = []
Data3D = []
FuncLib = funclib.TestFuncLib()

def exe1Dim():
    for i in np.arange(0,1,0.1):
        output = FuncLib.Forrester(i)
        FuncData.append(output)
        XData.append(i)
    plt.plot(XData,FuncData)
    plt.show()

def exe2Dim():
    XData  = []
    YData  = []
    ZData  = []
    for i in np.arange(-3,3,0.2):
        for j in np.arange(-3,3,0.2):
            xdata = [i,j]
            #output = FuncLib.Michalewicz(2,xdata,10)
            output = FuncLib.Sphere2(xdata,0.4)
            output2 = FuncLib.Easom2(i,j,10)
            #output2 = FuncLib.Easom(i+0.8,j+0.8)
            output3 = output + output2
            #output = FuncLib.Ackley2(2,i,j)
            #tempdata = [i,j,output]
            XData.append(i)
            YData.append(j)
            ZData.append(output3)
    #X,Y,Z = zip(*Data3D)

    #path = 'D:\\TestData\\'
    #np.savetxt(path + 'x.csv',XData,delimiter= ',')
    #np.savetxt(path + 'y.csv',YData,delimiter=',')
    #np.savetxt(path + 'z.csv',ZData,delimiter=',')
    
    #XData, YData = np.meshgrid(XData, YData)  
    fig = plt.figure()
    bx = p3.Axes3D(fig)
    bx.plot_trisurf(XData,YData,ZData)
    #ax = fig.gca(projection='3d')
    #ax.plot_surface(XData,YData,ZData,cmap = cm.winter)
    #ax.plot_wireframe(XData,YData,ZData)

    bx.set_xlabel('X')
    bx.set_ylabel('Y')
    bx.set_zlabel('Z')

    plt.show()
 
    
exe2Dim()        


