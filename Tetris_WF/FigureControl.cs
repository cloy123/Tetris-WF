using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris_WF.Figures;

namespace Tetris_WF
{
    static public class FigureControl
    {
        static public void Up(Figure figure, Dictionary<Point, bool> dict)
        {
                figure.Turn(dict);
        }

        static public bool Down(Figure figure, ref Dictionary<Point, bool> dict)
        {
            if(!figure.Down(dict))
            {
                foreach(Rect rect in figure.Rects)
                {
                    dict[rect.Point] = false;
                }
                return false;
            }
            else
            {
                return true;
            }
            
        }

        static public void Left(Figure figure, Dictionary<Point, bool> dict)
        {
                figure.Left(dict);
        }

        static public void Right(Figure figure, Dictionary<Point, bool> dict)
        {
                figure.Right(dict);
        }
    }
}
