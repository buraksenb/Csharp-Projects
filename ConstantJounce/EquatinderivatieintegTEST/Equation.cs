using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constantjouncemotion
{
    public class Equation
    {
        public uint timecounter = 0  ; 
        public float zerothorder;
        public float firstorder;
        public float secondorder;
        public float thirdorder;
        public float fourthorder;
        public float fifthorder;
        public float sixthorder;
        public float lastevaluatedvalue;
        public Equation()
        {
            zerothorder = 0;
            firstorder = 0;
            secondorder = 0;
            thirdorder = 0;
            fourthorder = 0;
            fifthorder = 0;
            sixthorder = 0;
            lastevaluatedvalue = 0; 
        }
        public Equation(float _zeroth)
        {
            this.zerothorder = _zeroth;
            firstorder = 0;
            secondorder = 0;
            thirdorder = 0;
            fourthorder = 0;
            fifthorder = 0;
            sixthorder = 0;
            lastevaluatedvalue = 0; 

        }
        public Equation(float _zeroth, float _first)
        {
            this.zerothorder = _zeroth;
            this.firstorder = _first;
            secondorder = 0;
            thirdorder = 0;
            fourthorder = 0;
            fifthorder = 0;
            sixthorder = 0;
            lastevaluatedvalue = 0; 

        }
        public Equation(float _zeroth, float _first, float _third)
        {
            this.zerothorder = _zeroth;
            this.firstorder = _first;
            this.thirdorder = _third;
            fourthorder = 0;
            fifthorder = 0;
            sixthorder = 0;
            lastevaluatedvalue = 0; 

        }
        public Equation(float _zeroth, float _first, float _third, float _fourth)
        {
            this.zerothorder = _zeroth;
            this.firstorder = _first;
            this.thirdorder = _third;
            this.fourthorder = _fourth;
            fifthorder = 0;
            sixthorder = 0;
            lastevaluatedvalue = 0; 

        }
        public Equation(float _zeroth, float _first, float _third, float _fourth, float _fifth)
        {
            this.zerothorder = _zeroth;
            this.firstorder = _first;
            this.thirdorder = _third;
            this.fourthorder = _fourth;
            this.fifthorder = _fifth;
            sixthorder = 0;
            lastevaluatedvalue = 0; 

        }
      
    }
}
