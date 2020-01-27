using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DxfReader
{

    public class DxfArc : IDxfComponent
    {

        public DxfPoint startPoint = new DxfPoint();
        public DxfPoint endPoint = new DxfPoint();
        public double startAngle;
        public double stopAngle;
        public double radius;

    }

    public class DxfLine : IDxfComponent
    {
        public DxfPoint startPoint = new DxfPoint();
        public DxfPoint endPoint = new DxfPoint();
        public DxfPoint extrusionDirection = new DxfPoint(0, 0, 1);
        public double thickness = 0;
    }

    public class DxfCircle : IDxfComponent
    {
        public DxfPoint startPoint = new DxfPoint();
        public DxfPoint endPoint = new DxfPoint();
        public double radius;


    }

    public class DxfPolyLine : IDxfComponent
    {
        public List<DxfPoint> controlPoints = new List<DxfPoint>();
        public double thickness;
        public DxfPoint extrusionDirection = new DxfPoint(0, 0, 1);
        public ushort startWidth;
        public ushort endWidth;
        public ushort meshMvertexcount;
        public ushort meshNvertexcount;

        public ushort surfaceMdensity;
        public ushort surfaceNdensity;
        public DxfPolyLine()
        {
            thickness = 0;
            startWidth = 0;
            endWidth = 0;
            meshMvertexcount = 0;
            meshNvertexcount = 0;
            surfaceMdensity = 0;
            surfaceNdensity = 0;
        }
    }

    public class DxfPoint
    {
        public double xval { get; set; }
        public double yval { get; set; }
        public double zval { get; set; }


        public DxfPoint()
        {
            xval = 0;
            yval = 0;
            zval = 0;

        }
        public DxfPoint(double xval, double yval, double zval)
        {
            this.xval = xval;
            this.yval = yval;
            this.zval = zval;

        }
    }

    public class DxfReaderFunctions
    {

        public void ReadArcAndCirle(StreamReader Reader, List<IDxfComponent> DxfList)
        {
            string temp;
            double x1 = 0;double y1 = 0;double z1 = 0;
            double x2 = 0;double y2 = 0;double z2 = 0;
            double startAngle = 0;
            double stopAngle  = 0;
            double radius = 0;

            for (int i = 0; i < 7; i++)
            {

                temp = (string)Reader.ReadLine();
                temp = temp.Trim();
                switch (temp)
                {
                    case "10":
                        temp = (string)Reader.ReadLine();
                        temp = temp.Trim();
                        x1 = Convert.ToDouble(temp.Replace(".", ","));
                        break;
      
                    case "20":
                        temp = (string)Reader.ReadLine();
                        temp = temp.Trim();
                        y1 = Convert.ToDouble(temp.Replace(".", ","));
                        break;
          
                    case "30":
                        temp = (string)Reader.ReadLine();
                        temp = temp.Trim();
                         z1 = Convert.ToDouble(temp.Replace(".", ","));
                        break;
                

                    case "40":
                        temp = (string)Reader.ReadLine();
                        temp = temp.Trim();
                        radius = Convert.ToDouble(temp.Replace(".", ","));
                        break;
         
                    case "210":
                        temp = (string)Reader.ReadLine();
                        temp = temp.Trim();
                        x2 = Convert.ToDouble(temp.Replace(".", ","));
                        break;
           
                    case "220":
                        temp = (string)Reader.ReadLine();
                        temp = temp.Trim();
                        y2 = Convert.ToDouble(temp.Replace(".", ","));
                        break;
                
                    case "230":
                        temp = (string)Reader.ReadLine();
                        temp = temp.Trim();
                        z2 = Convert.ToDouble(temp.Replace(".", ","));
                        break;
                }

            }

            string line1;
            string line2;

            line1 = (string)Reader.ReadLine();
            line2 = (string)Reader.ReadLine();

            if (line1 == "100" && line2 == "AcDbArc")
            {
        
                    for (int i = 0; i < 2; i++)
                    {
                            temp = (string)Reader.ReadLine();
                            temp = temp.Trim();
                            switch (temp)
                            {  
                                case "50":
                                    temp = (string)Reader.ReadLine();
                                    temp = temp.Trim();
                                    startAngle = Convert.ToDouble(temp.Replace(".", ","));
                                    break;

                                case "51":
                                    temp = (string)Reader.ReadLine();
                                    temp = temp.Trim();
                                    stopAngle = Convert.ToDouble(temp.Replace(".", ","));
                                    break;
                            }
  
                    }

                    DxfArc Arc = new DxfArc();
                    Arc.startPoint.xval = x1;
                    Arc.startPoint.yval = y1;
                    Arc.startPoint.zval = z1;
                    Arc.startAngle = startAngle;
                    Arc.radius = radius;
                    Arc.endPoint.xval = x2;
                    Arc.endPoint.yval = y2;
                    Arc.endPoint.zval = z2;
                    Arc.stopAngle = stopAngle;
                    DxfList.Add(Arc);

                }


                else
                {
                    DxfCircle Circle = new DxfCircle();
                    Circle.startPoint.xval = x1;
                    Circle.startPoint.yval = y1;
                    Circle.startPoint.zval = z1;

                    Circle.endPoint.xval = x2;
                    Circle.endPoint.yval = y2;
                    Circle.endPoint.zval = z2;
                    Circle.radius = radius;
                    DxfList.Add(Circle);


                }
              
        }

        public DxfLine ReadLine(StreamReader Reader)
        {

            string buffer;
            buffer = Reader.ReadLine();

            DxfLine Line = new DxfLine();

            while (buffer != "  0")
            {
                switch (buffer.Trim())
                {

                    case "10":
                        buffer = Reader.ReadLine();
                        Line.startPoint.xval = Convert.ToDouble(buffer.Replace(".", ","));
                        break;
                    case "20":
                        buffer = Reader.ReadLine();
                        Line.startPoint.yval = Convert.ToDouble(buffer.Replace(".", ","));
                        break;
                    case "30":
                        buffer = Reader.ReadLine();
                        Line.startPoint.zval = Convert.ToDouble(buffer.Replace(".", ","));
                        break;
                    case "39":
                        buffer = Reader.ReadLine();
                        Line.thickness = Convert.ToDouble(buffer.Replace(".", ","));
                        break;
                    case "11":
                        buffer = Reader.ReadLine();
                        Line.endPoint.xval = Convert.ToDouble(buffer.Replace(".", ","));
                        break;
                    case "21":
                        buffer = Reader.ReadLine();
                        Line.endPoint.yval = Convert.ToDouble(buffer.Replace(".", ","));
                        break;
                    case "31":
                        buffer = Reader.ReadLine();
                        Line.endPoint.zval = Convert.ToDouble(buffer.Replace(".", ","));
                        break;
                    case "210":
                        buffer = Reader.ReadLine();
                        Line.extrusionDirection.xval = Convert.ToDouble(buffer.Replace(".", ","));
                        break;
                    case "220":
                        buffer = Reader.ReadLine();
                        Line.extrusionDirection.yval = Convert.ToDouble(buffer.Replace(".", ","));
                        break;
                    case "230":
                        buffer = Reader.ReadLine();
                        Line.extrusionDirection.zval = Convert.ToDouble(buffer.Replace(".", ","));
                        break;

                    default:
                        break;
                }

                buffer = Reader.ReadLine();
              

            }
            return Line;
        }

        public DxfPolyLine ReadPolyLine(StreamReader Reader)
        {
            string buffer;
            buffer = Reader.ReadLine();
            bool Xvaluecheck = false;
            bool Yvaluecheck = false;
            bool Zvaluecheck = false;

            double xvalue = 0;
            double yvalue = 0;
            double zvalue = 0;

            byte Polylineflag;

            DxfPolyLine PolyLine = new DxfPolyLine();
            while (buffer != "ENDSEQ")
            {
                switch (buffer)
                {
                    case "10": buffer = Reader.ReadLine();
                        xvalue = double.Parse(buffer);
                        Xvaluecheck = true;
                        break;
                    case "20": buffer = Reader.ReadLine();
                        yvalue = double.Parse(buffer);
                        Yvaluecheck = true;
                        break;
                    case "30": buffer = Reader.ReadLine();
                        zvalue = double.Parse(buffer);
                        Zvaluecheck = true;
                        break;
                    case "39": buffer = Reader.ReadLine();
                        PolyLine.thickness = double.Parse(buffer);
                        break;
                    case "70": buffer = Reader.ReadLine();
                        Polylineflag = byte.Parse(buffer);
                        break;
                    case "40": buffer = Reader.ReadLine();
                        PolyLine.startWidth = ushort.Parse(buffer);
                        break;
                    case "41": buffer = Reader.ReadLine();
                        PolyLine.endWidth = ushort.Parse(buffer);
                        break;
                    case "71": buffer = Reader.ReadLine();
                        PolyLine.meshMvertexcount = ushort.Parse(buffer);
                        break;
                    case "72": buffer = Reader.ReadLine();
                        PolyLine.meshNvertexcount = ushort.Parse(buffer);
                        break;
                    case "73": buffer = Reader.ReadLine();
                        PolyLine.surfaceMdensity = ushort.Parse(buffer);
                        break;
                    case "74": buffer = Reader.ReadLine();
                        PolyLine.surfaceNdensity = ushort.Parse(buffer);
                        break;
                    case "210": buffer = Reader.ReadLine();
                        PolyLine.extrusionDirection.xval = double.Parse(buffer);
                        break;
                    case "220": buffer = Reader.ReadLine();
                        PolyLine.extrusionDirection.yval = double.Parse(buffer);
                        break;
                    case "230": buffer = Reader.ReadLine();
                        PolyLine.extrusionDirection.zval = double.Parse(buffer);
                        break;
                    default:
                        break;


                }
                if (Xvaluecheck && Yvaluecheck && Zvaluecheck)
                {
                    DxfPoint temporarypoint = new DxfPoint(xvalue, yvalue, zvalue);
                    PolyLine.controlPoints.Add(temporarypoint);
                    Xvaluecheck = false;
                    Yvaluecheck = false;
                    Zvaluecheck = false;
                }

            }
            return PolyLine;
        }

    }
}
