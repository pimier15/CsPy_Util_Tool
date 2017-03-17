using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using static Accord.Math.Matrix;

namespace ContourTest1
{
    public class Contour_Class
    {
        Contour_Func ConFunc = new Contour_Func();
        byte[][] Env;
        byte[][] CntrMap;

        /*Func Def*/
        Func<int[],List<int[]>> SingelCntr;
        Func<int[] , int[]> SelectNext;

        void InitFunc( byte[][] env ) {
            SingelCntr = ConFunc.FnSingleCntr(env, ConFunc.clockwiseOffset);
        }
        
        List<List<int[]>> ContourPoint;
        
        /* int[0] = y , int[1] = x */
        int[] StrPos ;
        int[] Previus;
        int[] CurPos ;
        int[] NxtPos ;

        public Contour_Class( byte[][] env ) {
            Env     = env.Copy();
            CntrMap = env.Copy();
            InitFunc( Env );
        }

        public List<List<int[]>> FindContour( byte[][] env ) {
            List<List<int[]>> contours = new List<List<int[]>>();
            var findNext = ConFunc.FnFindNext(1);
            int[] startPos = findNext(env , new int[] { 0, 0} ) ;
            int[] currPos  = startPos;
            bool notYet = true;

            while (notYet) {
                var currCntr = SingelCntr( currPos );
                if ( currCntr.Count > 4) {
                    contours.Add( currCntr );
                }

                /* Remove found contour and find next start position*/
                CntrMap = PopCntr( CntrMap , currCntr);
                currPos = findNext(CntrMap , currPos);

                // 정지 조건 
                if ( currPos == new int[] {-1,-1 } ) {
                    notYet = false;
                }
            }
            return contours;
        }

        /* Env */
        public byte[][] PopCntr( byte[][] map , List<int[]> cntrs) {
            byte[][] popedMap = map.Copy();


            for ( int i = 0 ; i < cntrs.Count ; i++ )
            {
               
                Console.WriteLine( $"|| {cntrs[i][0]}  ,  {cntrs[i][1]}  ||" );
            }

            var ymax = (from cntr in cntrs select cntr[0] ).Max().To<int>();
            var ymin = (from cntr in cntrs select cntr[0] ).Min().To<int>();

            //for ( int i = ymin ; i < ymax ; i++ )
            //{
            //    var xlist = (from cntr in cntrs where cntr[0] == i select cntr[1] ).ToArray<int>();
            //    var xmax = (from cntr in cntrs where cntr[0] == i select cntr[1] ).Max().To<int>();
            //    var xmin = (from cntr in cntrs where cntr[0] == i select cntr[1] ).Min().To<int>();
            //    Console.WriteLine( $"{xmin} | {xmax}" );
            //    Console.WriteLine( $"----------------" );
            //    for ( int j = xmin ; j < xmax ; j++ )
            //    {
            //        popedMap[i][j] = 0;
            //    }
            //}


            Parallel.For( ymin , ymax + 1 , y => {
                var xmax = (from cntr in cntrs where cntr[0] == y select cntr[1] ).Max().To<int>();
                var xmin = (from cntr in cntrs where cntr[0] == y select cntr[1] ).Min().To<int>();
                Parallel.For( xmin , xmax + 1 , x =>
                {
                    popedMap[y][x] = 0;
                } );
            } );

            for ( int i = 0 ; i < popedMap.GetLength(0) ; i++ )
            {
                for ( int j = 0 ; j < popedMap[i].GetLength(0) ; j++ )
                {
                    Console.Write( $"|  {popedMap[i][j]}  " );
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            return popedMap;
        }

        public byte[][] Padding(ref byte[][] src , int padSize ){
            byte[][] output = src.Copy();
            var height      = output.GetLength(0);
            var width       = output[0].GetLength(0);

            /*top bot*/
            byte[][] topbot = new byte[1][];
            topbot[0] = new byte[width];
            output = topbot.Stack( output );
            output = output.Stack( topbot );

            /*left right*/
            byte[][]lr = new byte[output.GetLength(0)][];
            Parallel.For( 0 , output.GetLength( 0 ) , x => lr[x] = new byte[] { 0 } );
            output = Matrix.Concatenate( lr , output );
            output = Matrix.Concatenate( output , lr );

            return output;
        }

    }
}
