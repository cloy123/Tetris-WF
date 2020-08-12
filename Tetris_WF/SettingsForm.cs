using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_WF
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        

        private void Ok_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.GridHeight = GridHeightUpDown.Value;
            Properties.Settings.Default.GridWidth = GridWidthUpDown.Value;
            Properties.Settings.Default.RectSize = RectSizeUpDown.Value;
            Properties.Settings.Default.Margin = MarginUpDown.Value;
            Properties.Settings.Default.MarginColor = button1.BackColor;
            Properties.Settings.Default.Speed = SpeedUpDown.Value;
            Properties.Settings.Default.Background = button2.BackColor;
            Properties.Settings.Default.BorderColor = BorderColor.BackColor;
            Properties.Settings.Default.Border = Border.Value;
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;

            if(ResetRecord.Checked)
            {
                Properties.Settings.Default.Record = 0;
            }

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button1.BackColor;
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button2.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDialog1.Color;
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            GridHeightUpDown.Value = Properties.Settings.Default.GridHeight;
            GridWidthUpDown.Value = Properties.Settings.Default.GridWidth;
            RectSizeUpDown.Value = Properties.Settings.Default.RectSize;
            MarginUpDown.Value = Properties.Settings.Default.Margin;
            button1.BackColor = Properties.Settings.Default.MarginColor;
            SpeedUpDown.Value = Properties.Settings.Default.Speed;
            button2.BackColor = Properties.Settings.Default.Background;
            BorderColor.BackColor = Properties.Settings.Default.BorderColor;
            Border.Value = Properties.Settings.Default.Border;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_Click(object sender, EventArgs e)
        {
            int r = Properties.Settings.Default.Record; 
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Record = r;
            SettingsForm_Load(sender, e);
        }

        private void BorderColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = BorderColor.BackColor;
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                BorderColor.BackColor = colorDialog1.Color;
            }
        }
    }
}
