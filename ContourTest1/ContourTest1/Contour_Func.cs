using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourTest1
{
    public class Contour_Func
    {
        public readonly Dictionary<int[], int[]> clockwiseOffset = new Dictionary<int[], int[]>(new EqualityComparer())
        {
             {new int[]{ 1,0 }, new int[]{1,-1 }},   // down         => down-left
             {new int[]{1,-1 }, new int[]{0,-1 }},   // down-left    => left
             {new int[]{0,-1 }, new int[]{-1,-1}},   // left         => top-left
             {new int[]{-1,-1}, new int[]{-1,0 }},   // top-left     => top
             {new int[]{-1,0 }, new int[]{-1,1 }},   // top          => top-right
             {new int[]{-1,1 }, new int[]{0,1  }},   // top-right    => right
             {new int[]{0,1  }, new int[]{1,1  }},   // right        => down-right
             {new int[]{1,1  }, new int[]{1,0  }}    // down-right   => down
        };
        

        public Func<int[] , List<int[]>> FnSingleCntr( byte[][] env , Dictionary<int[] , int[]> action , int thres = 1 )
        {
            return new Func<int[] , List<int[]>>( ( startPoint ) =>
            {
                List<int[]> result = new List<int[]>();
                bool isOn = true;
                int[] startDirection = new int[] { 0 , 1 };

               
                int[] explore_Dirction = new int[2] { -startDirection[0], - startDirection[1]};
                int[] explore_Base = new List<int>(startPoint).ToArray();
                int[] explore_Current  = Update_Pos( explore_Base , explore_Dirction );
                int[] explore_Next     = new int[2];

                //explore_Dirction = action[explore_Dirction];


                while ( isOn )
                {
                    if ( CheckBoundary ( explore_Base , explore_Dirction , env.GetLength(0), env[0].GetLength(0))) { explore_Dirction = action[explore_Dirction]; }
                    else
                    {
                        explore_Next = Update_Pos( explore_Base , explore_Dirction );

                        if ( env[explore_Next[0]][explore_Next[1]] >= thres )
                        {
                            result.Add( explore_Next );
                           
                            explore_Dirction = Minus_Vector( explore_Current , explore_Next );
                            explore_Base = new List<int>( explore_Next ).ToArray();

                            /*End Crit*/
                            if ( explore_Next.SequenceEqual( startPoint ) && Minus_Vector( explore_Next , explore_Current ).SequenceEqual( startDirection ) )
                            {
                                isOn = false;
                            }
                        }
                        else 
                        {
                            explore_Dirction = action[explore_Dirction];
                            explore_Current = new List<int>( explore_Next ).ToArray();
                        }
                    }
                }
                return result.Distinct().ToList(); 
            } );
        }

        //   파라미터들을 받아서 다음 시작점을 찾는다. 
        public Func<byte[][] , int[] , int[]> FnFindNext( int thres )
        {
            return new Func<byte[][],int[] , int[]>( ( env, start ) => {
                int[] result  = new int[2];
                int[] currPos = start;
                int hLimit    = env.Length;
                int wLimit    = env[0].Length;

                while (env[currPos[0]][currPos[1]] < thres ) {
                    if ( currPos[1] + 1 == wLimit )
                    {
                        if ( currPos[0] + 1 == hLimit ) return new int[] { -1 , -1 };
                        else
                        {
                            currPos[0] += 1; //Move Y + 1
                            currPos[1] = 0; // Move X = 0
                        }
                    }

                    else currPos[1] += 1; // Move X + 1
                }
                return currPos;
            } );
        }

        int[] Update_Pos(int[] posm ,int[] direction) {
            int[] nextpos = new int[2];
            nextpos[0] = posm[0] + direction[0];
            nextpos[1] = posm[1] + direction[1];
            return nextpos;
        }

        int[] Minus_Vector( int[] fV , int[] sV ) {
            int[] output = new int[2];
            output[0] = fV[0] - sV[0];
            output[1] = fV[1] - sV[1];
            return output;
        }

        public bool CheckBoundary( int[] startPos , int[] direction , int hLimit , int wLimit )
        {
            var nextPos = startPos.Zip(direction , (x,y) => x+y).ToArray<int>();
            if ( nextPos[0] < 0 || nextPos[0] > hLimit ) { return true; }
            if ( nextPos[1] < 0 || nextPos[1] > wLimit ) { return true; }
            return false;
        }
    }

    public class EqualityComparer : IEqualityComparer<int[]>
    {
        public bool Equals( int[] x , int[] y )
        {
            if ( x.Length != y.Length )
            {
                return false;
            }
            for ( int i = 0 ; i < x.Length ; i++ )
            {
                if ( x[i] != y[i] )
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode( int[] obj )
        {
            int result = 17;
            for ( int i = 0 ; i < obj.Length ; i++ )
            {
                unchecked
                {
                    result = result * 23 + obj[i];
                }
            }
            return result;
        }
    }
}
