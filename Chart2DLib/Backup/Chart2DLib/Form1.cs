using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Chart2DLib;

namespace Chart2DLib
{
    public partial class Form1 : Form
    {
        private DataSeries ds;
        Chart2D chart2D1 = new Chart2D();
        public Form1()
        {
            InitializeComponent();
            
            chart2D1.Dock = DockStyle.Fill;
            chart2D1.C2ChartArea.ChartBackColor = Color.White;
            ds = new DataSeries();
            chart2D1.C2Legend.IsLegendVisible = true;
            AddData();
        }
        private void AddData()
        {
            // Override ChartStyle properties:
            chart2D1.C2XAxis.XLimMin = 0f;
            chart2D1.C2XAxis.XLimMax = 6f;
            chart2D1.C2YAxis.YLimMin = -1.5f;
            chart2D1.C2YAxis.YLimMax = 1.5f;
            chart2D1.C2XAxis.XTick = 1.0f;
            chart2D1.C2YAxis.YTick = 0.5f;
            chart2D1.C2Label.XLabel = "This is X axis";
            chart2D1.C2Label.YLabel = "This is Y axis";
            chart2D1.C2Title.Title = "Sine and Cosine Chart";
            chart2D1.C2DataCollection.DataSeriesList.Clear();
            // Add Sine data with 7 data points:
            ds = new DataSeries();
            ds.LineStyle.LineColor = Color.Red;
            ds.LineStyle.Thickness = 2f;
            ds.LineStyle.Pattern = DashStyle.Dash;
            ds.LineStyle.PlotMethod =
            LineStyle.PlotLinesMethodEnum.Lines;
            ds.SeriesName = "Sine";
            ds.SymbolStyle.SymbolType =
            SymbolStyle.SymbolTypeEnum.Diamond;
            ds.SymbolStyle.BorderColor = Color.Red;
            ds.SymbolStyle.FillColor = Color.Yellow;
            ds.SymbolStyle.BorderThickness = 1f;
            for (int i = 0; i < 7; i++)
            {
                ds.AddPoint(new PointF(i / 1.0f,
                (float)Math.Sin(i / 1.0f)));
            }
            chart2D1.C2DataCollection.Add(ds);
            // Add Cosine data with 7 data points:
            ds = new DataSeries();
            ds.LineStyle.LineColor = Color.Blue;
            ds.LineStyle.Thickness = 1f;
            ds.LineStyle.Pattern = DashStyle.Solid;
            ds.LineStyle.PlotMethod =
            LineStyle.PlotLinesMethodEnum.Splines;
            ds.SeriesName = "Cosine";
            ds.SymbolStyle.SymbolType =
            SymbolStyle.SymbolTypeEnum.Triangle;
            ds.SymbolStyle.BorderColor = Color.Blue;
            for (int i = 0; i < 7; i++)
            {
                ds.AddPoint(new PointF(i / 1.0f,
                (float)Math.Cos(i / 1.0f)));
            }
            chart2D1.C2DataCollection.Add(ds);
        }
    }
}