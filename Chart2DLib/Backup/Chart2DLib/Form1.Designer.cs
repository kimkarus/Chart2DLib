namespace Chart2DLib
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
            Chart2DLib.DataCollection dataCollection1 = new Chart2DLib.DataCollection();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chart2D2 = new Chart2DLib.Chart2D();
            this.SuspendLayout();
            // 
            // chart2D2
            // 
            this.chart2D2.C2ChartArea.ChartBackColor = System.Drawing.SystemColors.Control;
            this.chart2D2.C2ChartArea.ChartBorderColor = System.Drawing.SystemColors.Control;
            this.chart2D2.C2ChartArea.PlotBackColor = System.Drawing.Color.White;
            this.chart2D2.C2ChartArea.PlotBorderColor = System.Drawing.Color.Black;
            dataCollection1.DataSeriesIndex = 0;
            dataCollection1.DataSeriesList = ((System.Collections.ArrayList)(resources.GetObject("dataCollection1.DataSeriesList")));
            this.chart2D2.C2DataCollection = dataCollection1;
            this.chart2D2.C2Grid.GridColor = System.Drawing.Color.LightGray;
            this.chart2D2.C2Grid.GridPattern = System.Drawing.Drawing2D.DashStyle.Solid;
            this.chart2D2.C2Grid.GridThickness = 1F;
            this.chart2D2.C2Grid.IsXGrid = true;
            this.chart2D2.C2Grid.IsYGrid = true;
            this.chart2D2.C2Label.LabelFont = new System.Drawing.Font("Arial", 10F);
            this.chart2D2.C2Label.LabelFontColor = System.Drawing.Color.Black;
            this.chart2D2.C2Label.TickFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chart2D2.C2Label.TickFontColor = System.Drawing.Color.Black;
            this.chart2D2.C2Label.XLabel = "X Axis";
            this.chart2D2.C2Label.Y2Label = "Y2 Axis";
            this.chart2D2.C2Label.YLabel = "Y Axis";
            this.chart2D2.C2Legend.IsBorderVisible = true;
            this.chart2D2.C2Legend.IsLegendVisible = false;
            this.chart2D2.C2Legend.LegendBackColor = System.Drawing.Color.White;
            this.chart2D2.C2Legend.LegendBorderColor = System.Drawing.Color.Black;
            this.chart2D2.C2Legend.LegendFont = new System.Drawing.Font("Arial", 8F);
            this.chart2D2.C2Legend.LegendPosition = Chart2DLib.Legend.LegendPositionEnum.NorthEast;
            this.chart2D2.C2Legend.TextColor = System.Drawing.Color.Black;
            this.chart2D2.C2Title.Title = "Title";
            this.chart2D2.C2Title.TitleFont = new System.Drawing.Font("Arial", 12F);
            this.chart2D2.C2Title.TitleFontColor = System.Drawing.Color.Black;
            this.chart2D2.C2XAxis.XLimMax = 10F;
            this.chart2D2.C2XAxis.XLimMin = 0F;
            this.chart2D2.C2XAxis.XTick = 2F;
            this.chart2D2.C2Y2Axis.IsY2Axis = false;
            this.chart2D2.C2Y2Axis.Y2LimMax = 100F;
            this.chart2D2.C2Y2Axis.Y2LimMin = 0F;
            this.chart2D2.C2Y2Axis.Y2Tick = 20F;
            this.chart2D2.C2YAxis.YLimMax = 10F;
            this.chart2D2.C2YAxis.YLimMin = 0F;
            this.chart2D2.C2YAxis.YTick = 2F;
            this.chart2D2.Location = new System.Drawing.Point(12, 12);
            this.chart2D2.Name = "chart2D2";
            this.chart2D2.Size = new System.Drawing.Size(487, 347);
            this.chart2D2.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 381);
            this.Controls.Add(this.chart2D2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Chart2D chart2D2;



    }
}