using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tetris_WF.Figures;

namespace Tetris_WF
{
    public partial class Form1 : Form
    {
        bool Pause = true;
        Figure NowFigure;
        Figure NextFigure;

        int nNextFigure;

        int Score = 0;

        List<Rect> Rects = new List<Rect>();

        public Dictionary<Point, bool> IfCellsFree = new Dictionary<Point, bool>();



        public Form1()
        {
            InitializeComponent();
        }

        private int RandomNumber(int a, int b)
        {
            Random r = new Random();
            return r.Next(a, b);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Start();
            nNextFigure = RandomNumber(1, 8);
            UpdatePicBox();
            Restart();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                NextFigure.Draw(ref e);
            }
            catch { };
        }





        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
                NowFigure.Draw(ref e);
                foreach(Rect rect in Rects)
                {
                    rect.DrawRect(ref e);
                }
        }

        private void UpdateFigures()
        {
            foreach (Rect rect in NowFigure.CloneRects())
            {
                Rects.Add(rect);
                IfCellsFree[rect.Point] = false;
            }

            


            NowFigure = new Figure(PicBoxSizeClass.PointForFigure(), PicBoxSizeClass.RectSize, PicBoxSizeClass.MarginSize, nNextFigure);

            foreach(Rect rect in NowFigure.Rects)
            {
                if(!IfCellsFree[rect.Point])
                {
                    Restart();
                    return;
                }
            }

            nNextFigure = RandomNumber(1, 8);
            NextFigure = new Figure(new Point(pictureBox2.Width / 2, pictureBox2.Height / 4), 20, 3, nNextFigure);
            pictureBox2.Invalidate();
            Score += 1;
            ScoreLabel.Text = "Score: " + Score.ToString();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Pause)
            {
                return;
            }

            if (!FigureControl.Down(NowFigure, ref IfCellsFree))
            {
                UpdateFigures();

                for(int y = PicBoxSizeClass.Border; y < PicBoxSizeClass.PicBoxSize.Height - PicBoxSizeClass.Border; y += (PicBoxSizeClass.RectSize + PicBoxSizeClass.MarginSize))
                {
                    List<Rect> NewRects = new List<Rect>();
                    
                    bool b = false;//if the line contains empty cells

                    for (int x = PicBoxSizeClass.Border; x < PicBoxSizeClass.PicBoxSize.Width - PicBoxSizeClass.Border; x += (PicBoxSizeClass.RectSize + PicBoxSizeClass.MarginSize))
                    {
                        if(IfCellsFree[new Point(x, y)])
                        {
                            b = true;
                            break;
                        }
                    }

                    if(!b)
                    {
                        foreach(Rect rect in Rects)
                        {
                            if(rect.Point.Y < y)
                            {
                                rect.Point.Y += (PicBoxSizeClass.MarginSize + PicBoxSizeClass.RectSize);
                                NewRects.Add(rect);
                            }
                            else if(rect.Point.Y != y)
                            {
                                NewRects.Add(rect);
                            }
                        }
                        Rects.Clear();
                        Rects.AddRange(NewRects);

                        Dictionary< Point, bool> NewDict = new Dictionary<Point, bool>();
                        
                        foreach (Point p in IfCellsFree.Keys)
                        {
                            NewDict.Add(p, true);
                        }
                        IfCellsFree = NewDict;
                        foreach (Rect rect in Rects)
                        {
                            IfCellsFree[rect.Point] = false;
                        }
                    }
                }
            }
            pictureBox1.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Pause)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    FigureControl.Left(NowFigure, IfCellsFree);
                    pictureBox1.Invalidate();
                    break;

                case Keys.Right:
                    FigureControl.Right(NowFigure, IfCellsFree);
                    pictureBox1.Invalidate();
                    break;

                case Keys.Up:
                    FigureControl.Up(NowFigure, IfCellsFree);
                    pictureBox1.Invalidate();
                    break;

                case Keys.Down:
                    FigureControl.Down(NowFigure, ref IfCellsFree);
                    pictureBox1.Invalidate();
                    break;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (Pause)
            {
                Pause = !Pause;
                Pause = false;
                pictureBox1.Invalidate();
                StartButton.Text = "Stop";
            }
            else
            {
                Pause = !Pause;
                StartButton.Text = "Start";
            }
        }


        public void UpdatePicBox()
        {
            pictureBox1.Size = PicBoxSizeClass.PicBoxSize;
            pictureBox1.BackColor = PicBoxSizeClass.Background;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            PicBoxSizeClass.DrawGrid(pictureBox1.Image);
            pictureBox1.Invalidate();
            IfCellsFree = PicBoxSizeClass.FillIsFree();
            timer2.Interval = 200 * (11 - PicBoxSizeClass.Speed);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();

            if(!Pause)
            { StartButton_Click(sender, e); }

            SettingsForm s = new SettingsForm();
            if (s.ShowDialog() == DialogResult.OK)
            {
                UpdatePicBox();
                Restart();
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void Restart()
        {
            UpdatePicBox();
            Rects.Clear();
            nNextFigure = RandomNumber(1, 8);
            NextFigure = new Figure(new Point(pictureBox2.Width / 2, pictureBox2.Height / 4), 20, 3, RandomNumber(1, 8));
            NowFigure = new Figure(PicBoxSizeClass.PointForFigure(), PicBoxSizeClass.RectSize, PicBoxSizeClass.MarginSize, RandomNumber(1, 7));
            StartButton.Text = "Start";
            Pause = true;
            PicBoxSizeClass.Record = Score;
            Score = 0;
            ScoreLabel.Text = "Score: " + Score.ToString();
            RecordLabel.Text = "Record: " + PicBoxSizeClass.Record.ToString() ;
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
        }
    }
}
