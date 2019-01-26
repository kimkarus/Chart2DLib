using System;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Chart2DLib
{
    public class LineStyle
    {
        private DashStyle linePattern = DashStyle.Solid;
        private Color lineColor = Color.Black;
        private float LineThickness = 1.0f;
        private bool isVisible = true;
        private PlotLinesMethodEnum pltLineMethod = PlotLinesMethodEnum.Lines;
        public PlotLinesMethodEnum PlotMethod
        {
            get { return pltLineMethod; }
            set { pltLineMethod = value; }
        }
        public enum PlotLinesMethodEnum //pltLineMethod
        {
            Lines = 0,
            Splines = 1
        }
        public LineStyle()
        {
        }
        public bool IsVisible
        {
        get { return isVisible; }
        set { isVisible = value; }
        }
        virtual public DashStyle Pattern
        {
        get { return linePattern; }
        set { linePattern = value; }
        }
        public float Thickness
        {
        get { return LineThickness; }
        set { LineThickness = value; }
        }
        virtual public Color LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }
    }
}