using System;
using System.Drawing;
using System.Collections;
namespace Chart2DLib
{
    public class SubChart
    {
        private int rows = 1;
        private int cols = 1;
        private int margin = 0;
        private Rectangle totalChartArea;
        private Color totalChartBackColor;
        private Color totalChartBorderColor;
       /* public SubChart(Form2 fm2)
        {
            form2 = fm2;
            TotalChartArea = form2.ClientRectangle;
            totalChartBackColor = fm2.BackColor;
            totalChartBorderColor = fm2.BackColor;
        }*/
        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }
        public int Cols
        {
            get { return cols; }
            set { cols = value; }
        }
        public int Margin
        {
            get { return margin; }
            set { margin = value; }
        }
        public Rectangle TotalChartArea
        {
            get { return totalChartArea; }
            set { totalChartArea = value; }
        }
        public Color TotalChartBackColor
        {
            get { return totalChartBackColor; }
            set { totalChartBackColor = value; }
        }
        public Color TotalChartBorderColor
        {
            get { return totalChartBorderColor; }
            set { totalChartBorderColor = value; }
        }
        public Rectangle[,] SetSubChart(Graphics g)
        {
            Rectangle[,] subRectangle = new Rectangle[Rows, Cols];
            int subWidth = (TotalChartArea.Width - 2 * Margin) / Cols;
            int subHeight = (TotalChartArea.Height - 4 * Margin) / Rows;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    int x = TotalChartArea.X + Margin + j * subWidth;
                    int y = TotalChartArea.Y + Margin + i * subHeight;
                    subRectangle[i, j] = new Rectangle(x, y,
                    subWidth, subHeight);
                }
            }
            // Draw total chart area:
            Pen aPen = new Pen(TotalChartBorderColor, 1f);
            SolidBrush aBrush = new SolidBrush(TotalChartBackColor);
            g.FillRectangle(aBrush, TotalChartArea);
            g.DrawRectangle(aPen, TotalChartArea);
            return subRectangle;
        }
    }
}