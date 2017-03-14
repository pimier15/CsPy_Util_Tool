using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emgu_processingTool
{
    public enum ThresholdMode
    {
        Auto,
        Manual
    }

    
    
    public class Vision_Tool
    {
        public static Func<double , double , double[,,]> FnEstChipPos_2Point( double[] pos_LT , double[] pos_RB )
        {
            var createEsted = new Func<double , double , double[,,]>( (double hChipN,double wChipN) => {
            double[,,] output = new double[(int)hChipN , (int)wChipN,2];
            double realImgROIH = Math.Abs(pos_RB[0] - pos_LT[0]);
            double realImgROIW = Math.Abs(pos_RB[1] - pos_LT[1]);

                for (int j = 0; j < hChipN; j++)
                {
                    for (int i = 0; i < wChipN; i++)
                    {
                        output[j,i,0] = realImgROIH / (hChipN-1) * j + pos_LT[0];
                        output[j,i,1] = realImgROIW / (wChipN-1) * i + pos_LT[1];
                    }
                }
                return output;
            } );
            return createEsted;
        }
        
        public static Func<double , double , double[,,]> FnEstChipPos_4Point( double[] realLT , double[] realLB , double[] realRT , double[] realRB )
        {
            // 여기 위치 추정 알고리즘에 문제가 있다. 중심점으로 다 모여버린다. 
            var createEsted = new Func<double , double , double[,,]>( (double hChipN,double wChipN) => {
                double[,,] output = new double[(int)hChipN , (int)wChipN,2];

                /* Avg of Gradient */
                /* Recalculate Bias with first chip position */

                /* X est model, Y fixed */
                double[] model_FH = Calc_YXAxis(realLT,realLB);
                double[] model_SH = Calc_YXAxis(realRT,realRB);

                /* Y est model, X fixed */
                double[] model_FW = Calc_XYAxis(realLT,realRT);
                double[] model_SW = Calc_XYAxis(realLB,realRB);

                /* Avg of Gradient */
                double[] model_H = new double[2] { (model_FH[0]+model_SH[0])/2 , 0};
                double[] model_W = new double[2] { (model_FW[0]+model_SW[0])/2 , 0};

                /* Recalculate Bias */
                model_H[1] = realLT[1] - model_H[0] * realLT[0];
                model_W[1] = realLT[0] - model_W[0] * realLT[1];

                double height_left  = realLB[0] - realLT[0] ;
                double height_right = realRB[0] - realRT[0] ;
                double width_top    = realRT[1] - realLT[1] ;
                double width_bot    = realRB[1] - realLB[1] ;

                double height = (height_left+height_right)/2;
                double width  = (width_top + width_bot)   /2;

                double hStep = height/(hChipN-1);
                double wStep = width_bot/(wChipN-1);

                for (int j = 0; j < hChipN; j++)
                {
                    for (int i = 0; i < wChipN; i++)
                    {
                        double xW = realLT[1] + i*wStep; // fixed X
                        double ested_Y  = xW *model_W[0] + model_W[1]; // Ested Y
                        output[j,i,0] = ested_Y;
                        output[j,i,1] = xW;
                    }
                    /*Update Bias*/
                    model_W[1] += hStep;
                }
                for (int i = 0; i < wChipN; i++)
                {
                    for (int j = 0; j < hChipN; j++)
                    {
                        double yH = realLT[0] + j*hStep; // fixed Y
                        double ested_X  = yH *model_H[0] + model_H[1]; // Ested X
                        output[j,i,0] = (output[j,i,0]+yH     )/2;
                        output[j,i,1] = (output[j,i,1]+ested_X)/2;
                    }
                    /*Update Bias*/
                    model_H[1] += wStep;
                }
                return output;
            } );
            return createEsted;
        }

        static double[] Calc_YXAxis( double[] first , double[] second )
        {
            double gradient = (second[1] - first[1])/(second[0] - first[0]);
            double bias = first[1] - gradient * first[0];
            return new double[] { gradient , bias };
        }
        static double[] Calc_XYAxis(double[] first,double[] second)
        {
                double gradient = (second[0] - first[0])/(second[1] - first[1]);
                double bias = first[0] - gradient * first[1];
                return new double[] { gradient, bias};
        }

        public static Func<System.Drawing.PointF , double> FnInContour( VectorOfPoint contour )
        {
            var incontour = new Func<System.Drawing.PointF , double>( ( pt ) =>
            {
                float ceilX = (float)Math.Ceiling( (double) pt.X);
                float ceilY = (float)Math.Ceiling( (double) pt.Y);
                float trunX = (float)Math.Truncate( (double) pt.X);
                float trunY = (float)Math.Truncate( (double) pt.Y);

                List<double> outlist = new List<double>();
                System.Drawing.PointF[] ptArr = new System.Drawing.PointF[4];

                ptArr[0] = new System.Drawing.PointF(ceilX,ceilY);
                ptArr[1] = new System.Drawing.PointF(trunX,ceilY);
                ptArr[2] = new System.Drawing.PointF(ceilX,trunY);
                ptArr[3] = new System.Drawing.PointF(trunX,trunY);

                for (int i = 0; i < ptArr.GetLength(0) ; i++)
                {
                    outlist.Add(CvInvoke.PointPolygonTest( contour , ptArr[i], false));
                }
                return outlist.Max();
            } );
            return incontour;
        }

        public static Func<Image<Gray , byte> , VectorOfVectorOfPoint> FnFindPassContour( double threshold , double areaUP , double areaDW , ThresholdMode mode )
        {
            var findpasscntr = new Func<Image<Gray , byte> , VectorOfVectorOfPoint>((Image<Gray,byte> imgori) => {
                var thresedimg = mode == ThresholdMode.Auto ? imgori.ThresholdAdaptive(new Gray(255),AdaptiveThresholdType.MeanC,ThresholdType.Binary,17,new Gray(2))
                                                            : imgori.ThresholdBinary(new Gray(threshold),new Gray(255));
                thresedimg.Save(@"D:\03JobPro\2017\01_LG\Data\2017_03_Test3_SampleData\3차테스트 이미지\노프레임\Selected\thresed.bmp");

                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                VectorOfVectorOfPoint passcontours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours( thresedimg , contours , null , RetrType.List , ChainApproxMethod.ChainApproxNone );

                for ( int i = 0 ; i < contours.Size ; i++ )
                {
                    double areaSize = CvInvoke.ContourArea( contours[i],false);  //  Find the area of contour
                    if ( areaSize >= areaDW && areaSize <= areaUP )
                    {
                        passcontours.Push( contours[i] );
                    }
                }
                return passcontours;
            } );
            return findpasscntr;
        }

        public static Func<double , double , bool> FnInBox( Rectangle box , int margin )
        {
            var inbox = new Func<double , double , bool>((double y,double x)=> {
                if(x > box.X + box.Width + margin || x < box.X -margin || y > box.Y + box.Height + margin || y < box.Y - margin )
                {
                    return false;
                }
                return true;
            } );
            return inbox;
        }

        public static Func<Rectangle , double> FnSumBox( Image<Gray , byte> src )
        {
            var sumbox = new Func<Rectangle , double>((Rectangle box)=>
            {
                double sum = 0;
                for (int i = box.X; i < box.X + box.Width; i++)
                {
                    for (int j = box.Y; j < box.Y + box.Height; j++)
                    {
                        sum += src.Data[j,i,0];
                    }
                }
                return sum;
            } );
            return sumbox;
        }

        public static Func<VectorOfVectorOfPoint> FnSortcontours( VectorOfVectorOfPoint inputContours )
        {
            var sort = new Func<VectorOfVectorOfPoint>(()=>
            {
                var temp = inputContours.ToArrayOfArray();
                var sorted = temp.OrderBy( p => p[0].Y ).ThenBy( p => p[0].X ).ToArray();
                return new VectorOfVectorOfPoint( sorted );
            } );
            return sort;
        }

        public static Func<VectorOfVectorOfPoint , List<Rectangle>> FnApplyBox( int upLimit , int dwLimit )
        {
            var applybox = new  Func<VectorOfVectorOfPoint , List<Rectangle>>((VectorOfVectorOfPoint contr)=>
            {
                List<System.Drawing.Rectangle> PassBoxArr = new List<System.Drawing.Rectangle>();
                for ( int i = 0 ; i < contr.Size ; i++ )
                {
                    System.Drawing.Rectangle rc = CvInvoke.BoundingRectangle(contr[i]);
                    PassBoxArr.Add( rc );
                    //if ( rc.Width * rc.Height <= upLimit && rc.Width * rc.Height >= dwLimit )
                    //{
                    //    PassBoxArr.Add( rc ); // box pass
                    //}
                    //else
                    //{
                    //    PassBoxArr.Add( rc );
                    //}
                }
                return PassBoxArr;
            } );
            return applybox;
        }

        public static Func<int , int , double> FnSumAreaPoint( int height , int width, Image<Gray , byte> img )
        {
            var sumareap = new Func<int , int , double>((int y, int x)=>
            {
                double output = 0;
                for (int i = x - width/2; i < x+width/2; i++)
                {
                    for (int j = y - height/2; j < y+height/2; j++)
                    {
                        output += img.Data[ j, i, 0 ];
                    }
                }
                return output;
            } );
            return sumareap;
        }


        #region Draw
        public static Action<double , double , dynamic> FnDrawCircle( int radius , Bgr color )
        {
            return new Action<double , double , dynamic>( ( y , x , image ) => {
                CircleF circle = new CircleF();
                circle.Center = new System.Drawing.PointF( ( float ) x , ( float ) y );
                circle.Radius = radius;
                image.Draw( circle , color , 1 );
            } );
        }

        #endregion  




        #region Helper


        #endregion


    }
}
