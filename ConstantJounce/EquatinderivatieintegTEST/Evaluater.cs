using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constantjouncemotion
{
    public class Evaluater
    {
        public float Evaluate(float _x, Equation evalutionequation)
        {
            _x += evalutionequation.timecounter;
            float evalutedvalue = 0;
            evalutedvalue = evalutionequation.zerothorder + evalutionequation.firstorder * _x + evalutionequation.secondorder * (float)Math.Pow(_x, 2) +
                evalutionequation.thirdorder * (float)Math.Pow(_x, 3) + evalutionequation.fourthorder * (float)Math.Pow(_x, 4) + evalutionequation.fifthorder * (float)Math.Pow(_x, 5);
            evalutionequation.lastevaluatedvalue = evalutedvalue;
            evalutionequation.timecounter++;
            return evalutedvalue;
        }
        public Equation Integration(Equation _integrand)
        {
            Equation integratedequation = new Equation();
            
            integratedequation.firstorder = _integrand.zerothorder;
            integratedequation.secondorder = _integrand.firstorder / 2;
            integratedequation.thirdorder = _integrand.secondorder / 3;
            integratedequation.fourthorder = _integrand.thirdorder / 4;
            integratedequation.fifthorder = _integrand.fourthorder / 5;
            integratedequation.sixthorder = _integrand.fifthorder / 6;

            integratedequation.timecounter = 0;
            return integratedequation;
        }
        public Equation Derivate(Equation _derivative)
        {
            Equation derivatedequation = new Equation();
            derivatedequation.zerothorder = _derivative.firstorder;
            derivatedequation.firstorder = _derivative.secondorder * 2;
            derivatedequation.secondorder = _derivative.thirdorder * 3;
            derivatedequation.thirdorder = _derivative.fourthorder * 4;
            derivatedequation.fourthorder = _derivative.fifthorder * 5;
            derivatedequation.fifthorder = _derivative.sixthorder * 6;

            return derivatedequation;
        }
        public void JounceandEquationchanger(float _jounce, List<Equation> _equationlist)
        {
            _equationlist[0].zerothorder = _jounce;
            float lastevhelper; 
            for (int i = 1; i < _equationlist.Count(); i++)
            {
                lastevhelper = _equationlist[i].lastevaluatedvalue;
                _equationlist[i] = Integration(_equationlist[i - 1]);
                _equationlist[i].zerothorder = lastevhelper;
            }
        }
        
        public void ListEvaluater(float _numberoftimes, List<List<float>> _evalutedvalues, List<Equation> _evaluationequation)
        {
            for (int j = 0; j < _numberoftimes; j++)
            {
                for (int i = 0; i < _evaluationequation.Count(); i++)
                {
                    _evalutedvalues[i].Add(Evaluate(1, _evaluationequation[i]));
                }
            }
        }
        public float DistanceEvaluater(float _distance,float _jounce, List<List<float>> _allevaluatedvalues, List<Equation> _evaluationequation)
        {
            float _timepassed;
            _timepassed = (float) Math.Pow(_distance / (2 * _jounce), 1.0 / 3.0);
            JounceandEquationchanger(_jounce, _evaluationequation);
            ListEvaluater(_timepassed, _allevaluatedvalues, _evaluationequation);

            JounceandEquationchanger(-_jounce, _evaluationequation);
            ListEvaluater(2 * _timepassed, _allevaluatedvalues, _evaluationequation);

            JounceandEquationchanger(_jounce, _evaluationequation);
            ListEvaluater(_timepassed, _allevaluatedvalues, _evaluationequation);

            return 4 * _timepassed; 
        }
        
        
        
        //public void ListPrinter(List<List<float>> _listforprint)
        //{
        //    for (int i = 0  ; i < _listforprint.Count(); i++)
        //    {
        //        for ( int j = 0  ; j < _listforprint[i].Count(); j++)
        //        {
        //            Console.Write(_listforprint[i][j].ToString() + "   "); 
        //        }
        //        Console.WriteLine(); 
        //    }
        //}
    }

}
