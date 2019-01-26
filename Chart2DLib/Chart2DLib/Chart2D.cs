using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Chart2DLib
{
    public partial class Chart2D : UserControl
    {
        private ChartStyle cs;
        private ChartArea ca;
        private DataCollection dc;
        private DataSeries ds;
        private Legend lg;
        private XAxis xa;
        private YAxis ya;
        private Y2Axis y2a;
        private Grid gd;
        private XYLabel lb;
        private Title2D tl;
        private SymbolStyle ss;
        public Chart2D()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            cs = new ChartStyle(this);
            ca = new ChartArea(this);
            dc = new DataCollection();
            ds = new DataSeries();
            lg = new Legend(this);
            xa = new XAxis(this);
            ya = new YAxis(this);
            y2a = new Y2Axis(this);
            gd = new Grid(this);
            lb = new XYLabel(this);
            tl = new Title2D(this);
            ss = new SymbolStyle();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            cs.ChartArea = this.ClientRectangle;
            cs.AddChartStyle(g, ca, xa, ya, y2a, gd, lb, tl);
            dc.AddCharts(g, cs, xa, ya, y2a);
            lg.AddLegend(g, dc, cs);
            g.Dispose();
        }
        [BrowsableAttribute(false)]
        public ChartStyle C2ChartStyle
        {
            get { return this.cs; }
            set { this.cs = value; }
        }
        [DesignerSerializationVisibility(
        DesignerSerializationVisibility.Content)]
        public ChartArea C2ChartArea
        {
            get { return this.ca; }
            set
            {
                if (value != null)
                {
                    this.ca = value;
                }
            }
        }
        [DesignerSerializationVisibility(
DesignerSerializationVisibility.Content)]
        public XAxis C2XAxis
        {
            get { return this.xa; }
            set
            {
                if (value != null)
                {
                    this.xa = value;
                }
            }
        }
        [DesignerSerializationVisibility(
        DesignerSerializationVisibility.Content)]
        public YAxis C2YAxis
        {
            get { return this.ya; }
            set
            {
                if (value != null)
                {
                    this.ya = value;
                }
            }
        }
        [DesignerSerializationVisibility(
        DesignerSerializationVisibility.Content)]
        public Y2Axis C2Y2Axis
        {
            get { return this.y2a; }
            set
            {
                if (value != null)
                {
                    this.y2a = value;
                }
            }
        }
        [DesignerSerializationVisibility(
DesignerSerializationVisibility.Content)]
        public Grid C2Grid
        {
            get { return this.gd; }
            set
            {
                if (value != null)
                {
                    this.gd = value;
                }
            }
        }
        [DesignerSerializationVisibility(
        DesignerSerializationVisibility.Content)]
        public XYLabel C2Label
        {
            get { return this.lb; }
            set
            {
                if (value != null)
                {
                    this.lb = value;
                }
            }
        }
        [DesignerSerializationVisibility(
        DesignerSerializationVisibility.Content)]
        public Title2D C2Title
        {
            get { return this.tl; }
            set
            {
                if (value != null)
                {
                    this.tl = value;
                }
            }
        }
        [BrowsableAttribute(false)]
        public DataCollection C2DataCollection
        {
            get { return this.dc; }
            set { this.dc = value; }
        }
        [DesignerSerializationVisibility(
DesignerSerializationVisibility.Content)]
        public Legend C2Legend
        {
            get { return this.lg; }
            set
            {
                if (value != null)
                {
                    this.lg = value;
                }
            }
        }
    }
}