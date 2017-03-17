import numpy as np


test_arr = [
    [ [1,2],[3,4] ],
    [ [5,6],[7,8] ],
    [ [9,10],[11,12] ]
    ]

print np.shape(test_arr)
temp = np.asarray(test_arr)
Z = temp.reshape((1,-1)) 
zzz = temp.ravel()

temp2 = np.float32(Z)
temp3 = np.float32(zzz)



print 'done'