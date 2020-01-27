using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; 

namespace Constantjouncemotion
{
    public partial class Form1 : Form
    {
            Evaluater functions = new Evaluater();
            Equation jounce = new Equation();
            Equation accel = new Equation();
            Equation velocity = new Equation();
            Equation position = new Equation();
            List<Equation> motionequations = new List<Equation>();
            
            List<List<float>> allevaluatedvalues = new List<List<float>>();
            List<float> jounceevaluated = new List<float>();
            List<float> accelerationevaluated = new List<float>();
            List<float> velocityevaluated = new List<float>();
            List<float> positionevaluated = new List<float>();
            float timepassed;
            int timeintervalcount;
            int timevaluesforgraph;
            string timeintervalstring;

        public Form1()
        {
            InitializeComponent();
            motionequations.Add(jounce);
            motionequations.Add(accel);
            motionequations.Add(velocity);
            motionequations.Add(position);
            allevaluatedvalues.Add(jounceevaluated);
            allevaluatedvalues.Add(accelerationevaluated);
            allevaluatedvalues.Add(velocityevaluated);
            allevaluatedvalues.Add(positionevaluated);
            timeintervalcount = 0;
            timevaluesforgraph = 1;
            
            var chart1 = jounceaccelGraph.ChartAreas[0];
            chart1.AxisX.Minimum = 0;
            var chart2 = velocpositGraph.ChartAreas[0];
            chart2.AxisX.Minimum = 0; 
            jounceaccelGraph.Series.Add("Jounce");
            jounceaccelGraph.Series["Jounce"].ChartType = SeriesChartType.Line;
            jounceaccelGraph.Series["Jounce"].Color = Color.Red;
            jounceaccelGraph.Series.Add("Acceleration");
            jounceaccelGraph.Series["Acceleration"].ChartType = SeriesChartType.Line;
            jounceaccelGraph.Series["Acceleration"].Color = Color.Blue;

            velocpositGraph.Series.Add("Velocity");
            velocpositGraph.Series["Velocity"].ChartType = SeriesChartType.Spline;
            velocpositGraph.Series["Velocity"].Color = Color.Red;
            velocpositGraph.Series.Add("Position");
            velocpositGraph.Series["Position"].ChartType = SeriesChartType.Spline;
            velocpositGraph.Series["Position"].Color = Color.Blue;

            jounceaccelGraph.Series["Acceleration"].Points.AddXY(0, 0);
            velocpositGraph.Series["Velocity"].Points.AddXY(0, 0);
            velocpositGraph.Series["Position"].Points.AddXY(0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (disttimeevaluater.Text == string.Empty || float.Parse((disttimeevaluater.Text)) < 0)
            {
                return;
            }
            if(timeelapseRad.Checked)
            {
                timepassed = float.Parse((disttimeevaluater.Text));

                functions.JounceandEquationchanger(float.Parse((jounceevaluater.Text)), motionequations);
                functions.ListEvaluater(timepassed, allevaluatedvalues, motionequations);

                timeintervalstring = "t= " + timeintervalcount.ToString() + " - " + (timeintervalcount + timepassed);
                intervalList.Items.Add(timeintervalstring);
                timeintervalcount += int.Parse((disttimeevaluater.Text));
                jounceList.Items.Add("j= " + jounceevaluater.Text);
            }
            else if(distanceRad.Checked)
            {
                timepassed = functions.DistanceEvaluater(float.Parse((disttimeevaluater.Text)), float.Parse(jounceevaluater.Text), allevaluatedvalues, motionequations);
                timeintervalstring = "t= " + timeintervalcount.ToString() + " - " + (timeintervalcount + timepassed);
                intervalList.Items.Add(timeintervalstring);
                timeintervalcount += (int) timepassed;
                jounceList.Items.Add("j= " + jounceevaluater.Text + ", -" + jounceevaluater.Text);
              

            }
            
            if(timeintervalcount == 0)
            {
                jounceaccelGraph.Series["Jounce"].Points.AddXY(0, float.Parse(jounceevaluater.Text));
            }
            evaluatedjounce.Text = allevaluatedvalues[0][allevaluatedvalues[0].Count() - 1].ToString();
            evaluatedaccel.Text = allevaluatedvalues[1][allevaluatedvalues[1].Count() - 1].ToString();
            evaluatedvelocity.Text = allevaluatedvalues[2][allevaluatedvalues[2].Count() - 1].ToString();
            evaluatedposition.Text = allevaluatedvalues[3][allevaluatedvalues[3].Count() - 1].ToString();
            
            for (int i = 0; i < (int) timepassed; i++)
            {
                jounceaccelGraph.Series["Jounce"].Points.AddXY(timevaluesforgraph, allevaluatedvalues[0][timevaluesforgraph-1]);
                jounceaccelGraph.Series["Acceleration"].Points.AddXY(timevaluesforgraph, allevaluatedvalues[1][timevaluesforgraph-1]);
                velocpositGraph.Series["Velocity"].Points.AddXY(timevaluesforgraph, allevaluatedvalues[2][timevaluesforgraph-1]);
                velocpositGraph.Series["Position"].Points.AddXY(timevaluesforgraph, allevaluatedvalues[3][timevaluesforgraph-1]);
                         
                timevaluesList.Items.Add(timevaluesforgraph.ToString());
                jouncevaluesList.Items.Add(allevaluatedvalues[0][timevaluesforgraph -1 ]);
                accelvaluesList.Items.Add(allevaluatedvalues[1][timevaluesforgraph -1 ]);
                velocityvaluesList.Items.Add(allevaluatedvalues[2][timevaluesforgraph -1 ]);
                positionvaluesList.Items.Add(allevaluatedvalues[3][timevaluesforgraph -1]);

                timevaluesforgraph++;
            }

        }
    }
}
