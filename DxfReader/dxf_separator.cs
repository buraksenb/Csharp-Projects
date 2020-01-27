/**
 ******************************************************************************
 * @file    dxf_separator.cs
 * @author  Ali Batuhan KINDAN
 * @version V1.0.0
 * @date    04 Ekim 2017
 * @brief   
 * 
 ******************************************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DXF_Tools
{

    public partial class DXFTools
    {

        /**
        * @brief  Bu fonksiyon disaridan string dizisi olarak aldigi *.dxf dosyasinin
        *         her bir satirini tek tek kontrol ederek, dosya icerisinde bulunan
        *         Line, Arc ve Spline'lari ayirir ve ilgili parametre listesine dizer
        * @param  DXF_Line_Array  	
        * @retval yok
        */
        public void DXF_Separator(string[] DXF_Line_Array)
        {
            /* dxf dosyasi satir dongu sayaci */
            int line_cnt = 0;
            /* dxf buffer tutucu degiskeni */
            string dxfBuffer = "";
            /* hata sayaci (sonsuz donguleri onlemek icin) */
            int error_cnt = 0;
            /* dxf sayisal degerleri icin gecici buffer degiskeni */
            double valueBuffer = 0;
            /* spline kontrol noktalarinin x, y, z parametrleri icin tutucu degiskenler */
            double controlPointX = 0;
            double controlPointY = 0;
            double controlPointZ = 0;
            /* spline kontrol noktalarinin tum parametrelerinin okunup 
             * okunmadigini test etmek icin kontrol degiskeni */
            bool pcX_Ok = false;
            bool pcY_Ok = false;
            bool pcZ_Ok = false;
            /* polyline duzlem kontrol degiskenleri, bu degiskenler; polyline ile gelen nokta bulutundaki
             * her bir noktanin x, y ve z eksenlerinde izdusumu var ise, ilgili degiskenler set edilir
             * son noktanin da izdusume gore degerlendirilmesinin ardindan polyline degiskeninin hangi 
             * eksenlerde izdusumu oldugu bulunur ve polyline parcasinin duzlem bilgisi tanimlanir */
            bool polylineXAxis = false;
            bool polylineYAxis = false;
            bool polylineZAxis = false;
            /* dxf dosyasindan gelen sketch arc merkez nokta parametrelerinin absolute koordinat sistemine
             * gore duzeltilmesi icin kullanilan buffer degiskenler. Buna gore eger bir ARC parcasi: 
             * 
             * XY duzleminde cizilmis ise = dxf XYZ si sirasiyla absolute XYZ sine esittir
             * YZ duzleminde cizilmis ise = dxf XYZ si sirasiyla absolute YZX ine esittir
             * XZ duzleminde cizilmis ise = dxf XYZ si sirasiyla absolute XZY ine esittir
             * 
             * */
            double arcCenterXBuffer = 0;
            double arcCenterYBuffer = 0;
            double arcCenterZBuffer = 0;

            /* parca listesini sifirla */
            DXF_AllPart_List.Clear();

            
            /* dxf parcalarinin ayiklanarak parcalara ayrilmasi ve ilgiliparcalarin listeye dizilmesi */
            for(line_cnt = 0; line_cnt < DXF_Line_Array.Length; line_cnt++)
            {
                /* dxf kod satiri sinif degiskeni */
                dxfPart part = new dxfPart();
                /* dxf kod satirinin buffer degiskene atanmasi */
                dxfBuffer = DXF_Line_Array[line_cnt];

                if(dxfBuffer == "LINE")
                {
                    /* dxf parca tipi line olarak ayarlaniyor */
                    part.partType = DXF_PartType.Line;
                    /* parca gorsellik durumu baslangic icin gorulebilir olarak ayarlaniyor */
                    part.partVisualType = DXF_PartVisualType.Visible;
                    /* hata sayaci islem oncesinde sifirlaniyor */
                    error_cnt = 0;

                    /* LINE satirinin gorulmesi ile line gorsel durum belirleme rutinine giriliyor */
                    while (dxfBuffer != "AcDbLine")
                    {
                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                        line_cnt++;
                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        /* ilgili line parcasi icin gorunmez cizgi ibaresi olup olmadigi kontrol ediliyor */
                        if (dxfBuffer == "PHANTOM")
                        {
                            /* PHANTOM ibaresi var ise parca gorsellik durumu phantom olarak ayarlaniyor */
                            part.partVisualType = DXF_PartVisualType.Phantom;
                        }
                        else
                        {
                            // bilerek bos birakildi
                        }

                        /* LINE ibaresi goruldukten 15 satir sonrasinde halen AcDbLine ibaresi gorulmemis ise veya
                         * okuma esnasinda satir indeks numarasi dizi uzunlugunu gecmis ise ilgili dxf dosyasi hatalidir */
                        if((error_cnt > 15) | (line_cnt >= DXF_Line_Array.Length))
                        {
                            /* dxf parcasi hata durumu true olarak degistiriliyor ve ilgili hata listeye ekleniyor */
                            DXF_ErrorList.Add(new E_DXF_File_Wrong_Format());
                            part.error = true;
                            dxfError = true;
                            /* donguden cikiliyor */
                            break;
                        }
                        else
                        {
                            /* sayac 14 ten kucuk ise degerini artir */
                            error_cnt++;
                        }

                    } // while (dxfBuffer != "AcDbLine")


                    /* bir onceki dongude bir hata bulunmadiysa, line baslangic ve bitis noktalarini bulmak icin donguye gir
                     * ve bitis sembolu olan "  0" a kadar okumaya devam et */
                    while ((!part.error) & (dxfBuffer != "  0"))
                    {
                        if(line_cnt >= (DXF_Line_Array.Length - 1))
                        {
                            break;
                        }
                        else
                        {
                            /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                            line_cnt++;
                        }

                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        switch(dxfBuffer)
                        {
                            /* Line X0 */
                            case " 10":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* line X0 degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer,out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if(part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* line x parametresi tutucu degiskenden parca x parametresine aktariliyor */
                                part.P0.X = valueBuffer;

                                break;

                            /* Line Y0 */
                            case " 20":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* line Y0 degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* line y parametresi tutucu degiskenden parca y parametresine aktariliyor */
                                part.P0.Y = valueBuffer;

                                break;

                            /* Line Z0 */
                            case " 30":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* line Z0 degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* line z parametresi tutucu degiskenden parca z parametresine aktariliyor */
                                part.P0.Z = valueBuffer;

                                break;

                            /* Line X1 */
                            case " 11":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* line X1 degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* line x parametresi tutucu degiskenden parca x parametresine aktariliyor */
                                part.P1.X = valueBuffer;

                                break;

                            /* Line Y1 */
                            case " 21":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* line Y1 degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* line y parametresi tutucu degiskenden parca y parametresine aktariliyor */
                                part.P1.Y = valueBuffer;

                                break;

                            /* Line Z1 */
                            case " 31":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* line Z1 degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* line z parametresi tutucu degiskenden parca z parametresine aktariliyor */
                                part.P1.Z = valueBuffer;

                                break;

                            /* default case */
                            default:

                                // bilerek bos birakildi

                                break;

                        } // switch(dxfBuffer)

                    } // while((!part.error) | (dxfBuffer != "  0") )

                    /* dxf line parcasi plane ve on axis tesbiti */
                    /* XY plane tesbiti */
                    if (((Math.Abs(part.P1.X) > dxfPointOnAxisTolerance) | (Math.Abs(part.P0.X) > dxfPointOnAxisTolerance)) &
                        ((Math.Abs(part.P1.Y) > dxfPointOnAxisTolerance) | (Math.Abs(part.P0.Y) > dxfPointOnAxisTolerance)) &
                        (Math.Abs(part.P0.Z) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P1.Z) < dxfPointOnAxisTolerance))
                    {
                        /* parca duzlemi XY olarak ayarlaniyor */
                        part.partPlane = DXF_PartPlane.XY;
                    }
                    /* YZ plane tesbiti */
                    else if (((Math.Abs(part.P1.Y) > dxfPointOnAxisTolerance) | (Math.Abs(part.P0.Y) > dxfPointOnAxisTolerance)) &
                        ((Math.Abs(part.P1.Z) > dxfPointOnAxisTolerance) | (Math.Abs(part.P0.Z) > dxfPointOnAxisTolerance)) &
                        (Math.Abs(part.P0.X) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P1.X) < dxfPointOnAxisTolerance))
                    {
                        /* parca duzlemi YZ olarak ayarlaniyor */
                        part.partPlane = DXF_PartPlane.YZ;
                    }
                    /* XZ plane tesbiti */
                    else if (((Math.Abs(part.P1.X) > dxfPointOnAxisTolerance) | (Math.Abs(part.P0.X) > dxfPointOnAxisTolerance)) &
                        ((Math.Abs(part.P1.Z) > dxfPointOnAxisTolerance) | (Math.Abs(part.P0.Z) > dxfPointOnAxisTolerance)) &
                        (Math.Abs(part.P0.Y) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P1.Y) < dxfPointOnAxisTolerance))
                    {
                        /* parca duzlemi XZ olarak ayarlaniyor */
                        part.partPlane = DXF_PartPlane.XZ;
                    }
                    /* OnAxisX */
                    else if ((Math.Abs(part.P1.X - part.P0.X) > dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P0.Y) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P1.Y) < dxfPointOnAxisTolerance) & 
                        (Math.Abs(part.P0.Z) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P1.Z) < dxfPointOnAxisTolerance))
                    {
                        /* parca duzlemi X ekseni uzerinde olarak ayarlaniyor */
                        part.partPlane = DXF_PartPlane.OnAxisX;
                    }
                    /* OnAxisY */
                    else if ((Math.Abs(part.P1.Y - part.P0.Y) > dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P0.X) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P1.X) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P0.Z) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P1.Z) < dxfPointOnAxisTolerance))
                    {
                        /* parca duzlemi Y ekseni uzerinde olarak ayarlaniyor */
                        part.partPlane = DXF_PartPlane.OnAxisY;
                    }
                    /* OnAxisZ */
                    else if ((Math.Abs(part.P1.Z - part.P0.Z) > dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P0.X) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P1.X) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P0.Y) < dxfPointOnAxisTolerance) &
                        (Math.Abs(part.P1.Y) < dxfPointOnAxisTolerance))
                    {
                        /* parca duzlemi Z ekseni uzerinde olarak ayarlaniyor */
                        part.partPlane = DXF_PartPlane.OnAxisZ;
                    }
                    /* NonPlanar */
                    else if (((Math.Abs(part.P1.X) > dxfPointOnAxisTolerance) | (Math.Abs(part.P0.X) > dxfPointOnAxisTolerance)) &
                        ((Math.Abs(part.P1.Y) > dxfPointOnAxisTolerance) | (Math.Abs(part.P0.Y) > dxfPointOnAxisTolerance)) &
                        ((Math.Abs(part.P1.Z) > dxfPointOnAxisTolerance) | (Math.Abs(part.P0.Z) > dxfPointOnAxisTolerance)))
                    {
                        /* parca duzlemi duzlemsiz olarak ayarlaniyor */
                        part.partPlane = DXF_PartPlane.NonPlanar;
                    }
                    else
                    {
                       /* eger dxf parcasi hicbir rutine girmediyse herhangi bir duzlem bilgisi yoktur ve hata vardir */
                        part.partPlane = DXF_PartPlane.Default;
                        part.error = true;
                        dxfError = true;
                        DXF_ErrorList.Add(new E_Part_Plane_Error()); // dxf parcasi plane hatasi listeye ekleniyor
                    }


                    /* dxf Line parcasinin sarim uzunlugu bulunuyor */
                    part.Sroll = Math.Sqrt((part.P1.X - part.P0.X) * (part.P1.X - part.P0.X) +
                                          (part.P1.Y - part.P0.Y) * (part.P1.Y - part.P0.Y) +
                                          (part.P1.Z - part.P0.Z) * (part.P1.Z - part.P0.Z));

                    /* dxf Line parcasinin XZ duzleminde olmasi halinde sahip olacagi Z projection
                     * egri uzunlugu hesaplaniyor */
                    part.Sprojection_XZ = Math.Abs(Math.Abs(part.P0.X) - Math.Abs(part.P1.X));

                    /* parametreleri doldurulan dxf parcasi listeye ekleniyor */
                    DXF_AllPart_List.Add(part);

                }
                else if (dxfBuffer == "ARC")
                {
                    /* dxf parca tipi arc olarak ayarlaniyor */
                    part.partType = DXF_PartType.Arc;
                    /* parca gorsellik durumu baslangic icin gorulebilir olarak ayarlaniyor */
                    part.partVisualType = DXF_PartVisualType.Visible;
                    /* hata sayaci islem oncesinde sifirlaniyor */
                    error_cnt = 0;

                    /* ARC satirinin gorulmesi ile line gorsel durum belirleme rutinine giriliyor */
                    while (dxfBuffer != "AcDbCircle")
                    {
                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                        line_cnt++;
                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        /* ilgili arc parcasi icin gorunmez cizgi ibaresi olup olmadigi kontrol ediliyor */
                        if (dxfBuffer == "PHANTOM")
                        {
                            /* PHANTOM ibaresi var ise parca gorsellik durumu phantom olarak ayarlaniyor */
                            part.partVisualType = DXF_PartVisualType.Phantom;
                        }
                        else
                        {
                            // bilerek bos birakildi
                        }

                        /* ARC ibaresi goruldukten 15 satir sonrasinde halen AcDbCircle ibaresi gorulmemis ise veya
                         * okuma esnasinda satir indeks numarasi dizi uzunlugunu gecmis ise ilgili dxf dosyasi hatalidir */
                        if ((error_cnt > 15) | (line_cnt >= DXF_Line_Array.Length))
                        {
                            /* dxf parcasi hata durumu true olarak degistiriliyor */
                            part.error = true;
                            dxfError = true;
                            DXF_ErrorList.Add(new E_DXF_File_Wrong_Format());
                            /* donguden cikiliyor */
                            break;
                        }
                        else
                        {
                            /* sayac 14 ten kucuk ise degerini artir */
                            error_cnt++;
                        }

                    } // while (dxfBuffer != "AcDbCircle")

                    /* bir onceki dongude bir hata bulunmadiysa, arc merkez noktasini, yaricapini ve duzlemini bulmak icin donguye gir
                      * ve bitis sembolu olan "AcDbArc" a kadar okumaya devam et */
                    while ((!part.error) & (dxfBuffer != "AcDbArc"))
                    {
                        if (line_cnt >= (DXF_Line_Array.Length - 1))
                        {
                            break;
                        }
                        else
                        {
                            /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                            line_cnt++;
                        }

                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        switch (dxfBuffer)
                        {
                            /* Arc Xc */
                            case " 10":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* arc merkez noktasi X degiskeni okunarak double tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* arc PC X parametresi tutucu degiskenden parca x parametresine aktariliyor */
                                part.PC.X = valueBuffer;

                                break;

                            /* Arc Yc */
                            case " 20":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* arc merkez noktasi Y degiskeni okunarak double tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* arc PC Y parametresi tutucu degiskenden parca y parametresine aktariliyor */
                                part.PC.Y = valueBuffer;

                                break;

                            /* Arc Zc */
                            case " 30":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* arc merkez noktasi Z degiskeni okunarak double tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* arc PC Z parametresi tutucu degiskenden parca z parametresine aktariliyor */
                                part.PC.Z = valueBuffer;

                                break;

                            /* Arc Radius */
                            case " 40":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* arc R degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* arc R parametresi tutucu degiskenden parca R parametresine aktariliyor */
                                part.R = valueBuffer;

                                /* arc parcasinin XY duzleminde olmasi durumunda " 40" parametreli radius bilgisinin okunmasinin
                                 * ardindan "100" parametresi olup olmadigi kontrol ediliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunuyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* eger ilgili satirda "100" parametresi var ise parca duzlem bilgisi XY olarak ayarlaniyor, yok ise
                                 * artirilan satir index degiskeni 1 azaltilarak geri aliniyor */
                                if(dxfBuffer == "100")
                                {
                                    part.partPlane = DXF_PartPlane.XY;
                                    /* parca normal degiskenleri XY duzlemine gore ayarlaniyor */
                                    part.normalX = 0;
                                    part.normalY = 0;
                                    part.normalZ = 1.0;
                                }
                                else
                                {
                                    line_cnt--;
                                }

                                
                                break;

                            /* normal X degiskeni */
                            case "210":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* normal x degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* x normal parametresi tutucu degiskenden parca normal x degiskenine aktariliyor */
                                part.normalX = valueBuffer;
                               
                                break;

                            /* normal Y degiskeni */
                            case "220":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* normal y degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* y normal parametresi tutucu degiskenden parca normal y degiskenine aktariliyor */
                                part.normalY = valueBuffer;

                                break;

                            /* normal Z degiskeni */
                            case "230":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* normal z degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* z normal parametresi tutucu degiskenden parca normal z degiskenine aktariliyor */
                                part.normalZ = valueBuffer;

                                break;

                            /* default case */
                            default:

                                // bilerek bos birakildi
                                
                                break;

                        } // switch(dxfBuffer)

                    } // while((!part.error) | (dxfBuffer != "  0") )

                    /* bir onceki dongude bir hata bulunmadiysa, arc baslangic ve bitis acilarini bulmak icin donguye gir
                      * ve bitis sembolu olan "  0" a kadar okumaya devam et */
                    while ((!part.error) & (dxfBuffer != "  0"))
                    {
                        if (line_cnt >= (DXF_Line_Array.Length - 1))
                        {
                            break;
                        }
                        else
                        {
                            /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                            line_cnt++;
                        }

                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        switch (dxfBuffer)
                        {

                            /* arc start angle */
                            case " 50":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* arc start angle degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* start angle parametresi tutucu degiskenden parca startAngle degiskenine aktariliyor */
                                part.startAngle = valueBuffer;

                                break;

                            /* arc end angle */
                            case " 51":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* arc end angle degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* end angle parametresi tutucu degiskenden parca endAngle degiskenine aktariliyor */
                                part.endAngle = valueBuffer;

                                break;

                            /* default case */
                            default:

                                // bilerek bos birakildi

                                break;

                        } // switch (dxfBuffer)

                    } // ((!part.error) & (dxfBuffer != "  0"))

                    /* XY */
                    if((Math.Abs(part.normalX) < dxfNormalTolerance) & 
                       (Math.Abs(part.normalY) < dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.XY;

                        /* XY duzleminde cizilmis ise = dxf XYZ si sirasiyla absolute XYZ sine esittir */
                        /* ilk olarak dxf ARC merkez parametreleri buffer degiskenlere esitleniyor */
                        arcCenterXBuffer = part.PC.X;
                        arcCenterYBuffer = part.PC.Y;
                        arcCenterZBuffer = part.PC.Z;
                        /* buffer degiskenler ilgili duzlemin gerektirdigi siraya gore parca merkez koordinatlarina diziliyor */
                        part.PC.X = arcCenterXBuffer;
                        part.PC.Y = arcCenterYBuffer;
                        part.PC.Z = arcCenterZBuffer;
                   
                        /* yay baslangic noktasi ilgili duzleme gore belirleniyor */
                        part.P0.X = part.PC.X + part.R * Math.Cos(part.startAngle * Math.PI / 180.0);
                        part.P0.Y = part.PC.Y + part.R * Math.Sin(part.startAngle * Math.PI / 180.0);
                        part.P0.Z = part.PC.Z;

                        /* yay bitis noktasi ilgili duzleme gore belirleniyor */
                        part.P1.X = part.PC.X + part.R * Math.Cos(part.endAngle * Math.PI / 180.0);
                        part.P1.Y = part.PC.Y + part.R * Math.Sin(part.endAngle * Math.PI / 180.0);
                        part.P1.Z = part.PC.Z;

                    }
                    /* YZ */
                    else if ((Math.Abs(part.normalY) < dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) < dxfNormalTolerance) &
                       (Math.Abs(part.normalX) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.YZ;

                        /* YZ duzleminde cizilmis ise = dxf XYZ si sirasiyla absolute ZXY sine esittir */
                        /* ilk olarak dxf ARC merkez parametreleri buffer degiskenlere esitleniyor */
                        arcCenterXBuffer = part.PC.X;
                        arcCenterYBuffer = part.PC.Y;
                        arcCenterZBuffer = part.PC.Z;
                        /* buffer degiskenler ilgili duzlemin gerektirdigi siraya gore parca merkez koordinatlarina diziliyor */
                        part.PC.X = arcCenterZBuffer;
                        part.PC.Y = arcCenterXBuffer;
                        part.PC.Z = arcCenterYBuffer;

                        /* yay baslangic noktasi ilgili duzleme gore belirleniyor */
                        part.P0.X = part.PC.X;
                        part.P0.Y = part.PC.Y + part.R * Math.Cos(part.startAngle * Math.PI / 180.0);
                        part.P0.Z = part.PC.Z + part.R * Math.Sin(part.startAngle * Math.PI / 180.0);

                        /* yay bitis noktasi ilgili duzleme gore belirleniyor */
                        part.P1.X = part.PC.X;
                        part.P1.Y = part.PC.Y + part.R * Math.Cos(part.endAngle * Math.PI / 180.0);
                        part.P1.Z = part.PC.Z + part.R * Math.Sin(part.endAngle * Math.PI / 180.0);

                    }
                    /* XZ */
                    else if ((Math.Abs(part.normalX) < dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) < dxfNormalTolerance) &
                       (Math.Abs(part.normalY) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.XZ;

                        /* XZ duzleminde cizilmis ise = dxf XYZ si sirasiyla absolute XZY sine esittir */
                        /* ilk olarak dxf ARC merkez parametreleri buffer degiskenlere esitleniyor */
                        arcCenterXBuffer = part.PC.X;
                        arcCenterYBuffer = part.PC.Y;
                        arcCenterZBuffer = part.PC.Z;
                        /* buffer degiskenler ilgili duzlemin gerektirdigi siraya gore parca merkez koordinatlarina diziliyor */
                        part.PC.X = arcCenterXBuffer;
                        part.PC.Y = arcCenterZBuffer;
                        part.PC.Z = arcCenterYBuffer;

                        /* yay baslangic noktasi ilgili duzleme gore belirleniyor */
                        part.P0.X = part.PC.X + part.R * Math.Cos(part.startAngle * Math.PI / 180.0);
                        part.P0.Y = part.PC.Y;
                        part.P0.Z = part.PC.Z + part.R * Math.Sin(part.startAngle * Math.PI / 180.0);

                        /* yay bitis noktasi ilgili duzleme gore belirleniyor */
                        part.P1.X = part.PC.X + part.R * Math.Cos(part.endAngle * Math.PI / 180.0);
                        part.P1.Y = part.PC.Y;
                        part.P1.Z = part.PC.Z + part.R * Math.Sin(part.endAngle * Math.PI / 180.0);

                    }
                    /* NonPlanar */
                    else if ((Math.Abs(part.normalX) > dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) > dxfNormalTolerance) &
                       (Math.Abs(part.normalY) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.NonPlanar;
                    }
                    else
                    {
                        /* eger dxf parcasi hicbir rutine girmediyse herhangi bir duzlem bilgisi yoktur ve hata vardir */
                        part.partPlane = DXF_PartPlane.Default;
                        part.error = true;
                        dxfError = true;
                        DXF_ErrorList.Add(new E_Part_Plane_Error()); // dxf parcasi plane hatasi listeye ekleniyor
                    }

                    /* eger 360 derecenin uzerinden bir gecis var ise ozel durum soz konusudur ve bitis acisina 360 eklenir */
                    if (part.endAngle < part.startAngle)
                    {
                        /* ilgili dxf ARC parcasinin sarim uzunlugu hesaplaniyor  */
                        part.Sroll = Math.Abs(2 * Math.PI * part.R * ((part.endAngle + 360.0) - part.startAngle) / 360.0);
                    }
                    else
                    {
                        /* ilgili dxf ARC parcasinin sarim uzunlugu hesaplaniyor  */
                        part.Sroll = Math.Abs(2 * Math.PI * part.R * (part.endAngle - part.startAngle) / 360.0);
                    }

                    /* DXF ARC parcasinin XZ duzleminde olmasi halinde, Z projeksiyon islemi icin sahip oldugu
                     * egri uzunlugu hesaplaniyor (bu hesaplama yukaridaki ARC parcasi duzlem tesbit rutinleri
                     * icerisindeki XZ sekmesine de yapilabilirdi) */
                    part.Sprojection_XZ = Math.Abs(Math.Abs(part.P0.X) - Math.Abs(part.P1.X));

                    /* parametreleri doldurulan dxf parcasi listeye ekleniyor */
                    DXF_AllPart_List.Add(part);

                }
                else if (dxfBuffer == "CIRCLE")
                {
                    /* dxf parca tipi circle olarak ayarlaniyor */
                    part.partType = DXF_PartType.Circle;
                    /* parca gorsellik durumu baslangic icin gorulebilir olarak ayarlaniyor */
                    part.partVisualType = DXF_PartVisualType.Visible;
                    /* hata sayaci islem oncesinde sifirlaniyor */
                    error_cnt = 0;

                    /* AcDbCircle satirinin gorulmesi ile line gorsel durum belirleme rutinine giriliyor */
                    while (dxfBuffer != "AcDbCircle")
                    {
                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                        line_cnt++;
                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        /* ilgili polyline parcasi icin gorunmez cizgi ibaresi olup olmadigi kontrol ediliyor */
                        if (dxfBuffer == "PHANTOM")
                        {
                            /* PHANTOM ibaresi var ise parca gorsellik durumu phantom olarak ayarlaniyor */
                            part.partVisualType = DXF_PartVisualType.Phantom;
                        }
                        else
                        {
                            // bilerek bos birakildi
                        }

                        /* CIRCLE ibaresi goruldukten 15 satir sonrasinde halen AcDbCircle ibaresi gorulmemis ise veya
                         * okuma esnasinda satir indeks numarasi dizi uzunlugunu gecmis ise ilgili dxf dosyasi hatalidir */
                        if ((error_cnt > 15) | (line_cnt >= DXF_Line_Array.Length))
                        {
                            /* dxf parcasi hata durumu true olarak degistiriliyor */
                            DXF_ErrorList.Add(new E_DXF_File_Wrong_Format());
                            dxfError = true;
                            part.error = true;
                            /* donguden cikiliyor */
                            break;
                        }
                        else
                        {
                            /* sayac 14 ten kucuk ise degerini artir */
                            error_cnt++;
                        }

                    } // while (dxfBuffer != "AcDbCircle")


                    /* bir onceki dongude bir hata bulunmadiysa, arc merkez noktasini, yaricapini ve duzlemini bulmak icin donguye gir
                      * ve bitis sembolu olan "  0" a kadar okumaya devam et */
                    while ((!part.error) & (dxfBuffer != "  0"))
                    {
                        if (line_cnt >= (DXF_Line_Array.Length - 1))
                        {
                            break;
                        }
                        else
                        {
                            /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                            line_cnt++;
                        }

                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        switch (dxfBuffer)
                        {
                            /* Circle Xc */
                            case " 10":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* Circle merkez noktasi X degiskeni okunarak double tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* circle PC X parametresi tutucu degiskenden parca x parametresine aktariliyor */
                                part.PC.X = valueBuffer;

                                break;

                            /* Circle Yc */
                            case " 20":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* circle merkez noktasi Y degiskeni okunarak double tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* circle PC Y parametresi tutucu degiskenden parca y parametresine aktariliyor */
                                part.PC.Y = valueBuffer;

                                break;

                            /* Circle Zc */
                            case " 30":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* circle merkez noktasi Z degiskeni okunarak double tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* circle PC Z parametresi tutucu degiskenden parca z parametresine aktariliyor */
                                part.PC.Z = valueBuffer;

                                break;

                            /* Circle Radius */
                            case " 40":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* circle R degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* circle R parametresi tutucu degiskenden parca R parametresine aktariliyor */
                                part.R = valueBuffer;

                                /* circle parcasinin XY duzleminde olmasi durumunda " 40" parametreli radius bilgisinin okunmasinin
                                 * ardindan "100" parametresi olup olmadigi kontrol ediliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunuyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* eger ilgili satirda "100" parametresi var ise parca duzlem bilgisi XY olarak ayarlaniyor, yok ise
                                 * artirilan satir index degiskeni 1 azaltilarak geri aliniyor */
                                if (dxfBuffer == "  0")
                                {
                                    part.partPlane = DXF_PartPlane.XY;
                                    /* parca normal degiskenleri XY duzlemine gore ayarlaniyor */
                                    part.normalX = 0;
                                    part.normalY = 0;
                                    part.normalZ = 1.0;
                                }
                                else
                                {
                                    line_cnt--;
                                }


                                break;

                            /* normal X degiskeni */
                            case "210":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* normal x degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* x normal parametresi tutucu degiskenden parca normal x degiskenine aktariliyor */
                                part.normalX = valueBuffer;

                                break;

                            /* normal Y degiskeni */
                            case "220":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* normal y degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* y normal parametresi tutucu degiskenden parca normal y degiskenine aktariliyor */
                                part.normalY = valueBuffer;

                                break;

                            /* normal Z degiskeni */
                            case "230":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* normal z degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                if (part.error)
                                {
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                }
                                else
                                {
                                    // bilerek bos birakildi
                                }

                                /* z normal parametresi tutucu degiskenden parca normal z degiskenine aktariliyor */
                                part.normalZ = valueBuffer;

                                break;

                            /* default case */
                            default:

                                // bilerek bos birakildi

                                break;

                        } // switch(dxfBuffer)

                    } // while((!part.error) | (dxfBuffer != "  0") )

                    /* XY */
                    if ((Math.Abs(part.normalX) < dxfNormalTolerance) &
                       (Math.Abs(part.normalY) < dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.XY;

                        /* XY duzleminde cizilmis ise = dxf XYZ si sirasiyla absolute XYZ sine esittir */
                        /* ilk olarak dxf ARC merkez parametreleri buffer degiskenlere esitleniyor */
                        arcCenterXBuffer = part.PC.X;
                        arcCenterYBuffer = part.PC.Y;
                        arcCenterZBuffer = part.PC.Z;
                        /* buffer degiskenler ilgili duzlemin gerektirdigi siraya gore parca merkez koordinatlarina diziliyor */
                        part.PC.X = arcCenterXBuffer;
                        part.PC.Y = arcCenterYBuffer;
                        part.PC.Z = arcCenterZBuffer;

                    }
                    /* YZ */
                    else if ((Math.Abs(part.normalY) < dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) < dxfNormalTolerance) &
                       (Math.Abs(part.normalX) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.YZ;

                        /* YZ duzleminde cizilmis ise = dxf XYZ si sirasiyla absolute ZXY sine esittir */
                        /* ilk olarak dxf ARC merkez parametreleri buffer degiskenlere esitleniyor */
                        arcCenterXBuffer = part.PC.X;
                        arcCenterYBuffer = part.PC.Y;
                        arcCenterZBuffer = part.PC.Z;
                        /* buffer degiskenler ilgili duzlemin gerektirdigi siraya gore parca merkez koordinatlarina diziliyor */
                        part.PC.X = arcCenterZBuffer;
                        part.PC.Y = arcCenterXBuffer;
                        part.PC.Z = arcCenterYBuffer;

                    }
                    /* XZ */
                    else if ((Math.Abs(part.normalX) < dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) < dxfNormalTolerance) &
                       (Math.Abs(part.normalY) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.XZ;

                        /* XZ duzleminde cizilmis ise = dxf XYZ si sirasiyla absolute XZY sine esittir */
                        /* ilk olarak dxf ARC merkez parametreleri buffer degiskenlere esitleniyor */
                        arcCenterXBuffer = part.PC.X;
                        arcCenterYBuffer = part.PC.Y;
                        arcCenterZBuffer = part.PC.Z;
                        /* buffer degiskenler ilgili duzlemin gerektirdigi siraya gore parca merkez koordinatlarina diziliyor */
                        part.PC.X = arcCenterXBuffer;
                        part.PC.Y = arcCenterZBuffer;
                        part.PC.Z = arcCenterYBuffer;

                    }
                    /* NonPlanar */
                    else if ((Math.Abs(part.normalX) > dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) > dxfNormalTolerance) &
                       (Math.Abs(part.normalY) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.NonPlanar;
                    }
                    else
                    {
                        /* eger dxf parcasi hicbir rutine girmediyse herhangi bir duzlem bilgisi yoktur ve hata vardir */
                        part.partPlane = DXF_PartPlane.Default;
                        part.error = true;
                        dxfError = true;
                        DXF_ErrorList.Add(new E_Part_Plane_Error()); // dxf parcasi plane hatasi listeye ekleniyor
                    }

                    /* parametreleri doldurulan dxf parcasi listeye ekleniyor */
                    DXF_AllPart_List.Add(part);

                }
                else if(dxfBuffer == "POLYLINE")
                {
                    /* dxf parca tipi polyline olarak ayarlaniyor */
                    part.partType = DXF_PartType.Polyline;
                    /* parca gorsellik durumu baslangic icin gorulebilir olarak ayarlaniyor */
                    part.partVisualType = DXF_PartVisualType.Visible;
                    /* hata sayaci islem oncesinde sifirlaniyor */
                    error_cnt = 0;
                    /* polyline duzlem kontrol degiskenleri sifirlaniyor */
                    polylineXAxis = false;
                    polylineYAxis = false;
                    polylineZAxis = false;

                    /* POLYLINE satirinin gorulmesi ile line gorsel durum belirleme rutinine giriliyor */
                    while (dxfBuffer != "AcDb3dPolyline")
                    {
                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                        line_cnt++;
                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        /* ilgili polyline parcasi icin gorunmez cizgi ibaresi olup olmadigi kontrol ediliyor */
                        if (dxfBuffer == "PHANTOM")
                        {
                            /* PHANTOM ibaresi var ise parca gorsellik durumu phantom olarak ayarlaniyor */
                            part.partVisualType = DXF_PartVisualType.Phantom;
                        }
                        else
                        {
                            // bilerek bos birakildi
                        }

                        /* POLYLINE ibaresi goruldukten 15 satir sonrasinde halen AcDb3dPolyline ibaresi gorulmemis ise veya
                         * okuma esnasinda satir indeks numarasi dizi uzunlugunu gecmis ise ilgili dxf dosyasi hatalidir */
                        if ((error_cnt > 15) | (line_cnt >= DXF_Line_Array.Length))
                        {
                            /* dxf parcasi hata durumu true olarak degistiriliyor */
                            part.error = true;
                            dxfError = true;
                            DXF_ErrorList.Add(new E_DXF_File_Wrong_Format()); // hata listesine ilgili hata ekleniyor
                            /* donguden cikiliyor */
                            break;
                        }
                        else
                        {
                            /* sayac 14 ten kucuk ise degerini artir */
                            error_cnt++;
                        }

                    } // while (dxfBuffer != "AcDb3dPolyline")

                    /* polyline tipinin okunarak 3d polyline olup olmadigi teyit ediliyor */
                    while (dxfBuffer != "  0")
                    {
                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                        line_cnt++;
                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        /* polyline tipini tanimlayan dxf parametresi okunuyor */
                        if (dxfBuffer == " 70")
                        {
                            /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                            line_cnt++;
                            /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                            dxfBuffer = DXF_Line_Array[line_cnt];
                            /* double deger tutucu degisken sifirlaniyor */
                            valueBuffer = 0;

                            /* polyline tipi bilgisi okunarak, degerlendirilmek uzere double tipine donusturuluyor */
                            part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                            /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                            if (part.error)
                            {
                                dxfError = true;
                                DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                            }
                            else
                            {
                                // bilerek bos birakildi
                            }

                            /* polyline tipinin 3d polyline olup olmadigi, " 70" numarali dxf refesansindan
                             * alinan bilgiye gore kontrol ediliyor. 3d polyline olmamasi durumunda hata uretiliyor */
                            if (((int)valueBuffer & 0x00000008) == 0x00000008)
                            {
                                // bilerek bos birakildi
                            }
                            else
                            {
                                part.error = true;
                                dxfError = true;
                                DXF_ErrorList.Add(new E_Polyline_Wrong_Format());
                            }
                        }

                    } // while (dxfBuffer != "  0")


                    /* vertex tanimlama grubu bitis isareti "SEQEND" satirina kadar tum polyline noktalarinin okuma rutini */
                    while(dxfBuffer != "SEQEND")
                    {
                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                        line_cnt++;
                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        /* vertex okuma rutinine giriliyor */
                        if(dxfBuffer == "VERTEX")
                        {

                            /* hata sayaci islem oncesinde sifirlaniyor */
                            error_cnt = 0;
                            /* x, y, z parametre okuma kontrol degiskenleri yeni kontrol islemi icin sifirlaniyor */
                            pcX_Ok = false;
                            pcY_Ok = false;
                            pcZ_Ok = false;

                            /* vertex parametre okuma rutini */
                            while (dxfBuffer != "  0")
                            {
                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];

                                switch (dxfBuffer)
                                {

                                    case " 10":

                                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                        line_cnt++;
                                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                        dxfBuffer = DXF_Line_Array[line_cnt];
                                        /* double deger tutucu degisken sifirlaniyor */
                                        valueBuffer = 0;

                                        /* vertex X degiskeni okunuyor */
                                        part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                        /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                        if (part.error)
                                        {
                                            dxfError = true;
                                            DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                        }
                                        else
                                        {
                                            // bilerek bos birakildi
                                        }

                                        /* vertex X parametresi tutucu degiskenden controlPointX degiskenine aktariliyor */
                                        controlPointX = valueBuffer;
                                        /* x parametre okuma kontrol degiskeni set ediliyor */
                                        pcX_Ok = true;

                                        break;

                                    case " 20":

                                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                        line_cnt++;
                                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                        dxfBuffer = DXF_Line_Array[line_cnt];
                                        /* double deger tutucu degisken sifirlaniyor */
                                        valueBuffer = 0;

                                        /* vertex Y degiskeni okunuyor */
                                        part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                        /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                        if (part.error)
                                        {
                                            dxfError = true;
                                            DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                        }
                                        else
                                        {
                                            // bilerek bos birakildi
                                        }

                                        /* vertex Y parametresi tutucu degiskenden controlPointY degiskenine aktariliyor */
                                        controlPointY = valueBuffer;
                                        /* y parametre okuma kontrol degiskeni set ediliyor */
                                        pcY_Ok = true;

                                        break;

                                    case " 30":

                                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                        line_cnt++;
                                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                        dxfBuffer = DXF_Line_Array[line_cnt];
                                        /* double deger tutucu degisken sifirlaniyor */
                                        valueBuffer = 0;

                                        /* vertex Z degiskeni okunuyor */
                                        part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                        /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                        if (part.error)
                                        {
                                            dxfError = true;
                                            DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                        }
                                        else
                                        {
                                            // bilerek bos birakildi
                                        }

                                        /* vertex Z parametresi tutucu degiskenden controlPointZ degiskenine aktariliyor */
                                        controlPointZ = valueBuffer;
                                        /* z parametre okuma kontrol degiskeni set ediliyor */
                                        pcZ_Ok = true;

                                        break;

                                    case " 70":

                                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                        line_cnt++;
                                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                        dxfBuffer = DXF_Line_Array[line_cnt];
                                        /* double deger tutucu degisken sifirlaniyor */
                                        valueBuffer = 0;

                                        /* vertex Z degiskeni okunuyor */
                                        part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                        /* string to sayi donusumunda hata olmasi durumunda ilgili hata tipi hata listesine ekleniyor */
                                        if (part.error)
                                        {
                                            dxfError = true;
                                            DXF_ErrorList.Add(new E_Numerical_Conversion_Error(line_cnt, dxfBuffer));
                                        }
                                        else
                                        {
                                            // bilerek bos birakildi
                                        }

                                        /* vertex tipi kontrol edilerek dxf referansinda belirtildigi uzere 3d polyline vertex
                                         * olup olmadigi kontrol ediliyor */
                                        if ((int)valueBuffer != 32)
                                        {
                                            /* eger okunan vertex 3d polyline vertex degil ise hata uretiliyor */
                                            part.error = true;
                                            DXF_ErrorList.Add(new E_Polyline_Wrong_Format());
                                        }
                                        else
                                        {
                                            // bilerek bos birakildi
                                        }

                                        break;

                                } // switch(dxfBuffer)

                                /* VERTEX ibaresi goruldukten 35 satir sonrasinde halen "  0" ibaresi gorulmemis ise veya
                                 * okuma esnasinda satir indeks numarasi dizi uzunlugunu gecmis ise ilgili dxf dosyasi hatalidir */
                                if ((error_cnt > 35) | (line_cnt >= DXF_Line_Array.Length))
                                {
                                    /* dxf parcasi hata durumu true olarak degistiriliyor */
                                    part.error = true;
                                    dxfError = true;
                                    DXF_ErrorList.Add(new E_DXF_File_Wrong_Format());
                                    /* donguden cikiliyor */
                                    break;
                                }
                                else
                                {
                                    /* sayac 14 ten kucuk ise degerini artir */
                                    error_cnt++;
                                }

                            } // while(dxfBuffer != "  0")


                            /* ilgili polyline noktasinin X ekseninde degeri olup olmadigi kontrol ediliyor */
                            if(Math.Abs(controlPointX) > dxfPointOnAxisTolerance )
                            {
                                polylineXAxis = true;
                            }
                            else
                            {
                                // bilerek bos birakildi
                            }

                            /* ilgili polyline noktasinin Y ekseninde degeri olup olmadigi kontrol ediliyor */
                            if (Math.Abs(controlPointY) > dxfPointOnAxisTolerance)
                            {
                                polylineYAxis = true;
                            }
                            else
                            {
                                // bilerek bos birakildi
                            }

                            /* ilgili polyline noktasinin Z ekseninde degeri olup olmadigi kontrol ediliyor */
                            if (Math.Abs(controlPointZ) > dxfPointOnAxisTolerance)
                            {
                                polylineZAxis = true;
                            }
                            else
                            {
                                // bilerek bos birakildi
                            }


                            /* kontrol noktasinin son parametresi olan vertex tipi parametresinin de okunmasinin ardindan,  
                             * nokta parametrelerinin de okunup okunmadigi kontrol edilmesiyle kontrol noktasi parca
                             * kontrol nokta listesine ekleniyor  aksi durumda hata olusturuluyor */
                            if (pcX_Ok & pcY_Ok & pcZ_Ok)
                            {
                                /* okunacak vertex degiskeninin parametrelerinin yerlestirilecegi nokta degiskeni tanimlaniyor */
                                dxfPoint vertexPoint = new dxfPoint(0, 0, 0);

                                /* okunan x, y ve z degiskenleri nokta degiskeninin ilgili parametrelerine aktariliyor */
                                vertexPoint.X = controlPointX;
                                vertexPoint.Y = controlPointY;
                                vertexPoint.Z = controlPointZ;

                                /* parametreleri doldurulan kontrol nokta degiskeni parca kontrol nokta listesine ekleniyor */
                                part.PControlList.Add(vertexPoint);

                                /* okunan noktalarin parca kontrol listesinin kontrol nokta degiskenine aktarilmasinin ardindan 
                                 * degiskenler sifirlaniyor */
                                controlPointX = 0;
                                controlPointY = 0;
                                controlPointZ = 0;

                                /* kontrol noktasi x, y, z okuma kontrol degiskenleri yeni nokta icin sifirlaniyor */
                                pcX_Ok = false;
                                pcY_Ok = false;
                                pcZ_Ok = false;
                            }
                            else
                            {
                                part.error = true;
                                dxfError = true;
                                DXF_ErrorList.Add(new E_Polyline_ControlPoint_NotValid());
                            }


                        } // if(dxfBuffer == "VERTEX")
                        else
                        {
                            // bilerek bos birakildi
                        }

                    } // while(dxfBuffer != "SEQEND")


                    /* tum polyline noktalarinin okunmasinin ardindan, duzlem kontrol degiskenlerinin
                     * durumuna gore parca duzlemi tesbit ediliyor */
                    if((polylineXAxis == true) &
                       (polylineYAxis == false) &
                       (polylineZAxis == false))
                    {
                        part.partPlane = DXF_PartPlane.OnAxisX;
                    }
                    else if ((polylineXAxis == false) &
                            (polylineYAxis == true) &
                            (polylineZAxis == false))
                    {
                        part.partPlane = DXF_PartPlane.OnAxisY;
                    }
                    else if ((polylineXAxis == false) &
                            (polylineYAxis == false) &
                            (polylineZAxis == true))
                    {
                        part.partPlane = DXF_PartPlane.OnAxisZ;
                    }
                    else if ((polylineXAxis == true) &
                            (polylineYAxis == true) &
                            (polylineZAxis == false))
                    {
                        part.partPlane = DXF_PartPlane.XY;
                    }
                    else if ((polylineXAxis == false) &
                            (polylineYAxis == true) &
                            (polylineZAxis == true))
                    {
                        part.partPlane = DXF_PartPlane.YZ;
                    }
                    else if ((polylineXAxis == true) &
                            (polylineYAxis == false) &
                            (polylineZAxis == true))
                    {
                        part.partPlane = DXF_PartPlane.XZ;
                    }
                    else if ((polylineXAxis == true) &
                            (polylineYAxis == true) &
                            (polylineZAxis == true))
                    {
                        part.partPlane = DXF_PartPlane.NonPlanar;
                    }
                    else
                    {
                        part.partPlane = DXF_PartPlane.Default;
                        part.error = true;
                        dxfError = true;
                        DXF_ErrorList.Add(new E_Part_Plane_Error());
                    }

                    /* polyline baslangic noktasi kontrol nokta dizisine gore belirleniyor */
                    part.P0.X = part.PControlList[0].X;
                    part.P0.Y = part.PControlList[0].Y;
                    part.P0.Z = part.PControlList[0].Z;

                    /* polyline bitis noktasi kontrol nokta dizisine gore belirleniyor */
                    part.P1.X = part.PControlList[part.PControlList.Count - 1].X;
                    part.P1.Y = part.PControlList[part.PControlList.Count - 1].Y;
                    part.P1.Z = part.PControlList[part.PControlList.Count - 1].Z;
                    
                    /* parametreleri doldurulan polyline parcasi parca listesine ekleniyor */
                    DXF_AllPart_List.Add(part);

                }
                else if(dxfBuffer == "SPLINE")
                {
                    /* dxf parca tipi spline olarak ayarlaniyor */
                    part.partType = DXF_PartType.Spline;
                    /* parca gorsellik durumu baslangic icin gorulebilir olarak ayarlaniyor */
                    part.partVisualType = DXF_PartVisualType.Visible;
                    /* hata sayaci islem oncesinde sifirlaniyor */
                    error_cnt = 0;

                    /* ARC satirinin gorulmesi ile line gorsel durum belirleme rutinine giriliyor */
                    while (dxfBuffer != "AcDbSpline")
                    {
                        /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                        line_cnt++;
                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        /* ilgili spline parcasi icin gorunmez cizgi ibaresi olup olmadigi kontrol ediliyor */
                        if (dxfBuffer == "PHANTOM")
                        {
                            /* PHANTOM ibaresi var ise parca gorsellik durumu phantom olarak ayarlaniyor */
                            part.partVisualType = DXF_PartVisualType.Phantom;
                        }
                        else
                        {
                            // bilerek bos birakildi
                        }

                        /* SPLINE ibaresi goruldukten 15 satir sonrasinde halen AcDbSpline ibaresi gorulmemis ise veya
                         * okuma esnasinda satir indeks numarasi dizi uzunlugunu gecmis ise ilgili dxf dosyasi hatalidir */
                        if ((error_cnt > 15) | (line_cnt >= DXF_Line_Array.Length))
                        {
                            /* dxf parcasi hata durumu true olarak degistiriliyor */
                            part.error = true;
                            /* donguden cikiliyor */
                            break;
                        }
                        else
                        {
                            /* sayac 14 ten kucuk ise degerini artir */
                            error_cnt++;
                        }

                    } // while (dxfBuffer != "AcDbSpline")


                     /* bir onceki dongude bir hata bulunmadiysa, arc merkez noktasini, yaricapini ve duzlemini bulmak icin donguye gir
                      * ve kontrol noktalarinin baslangici olan " 10" a kadar okumaya devam et */
                    while ((!part.error) & (dxfBuffer != " 10"))
                    {
                        if (line_cnt >= (DXF_Line_Array.Length - 1))
                        {
                            break;
                        }
                        else
                        {
                            /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                            line_cnt++;
                        }

                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        switch (dxfBuffer)
                        {

                            /* normal X degiskeni */
                            case "210":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* normal x degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* x normal parametresi tutucu degiskenden parca normal x degiskenine aktariliyor */
                                part.normalX = valueBuffer;

                                break;

                            /* normal Y degiskeni */
                            case "220":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* normal y degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* y normal parametresi tutucu degiskenden parca normal y degiskenine aktariliyor */
                                part.normalY = valueBuffer;

                                break;

                            /* normal Z degiskeni */
                            case "230":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* normal z degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* z normal parametresi tutucu degiskenden parca normal z degiskenine aktariliyor */
                                part.normalZ = valueBuffer;

                                break;

                            /* Spline tipi */
                            case " 70":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* spline tipi degiskeni okunarak double tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* dxf dosyasina bit ile kodlanmis spline tipinin 3. biti denetlenerek ilgili spline'in
                                 * planar olup olmadigi kontrol ediliyor */
                                if(((int)valueBuffer & 0x00000008) == 0x00000008 )
                                {
                                    /* okunan degerin 3. biti set ise spline tipi planar olarak ayarlaniyor */
                                    part.partSplineType = DXF_SplineType.Planar;
                                }
                                else
                                {
                                    /* okunan degerin 3. biti clear ise spline tipi default olarak ayarlaniyor */
                                    part.partSplineType = DXF_SplineType.Default;
                                }

                                break;

                            /* Spline derece */
                            case " 71":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* spline derece degiskeni okunarak double tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* spline derecesi degiskeni tutucu degiskenden parca degiskenine aktariliyor */
                                part.degreeOfSpline = (int)valueBuffer;

                                break;

                            /* Spline kontrol noktasi sayisi */
                            case " 73":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* spline kontrol nokta sayisi degiskeni okunarak double tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* spline derecesi degiskeni tutucu degiskenden parca degiskenine aktariliyor */
                                part.numberOfControlPoints = (int)valueBuffer;

                                break;
                        } // switch (dxfBuffer)

                    } // while((!part.error) & (dxfBuffer != " 10"))

                    /* XY */
                    if ((Math.Abs(part.normalX) < dxfNormalTolerance) &
                       (Math.Abs(part.normalY) < dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.XY;
                    }
                    /* YZ */
                    else if ((Math.Abs(part.normalY) < dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) < dxfNormalTolerance) &
                       (Math.Abs(part.normalX) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.YZ;
                    }
                    /* XZ */
                    else if ((Math.Abs(part.normalX) < dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) < dxfNormalTolerance) &
                       (Math.Abs(part.normalY) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.XZ;
                    }
                    /* NonPlanar */
                    else if ((Math.Abs(part.normalX) > dxfNormalTolerance) &
                       (Math.Abs(part.normalZ) > dxfNormalTolerance) &
                       (Math.Abs(part.normalY) > dxfNormalTolerance))
                    {
                        part.partPlane = DXF_PartPlane.NonPlanar;
                    }
                    else
                    {
                        /* eger dxf parcasi hicbir rutine girmediyse herhangi bir duzlem bilgisi yoktur ve hata vardir */
                        part.partPlane = DXF_PartPlane.Default;
                        part.error = true;
                    }

                    /* kontrol noktalarinin ilk X karakterini tanimlayan " 10" karakteri bulunduktan sonra donguden cikiliyordu 
                     * noktalarin parametrelere dizilmesi sirasinda standart bir dongunun kullanilabilmesi icin yeni okunacak 
                     * satirin " 10", " 20", " 30" metinlerinden biri olmalidir. Su anki gecerli satir " 10" olmasi nedeniyle
                     * satir indeksi 1 geri aliniyor */
                    line_cnt--;

                    /* spline kontrol noktasi okuma rutini ana dongusu */
                     while((!part.error) & (dxfBuffer != "  0"))
                     {

                        if (line_cnt >= (DXF_Line_Array.Length - 1))
                        {
                            break;
                        }
                        else
                        {
                            /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                            line_cnt++;
                        }

                        /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                        dxfBuffer = DXF_Line_Array[line_cnt];

                        switch (dxfBuffer)
                        {

                            /* kontrol noktasi x parametresi */
                            case " 10":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* kontrol noktasi x degiskeni okunarak double tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* kontrol noktasi x parametresi tutucu degiskenden nokta x degiskenine aktariliyor */
                                controlPointX = valueBuffer;

                                /* x parametresi okundu */
                                pcX_Ok = true;

                                break;

                            /* kontrol noktasi y parametresi */
                            case " 20":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* kontrol noktasi y degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* kontrol noktasi y parametresi tutucu degiskenden nokta y degiskenine aktariliyor */
                                controlPointY = valueBuffer;

                                /* y parametresi okundu */
                                pcY_Ok = true;

                                break;

                            /* kontrol noktasi z parametresi */
                            case " 30":

                                /* dxf dizisi indeks degeri bir sonraki satirin okunmasi icin artiriliyor */
                                line_cnt++;
                                /* ilgili indexteki dxf dosya satiri okunarak tutucu degiskene esitleniyor */
                                dxfBuffer = DXF_Line_Array[line_cnt];
                                /* double deger tutucu degisken sifirlaniyor */
                                valueBuffer = 0;

                                /* kontrol noktasi z degiskeni okunarak doble tipine cevriliyor ve buffer degiskenine yaziliyor
                                 * eger okunan veri double tipine donusturulmeye uygun degilse hata olusturuluyor */
                                part.error = !double.TryParse(dxfBuffer, out valueBuffer);

                                /* kontrol noktasi z parametresi tutucu degiskenden nokta z degiskenine aktariliyor */
                                controlPointZ = valueBuffer;

                                /* z parametresi okundu */
                                pcZ_Ok = true;

                                /* kontrol noktasinin son parametresi olan z parametresinin de okunmasinin ardindan, diger 
                                 * nokta parametrelerinin de okunup okunmadigi kontrol edilmesiyle kontrol noktasi parca
                                 kontrol nokta listesine ekleniyor  aksi durumda hata olusturuluyor */
                                if(pcX_Ok & pcY_Ok & pcZ_Ok)
                                {
                                    /* z parametresinin de okunmasi ile, parca kontrol nokta listesine eklenmek uzere
                                     * yeni bir nokta degiskeni olusturuluyor */
                                    dxfPoint controlPoint = new dxfPoint(0, 0, 0);

                                    /* okunan x, y ve z degiskenleri nokta degiskeninin ilgili parametrelerine aktariliyor */
                                    controlPoint.X = controlPointX;
                                    controlPoint.Y = controlPointY;
                                    controlPoint.Z = controlPointZ;

                                    /* parametreleri doldurulan kontrol nokta degiskeni parca kontrol nokta listesine ekleniyor */
                                    part.PControlList.Add(controlPoint);

                                    /* okunan noktalarin parca kontrol listesinin kontrol nokta degiskenine aktarilmasinin ardindan 
                                     * degiskenler sifirlaniyor */
                                    controlPointX = 0;
                                    controlPointY = 0;
                                    controlPointZ = 0;

                                    /* kontrol noktasi x, y, z okuma kontrol degiskenleri yeni nokta icin sifirlaniyor */
                                    pcX_Ok = false;
                                    pcY_Ok = false;
                                    pcZ_Ok = false;
                                }
                                else
                                {
                                    part.error = true;
                                }

                                break;


                            default:

                                    // bilerek bos birakildi

                                break;


                        } // switch (dxfBuffer)

                     } // while((!part.error) & (dxfBuffer != "  0"))

                    /* eger spline kontrol nokta listesinin eleman sayisi parametre olarak dxf dosyasindan okunan
                     * spline kontrol noktasi sayisina esit degil ise hata olustur */ 
                    if(part.PControlList.Count != part.numberOfControlPoints)
                    {
                        part.error = true;
                    }
                    else
                    {
                        // bilerek bos birakildi
                    }

                    /* spline baslangic noktasi kontrol nokta dizisinin ilk elemani olarak belirleniyor.
                     * bunun nedeni; bir spline'in pol noktalari ile dahi tanimlaniyor olsa ilk ve son 
                     * pol noktasindan gecme gerekliligidir. */
                    part.P0.X = part.PControlList[0].X;
                    part.P0.Y = part.PControlList[0].Y;
                    part.P0.Z = part.PControlList[0].Z;

                    /* spline bitis noktasi kontrol nokta dizisinin son elemani olarak belirleniyor.
                     * bunun nedeni; bir spline'in pol noktalari ile dahi tanimlaniyor olsa ilk ve son 
                     * pol noktasindan gecme gerekliligidir. */
                    part.P1.X = part.PControlList[part.PControlList.Count - 1].X;
                    part.P1.Y = part.PControlList[part.PControlList.Count - 1].Y;
                    part.P1.Z = part.PControlList[part.PControlList.Count - 1].Z;

                     /* parametlerleri doldurulan dxf spline parcasi listeye ekleniyor */
                     DXF_AllPart_List.Add(part);
                }
                else
                {
                    // bilerek bos birakildi
                }

                /* dosya cozumlemesi sirasinda bir hata olusur ise bu hata ana dosya hata degiskenine isleniyor */
                dxfError |= part.error;

            } // function main for loop


        } // DXF_Separator function


    }

}