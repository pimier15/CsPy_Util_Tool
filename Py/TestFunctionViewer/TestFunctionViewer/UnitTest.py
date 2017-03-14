import numpy as np


x = [2,2]
m = 2
sum = 0
dim = 2
def Michalewicz(dim,x,m):
    sum = 0
    for i in range(dim):
        new = np.sin(x[i]) * ((np.sin(i*x[i]**2/np.pi))**(2*m));
        sum  = sum + new
    return -1 * sum

output = Michalewicz(dim,x,m)

print output


