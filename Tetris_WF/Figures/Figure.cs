using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_WF.Figures
{
    public class Figure : ICloneable
    {
        public Rect[] Rects;

        public int nFigure;
        

        int RectSize = 20;
        int MarginSize = 3;

        public Point Point = new Point(0, 0);

        protected Pen pen;

        protected int Width;//the number of rectangles wide
        protected int Height;//the number of rectangles high

        public Figure(Point p, int rectSize, int marginSize, int NumberFigure)
        {
            nFigure = NumberFigure;
            Point = p;
            RectSize = rectSize;
            MarginSize = marginSize;
            CreateFigure();
        }

        public object Clone()
        {
            Figure f = new Figure(this.Point, this.RectSize, this.MarginSize, this.nFigure) {Rects = CloneRects()};
            f.CreateFigure();
            return f;
        }

        public Rect[] CloneRects()
        {
            Rect[] r = new Rect[this.Rects.Length];
            
            for (int i = 0; i < r.Length; i++)
            {
                r[i] = new Rect(Rects[i].Point, RectSize, Rects[i].IsCenter, Rects[i].Pen);
            }
            return r;
        }

        public List<Rect> CloneListRects()
        {
            List<Rect> r = new List<Rect>();
            for (int i = 0; i < r.Count; i++)
            {
                r[i] = new Rect(Rects[i].Point, RectSize, Rects[i].IsCenter, Rects[i].Pen);
            }
            return r;
        }


        private void CreateFigure()
        {
            switch (nFigure)
            {
                case (1):
                    Width = 2;
                    Height = 2;
                    pen = new Pen(Color.Red, 2);
                    Rects = FigureOne.Create(Point, RectSize, MarginSize, pen);
                    break;
                case (2):
                    Width = 4;
                    Height = 1;
                    pen = new Pen(Color.LightBlue, 2);
                    Rects = FigureTwo.Create(Point, RectSize, MarginSize, pen);
                    break;
                case (3):
                    Width = 2;
                    Height = 3;
                    pen = new Pen(Color.Green, 2);
                    Rects = FigureThree.Create(Point, RectSize, MarginSize, pen);
                    break;
                case (4):
                    Width = 2;
                    Height = 3;
                    pen = new Pen(Color.Yellow, 2);
                    Rects = FigureFour.Create(Point, RectSize, MarginSize, pen);
                    break;
                case (5):
                    Width = 3;
                    Height = 2;
                    pen = new Pen(Color.Violet, 2);
                    Rects = FigureFive.Create(Point, RectSize, MarginSize, pen);
                    break;
                case (6):
                    Width = 3;
                    Height = 2;
                    pen = new Pen(Color.Aqua, 2);
                    Rects = FigureSix.Create(Point, RectSize, MarginSize, pen);
                    break;
                case (7):
                    Width = 3;
                    Height = 2;
                    pen = new Pen(Color.Olive, 2);
                    Rects = FigureSeven.Create(Point, RectSize, MarginSize, pen);
                    break;
                default:
                    goto case (1);
            }

        }


        public void Draw(ref PaintEventArgs e)
        {
            foreach (Rect rect in Rects)
            {
                rect.DrawRect(ref e);
            }
        }

        public void Draw(Graphics g)
        {
            foreach (Rect rect in Rects)
            {
                rect.DrawRect(g);
            }
        }

        public bool Down(Dictionary<Point, bool> d)
        {
            int y = MarginSize + RectSize;
            foreach (Rect rect in Rects)
            {
                Point p = new Point(rect.Point.X, rect.Point.Y + y);
                if(!d.ContainsKey(p) || !d[p])
                {
                    return false;
                }
            }

            Point.Y += y;
            foreach (Rect rect in Rects)
            {
                rect.Point.Y += y;
            }
            return true;

        }

        public void Left(Dictionary<Point, bool> d)
        {
            int x = MarginSize + RectSize;

            foreach (Rect rect in Rects)
            {
                Point p = new Point(rect.Point.X - x, rect.Point.Y);
                if (!d.ContainsKey(p) || !d[p])
                {
                    return;
                }
            }

            Point.X -= x;
            foreach (Rect rect in Rects)
            {
                rect.Point.X -= x;
            }
        }

        public void Right(Dictionary<Point, bool> d)
        {

            int x = MarginSize + RectSize;

            foreach (Rect rect in Rects)
            {
                Point p = new Point(rect.Point.X + x, rect.Point.Y);
                if (!d.ContainsKey(p) || !d[p])
                {
                    return;
                }
            }


            Point.X += x;
            foreach (Rect rect in Rects)
            {
                rect.Point.X += x;
            }
        }


        public void Turn(Dictionary<Point, bool> d)
        {
            Point p = new Point();
            bool IsCenterExist = false;

            foreach (Rect rect in Rects)
            {
                if(rect.IsCenter == true)
                {
                    p = rect.Point;
                    IsCenterExist = true;
                }
            }

            if(!IsCenterExist)
            {
                return;
            }


            foreach (Rect rect in Rects)
            {
                int x = rect.Point.X - p.X;
                int y = rect.Point.Y - p.Y;

                Point pnt = new Point(rect.Point.X - x - y , rect.Point.Y + (x-y));

                if (!d.ContainsKey(pnt) || !d[pnt])
                {
                    return;
                }
            }

            foreach (Rect rect in Rects)
            {
                int x = rect.Point.X - p.X;
                int y = rect.Point.Y - p.Y;
                rect.Point.Y += (x - y); // rect.point.Y = rect.point.Y + x - y;
                rect.Point.X += -x - y;  // rect.point.X = rect.point.X - x - y; 
            }
        }
    }
}
