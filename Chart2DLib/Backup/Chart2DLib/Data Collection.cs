using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Chart2DLib
{
    public class DataCollection
    {
        private ArrayList dataSeriesList;
        private int dataSeriesIndex = 0;
        public DataCollection()
        {
            dataSeriesList = new ArrayList();
        }
        public ArrayList DataSeriesList
        {
            get { return dataSeriesList; }
            set { dataSeriesList = value; }
        }
        public int DataSeriesIndex
        {
            get { return dataSeriesIndex; }
            set { dataSeriesIndex = value; }
        }
        public void Add(DataSeries ds)
        {
            dataSeriesList.Add(ds);
            if (ds.SeriesName == "Default Name")
            {
                ds.SeriesName = "DataSeries" +
                dataSeriesList.Count.ToString();
            }
        }
        public void Insert(int dataSeriesIndex, DataSeries ds)
        {
            dataSeriesList.Insert(dataSeriesIndex, ds);
            if (ds.SeriesName == "Default Name")
            {
                dataSeriesIndex = dataSeriesIndex + 1;
                ds.SeriesName = "DataSeries" +
                    dataSeriesIndex.ToString();
            }
        }
        public void Remove(string dataSeriesName)
        {
            if (dataSeriesList != null)
            {
                for (int i = 0; i < dataSeriesList.Count; i++)
                {
                    DataSeries ds = (DataSeries)dataSeriesList[i];
                    if (ds.SeriesName == dataSeriesName)
                    {
                        dataSeriesList.RemoveAt(i);
                    }
                }
            }
        }
        public void RemoveAll()
        {
            dataSeriesList.Clear();
        }
        public void AddLines(Graphics g, ChartStyle cs, XAxis xa, YAxis ya, Y2Axis y2a)
        {
            // Plot lines:
            foreach (DataSeries ds in DataSeriesList)
            {
                if (ds.LineStyle.IsVisible == true)
                {
                    Pen aPen = new Pen(ds.LineStyle.LineColor,
                    ds.LineStyle.Thickness);
                    aPen.DashStyle = ds.LineStyle.Pattern;
                    if (ds.LineStyle.PlotMethod ==
                    LineStyle.PlotLinesMethodEnum.Lines)
                    {
                        for (int i = 1; i < ds.PointList.Count; i++)
                        {
                            if (!ds.IsY2Data)
                            {
                                g.DrawLine(aPen,
                                    cs.Point2D((PointF)ds.PointList[i - 1], xa, ya),
                                    cs.Point2D((PointF)ds.PointList[i],xa,ya));
                            }
                            else
                            {
                                g.DrawLine(aPen,
                                    cs.Point2DY2((PointF)ds.PointList[i - 1], xa, y2a),
                                    cs.Point2DY2((PointF)ds.PointList[i],xa,y2a));
                            }
                        }
                    }
                    else if (ds.LineStyle.PlotMethod ==
                    LineStyle.PlotLinesMethodEnum.Splines)
                    {
                        ArrayList al = new ArrayList();
                        for (int i = 0; i < ds.PointList.Count; i++)
                        {
                            PointF pt = (PointF)ds.PointList[i];
                            if (!ds.IsY2Data)
                            {
                                if (pt.X >= xa.XLimMin &&
                                pt.X <= xa.XLimMax &&
                                pt.Y >= ya.YLimMin &&
                                pt.Y <= ya.YLimMax)
                                {
                                    al.Add(pt);
                                }
                            }
                            else
                            {
                                if (pt.X >= xa.XLimMin &&
                                pt.X <= xa.XLimMax &&
                                pt.Y >= y2a.Y2LimMin &&
                                pt.Y <= y2a.Y2LimMax)
                                {
                                    al.Add(pt);
                                }
                            }
                        }
                        PointF[] pts = new PointF[al.Count];
                        for (int i = 0; i < pts.Length; i++)
                        {
                            if (!ds.IsY2Data)
                            {
                                pts[i] = cs.Point2D((PointF)(al[i]),xa,ya);
                            }
                            else
                            {
                                pts[i] = cs.Point2DY2((PointF)(al[i]),xa,y2a);
                            }
                        }
                        g.DrawCurve(aPen, pts);
                    }
                    aPen.Dispose();
                }
            }
            // Plot Symbols:
            foreach (DataSeries ds in DataSeriesList)
            {
                for (int i = 0; i < ds.PointList.Count; i++)
                {
                    PointF pt = (PointF)ds.PointList[i];
                    if (!ds.IsY2Data)
                    {
                        if (pt.X >= xa.XLimMin && pt.X <= xa.XLimMax &&
                        pt.Y >= ya.YLimMin && pt.Y <= ya.YLimMax)
                        {
                            ds.SymbolStyle.DrawSymbol(g,
                            cs.Point2D((PointF)ds.PointList[i],xa,ya));
                        }
                    }
                    else
                    {
                        if (pt.X >= xa.XLimMin && pt.X <= xa.XLimMax &&
                        pt.Y >= y2a.Y2LimMin && pt.Y <= y2a.Y2LimMax)
                        {
                            ds.SymbolStyle.DrawSymbol(g,
                            cs.Point2DY2((PointF)ds.PointList[i],xa,y2a));
                        }
                    }
                }
            }
        }
        /*public void AddBars(Graphics g, ChartStyle cs, int numberOfDataSeries, int numberOfPoints)
        {
            // Draw bars:
            ArrayList temp = new ArrayList();
            float[] tempy = new float[numberOfPoints];
            int n = 0;
            foreach (DataSeries ds in DataSeriesList)
            {
                Pen aPen = new Pen(ds.BarStyle.BorderColor,
                ds.BarStyle.BorderThickness);
                SolidBrush aBrush = new SolidBrush(ds.BarStyle.FillColor);
                aPen.DashStyle = ds.BarStyle.BorderPattern;
                PointF[] pts = new PointF[4];
                PointF pt;
                float width;
                if (cs.BarType == ChartStyle.BarTypeEnum.Vertical)
                {
                    if (numberOfDataSeries == 1)
                    {
                        width = cs.XTick * ds.BarStyle.BarWidth;
                        for (int i = 0; i < ds.PointList.Count; i++)
                        {
                            pt = (PointF)ds.PointList[i];
                            float x = pt.X - cs.XTick / 2;
                            pts[0] = cs.Point2D(new PointF(x -
                            width / 2, 0));
                            pts[1] = cs.Point2D(new PointF(x +
                            width / 2, 0));
                            pts[2] = cs.Point2D(new PointF(x +
                            width / 2, pt.Y));
                            pts[3] = cs.Point2D(new PointF(x -
                            width / 2, pt.Y));
                            g.FillPolygon(aBrush, pts);
                            g.DrawPolygon(aPen, pts);
                        }
                    }
                    else if (numberOfDataSeries > 1)
                    {
                        width = 0.7f * cs.XTick;
                        for (int i = 0; i < ds.PointList.Count; i++)
                        {
                            pt = (PointF)ds.PointList[i];
                            float w1 = width / numberOfDataSeries;
                            float w = ds.BarStyle.BarWidth * w1;
                            float space = (w1 - w) / 2;
                            float x = pt.X - cs.XTick / 2;
                            pts[0] = cs.Point2D(new PointF(
                            x - width / 2 + space + n * w1, 0));
                            pts[1] = cs.Point2D(new PointF(
                            x - width / 2 + space + n * w1 + w, 0));
                            pts[2] = cs.Point2D(new PointF(
                        x - width / 2 + space +
                        n * w1 + w, pt.Y));
                            pts[3] = cs.Point2D(new PointF(
                            x - width / 2 + space + n * w1, pt.Y));
                            g.FillPolygon(aBrush, pts);
                            g.DrawPolygon(aPen, pts);
                        }
                    }
                }
                else if (cs.BarType ==
                ChartStyle.BarTypeEnum.VerticalOverlay
                && numberOfDataSeries > 1)
                {
                    width = cs.XTick * ds.BarStyle.BarWidth;
                    width = width / (float)Math.Pow(2, n);
                    for (int i = 0; i < ds.PointList.Count; i++)
                    {
                        pt = (PointF)ds.PointList[i];
                        float x = pt.X - cs.XTick / 2;
                        pts[0] = cs.Point2D(new PointF(x -
                        width / 2, 0));
                        pts[1] = cs.Point2D(new PointF(x +
                        width / 2, 0));
                        pts[2] = cs.Point2D(new PointF(x +
                        width / 2, pt.Y));
                        pts[3] = cs.Point2D(new PointF(x -
                            width / 2, pt.Y));
                        g.FillPolygon(aBrush, pts);
                        g.DrawPolygon(aPen, pts);
                    }
                }
                else if (cs.BarType ==
                ChartStyle.BarTypeEnum.VerticalStack
                && numberOfDataSeries > 1)
                {
                    width = cs.XTick * ds.BarStyle.BarWidth;
                    for (int i = 0; i < ds.PointList.Count; i++)
                    {
                        pt = (PointF)ds.PointList[i];
                        if (temp.Count > 0)
                        {
                            tempy[i] = tempy[i] + ((PointF)temp[i]).Y;
                        }
                        float x = pt.X - cs.XTick / 2;
                        pts[0] = cs.Point2D(new PointF(x -
                        width / 2, 0 + tempy[i]));
                        pts[1] = cs.Point2D(new PointF(x +
                        width / 2, 0 + tempy[i]));
                        pts[2] = cs.Point2D(new PointF(x +
                        width / 2, pt.Y + tempy[i]));
                        pts[3] = cs.Point2D(new PointF(x -
                        width / 2, pt.Y + tempy[i]));
                        g.FillPolygon(aBrush, pts);
                        g.DrawPolygon(aPen, pts);
                    }
                    temp = ds.PointList;
                }
                else if (cs.BarType ==
                ChartStyle.BarTypeEnum.Horizontal)
                {
                    if (numberOfDataSeries == 1)
                    {
                        width = cs.YTick * ds.BarStyle.BarWidth;
                        for (int i = 0; i < ds.PointList.Count; i++)
                        {
                            pt = (PointF)ds.PointList[i];
                            float y = pt.Y - cs.YTick / 2;
                            pts[0] = cs.Point2D(new PointF(0, y -
                            width / 2));
                            pts[1] = cs.Point2D(new PointF(0, y +
                            width / 2));
                            pts[2] = cs.Point2D(new PointF(pt.X,
                            y + width / 2));
                            pts[3] = cs.Point2D(new PointF(pt.X,
                            y - width / 2));
                            g.FillPolygon(aBrush, pts);
                            g.DrawPolygon(aPen, pts);
                        }
                    }
                    else if (numberOfDataSeries > 1)
                    {
                        width = 0.7f * cs.YTick;
                        for (int i = 0; i < ds.PointList.Count; i++)
                        {
                            pt = (PointF)ds.PointList[i];
                            float w1 = width / numberOfDataSeries;
                            float w = ds.BarStyle.BarWidth * w1;
                            float space = (w1 - w) / 2;
                            float y = pt.Y - cs.YTick / 2;
                            pts[0] = cs.Point2D(new PointF(0,
                            y - width / 2 + space + n * w1));
                            pts[1] = cs.Point2D(new PointF(0,
                            y - width / 2 + space + n * w1 + w));
                            pts[2] = cs.Point2D(new PointF(pt.X,
                            y - width / 2 + space + n * w1 + w));
                            pts[3] = cs.Point2D(new PointF(pt.X,
                            y - width / 2 + space + n * w1));
                            g.FillPolygon(aBrush, pts);
                            g.DrawPolygon(aPen, pts);
                        }
                    }
                }
                else if (cs.BarType ==
                ChartStyle.BarTypeEnum.HorizontalOverlay &&
                numberOfDataSeries > 1)
                {
                    width = cs.YTick * ds.BarStyle.BarWidth;
                    width = width / (float)Math.Pow(2, n);
                    for (int i = 0; i < ds.PointList.Count; i++)
                    {
                        pt = (PointF)ds.PointList[i];
                        float y = pt.Y - cs.YTick / 2;
                        pts[0] = cs.Point2D(new PointF(0, y - width / 2));
                        pts[1] = cs.Point2D(new PointF(0, y + width / 2));
                        pts[2] = cs.Point2D(new PointF(pt.X,y + width / 2));
                        pts[3] = cs.Point2D(new PointF(pt.X, y - width / 2));
                        g.FillPolygon(aBrush, pts);
                        g.DrawPolygon(aPen, pts);
                    }
                }
                else if (cs.BarType ==
                ChartStyle.BarTypeEnum.HorizontalStack &&
                numberOfDataSeries > 1)
                {
                    {
                        width = cs.YTick * ds.BarStyle.BarWidth;
                        for (int i = 0; i < ds.PointList.Count; i++)
                        {
                            pt = (PointF)ds.PointList[i];
                            if (temp.Count > 0)
                            {
                                tempy[i] = tempy[i] + ((PointF)temp[i]).X;
                            }
                            float y = pt.Y - cs.YTick / 2;
                            pts[0] = cs.Point2D(new PointF(0 + tempy[i], y - width / 2));
                            pts[1] = cs.Point2D(new PointF(0 + tempy[i], y + width / 2));
                            pts[2] = cs.Point2D(new PointF(pt.X +tempy[i], y + width / 2));
                            pts[3] = cs.Point2D(new PointF(pt.X + tempy[i], y - width / 2));
                            g.FillPolygon(aBrush, pts);
                            g.DrawPolygon(aPen, pts);
                        }
                        temp = ds.PointList;
                    }
                }
                n++;
                aPen.Dispose();
            }
        }*/
    }
}
