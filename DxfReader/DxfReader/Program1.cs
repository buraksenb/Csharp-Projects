using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DxfReader
{
    class Program
    {
        static void Main(string[] args)
        {

            DxfReaderFunctions Readerfunction = new DxfReaderFunctions();
            List<IDxfComponent> Componentlist = new List<IDxfComponent>();

            Console.WriteLine("Enter the path of text file: ");
            string pathtoTxt = Console.ReadLine();
            StreamReader Reader = new StreamReader(pathtoTxt);
            string temp;

            do
            {
                temp = Reader.ReadLine();
                if (temp == "AcDbCircle")
                {
                    Readerfunction.ReadArcAndCirle(Reader, Componentlist);
                }

                if (temp == "AcDbLine")
                {
                    Componentlist.Add(Readerfunction.ReadLine(Reader));
                }
                if (temp == "Acdb")
                {
                    Componentlist.Add(Readerfunction.ReadPolyLine(Reader));
                }

            } while (!Reader.EndOfStream);



            Console.ReadLine();             

        }
     
    }
}
