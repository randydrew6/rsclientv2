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
    public partial class popout : Form
    {
        public popout()
        {
            InitializeComponent();
        }

        public void SetImage(Image img)
        {
            pic_image.Image = img;
        }
    }
}
