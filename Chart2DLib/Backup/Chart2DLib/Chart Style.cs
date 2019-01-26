using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Chart2DLib
{
    public class ChartStyle
    {
        private Chart2D chart2d;
        private Rectangle chartArea;
        private Rectangle plotArea;
        public ChartStyle(Chart2D ct2d)
        {
            chart2d = ct2d;
            chartArea = chart2d.ClientRectangle;
            PlotArea = chartArea;
        }
        [Description("Sets the size for the chart area."), Category("Appearance")]
        public Rectangle ChartArea
        {
            get { return chartArea; }
            set { chartArea = value; }
        }
        [Description("Sets size for the plot area."), Category("Appearance")]
        public Rectangle PlotArea
        {
            get { return plotArea; }
            set { plotArea = value; }
        }
        public void AddChartStyle(Graphics g, ChartArea ca, XAxis xa, YAxis ya, Y2Axis y2a, Grid gd, XYLabel lb, Title2D tl)
        {
            // Draw TotalChartArea, ChartArea, and PlotArea:
            SetPlotArea(g, xa, ya, y2a, gd, lb, tl);
            Pen aPen = new Pen(ca.ChartBorderColor, 1f);
            SolidBrush aBrush = new SolidBrush(ca.ChartBackColor);
            g.FillRectangle(aBrush, ChartArea);
            g.DrawRectangle(aPen, ChartArea);
            aPen = new Pen(ca.PlotBorderColor, 1f);
            aBrush = new SolidBrush(ca.PlotBackColor);
            g.FillRectangle(aBrush, PlotArea);
            g.DrawRectangle(aPen, PlotArea);
            SizeF tickFontSize = g.MeasureString("A", lb.TickFont);
            // Create vertical gridlines:
            float fX, fY;
            if (gd.IsYGrid == true)
            {
                aPen = new Pen(gd.GridColor, 1f);
                aPen.DashStyle = gd.GridPattern;
                for (fX = xa.XLimMin + xa.XTick; fX < xa.XLimMax;
                fX += xa.XTick)
                {
                    g.DrawLine(aPen, Point2D(new PointF(fX, ya.YLimMin), xa, ya), Point2D(new PointF(fX, ya.YLimMax), xa, ya));
                }
                // Create horizontal gridlines:
                if (gd.IsXGrid == true)
                {
                    aPen = new Pen(gd.GridColor, 1f);
                    aPen.DashStyle = gd.GridPattern;
                    for (fY = ya.YLimMin + ya.YTick; fY < ya.YLimMax;
                    fY += ya.YTick)
                    {
                        g.DrawLine(aPen, Point2D(new PointF(xa.XLimMin, fY), xa, ya), Point2D(new PointF(xa.XLimMax, fY), xa, ya));
                    }
                }
                // Create the x-axis tick marks:
                aBrush = new SolidBrush(lb.TickFontColor);
                for (fX = xa.XLimMin; fX <= xa.XLimMax; fX += xa.XTick)
                {
                    PointF yAxisPoint = Point2D(new PointF(fX, ya.YLimMin), xa, ya);
                    g.DrawLine(Pens.Black, yAxisPoint, new PointF(yAxisPoint.X, yAxisPoint.Y - 5f));
                    StringFormat sFormat = new StringFormat();
                    sFormat.Alignment = StringAlignment.Far;
                    SizeF sizeXTick = g.MeasureString(fX.ToString(), lb.TickFont);
                    g.DrawString(fX.ToString(), lb.TickFont, aBrush, new PointF(yAxisPoint.X + sizeXTick.Width / 2, yAxisPoint.Y + 4f), sFormat);
                }
                for (fY = ya.YLimMin; fY <= ya.YLimMax; fY += ya.YTick)
                {
                    PointF xAxisPoint = Point2D(new PointF(xa.XLimMin, fY), xa, ya);
                    g.DrawLine(Pens.Black, xAxisPoint, new PointF(xAxisPoint.X + 5f, xAxisPoint.Y));
                    StringFormat sFormat = new StringFormat();
                    sFormat.Alignment = StringAlignment.Far;
                    ///////////
                    float n1 = 0;
                    float n2 = 0;
                    //float n3 = 0;
                    n1 = xAxisPoint.X - 3f;
                    n2 = xAxisPoint.Y - tickFontSize.Height / 2;
                    //g.DrawString(fY.ToString, lb.TickFont, aBrush, new PointF());
                    g.DrawString(fY.ToString(), lb.TickFont, aBrush, new PointF(n1, n2), sFormat);
                }
                // Create the y2-axis tick marks:
                if (y2a.IsY2Axis)
                {
                    for (fY = y2a.Y2LimMin; fY <= y2a.Y2LimMax;
                    fY += y2a.Y2Tick)
                    {
                        PointF x2AxisPoint = Point2DY2(new
                        PointF(xa.XLimMax, fY), xa, y2a);
                        g.DrawLine(Pens.Black, x2AxisPoint,
                        new PointF(x2AxisPoint.X - 5f,
                        x2AxisPoint.Y));
                        StringFormat sFormat = new StringFormat();
                        sFormat.Alignment = StringAlignment.Near;
                        g.DrawString(fY.ToString(), lb.TickFont,
                        aBrush, new PointF(x2AxisPoint.X + 3f,
                        x2AxisPoint.Y - tickFontSize.Height / 2),
                        sFormat);
                    }
                }
                aPen.Dispose();
                aBrush.Dispose();
                AddLabels(g, xa, ya, y2a, gd, lb, tl);
            }
        }
        private void SetPlotArea(Graphics g, XAxis xa, YAxis ya, Y2Axis y2a, Grid gd, XYLabel lb, Title2D tl)
        {
            // Set PlotArea:
            int n1 = 0;
            /*int n2 = 0;
            int n3 = 0;
            int n4 = 0;*/
            float xOffset = ChartArea.Width / 30.0f;
            float yOffset = ChartArea.Height / 30.0f;
            SizeF labelFontSize = g.MeasureString("A", lb.LabelFont);
            SizeF titleFontSize = g.MeasureString("A", tl.TitleFont);
            if (tl.Title.ToUpper() == "NO TITLE")
            {
                titleFontSize.Width = 8f;
                titleFontSize.Height = 8f;
            }
            float xSpacing = xOffset / 3.0f;
            float ySpacing = yOffset / 3.0f;
            SizeF tickFontSize = g.MeasureString("A", lb.TickFont);
            float tickSpacing = 2f;
            SizeF yTickSize = g.MeasureString(ya.YLimMin.ToString(), lb.TickFont);
            for (float yTick = ya.YLimMin; yTick <= ya.YLimMax; yTick += ya.YTick)
            {
                SizeF tempSize = g.MeasureString(yTick.ToString(),
                lb.TickFont);
                if (yTickSize.Width < tempSize.Width)
                {
                    yTickSize = tempSize;
                }
            }
            float leftMargin = xOffset + labelFontSize.Width + xSpacing + yTickSize.Width + tickSpacing;
            float rightMargin = xOffset;
            float topMargin = yOffset + titleFontSize.Height +
            ySpacing;
            float bottomMargin = yOffset + labelFontSize.Height +
            ySpacing + tickSpacing + tickFontSize.Height;
            if (!y2a.IsY2Axis)
            {
                // Define the plot area with one Y axis:
                int plotX = ChartArea.X + (int)leftMargin;
                int plotY = ChartArea.Y + (int)topMargin;
                n1 = ChartArea.Width - (int)leftMargin - 2 * (int)rightMargin;
                //int plotWidth = (ChartArea.Width - (int)leftMargin) – 2* (int)rightMargin;
                int plotWidth = n1;
                //
                n1 = ChartArea.Height - (int)topMargin - (int)bottomMargin;
                int plotHeight = n1;
                //int plotHeight = ChartArea.Height – (int)topMargin - (int)bottomMargin;
                PlotArea = new Rectangle(plotX, plotY, plotWidth, plotHeight);
            }
            else
            {
                // Define the plot area with Y and Y2 axes:
                SizeF y2TickSize = g.MeasureString(
                y2a.Y2LimMin.ToString(), lb.TickFont);
                for (float y2Tick = y2a.Y2LimMin; y2Tick <=
                y2a.Y2LimMax; y2Tick += y2a.Y2Tick)
                {
                    SizeF tempSize2 = g.MeasureString(
                    y2Tick.ToString(), lb.TickFont);
                    if (y2TickSize.Width < tempSize2.Width)
                    {
                        y2TickSize = tempSize2;
                    }
                }
                rightMargin = xOffset + labelFontSize.Width + xSpacing + y2TickSize.Width + tickSpacing;
                int plotX = ChartArea.X + (int)leftMargin;
                int plotY = ChartArea.Y + (int)topMargin;
                n1 = ChartArea.Width - (int)leftMargin - (int)rightMargin;
                int plotWidth = n1;
                //int plotWidth = ChartArea.Width – (int)leftMargin - (int)rightMargin;
                n1 = ChartArea.Height - (int)leftMargin - (int)rightMargin;
                int plotHeight = n1;
                //int plotHeight = ChartArea.Height – (int)topMargin - (int)bottomMargin;
                PlotArea = new Rectangle(plotX, plotY, plotWidth, plotHeight);
            }
        }
        private void AddLabels(Graphics g, XAxis xa, YAxis ya, Y2Axis y2a, Grid gd, XYLabel lb, Title2D tl)
        {
            float xOffset = ChartArea.Width / 30.0f;
            float yOffset = ChartArea.Height / 30.0f;
            SizeF labelFontSize = g.MeasureString("A", lb.LabelFont);
            SizeF titleFontSize = g.MeasureString("A", tl.TitleFont);
            // Add horizontal axis label:
            SolidBrush aBrush = new SolidBrush(lb.LabelFontColor);
            SizeF stringSize = g.MeasureString(lb.XLabel,
            lb.LabelFont);
            g.DrawString(lb.XLabel, lb.LabelFont, aBrush,
            new Point(PlotArea.Left + PlotArea.Width / 2 -
            (int)stringSize.Width / 2, ChartArea.Bottom -
            (int)yOffset - (int)labelFontSize.Height));
            // Add y-axis label:
            StringFormat sFormat = new StringFormat();
            sFormat.Alignment = StringAlignment.Center;
            stringSize = g.MeasureString(lb.YLabel, lb.LabelFont);
            // Save the state of the current Graphics object
            GraphicsState gState = g.Save();
            g.TranslateTransform(ChartArea.X + xOffset, ChartArea.Y
            + yOffset + titleFontSize.Height
            + yOffset / 3 + PlotArea.Height / 2);
            g.RotateTransform(-90);
            g.DrawString(lb.YLabel, lb.LabelFont, aBrush, 0, 0,
            sFormat);
            // Restore it:
            g.Restore(gState);
            // Add y2-axis label:
            if (y2a.IsY2Axis)
            {
                stringSize = g.MeasureString(lb.Y2Label,
                lb.LabelFont);
                // Save the state of the current Graphics object
                GraphicsState gState2 = g.Save();
                g.TranslateTransform(ChartArea.X + ChartArea.Width -
                xOffset - labelFontSize.Width,
                ChartArea.Y + yOffset + titleFontSize.Height
                + yOffset / 3 + PlotArea.Height / 2);
                g.RotateTransform(-90);
                g.DrawString(lb.Y2Label, lb.LabelFont, aBrush, 0, 0,
                sFormat);
                // Restore it:
                g.Restore(gState2);
            }
            // Add title:
            aBrush = new SolidBrush(tl.TitleFontColor);
            stringSize = g.MeasureString(tl.Title, tl.TitleFont);
            if (tl.Title.ToUpper() != "NO TITLE")
            {
                g.DrawString(tl.Title, tl.TitleFont, aBrush,
                new Point(PlotArea.Left + PlotArea.Width / 2 -
                (int)stringSize.Width / 2, ChartArea.Top +
                (int)yOffset));
            }
            aBrush.Dispose();
        }
        public PointF Point2DY2(PointF pt, XAxis xa, Y2Axis y2a)
        {
            PointF aPoint = new PointF();
            if (pt.X < xa.XLimMin || pt.X > xa.XLimMax ||
            pt.Y < y2a.Y2LimMin || pt.Y > y2a.Y2LimMax)
            {
                pt.X = Single.NaN;
                pt.Y = Single.NaN;
            }
            aPoint.X = PlotArea.X + (pt.X - xa.XLimMin) *
            PlotArea.Width / (xa.XLimMax - xa.XLimMin);
            aPoint.Y = PlotArea.Bottom - (pt.Y - y2a.Y2LimMin) *
            PlotArea.Height / (y2a.Y2LimMax - y2a.Y2LimMin);
            return aPoint;
        }
        public PointF Point2D(PointF pt, XAxis xa, YAxis ya)
        {
            PointF aPoint = new PointF();
            if (pt.X < xa.XLimMin || pt.X > xa.XLimMax ||
            pt.Y < ya.YLimMin || pt.Y > ya.YLimMax)
            {
                pt.X = Single.NaN;
                pt.Y = Single.NaN;
            }
            aPoint.X = PlotArea.X + (pt.X - xa.XLimMin) *
            PlotArea.Width / (xa.XLimMax - xa.XLimMin);
            aPoint.Y = PlotArea.Bottom - (pt.Y - ya.YLimMin) *
            PlotArea.Height / (ya.YLimMax - ya.YLimMin);
            return aPoint;
        }
    }
    [TypeConverter(typeof(ChartAreaConverter))]
    public class ChartArea
    {
        private Chart2D chart2d;
        private Color chartBackColor;
        private Color chartBorderColor;
        private Color plotBackColor = Color.White;
        private Color plotBorderColor = Color.Black;
        public ChartArea(Chart2D ct2d)
        {
            chart2d = ct2d;
            chartBackColor = chart2d.BackColor;
            chartBorderColor = chart2d.BackColor;
        }
        [Description("The background color of the chart area."),
        Category("Appearance")]
        public Color ChartBackColor
        {
            get { return chartBackColor; }
            set
            {
                chartBackColor = value;
                chart2d.Invalidate();
            }
        }
        [Description("The border color of the chart area."),
        Category("Appearance")]
        public Color ChartBorderColor
        {
            get { return chartBorderColor; }
            set
            {
                chartBorderColor = value;
                chart2d.Invalidate();
            }
        }
        [Description("The background color of the plot area."),
        Category("Appearance")]
        public Color PlotBackColor
        {
            get { return plotBackColor; }
            set
            {
                plotBackColor = value;
                chart2d.Invalidate();
            }
        }
        [Description("The border color of the plot area."),
        Category("Appearance")]
        public Color PlotBorderColor
        {
            get { return plotBorderColor; }
            set
            {
                plotBorderColor = value;
                chart2d.Invalidate();
            }
        }
    }
    public class ChartAreaConverter : TypeConverter
    {
        // allows us to display the + symbol near the property name
        public override bool GetPropertiesSupported(
        ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection
        GetProperties(ITypeDescriptorContext context,
        object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(ChartArea));
        }
    }
    [TypeConverter(typeof(XAxisConverter))]
    public class XAxis
    {
        private float xLimMin = 0f;
        private float xLimMax = 10f;
        private float xTick = 2f;
        private Chart2D chart2d;
        public XAxis(Chart2D ct2d)
        {
            chart2d = ct2d;
        }
        [Description("Sets the maximum limit for the X axis."),
        Category("Appearance")]
        public float XLimMax
        {
            get { return xLimMax; }
            set
            {
                xLimMax = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the minimum limit for the X axis."),
        Category("Appearance")]
        public float XLimMin
        {
            get { return xLimMin; }
            set
            {
                xLimMin = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the ticks for the X axis."),
        Category("Appearance")]
        public float XTick
        {
            get { return xTick; }
            set
            {
                xTick = value;
                chart2d.Invalidate();
            }
        }
    }
    public class XAxisConverter : TypeConverter
    {
        public XAxisConverter()
        {
        }
        // allows us to display the + symbol near the property name
        public override bool GetPropertiesSupported(
        ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection
        GetProperties(ITypeDescriptorContext context,
        object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(XAxis));
        }
    }
    [TypeConverter(typeof(YAxisConverter))]
    public class YAxis
    {
        private float yLimMin = 0f;
        private float yLimMax = 10f;
        private float yTick = 2f;
        private Chart2D chart2d;
        public YAxis(Chart2D ct2d)
        {
            chart2d = ct2d;
        }
        [Description("Sets the maximum limit for the Y axis."),
        Category("Appearance")]
        public float YLimMax
        {
            get { return yLimMax; }
            set
            {
                yLimMax = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the minimum limit for the Y axis."),
        Category("Appearance")]
        public float YLimMin
        {
            get { return yLimMin; }
            set
            {
                yLimMin = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the ticks for the Y axis."),
        Category("Appearance")]
        public float YTick
        {
            get { return yTick; }
            set
            {
                yTick = value;
                chart2d.Invalidate();
            }
        }
    }
    public class YAxisConverter : TypeConverter
    {
        public YAxisConverter()
        {
        }
        // allows us to display the + symbol near the property name
        public override bool GetPropertiesSupported(
        ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection
        GetProperties(ITypeDescriptorContext context,
        object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(YAxis));
        }
    }
    [TypeConverter(typeof(Y2AxisConverter))]
    public class Y2Axis
    {
        private float y2LimMin = 0f;
        private float y2LimMax = 100f;
        private float y2Tick = 20f;
        private bool isY2Axis = false;
        private Chart2D chart2d;
        public Y2Axis(Chart2D ct2d)
        {
            chart2d = ct2d;
        }
        [Description("Indicates whether the chart has the Y2 axis."),
        Category("Appearance")]
        public bool IsY2Axis
        {
            get { return isY2Axis; }
            set
            {
                isY2Axis = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the maximum limit for the Y2 axis."),
        Category("Appearance")]
        public float Y2LimMax
        {
            get { return y2LimMax; }
            set
            {
                y2LimMax = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the minimum limit for the Y2 axis."),
        Category("Appearance")]
        public float Y2LimMin
        {
            get { return y2LimMin; }
            set
            {
                y2LimMin = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the ticks for the Y2 axis."),
        Category("Appearance")]
        public float Y2Tick
        {
            get { return y2Tick; }
            set
            {
                y2Tick = value;
                chart2d.Invalidate();
            }
        }
    }
    public class Y2AxisConverter : TypeConverter
    {
        public Y2AxisConverter()
        {
        }
        // allows us to display the + symbol near the property name
        public override bool GetPropertiesSupported(
        ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection
        GetProperties(ITypeDescriptorContext context,
        object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(Y2Axis));
        }
    }
    [TypeConverter(typeof(GridConverter))]
    public class Grid
    {
        private DashStyle gridPattern = DashStyle.Solid;
        private Color gridColor = Color.LightGray;
        private float gridLineThickness = 1.0f;
        private bool isXGrid = true;
        private bool isYGrid = true;
        private Chart2D chart2d;
        public Grid(Chart2D ct2d)
        {
            chart2d = ct2d;
        }
        [Description("Indicates whether the X grid is shown."),
        Category("Appearance")]
        public bool IsXGrid
        {
            get { return isXGrid; }
            set
            {
                isXGrid = value;
                chart2d.Invalidate();
            }
        }
        [Description("Indicates whether the Y grid is shown."),
        Category("Appearance")]
        public bool IsYGrid
        {
            get { return isYGrid; }
            set
            {
                isYGrid = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the line pattern for the grid lines."),
        Category("Appearance")]
        virtual public DashStyle GridPattern
        {
            get { return gridPattern; }
            set
            {
                gridPattern = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the thickness for the grid lines."),
        Category("Appearance")]
        public float GridThickness
        {
            get { return gridLineThickness; }
            set
            {
                gridLineThickness = value;
                chart2d.Invalidate();
            }
        }
        [Description("The color used to display the grid lines."),
        Category("Appearance")]
        virtual public Color GridColor
        {
            get { return gridColor; }
            set
            {
                gridColor = value;
                chart2d.Invalidate();
            }
        }
    }
    public class GridConverter : TypeConverter
    {
        public GridConverter()
        {
        }
        // allows us to display the + symbol near the property name
        public override bool GetPropertiesSupported(
        ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection
        GetProperties(ITypeDescriptorContext context,
        object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(Grid));
        }
    }
    [TypeConverter(typeof(XYLabelConverter))]
    public class XYLabel
    {
        private string xLabel = "X Axis";
        private string yLabel = "Y Axis";
        private string y2Label = "Y2 Axis";
        private Font labelFont = new Font("Arial", 10, FontStyle.Regular);
        private Color labelFontColor = Color.Black;
        private Font tickFont;
        private Color tickFontColor = Color.Black;
        private Chart2D chart2d;
        public XYLabel(Chart2D ct2d)
        {
            chart2d = ct2d;
            tickFont = ct2d.Font;
        }
        [Description("Creates a label for the X axis."),
        Category("Appearance")]
        public string XLabel
        {
            get { return xLabel; }
            set
            {
                xLabel = value;
                chart2d.Invalidate();
            }
        }
        [Description("Creates a label for the Y axis."),
        Category("Appearance")]
        public string YLabel
        {
            get { return yLabel; }
            set
            {
                yLabel = value;
                chart2d.Invalidate();
            }
        }
        [Description("Creates a label for the Y2 axis."),
        Category("Appearance")]
        public string Y2Label
        {
            get { return y2Label; }
            set
            {
                y2Label = value;
                chart2d.Invalidate();
            }
        }
        [Description("The font used to display the axis labels."),
        Category("Appearance")]
        public Font LabelFont
        {
            get { return labelFont; }
            set
            {
                labelFont = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the color of the axis labels."),
        Category("Appearance")]
        public Color LabelFontColor
        {
            get { return labelFontColor; }
            set
            {
                labelFontColor = value;
                chart2d.Invalidate();
            }
        }
        [Description("The font used to display the tick labels."),
        Category("Appearance")]
        public Font TickFont
        {
            get { return tickFont; }
            set
            {
                tickFont = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the color of the tick labels."),
        Category("Appearance")]
        public Color TickFontColor
        {
            get { return tickFontColor; }
            set
            {
                tickFontColor = value;
                chart2d.Invalidate();
            }
        }
    }
    public class XYLabelConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(
        ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection
        GetProperties(ITypeDescriptorContext context,
        object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(XYLabel));
        }
    }
    [TypeConverter(typeof(Title2DConverter))]
    public class Title2D
    {
        private string title = "Title";
        private Font titleFont = new Font("Arial", 12, FontStyle.Regular);
        private Color titleFontColor = Color.Black;
        private Chart2D chart2d;
        public Title2D(Chart2D ct2d)
        {
            chart2d = ct2d;
        }
        [Description("Creates a title for the chart."),
        Category("Appearance")]
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                chart2d.Invalidate();
            }
        }
        [Description("The font used to display the title."),
        Category("Appearance")]
        public Font TitleFont
        {
            get { return titleFont; }
            set
            {
                titleFont = value;
                chart2d.Invalidate();
            }
        }
        [Description("Sets the color of the tile."),
        Category("Appearance")]
        public Color TitleFontColor
        {
            get { return titleFontColor; }
            set
            {
                titleFontColor = value;
                chart2d.Invalidate();
            }
        }
    }
    public class Title2DConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(
        ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection
        GetProperties(ITypeDescriptorContext context,
        object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(Title2D));
        }
    }
}