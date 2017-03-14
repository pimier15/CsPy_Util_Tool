using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContourTest1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            byte[][] env = new byte[2][];
            env[0] = new byte[] { 2 , 3 };
            env[1] = new byte[] { 4 , 5 };

            byte[][] target = new byte[4][];
            env[0] = new byte[] { 0 , 0 , 0 , 0 };
            env[1] = new byte[] { 0 , 2 , 3 , 0 };
            env[2] = new byte[] { 0 , 4 , 5 , 0 };
            env[3] = new byte[] { 0 , 0 , 0 , 0 };

            Contour_Class ct = new Contour_Class(env);
            var temp =  ct.Padding( env , 1 );
            Console.ReadLine();
            Assert.Equals( temp , target );
        }
    }
}
