using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_WF.Figures
{
    static class FigureFour
    {

        //
        //       /_
        //       |_|
        //     _ |_|
        //    |_||_|
        //


        static public Rect[] Create(Point p, int RectSize, int MarginSize, Pen pen)
        {
            Rect[] Rects = new Rect[4]
            {
                 new Rect(new Point(p.X, p.Y), RectSize, false, pen),
                 new Rect(new Point(p.X, p.Y + 1*(RectSize + MarginSize)), RectSize, true, pen),
                 new Rect(new Point(p.X, p.Y + 2*(RectSize + MarginSize)), RectSize, false, pen),
                 new Rect(new Point(p.X - 1*(RectSize + MarginSize), p.Y + 2*(RectSize + MarginSize)), RectSize, false, pen)
            };
            return Rects;
        }

    }
}
