using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_WF
{
    static class PicBoxSizeClass
    {
  
        static public int RectSize
        {
            get
            {
                return Convert.ToInt32(Properties.Settings.Default.RectSize);
            }
        }

        //     PICTUREBOX SIZE --------------------------------------------------------------------------------------------
        static private int PicBoxWidth
        {
            get
            {
                int margins = ((GridWidth - 1) * MarginSize) + 2 * Border;
                int rects = GridWidth * RectSize;
                return margins + rects;
            }
        }
        static private int PicBoxHeight
        {
            get
            {
                int margins = ((GridHeight - 1) * MarginSize) + 2 * Border;
                int rects = GridHeight * RectSize;
                return margins + rects;
            }
        }
        static public Size PicBoxSize
        {
            get
            {
                return new Size(PicBoxWidth, PicBoxHeight);
            }
        }
        //     PICTUREBOX SIZE --------------------------------------------------------------------------------------------


        static public int MarginSize
        {
            get
            {
                return Convert.ToInt32(Properties.Settings.Default.Margin);
            }
        }

        static public int GridHeight
        {
            get
            {
                return Convert.ToInt32(Properties.Settings.Default.GridHeight);
            }
        }

        static public int GridWidth
        {
            get
            {
                return Convert.ToInt32(Properties.Settings.Default.GridWidth);
            }
        }

        static public int Border
        {
            get
            {
                return Convert.ToInt32(Properties.Settings.Default.Border);
            }
        }

        static public Color BorderColor
        {
            get
            {
                return Properties.Settings.Default.BorderColor;
            }
        }

        static public Color MarginColor
        {
            get
            {
                return Properties.Settings.Default.MarginColor;
            }
        }

        static public Color Background
        {
            get
            {
                return Properties.Settings.Default.Background;
            }
        }

        static public int Speed
        {
            get { return Convert.ToInt32(Properties.Settings.Default.Speed); }
        }


        public static Dictionary<Point, bool> FillIsFree()
        {
            Dictionary<Point, bool> IsFree = new Dictionary<Point, bool>();
            // составляю словарь  --  если клетка пустая то true (point : bool)
            IsFree.Clear();
            for (int x = Border; x < PicBoxWidth - Border; x += (MarginSize + RectSize))
            {
                for (int y = Border; y < PicBoxHeight - Border; y += (MarginSize + RectSize))
                {
                    IsFree.Add(new Point(x, y), true);
                }
            }
            return IsFree;
        }

        public static int Record
        {
            get
            {
                return Properties.Settings.Default.Record;
            }
            set
            {
                if(value > Properties.Settings.Default.Record)
                {
                    Properties.Settings.Default.Record = value;
                    Properties.Settings.Default.Save();
                }
            }
        }


        static Graphics DrawBorder(Graphics g)
        {
            Pen pen = new Pen(BorderColor, Border);
            pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

            if(pen.Width == 1)
            {
                g.DrawRectangle(pen, new Rectangle(new Point(0, 0), new Size(PicBoxWidth-1, PicBoxHeight-1)));
            }
            else 
            {
                g.DrawRectangle(pen, new Rectangle(new Point(0, 0), new Size(PicBoxWidth, PicBoxHeight)));
            }
            
            return g;
        }

        static Graphics DrawMargin(Graphics g)
        {
            Pen pen = new Pen(MarginColor, MarginSize);


            for(int i = Border + RectSize + MarginSize/2; i < PicBoxHeight - RectSize; i += RectSize + MarginSize)
            {
                g.DrawLine(pen, new Point(Border, i), new Point(PicBoxWidth - Border, i));
            }

            for(int i = Border + RectSize + MarginSize/2; i < PicBoxWidth - RectSize; i += RectSize + MarginSize)
            {
                g.DrawLine(pen, new Point(i, Border), new Point(i, PicBoxHeight - Border));
            }


            return g;
        }

        static public Graphics DrawGrid(Image img)
        {
            Graphics g = Graphics.FromImage(img);

            if (MarginSize > 0)
            {
                g = DrawMargin(g);
            }

            if (Border > 0)
            {
                g = DrawBorder(g);
            }
            return g;
        }


        static public Point PointForFigure()
        {
            int x = (GridWidth / 2 * RectSize) + (GridWidth / 2 * MarginSize) + Border;
            return new Point(x, Border);
        }
    }
}
