import matplotlib.pyplot as plt
import numpy as np


class TestFuncLib():
    
    def Forrester(self,x):
        return ((6*x-2)**2)*np.sin(12*x-4)

    def LowerFidelity(self,x,A,B,C):
        return A*self.Forrester(x) + B*self.Forrester(x-0.5) - C

    def Sphere(self,x):
        sum = 0
        for i in range(len(x)):
            new = x[i]**2;
            sum  = sum + new
        return sum

    def Sphere2(self,x,peak):
        sum = 0
        for i in range(len(x)):
            new = -1*peak*x[i]**2;
            sum  = sum + new
        return sum

    def Easom(self,x1,x2,peak):
        return -1*( np.cos(x1)*np.cos(x2)*np.exp( -1*(x1 )**2 -1*(x2 )**2) )

    def Easom2(self,x1,x2,peak):
        return peak*( np.cos(x1)*np.cos(x2)*np.exp( -1*(x1 )**2 -1*(x2 )**2) )
        
    def Michalewicz(self,dim,x,m):
        sum = 0
        for i in range(dim):
            new = np.sin(x[i]) * ((np.sin((i+1)*(x[i]**2) / np.pi))**2*m)
            sum  = sum + new
        return -1 * sum

    def Ackley(self,dim,x):
        firstSum = 0
        secondSum = 0
        for i in range(dim):
            firstSum = firstSum + x[i]**2
            secondSum = secondSum + np.cos(6.18*x[i])

        firstterm = 20*np.exp( -1*0.2*np.sqrt((1/6.28)*firstSum) )
        secondterm = np.exp( 1/6.18 * (1/6.28*secondSum) )
        output = -1*firstterm - secondterm + 20 + np.e
        return output 

    def Ackley2(self,dim,x,y):
        firstterm = 20*np.exp( -0.2*np.sqrt(0.5*(x**2 + y**2)) )
        secondterm = np.exp( 0.5 * ( np.cos(6.18*x) + np.cos(6.18*y)  ) )
        output = -firstterm - secondterm + np.e + 20
        return output 

    def AckleyCustom(self,dim,x,a,b,c):
        pass


 

        




