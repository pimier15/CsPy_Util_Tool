import numpy as np

def Ackley2(dim,x,y):
    firstterm = 20*np.exp( -0.2*np.sqrt(0.5*(x**2 + y**2)) )
    secondterm = np.exp( 0.5 * ( np.cos(6.18*x) + np.cos(6.18*y)  ) )
    output = -1*firstterm -1*secondterm + np.e + 20
    return output

for i in np.arange(-10,10,0.5):
        for j in np.arange(-10,10,0.5):
            out = Ackley2(2,i,j)
            D = [i,j,out]

            print D
