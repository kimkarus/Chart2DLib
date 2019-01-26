using System;
using System.Collections;
using System.Drawing;
namespace Chart2DLib
{
    public class DataSeries
    {
        private ArrayList pointList;
        private LineStyle lineStyle;
        private BarStyle barStyle;
        private SymbolStyle symbolStyle;
        private ChartStyleEnum chartStyle;
        private bool isY2Data = false;
        private string seriesName = "Default Name";
        public DataSeries(ChartStyleEnum ChartStyle=0)
        {
            chartStyle = ChartStyle;
            if (ChartStyle == ChartStyleEnum.LineStyle)
            {
                lineStyle = new LineStyle();
                symbolStyle = new SymbolStyle();
            }
            if (ChartStyle == ChartStyleEnum.BarStyle)
            {
                barStyle = new BarStyle();
            }
            pointList = new ArrayList();
            
        }
        public LineStyle LineStyle
        {
            get { return lineStyle; }
            set { lineStyle = value; }
        }
        public BarStyle BarStyle
        {
            get { return barStyle; }
            set { barStyle = value; }
        }
        public SymbolStyle SymbolStyle
        {
            get { return symbolStyle; }
            set { symbolStyle = value; }
        }
        public bool IsY2Data
        {
            get { return isY2Data; }
            set { isY2Data = value; }
        }
        public string SeriesName
        {
            get { return seriesName; }
            set { seriesName = value; }
        }
        public ArrayList PointList
        {
            get { return pointList; }
            set { pointList = value; }
        }
        public void AddPoint(PointF pt)
        {
            pointList.Add(pt);
        }
        public ChartStyleEnum ChartStyle
        {
            get { return chartStyle; }
            set { chartStyle = value; }
        }
        public enum ChartStyleEnum
        {
            LineStyle = 0,
            BarStyle = 1
        }
    }
}