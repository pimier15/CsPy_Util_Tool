using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Util_Tool.FileIO.Xml;

namespace Console_Test
{
    class Program
    {
        static void Main( string[] args )
        {
            WriteXml();
            ReadXml();

        }

        static void WriteXml() {
            TestData data = new TestData();
            data.a = 1;
            data.b = 0.8;
            data.aArray = new dynamic[] { 1 , "ststst" , 34.99 };
            data.bList = new List<dynamic>();
            data.bList.Add( 5.6 );
            data.bList.Add( 5.3 );
            data.bList.Add( 2.6 );
            data.bList.Add( 1.6 );

            using ( StreamWriter wr = new StreamWriter( @"C:\Users\idiol\Desktop\Local Project\Util_Tool\TestFolder\test.xml" ) )
            {
                XmlSerializer xs = new XmlSerializer(typeof(TestData));
                xs.Serialize( wr , data );
            }
        }


        static void ReadXml() {
            TestData data = new TestData();
            using ( StreamReader sr = new StreamReader( @"C:\Users\idiol\Desktop\Local Project\Util_Tool\TestFolder\test.xml" ) )
            {
                XmlSerializer xs = new XmlSerializer(typeof(TestData));
                data = ( TestData ) xs.Deserialize( sr );

                Console.WriteLine( data.bList );

                




            }
        }
    }
}
