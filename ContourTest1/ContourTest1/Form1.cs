using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Imaging;
using Accord;


namespace ContourTest1
{
    public partial class Form1 : Form
    {
        Contour_Class con;
        byte[][] env;
        public Form1()
        {
            InitializeComponent();
            env = new byte[10][];
            env[0] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 };
            env[1] = new byte[] { 0 , 2 , 3 , 7 , 3 , 0 , 0 , 0 , 0 , 0 };
            env[2] = new byte[] { 0 , 4 , 5 , 7 , 0 , 0 , 0 , 0 , 0 , 0 };
            env[3] = new byte[] { 0 , 4 , 4 , 3 , 0 , 0 , 0 , 0 , 0 , 0 };
            env[4] = new byte[] { 0 , 4 , 4 , 3 , 0 , 0 , 0 , 0 , 9 , 0 };
            env[5] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 1 , 2 , 9 , 0 };
            env[6] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 3 , 9 , 0 };
            env[7] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 9 , 7 , 9 , 0 };
            env[8] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 };
            env[9] = new byte[] { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 };

            //env = new byte[6][];
            //env[0] = new byte[] { 0 , 0 , 0 , 0 , 0 };
            //env[1] = new byte[] { 0 , 0 , 0 , 9 , 0 };
            //env[2] = new byte[] { 0 , 2 , 2 , 9 , 0 };
            //env[3] = new byte[] { 0 , 0 , 3 , 9 , 0 };
            //env[4] = new byte[] { 0 , 9 , 7 , 9 , 0 };
            //env[5] = new byte[] { 0 , 0 , 0 , 0 , 0 };
            con = new Contour_Class( env );
        }

        private void button1_Click( object sender , EventArgs e )
        {
            Console.WriteLine();
            
            var result = con.FindContour(env);

        }
    }
}
