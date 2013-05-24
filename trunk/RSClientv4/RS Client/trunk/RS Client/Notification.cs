using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RS_Client
{
    public partial class Notification : Form
    {
        public Notification()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.004;

            if (this.Opacity <= 0.8)
            {
                this.Close();
            }
        }

        public void Set_Text(string p)
        {
            lbl_text.Text = p;
            this.MaximumSize = new System.Drawing.Size(lbl_text.Width, 20);
            this.MinimumSize = new System.Drawing.Size(lbl_text.Width, 20);
            this.Size = new System.Drawing.Size(lbl_text.Width, 20);
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Size.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Size.Height);
            timer1.Enabled = true;
        }
    }
}
