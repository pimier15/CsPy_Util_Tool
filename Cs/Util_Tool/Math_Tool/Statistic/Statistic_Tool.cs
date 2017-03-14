using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Analysis;

namespace Math_Tool.Statistic
{
    public class Statistic_Tool<T_target,T_predict>
    {
        // only string or number
        public Dictionary<string , int[]> Convert2intLabel( T_target[] target, T_predict[] predict , T_target positive_target , T_predict positive_predict ) {
            var output = new Dictionary<string, int[]> ();
            var targ = target.Select( x=> x.Equals(positive_target) ? 1 : 0).ToArray<int>() ;
            var pred = predict.Select(x=> x.Equals(positive_predict) ? 1 : 0).ToArray<int>() ;
            output.Add( "Target" , targ );
            output.Add( "Predict" , pred );
            return output;
        }

        public Dictionary<string , dynamic> ConfusionMatrix(int[] target, int[] predict) {
            var output = new Dictionary<string,dynamic>();
            ConfusionMatrix cm = new ConfusionMatrix( predict, target  , 1 , 0);
            output.Add( "RefNG" , cm.ActualNegatives );

            output.Add( "N" , cm.PredictedNegatives );
            output.Add( "P" , cm.PredictedPositives );
            output.Add( "TP" , cm.TruePositives );
            output.Add( "TN" , cm.TrueNegatives );
            output.Add( "FP" , cm.FalsePositives );
            output.Add( "FN" , cm.FalseNegatives );

            output.Add("Accuracy",cm.Accuracy);
            output.Add("Matrix",cm.Matrix);

            output.Add( "Sensitivity" , cm.Sensitivity);
            output.Add( "Specificity" , cm.Specificity);
            output.Add( "Precision" , cm.Precision);
            output.Add( "NPV" , cm.NegativePredictiveValue);
            return output;
        } 
        
    }
}
