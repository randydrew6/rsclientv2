namespace RS_Client
{
    partial class Splash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.lst_login = new System.Windows.Forms.ListBox();
            this.btn_deleteuser = new System.Windows.Forms.Button();
            this.btn_createuser = new System.Windows.Forms.Button();
            this.btn_useuser = new System.Windows.Forms.Button();
            this.lbl_useradd = new System.Windows.Forms.Label();
            this.txt_login = new System.Windows.Forms.TextBox();
            this.txt_rsncreate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_create = new System.Windows.Forms.Button();
            this.btn_cancelcreate = new System.Windows.Forms.Button();
            this.tbSample = new System.Windows.Forms.TextBox();
            this.lst_rsn = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lst_login
            // 
            this.lst_login.BackColor = System.Drawing.SystemColors.Window;
            this.lst_login.FormattingEnabled = true;
            this.lst_login.Location = new System.Drawing.Point(12, 12);
            this.lst_login.Name = "lst_login";
            this.lst_login.Size = new System.Drawing.Size(267, 173);
            this.lst_login.TabIndex = 0;
            this.lst_login.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lst_login_KeyPress);
            this.lst_login.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lst_login_MouseDoubleClick);
            // 
            // btn_deleteuser
            // 
            this.btn_deleteuser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_deleteuser.Image = global::RS_Client.Properties.Resources.dselbtn;
            this.btn_deleteuser.Location = new System.Drawing.Point(12, 236);
            this.btn_deleteuser.Name = "btn_deleteuser";
            this.btn_deleteuser.Size = new System.Drawing.Size(103, 21);
            this.btn_deleteuser.TabIndex = 1;
            this.btn_deleteuser.UseVisualStyleBackColor = true;
            this.btn_deleteuser.Click += new System.EventHandler(this.btn_deleteuser_Click);
            // 
            // btn_createuser
            // 
            this.btn_createuser.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_createuser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_createuser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_createuser.Image = global::RS_Client.Properties.Resources.cnewbtn;
            this.btn_createuser.Location = new System.Drawing.Point(12, 207);
            this.btn_createuser.Name = "btn_createuser";
            this.btn_createuser.Size = new System.Drawing.Size(103, 21);
            this.btn_createuser.TabIndex = 2;
            this.btn_createuser.UseVisualStyleBackColor = false;
            this.btn_createuser.Click += new System.EventHandler(this.btn_createuser_Click);
            // 
            // btn_useuser
            // 
            this.btn_useuser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_useuser.Image = global::RS_Client.Properties.Resources.uselbtn;
            this.btn_useuser.Location = new System.Drawing.Point(174, 220);
            this.btn_useuser.Name = "btn_useuser";
            this.btn_useuser.Size = new System.Drawing.Size(103, 21);
            this.btn_useuser.TabIndex = 3;
            this.btn_useuser.UseVisualStyleBackColor = true;
            this.btn_useuser.Click += new System.EventHandler(this.btn_useuser_Click);
            // 
            // lbl_useradd
            // 
            this.lbl_useradd.AutoSize = true;
            this.lbl_useradd.Location = new System.Drawing.Point(347, 41);
            this.lbl_useradd.Name = "lbl_useradd";
            this.lbl_useradd.Size = new System.Drawing.Size(67, 13);
            this.lbl_useradd.TabIndex = 5;
            this.lbl_useradd.Text = "Login Name:";
            // 
            // txt_login
            // 
            this.txt_login.Location = new System.Drawing.Point(451, 38);
            this.txt_login.Name = "txt_login";
            this.txt_login.Size = new System.Drawing.Size(100, 20);
            this.txt_login.TabIndex = 6;
            this.txt_login.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_login_KeyPress);
            // 
            // txt_rsncreate
            // 
            this.txt_rsncreate.Location = new System.Drawing.Point(451, 64);
            this.txt_rsncreate.Name = "txt_rsncreate";
            this.txt_rsncreate.Size = new System.Drawing.Size(100, 20);
            this.txt_rsncreate.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(347, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "RuneScape Name:";
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(476, 90);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(75, 23);
            this.btn_create.TabIndex = 9;
            this.btn_create.Text = "Create User";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // btn_cancelcreate
            // 
            this.btn_cancelcreate.Location = new System.Drawing.Point(350, 90);
            this.btn_cancelcreate.Name = "btn_cancelcreate";
            this.btn_cancelcreate.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelcreate.TabIndex = 10;
            this.btn_cancelcreate.Text = "Cancel";
            this.btn_cancelcreate.UseVisualStyleBackColor = true;
            this.btn_cancelcreate.Click += new System.EventHandler(this.btn_cancelcreate_Click);
            // 
            // tbSample
            // 
            this.tbSample.Location = new System.Drawing.Point(451, 193);
            this.tbSample.Name = "tbSample";
            this.tbSample.Size = new System.Drawing.Size(100, 20);
            this.tbSample.TabIndex = 11;
            this.tbSample.Visible = false;
            // 
            // lst_rsn
            // 
            this.lst_rsn.FormattingEnabled = true;
            this.lst_rsn.Location = new System.Drawing.Point(200, 191);
            this.lst_rsn.Name = "lst_rsn";
            this.lst_rsn.Size = new System.Drawing.Size(44, 17);
            this.lst_rsn.TabIndex = 12;
            this.lst_rsn.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::RS_Client.Properties.Resources.logosplash;
            this.panel1.Location = new System.Drawing.Point(121, 204);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(44, 52);
            this.panel1.TabIndex = 13;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::RS_Client.Properties.Resources.Loginscreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(295, 266);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lst_rsn);
            this.Controls.Add(this.tbSample);
            this.Controls.Add(this.btn_cancelcreate);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.txt_rsncreate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_login);
            this.Controls.Add(this.lbl_useradd);
            this.Controls.Add(this.btn_useuser);
            this.Controls.Add(this.btn_createuser);
            this.Controls.Add(this.btn_deleteuser);
            this.Controls.Add(this.lst_login);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log In - RS Client";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Splash_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_login;
        private System.Windows.Forms.Button btn_deleteuser;
        private System.Windows.Forms.Button btn_createuser;
        private System.Windows.Forms.Button btn_useuser;
        private System.Windows.Forms.Label lbl_useradd;
        private System.Windows.Forms.TextBox txt_login;
        private System.Windows.Forms.TextBox txt_rsncreate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.Button btn_cancelcreate;
        private System.Windows.Forms.TextBox tbSample;
        private System.Windows.Forms.ListBox lst_rsn;
        private System.Windows.Forms.Panel panel1;

    }
}