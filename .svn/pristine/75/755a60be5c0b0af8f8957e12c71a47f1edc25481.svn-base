using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RS_Client
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        int count = 0;

        private void btn_create_Click(object sender, EventArgs e)
        {
            if (txt_login.Text != "" && txt_rsncreate.Text != "")
            {
                System.IO.StreamWriter w = System.IO.File.AppendText(Config.Loginfile);
                w.WriteLine(txt_login.Text);
                w.WriteLine(txt_rsncreate.Text);
                w.Close();
                lst_login.Items.Add(txt_login.Text);
                lst_rsn.Items.Add(txt_rsncreate.Text);
                count++;
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Config.Folder))
            {
                Directory.CreateDirectory(Config.Folder);
                StreamWriter write = File.CreateText(Config.Loginfile);
                write.Close();
            }
            if (!Directory.Exists(Config.ScreenshotFolder))
            {
                Directory.CreateDirectory(Config.ScreenshotFolder);
            }

            
            if (File.Exists(Config.Loginfile))
            {
                string _user = "";
                string _rsn = "";
                using (System.IO.StreamReader r = new System.IO.StreamReader(Config.Loginfile))
                {
                    while ((_user = r.ReadLine()) != null)
                    {
                        _rsn = r.ReadLine();
                        lst_login.Items.Add(_user);
                        lst_rsn.Items.Add(_rsn);
                        count++;
                    }
                    r.Close();
                }
            }

            Validatefiles();
        }

        private void Validatefiles()
        {
            if (!File.Exists(Config.Historyfile))
            {
                StreamWriter w = File.CreateText(Config.Historyfile);
                w.Close();
            }
            if (!File.Exists(Config.Browserfile))
            {
                StreamWriter w = File.CreateText(Config.Browserfile);
                w.WriteLine("1");
                w.Close();
            }
            if (!File.Exists(Config.Extfile))
            {
                StreamWriter w = File.CreateText(Config.Extfile);
                w.Close();
            }
            if (!File.Exists(Config.Favlinksfile))
            {
                StreamWriter w = File.CreateText(Config.Favlinksfile);
                w.WriteLine("false");
                w.WriteLine("http://google.com/");
                w.WriteLine("Google");
                w.WriteLine("http://google.com/");
                w.WriteLine("Google");
                w.WriteLine("http://google.com/");
                w.WriteLine("Google");
                w.WriteLine("http://google.com/");
                w.WriteLine("Google");
                w.WriteLine("http://google.com/");
                w.WriteLine("Google");
                w.WriteLine("http://google.com/");
                w.WriteLine("Google");
                w.Close();
            }
            if (!File.Exists(Config.Flashfile))
            {
                StreamWriter w = File.CreateText(Config.Flashfile);
                w.Close();
            }
            if (!File.Exists(Config.Itemfile))
            {
                StreamWriter w = File.CreateText(Config.Itemfile);
                w.Close();
            }
            if (!File.Exists(Config.Mangafile))
            {
                StreamWriter w = File.CreateText(Config.Mangafile);
                w.Close();
            }
            if (!File.Exists(Config.Notesfile))
            {
                StreamWriter w = File.CreateText(Config.Notesfile);
                w.Close();
            }
            if (!File.Exists(Config.Otherfile))
            {
                StreamWriter w = File.CreateText(Config.Otherfile);
                w.Close();
            }
            if (!File.Exists(Config.Vidfile))
            {
                StreamWriter w = File.CreateText(Config.Vidfile);
                w.Close();
            }
        }

        private void btn_cancelcreate_Click(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(294, 266);
        }

        private void btn_createuser_Click(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(614, 266);
        }

        private void btn_deleteuser_Click(object sender, EventArgs e)
        {
            if (lst_login.Items.Count != 0 && lst_login.SelectedIndex > -1)
            {
                int ii = lst_login.SelectedIndex;
                lst_login.Items.RemoveAt(ii);
                lst_rsn.Items.RemoveAt(ii);
                System.IO.StreamWriter c =File.CreateText(Config.Loginfile);
                c.Close();
                if (count != 0)
                {
                    count--;
                }
                System.IO.StreamWriter w = File.AppendText(Config.Loginfile);
                for (int io = 0; io < count; io++)
                {
                    w.WriteLine(lst_login.Items[io].ToString());
                    w.WriteLine(lst_rsn.Items[io].ToString());
                }
                w.Close();
            }
        }

        private void btn_useuser_Click(object sender, EventArgs e)
        {
            login();
        }

        private void txt_login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                login();
            }
        }

        private void lst_login_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            login();
        }

        private void login()
        {
            if (lst_login.Items.Count != 0 && lst_login.SelectedIndex > -1)
            {
                tbSample.Text = lst_rsn.Items[lst_login.SelectedIndex].ToString();
                string mainuser = tbSample.Text;

                this.Visible = false;

                //Create an instance of the output form
                Form1 frmOut = new Form1();

                //We set values through a property
                frmOut.Mainuser = mainuser;

                //We show the output form
                frmOut.Show();

                this.Hide();
            }
        }

        private void lst_login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                login();
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.smokinmils.com", null);
        }
    }
}
