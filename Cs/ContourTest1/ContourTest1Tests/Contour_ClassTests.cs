using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContourTest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ContourTest1.Tests
{
    [TestClass()]
    public class Contour_ClassTests
    {
        [TestMethod()]
        public void Padding_Test()
        {

            byte[][] env = new byte[2][];
            env[0] = new byte[] { 2 , 3 };
            env[1] = new byte[] { 4 , 5 };

            byte[][] target = new byte[4][];
            target[0] = new byte[] { 0 , 0 , 0 , 0 };
            target[1] = new byte[] { 0 , 2 , 3 , 0 };
            target[2] = new byte[] { 0 , 4 , 5 , 0 };
            target[3] = new byte[] { 0 , 0 , 0 , 0 };

            Contour_Class ct = new Contour_Class(env);
            var temp =  ct.Padding(ref env , 1 );
            Assert.IsTrue( target.SequenceEqual( temp ) );
            //CollectionAssert.AreEqual( temp , target );
        }

        [TestMethod()]
        public void PopCntrTest()
        {
            byte[][] env = new byte[6][];
            env[0] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 };
            env[1] = new byte[] { 0 , 2 , 3 , 7 , 3 , 0 };
            env[2] = new byte[] { 0 , 4 , 5 , 7 , 0 , 0 };
            env[3] = new byte[] { 0 , 4 , 4 , 3 , 0 , 0 };
            env[4] = new byte[] { 0 , 4 , 4 , 3 , 0 , 0 };
            env[5] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 };

            byte[][] target = new byte[6][];
            target[0] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 };
            target[1] = new byte[] { 0 , 0 , 0 , 0 , 3 , 0 };
            target[2] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 };
            target[3] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 };
            target[4] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 };
            target[5] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 };

            List<int[]> cntr = new List<int[]>();
            cntr.Add( new int[] { 1 , 1 } );
            cntr.Add( new int[] { 1 , 2 } );
            cntr.Add( new int[] { 1 , 3 } );
            cntr.Add( new int[] { 1 , 4 } );
            cntr.Add( new int[] { 2 , 1 } );
            cntr.Add( new int[] { 2 , 3 } );
            cntr.Add( new int[] { 3 , 1 } );
            cntr.Add( new int[] { 3 , 3 } );
            cntr.Add( new int[] { 4 , 1 } );
            cntr.Add( new int[] { 4 , 2 } );
            cntr.Add( new int[] { 4 , 3 } );


            Contour_Class ct = new Contour_Class(env);
            var temp =  ct.PopCntr( env , cntr );
            Assert.Equals( temp , target );
        }

        [TestMethod()]
        public void FnSingleCntrTest()
        {
            byte[][] env = new byte[6][];
            env[0] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 };
            env[1] = new byte[] { 0 , 2 , 3 , 7 , 3 , 0 };
            env[2] = new byte[] { 0 , 4 , 5 , 7 , 0 , 0 };
            env[3] = new byte[] { 0 , 4 , 4 , 3 , 0 , 0 };
            env[4] = new byte[] { 0 , 4 , 4 , 3 , 0 , 0 };
            env[5] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 };

            Contour_Func fn = new Contour_Func();
            var single = fn.FnSingleCntr(env , fn.clockwiseOffset);

            Assert.Fail();
        }

        [TestMethod()]
        public void CheckBoundaryTest()
        {
            int[] startPos = new int[] { 0,0 };

            Contour_Func fn= new Contour_Func();
            var expl = fn.clockwiseOffset;
            int[] currddirection = new int[] { 1 , 0 };
            for ( int i = 0 ; i < 8 ; i++ )
            {
                Console.WriteLine( fn.CheckBoundary( startPos , currddirection , 6 , 6 ) );
                currddirection = expl[currddirection];
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void FindContourTest()
        {

            //byte[][] env = new byte[10][];
            //env[0] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 };
            //env[1] = new byte[] { 0 , 2 , 3 , 7 , 3 , 0 , 0, 0, 0, 0};
            //env[2] = new byte[] { 0 , 4 , 5 , 7 , 0 , 0 , 0, 0, 0, 0};
            //env[3] = new byte[] { 0 , 4 , 4 , 3 , 0 , 0 , 0, 0, 0, 0};
            //env[4] = new byte[] { 0 , 4 , 4 , 3 , 0 , 0 , 0, 0, 9, 0};
            //env[5] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 1, 2, 9, 0};
            //env[6] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 0, 3, 9, 0};
            //env[7] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 9, 7, 9, 0};
            //env[8] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 0, 0, 0, 0};
            //env[9] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 0, 0, 0, 0};

            byte[][] env = new byte[5][];
            env[0] = new byte[] { 0 , 0 , 0 , 0 , 0 };
            env[1] = new byte[] { 0 , 0 , 0 , 9 , 0 };
            env[2] = new byte[] { 0 , 1 , 2 , 9 , 0 };
            env[3] = new byte[] { 0 , 0 , 3 , 9 , 0 };
            env[4] = new byte[] { 0 , 9 , 7 , 9 , 0 };
            env[5] = new byte[] { 0 , 0 , 0 , 0 , 0 };




            Contour_Class con = new Contour_Class(env);

            var result = con.FindContour(env);


            Assert.Fail();
        }
    }
}