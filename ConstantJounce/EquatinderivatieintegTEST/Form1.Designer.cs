namespace Constantjouncemotion
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.jounceaccelGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.velocpositGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.jounceevaluater = new System.Windows.Forms.TextBox();
            this.disttimeevaluater = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.evaluatedjounce = new System.Windows.Forms.TextBox();
            this.evaluatedposition = new System.Windows.Forms.TextBox();
            this.evaluatedvelocity = new System.Windows.Forms.TextBox();
            this.evaluatedaccel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.jounceList = new System.Windows.Forms.ListBox();
            this.intervalList = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.positionvaluesList = new System.Windows.Forms.ListBox();
            this.velocityvaluesList = new System.Windows.Forms.ListBox();
            this.accelvaluesList = new System.Windows.Forms.ListBox();
            this.jouncevaluesList = new System.Windows.Forms.ListBox();
            this.timevaluesList = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.timeelapseRad = new System.Windows.Forms.RadioButton();
            this.distanceRad = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.jounceaccelGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.velocpositGraph)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // jounceaccelGraph
            // 
            chartArea1.Name = "ChartArea1";
            this.jounceaccelGraph.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.jounceaccelGraph.Legends.Add(legend1);
            this.jounceaccelGraph.Location = new System.Drawing.Point(65, 35);
            this.jounceaccelGraph.Name = "jounceaccelGraph";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.jounceaccelGraph.Series.Add(series1);
            this.jounceaccelGraph.Size = new System.Drawing.Size(448, 187);
            this.jounceaccelGraph.TabIndex = 0;
            this.jounceaccelGraph.Text = "chart1";
            // 
            // velocpositGraph
            // 
            chartArea2.Name = "ChartArea1";
            this.velocpositGraph.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.velocpositGraph.Legends.Add(legend2);
            this.velocpositGraph.Location = new System.Drawing.Point(53, 250);
            this.velocpositGraph.Name = "velocpositGraph";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.velocpositGraph.Series.Add(series2);
            this.velocpositGraph.Size = new System.Drawing.Size(460, 213);
            this.velocpositGraph.TabIndex = 1;
            this.velocpositGraph.Text = "chart2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(255, 43);
            this.button1.TabIndex = 2;
            this.button1.Text = "Simulate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // jounceevaluater
            // 
            this.jounceevaluater.Location = new System.Drawing.Point(77, 50);
            this.jounceevaluater.Name = "jounceevaluater";
            this.jounceevaluater.Size = new System.Drawing.Size(115, 22);
            this.jounceevaluater.TabIndex = 3;
            // 
            // disttimeevaluater
            // 
            this.disttimeevaluater.Location = new System.Drawing.Point(160, 90);
            this.disttimeevaluater.Name = "disttimeevaluater";
            this.disttimeevaluater.Size = new System.Drawing.Size(117, 22);
            this.disttimeevaluater.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(73, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Jounce value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(32, 375);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Jounce";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(32, 410);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Acceleration";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(32, 443);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Velocity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(29, 480);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Position";
            // 
            // evaluatedjounce
            // 
            this.evaluatedjounce.Location = new System.Drawing.Point(152, 373);
            this.evaluatedjounce.Name = "evaluatedjounce";
            this.evaluatedjounce.Size = new System.Drawing.Size(100, 22);
            this.evaluatedjounce.TabIndex = 11;
            // 
            // evaluatedposition
            // 
            this.evaluatedposition.Location = new System.Drawing.Point(152, 478);
            this.evaluatedposition.Name = "evaluatedposition";
            this.evaluatedposition.Size = new System.Drawing.Size(100, 22);
            this.evaluatedposition.TabIndex = 12;
            // 
            // evaluatedvelocity
            // 
            this.evaluatedvelocity.Location = new System.Drawing.Point(152, 441);
            this.evaluatedvelocity.Name = "evaluatedvelocity";
            this.evaluatedvelocity.Size = new System.Drawing.Size(100, 22);
            this.evaluatedvelocity.TabIndex = 13;
            // 
            // evaluatedaccel
            // 
            this.evaluatedaccel.Location = new System.Drawing.Point(152, 408);
            this.evaluatedaccel.Name = "evaluatedaccel";
            this.evaluatedaccel.Size = new System.Drawing.Size(100, 22);
            this.evaluatedaccel.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(108, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(295, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Velocity and Position versus Time";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(108, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(325, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Jounce and Acceleration versus Time";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(49, 340);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(192, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Last evaluated values";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(29, 182);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 20);
            this.label10.TabIndex = 18;
            this.label10.Text = "Jounce";
            // 
            // jounceList
            // 
            this.jounceList.FormattingEnabled = true;
            this.jounceList.ItemHeight = 16;
            this.jounceList.Location = new System.Drawing.Point(12, 205);
            this.jounceList.Name = "jounceList";
            this.jounceList.Size = new System.Drawing.Size(94, 132);
            this.jounceList.TabIndex = 19;
            // 
            // intervalList
            // 
            this.intervalList.FormattingEnabled = true;
            this.intervalList.ItemHeight = 16;
            this.intervalList.Location = new System.Drawing.Point(114, 205);
            this.intervalList.Name = "intervalList";
            this.intervalList.Size = new System.Drawing.Size(111, 132);
            this.intervalList.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(107, 182);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 20);
            this.label11.TabIndex = 21;
            this.label11.Text = "Time Interval";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(315, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(553, 506);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Teal;
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.positionvaluesList);
            this.tabPage1.Controls.Add(this.velocityvaluesList);
            this.tabPage1.Controls.Add(this.accelvaluesList);
            this.tabPage1.Controls.Add(this.jouncevaluesList);
            this.tabPage1.Controls.Add(this.timevaluesList);
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(545, 477);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "All Values";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(36, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 18);
            this.label16.TabIndex = 23;
            this.label16.Text = "Time";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(433, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 18);
            this.label15.TabIndex = 23;
            this.label15.Text = "Position";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(333, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 18);
            this.label14.TabIndex = 11;
            this.label14.Text = "Velocity";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(202, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 18);
            this.label13.TabIndex = 10;
            this.label13.Text = "Acceleration";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(99, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 18);
            this.label12.TabIndex = 9;
            this.label12.Text = "Jounce";
            // 
            // positionvaluesList
            // 
            this.positionvaluesList.FormattingEnabled = true;
            this.positionvaluesList.ItemHeight = 16;
            this.positionvaluesList.Location = new System.Drawing.Point(432, 44);
            this.positionvaluesList.Name = "positionvaluesList";
            this.positionvaluesList.Size = new System.Drawing.Size(80, 420);
            this.positionvaluesList.TabIndex = 8;
            // 
            // velocityvaluesList
            // 
            this.velocityvaluesList.FormattingEnabled = true;
            this.velocityvaluesList.ItemHeight = 16;
            this.velocityvaluesList.Location = new System.Drawing.Point(332, 44);
            this.velocityvaluesList.Name = "velocityvaluesList";
            this.velocityvaluesList.Size = new System.Drawing.Size(80, 420);
            this.velocityvaluesList.TabIndex = 6;
            // 
            // accelvaluesList
            // 
            this.accelvaluesList.FormattingEnabled = true;
            this.accelvaluesList.ItemHeight = 16;
            this.accelvaluesList.Location = new System.Drawing.Point(214, 44);
            this.accelvaluesList.Name = "accelvaluesList";
            this.accelvaluesList.Size = new System.Drawing.Size(80, 420);
            this.accelvaluesList.TabIndex = 4;
            // 
            // jouncevaluesList
            // 
            this.jouncevaluesList.FormattingEnabled = true;
            this.jouncevaluesList.ItemHeight = 16;
            this.jouncevaluesList.Location = new System.Drawing.Point(102, 43);
            this.jouncevaluesList.Name = "jouncevaluesList";
            this.jouncevaluesList.Size = new System.Drawing.Size(80, 420);
            this.jouncevaluesList.TabIndex = 2;
            // 
            // timevaluesList
            // 
            this.timevaluesList.FormattingEnabled = true;
            this.timevaluesList.ItemHeight = 16;
            this.timevaluesList.Location = new System.Drawing.Point(31, 44);
            this.timevaluesList.Name = "timevaluesList";
            this.timevaluesList.Size = new System.Drawing.Size(50, 420);
            this.timevaluesList.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.jounceaccelGraph);
            this.tabPage2.Controls.Add(this.velocpositGraph);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(545, 477);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graphs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // timeelapseRad
            // 
            this.timeelapseRad.AutoSize = true;
            this.timeelapseRad.Location = new System.Drawing.Point(12, 80);
            this.timeelapseRad.Name = "timeelapseRad";
            this.timeelapseRad.Size = new System.Drawing.Size(126, 21);
            this.timeelapseRad.TabIndex = 27;
            this.timeelapseRad.TabStop = true;
            this.timeelapseRad.Text = "Time elapsed";
            this.timeelapseRad.UseVisualStyleBackColor = true;
            // 
            // distanceRad
            // 
            this.distanceRad.AutoSize = true;
            this.distanceRad.Location = new System.Drawing.Point(12, 107);
            this.distanceRad.Name = "distanceRad";
            this.distanceRad.Size = new System.Drawing.Size(92, 21);
            this.distanceRad.TabIndex = 28;
            this.distanceRad.TabStop = true;
            this.distanceRad.Text = "Distance";
            this.distanceRad.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(927, 537);
            this.Controls.Add(this.distanceRad);
            this.Controls.Add(this.timeelapseRad);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.intervalList);
            this.Controls.Add(this.jounceList);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.evaluatedaccel);
            this.Controls.Add(this.evaluatedvelocity);
            this.Controls.Add(this.evaluatedposition);
            this.Controls.Add(this.evaluatedjounce);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.disttimeevaluater);
            this.Controls.Add(this.jounceevaluater);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Constant Jounce Motion Analyzer";
            ((System.ComponentModel.ISupportInitialize)(this.jounceaccelGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.velocpositGraph)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart jounceaccelGraph;
        private System.Windows.Forms.DataVisualization.Charting.Chart velocpositGraph;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox jounceevaluater;
        private System.Windows.Forms.TextBox disttimeevaluater;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox evaluatedjounce;
        private System.Windows.Forms.TextBox evaluatedposition;
        private System.Windows.Forms.TextBox evaluatedvelocity;
        private System.Windows.Forms.TextBox evaluatedaccel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox jounceList;
        private System.Windows.Forms.ListBox intervalList;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox positionvaluesList;
        private System.Windows.Forms.ListBox velocityvaluesList;
        private System.Windows.Forms.ListBox accelvaluesList;
        private System.Windows.Forms.ListBox jouncevaluesList;
        private System.Windows.Forms.ListBox timevaluesList;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RadioButton timeelapseRad;
        private System.Windows.Forms.RadioButton distanceRad;
    }
}