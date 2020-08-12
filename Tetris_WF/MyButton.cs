using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris_WF
{
    class MyButton : Button
    {
        public MyButton()
        {
            this.SetStyle(ControlStyles.Selectable, false);
        }
    }
}
