using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_WF
{
    public class Rect
    {
        public Point Point;
        public bool IsCenter;

        public Pen Pen;

        public int RectSize;

        public void DrawRect(Graphics g)
        {
            Rectangle rect = new Rectangle(Point, new Size(RectSize, RectSize));

            Pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

            g.DrawRectangle(Pen, rect);
            g.FillRectangle(Pen.Brush, rect);
        }

        public void DrawRect(ref PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(Point, new Size(RectSize, RectSize));

            Pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

            e.Graphics.DrawRectangle(Pen, rect);
            e.Graphics.FillRectangle(Pen.Brush, rect);
        }

        public Rect(Point p, int rectSize, bool b, Pen pen)
        {
            Point = p;
            IsCenter = b;
            RectSize = rectSize;
            Pen = pen;
        }
    }
}
