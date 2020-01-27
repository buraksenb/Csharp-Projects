using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO ;
using System.Diagnostics;
// C:\\Users\\burak\\Desktop\\C#\\Readhexfile\\stFlash.txt
namespace Hexclassfile
{
    public class HexTools
    {
        public enum RecordType { DataRecord = 0x00 , EOFRecord =0x01 , ExtSegAddressRecord = 0x02, ExtLinAdressRecord = 0x04, StartLinAddressRecord = 0x05 } ;
        public struct lineofData
        {
            public byte numofData ;
            public UInt16 address;
            public RecordType recordType;
            public byte[] dataArray ;
            public byte checksum;

           
        }
        byte Stringtohex(string hexadecimalnum)
        {
            byte decinum = 0;
            byte[] temp = new byte[2];
            for (int i = 0; i < hexadecimalnum.Length; i++)
            {
                
                switch (hexadecimalnum[i])
                {
                    case '0': temp[i] = 0x00;
                        break;
                    case '1': temp[i] = 0x01;
                        break;
                    case '2': temp[i] = 0x02;
                        break;
                    case '3': temp[i] = 0x03;
                        break;
                    case '4': temp[i] = 0x04;
                        break;
                    case '5': temp[i] = 0x05;
                        break;
                    case '6': temp[i] = 0x06;
                        break;
                    case '7': temp[i] = 0x07;
                        break;
                    case '8': temp[i] = 0x08;
                        break;
                    case '9': temp[i] = 0x09;
                        break;
                    case 'A': temp[i] = 0x0A;
                        break;
                    case 'B': temp[i] = 0x0B;
                        break;
                    case 'C': temp[i] = 0x0C;
                        break;
                    case 'D': temp[i] = 0x0D;
                        break;
                    case 'E': temp[i] = 0x0E;
                        break;
                    case 'F': temp[i] = 0x0F;
                        break;
                    default: break;

                }
            
            }
           
                temp[0] = (byte)(temp[0] << 4);

                decinum = (byte)((temp[0]) | (temp[1]));
           
                return decinum;
        }
        
        public List<lineofData> ReadHexFile (string path)
        {
            if(File.Exists(path))
            {
                Console.WriteLine("   ");
            }

            StreamReader sr = new StreamReader(path) ; 
            List<lineofData> listofdata = new List<lineofData>();
            lineofData val = new lineofData();
            do
            {
                string line = sr.ReadLine();
                string temp = " ";
                val.numofData = Stringtohex(temp = line.Substring(1, 2));

                val.address = (UInt16)(256 * Stringtohex(temp = line.Substring(3, 2)));

                val.address += (UInt16)Stringtohex(temp = line.Substring(5, 2));

                temp = line.Substring(7, 2);

                switch (Stringtohex(temp))
                {
                    case 0x00: val.recordType = RecordType.DataRecord;
                        break;
                    case 0x01: val.recordType = RecordType.EOFRecord;
                        break;
                    case 0x02: val.recordType = RecordType.ExtSegAddressRecord;
                        break;
                    case 0x04: val.recordType = RecordType.ExtLinAdressRecord;
                        break;
                    case 0x05: val.recordType = RecordType.StartLinAddressRecord;
                        break;
                    default:
                       
                        break;
                }

                val.dataArray = new byte[16];
                for (int i = 0; i < val.numofData; i++)
                {
                    val.dataArray[i] = Stringtohex(temp = line.Substring(9 + 2 * i, 2));

                }
                val.checksum = Stringtohex((temp = line.Substring((9 + val.numofData * 2), 2)));

                if (CheckHexLine(val))
                {
                    Console.WriteLine("TRUE");
                }
                else
                {
                    Console.WriteLine("ERROR");
                } 
                listofdata.Add(val);
            } while (!(sr.EndOfStream));
            
            return listofdata ;
        }
      
       
        public bool CheckHexLine(lineofData teststruct)
        
        
        {
            byte count = 0x00;
            count += teststruct.numofData ;
            
            string address = teststruct.address.ToString("X") ; 
            
            while( address.Length < 4)
            {
                address = "0" + address; 
            }
            
            string upperBofaddress = " ";
            string lowerBofaddress = " ";

            upperBofaddress = address.Substring(0, 2);
            lowerBofaddress = address.Substring(2,2);

            // address i hex e çevirmek için
            count += byte.Parse(upperBofaddress, System.Globalization.NumberStyles.HexNumber);
            // address i hex e çevirmek için
            count += byte.Parse(lowerBofaddress, System.Globalization.NumberStyles.HexNumber);

            count += (byte)teststruct.recordType;
            
            for (int i = 0; i < teststruct.dataArray.Length; i++ )
            {
                count += (byte) teststruct.dataArray[i] ; 
            }
 
            if (teststruct.checksum == ((0xFF ^ count) + 0x01) % 256)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool CheckHexLine(string path)  
        {
            StreamReader sr = new StreamReader(path);
            do
            {
                string line = sr.ReadLine();
                int dataleng = Stringtohex(line.Substring(1, 2));
                byte count = 0x00;
                for (int i = 1; i < line.Length - 2; i += 2)
                {
                    count += (byte)(Stringtohex(line.Substring(i, 2)));
                }
                byte checksumm = Stringtohex(line.Substring(line.Length - 2, 2));
                if (checksumm == (byte)((0xFF ^ count) + 0x01))
                {
                    // Bilerek boş bırakıldı
                }
                else
                {
                    return false;

                }
            } while (!(sr.EndOfStream));

            return true;

        }     
    }
}
