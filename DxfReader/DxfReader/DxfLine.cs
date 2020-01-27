using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxfReader
{
    public class DxfLine
    {
        public DxfPoint startPoint = new DxfPoint();
        public DxfPoint endPoint = new DxfPoint();
        public DxfPoint extrusionDirection = new DxfPoint(0,0,1);
        public double thickness = 0; 

        
    }

    public class DxfPolyLine
    {
        public List<DxfPoint> controlPoints = new List<DxfPoint>() ; 
        public double thickness ;
        public DxfPoint extrusionDirection = new DxfPoint(0, 0, 1);
        public ushort startWidth;
        public ushort endWidth;
        public ushort meshMvertexcount ;
        public ushort meshNvertexcount ;
        
        public ushort surfaceMdensity ;
        public ushort surfaceNdensity ;
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
        public double xval {get; set;}
        public double yval {get; set;}
        public double zval {get; set;}
      

        public DxfPoint()
        {
            xval = 0;
            yval = 0;
            zval = 0;
             
        }
        public DxfPoint(double xval,double yval,double zval)
        {
            this.xval = xval ;
            this.yval = yval ;
            this.zval = zval;

        }
    }
}