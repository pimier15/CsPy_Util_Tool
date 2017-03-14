from sklearn.metrics import *

class Analysis():

    def __init__(self,target,predict,labelName = None):
        self.confusionMetrics = confusion_matrix(target,predict,labelName)
       
        self.total = float(len(target)               )
        self.TP    = float(self.confusionMetrics[0,0])
        self.TN    = float(self.confusionMetrics[1,1])
        self.FP    = float(self.confusionMetrics[0,1])
        self.FN    = float(self.confusionMetrics[1,0])

        self.accuracy = accuracy_score(target, predict)
        self.misclassification = (self.FP + self.FN)/self.total 


    def CalcSpecificity(self,targetN_count):

        self.specificity = self.TN / targetN_count 
       





