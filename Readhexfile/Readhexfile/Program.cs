using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Hexclassfile; 

namespace Readhexfile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter Path of the File") ;
            string filepath;
            filepath = Console.ReadLine() ;
            
            HexTools program= new HexTools() ;
            
            List<HexTools.lineofData> Completedatalist = new List<HexTools.lineofData>() ;
            Completedatalist = program.ReadHexFile(filepath) ;

            Console.ReadLine();
            

        }
    }
}
