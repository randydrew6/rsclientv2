﻿using System.Windows.Forms;
using System.IO;
using System;
namespace RS_Client
{
    partial class Form1
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing
        }

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
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch
            {
                //----do nothing----//
            }
        }

        // Hides script errors without hiding other dialog boxes.
        private void SuppressScriptErrorsOnly(WebBrowser browser)
        {
            // Ensure that ScriptErrorsSuppressed is set to false.
            browser.ScriptErrorsSuppressed = false;

            // Handle DocumentCompleted to gain access to the Document object.
            browser.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(
                    browser_DocumentCompleted);
        }

        private void browser_DocumentCompleted(object sender,
            WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error +=
                new HtmlElementErrorEventHandler(Window_Error);
        }

        private void Window_Error(object sender,
            HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = true;
        }

        private void FormClosingEventCancle_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                SetParent(p.MainWindowHandle, GetDesktopWindow());
                SetWindowLong(p.MainWindowHandle, GWL_STYLE, style);
                ShowWindow(p.MainWindowHandle.ToInt32(), 3);
                ShowWindow(p.MainWindowHandle.ToInt32(), 9);
            }
            catch { }

            if (saveAndExitToolStripMenuItem.Checked == true)
            {
                StreamWriter extw = File.CreateText(Config.Extfile);
                string tempreadext = "";
                string tempreadextloc = "";
                int tempextnums = 0;
                while (tempreadext != null && tempreadext != "")
                {
                    tempreadext = lst_external.Items[tempextnums].ToString();
                    tempreadextloc = lst_loc.Items[tempextnums].ToString();
                    extw.WriteLine(tempreadext);
                    extw.WriteLine(tempreadextloc);
                    tempextnums++;
                    if (tempextnums + 1 > Extnums)
                    {
                        break;
                    }
                }
                extw.Close();

                StreamWriter webvidw = File.CreateText(Config.Vidfile);
                webvidw.WriteLine(webvideo.Url.ToString());
                webvidw.WriteLine(txt_searchvids.Text);
                webvidw.WriteLine(rad_youtubesearch.Checked);
                webvidw.WriteLine(rad_showsearch.Checked);
                webvidw.WriteLine(rad_animesearch.Checked);
                webvidw.WriteLine(rad_moviesearch.Checked);
                if (VidType != "Animep3")
                {
                    webvidw.WriteLine(VidType);
                }
                else
                {
                    webvidw.WriteLine("Animep2");
                }
                if (ytthrochannel == true)
                {
                    webvidw.WriteLine("true");
                }
                else
                {
                    webvidw.WriteLine("false");
                }
                webvidw.WriteLine(cmb_youtubechnfavs.Items.Count);
                for (int _i = 0; _i < ytFavTitle.Count; _i++)
                {
                    webvidw.WriteLine(ytFavTitle[_i]);
                    webvidw.WriteLine(ytFavURL[_i]);
                }
                for (int _i = 0; _i < VidTitle.Count; _i++)
                {
                    webvidw.WriteLine(VidTitle[_i]);
                    webvidw.WriteLine(VidUrl[_i]);
                    try
                    {
                        webvidw.WriteLine(VidPic[_i]);
                    }
                    catch
                    {
                        webvidw.WriteLine("");
                    }
                }
                if (lst_vids.Items.Count > 0 && VidType == "Showp1")
                {
                    webvidw.WriteLine("True");
                    for (int _i = 0; _i < VidSeasonlist.Count; _i++)
                    {
                        webvidw.WriteLine(VidSeasonlist[_i]);
                        webvidw.WriteLine(VidEpisodelist[_i]);
                    }
                }
                else
                {
                    webvidw.WriteLine("False");
                }
                webvidw.Close();

                StreamWriter webgamew = File.CreateText(Config.Flashfile);
                webgamew.WriteLine(web_games.Url.ToString());
                webgamew.WriteLine(txt_game.Text);
                for (int _i = 0; _i < GameTitle.Count; _i++)
                {
                    webgamew.WriteLine(GameTitle[_i]);
                    webgamew.WriteLine(GameEmbedURL[_i]);
                    webgamew.WriteLine(GameURL[_i]);
                    webgamew.WriteLine(GamePreview[_i]);
                }
                webgamew.Close();

                StreamWriter linkw = File.CreateText(Config.Favlinksfile);
                if (chk_lucky.Checked == true)
                {
                    linkw.WriteLine("true");
                }
                else
                {
                    linkw.WriteLine("false");
                }
                linkw.WriteLine(link1);
                linkw.WriteLine(link1_string);
                linkw.WriteLine(link2);
                linkw.WriteLine(link2_string);
                linkw.WriteLine(link3);
                linkw.WriteLine(link3_string);
                linkw.WriteLine(link4);
                linkw.WriteLine(link4_string);
                linkw.WriteLine(link5);
                linkw.WriteLine(link5_string);
                linkw.WriteLine(link6);
                linkw.WriteLine(link6_string);
                linkw.Close();

                StreamWriter mangaw = File.CreateText(Config.Mangafile);
                mangaw.WriteLine(mangapicurl);
                mangaw.WriteLine(originaltitle);
                mangaw.WriteLine(title);
                mangaw.WriteLine(chapter);
                mangaw.WriteLine(page);
                mangaw.Close();

                StreamWriter plw = File.CreateText(Config.Itemfile);
                plw.WriteLine(txt_item.Text);
                plw.WriteLine(txt_pricelookup.Text);
                plw.WriteLine(txt_alch.Text);
                plw.WriteLine(txt_profit.Text);
                plw.WriteLine(priceitem);
                plw.Close();

                StreamWriter sww = File.CreateText(Config.Otherfile);
                sww.WriteLine(lbl_stopwatch.Text);
                sww.WriteLine(playtype);
                sww.WriteLine(txt_user.Text);
                sww.WriteLine(splitContainer1.RightToLeft.ToString());
                sww.WriteLine(splitContainer1.SplitterDistance.ToString());
                sww.WriteLine(stagevuToolStripMenuItem.Checked.ToString());
                sww.WriteLine(putlockerToolStripMenuItem.Checked.ToString());
                sww.WriteLine(gorillavidToolStripMenuItem.Checked.ToString());
                sww.WriteLine(movshareToolStripMenuItem.Checked.ToString());
                sww.WriteLine(nowvideoToolStripMenuItem.Checked.ToString());
                sww.WriteLine(videoweedToolStripMenuItem1.Checked.ToString());
                sww.WriteLine(uploadcToolStripMenuItem.Checked.ToString());
                sww.WriteLine(vidbullToolStripMenuItem.Checked.ToString());
                sww.WriteLine(iRCToolStripMenuItem.Checked.ToString());
                sww.WriteLine(putlockerToolStripMenuItem1.Checked.ToString());
                sww.WriteLine(sockshareToolStripMenuItem.Checked.ToString());
                sww.WriteLine(nowvideoToolStripMenuItem1.Checked.ToString());
                sww.WriteLine(dwnToolStripMenuItem.Checked.ToString());
                sww.WriteLine(videoweedToolStripMenuItem.Checked.ToString());
                sww.WriteLine(twitteruser);
                sww.WriteLine(emailuser);
                sww.WriteLine(emailpass);
                sww.Close();

                string s = txt_notes.Text;
                string[] lines = s.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                File.WriteAllLines(Config.Notesfile, lines);

                StreamWriter w = File.CreateText(Config.Browserfile);
                w.WriteLine(Convert.ToString(page_number));
                w.Close();
                try
                {
                    for (int vari = 0; vari < page_number; vari++)
                    {
                        try
                        {
                            StreamWriter sw = File.AppendText(Config.Browserfile);
                            tabControl2.SelectTab(vari);
                            sw.WriteLine(((WebBrowser)tabControl2.SelectedTab.Controls[0]).Url.ToString());
                            sw.Close();
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                catch { }
            }
            Application.DoEvents();
            Application.Exit();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tmr_stopwatch = new System.Windows.Forms.Timer(this.components);
            this.label28 = new System.Windows.Forms.Label();
            this.tmr_blockads = new System.Windows.Forms.Timer(this.components);
            this.clearHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAndExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenShotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blockInternetExplorerPopupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForGrandExchangeUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.swapPanelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twitterFollowstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.txttwitteraccount = new System.Windows.Forms.ToolStripTextBox();
            this.startFollowingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTimeToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accontUsernameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_emailuser = new System.Windows.Forms.ToolStripTextBox();
            this.emailPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_emailpass = new System.Windows.Forms.ToolStripTextBox();
            this.getEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmb_emailtime = new System.Windows.Forms.ToolStripComboBox();
            this.rSSEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentEmailNotificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideUtilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iRCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netxtPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stagevuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.putlockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gorillavidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movshareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nowvideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoweedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vidbullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movieSourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.putlockerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sockshareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nowvideoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dwnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoweedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepEmbededToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel5 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txt_news = new System.Windows.Forms.TextBox();
            this.btn_p2p = new System.Windows.Forms.Panel();
            this.web_ad = new System.Windows.Forms.WebBrowser();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.web_irc = new System.Windows.Forms.WebBrowser();
            this.UsageStrip = new System.Windows.Forms.StatusStrip();
            this.cpu_strip = new System.Windows.Forms.ToolStripStatusLabel();
            this.ram_strip = new System.Windows.Forms.ToolStripStatusLabel();
            this.twitterfeeds = new System.Windows.Forms.ToolStripDropDownButton();
            this.emailstrip = new System.Windows.Forms.ToolStripDropDownButton();
            this.maintab = new System.Windows.Forms.TabControl();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txt_ur = new System.Windows.Forms.ComboBox();
            this.tabPage21 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_delscreen = new System.Windows.Forms.Button();
            this.btn_prevscreen = new System.Windows.Forms.Button();
            this.btn_nextscreen = new System.Windows.Forms.Button();
            this.pic_shots = new System.Windows.Forms.PictureBox();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.pic_video = new System.Windows.Forms.PictureBox();
            this.webvideo = new System.Windows.Forms.WebBrowser();
            this.MANGA0 = new System.Windows.Forms.TabPage();
            this.pic_manga0 = new System.Windows.Forms.PictureBox();
            this.tabPage19 = new System.Windows.Forms.TabPage();
            this.web_games = new System.Windows.Forms.WebBrowser();
            this.tabPage20 = new System.Windows.Forms.TabPage();
            this.pnl_gba = new System.Windows.Forms.Panel();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.trwFileExplorer = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.wmp_main = new AxWMPLib.AxWindowsMediaPlayer();
            this.tbCalc = new System.Windows.Forms.TabPage();
            this.wbCalc = new System.Windows.Forms.WebBrowser();
            this.maintabimages = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl5 = new System.Windows.Forms.TabControl();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.chk_lucky = new System.Windows.Forms.CheckBox();
            this.btn_close_web = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_clearurl = new System.Windows.Forms.Button();
            this.btn_forward = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_link2 = new System.Windows.Forms.Button();
            this.btn_link1 = new System.Windows.Forms.Button();
            this.btn_link6 = new System.Windows.Forms.Button();
            this.btn_link4 = new System.Windows.Forms.Button();
            this.btn_link5 = new System.Windows.Forms.Button();
            this.btn_link3 = new System.Windows.Forms.Button();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.btn_setlink = new System.Windows.Forms.Button();
            this.txt_linkurl = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_linkname = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.grp_linknum = new System.Windows.Forms.GroupBox();
            this.rad_6 = new System.Windows.Forms.RadioButton();
            this.rad_5 = new System.Windows.Forms.RadioButton();
            this.rad_4 = new System.Windows.Forms.RadioButton();
            this.rad_3 = new System.Windows.Forms.RadioButton();
            this.rad_2 = new System.Windows.Forms.RadioButton();
            this.rad_1 = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_highscoressearch = new System.Windows.Forms.Button();
            this.xptolvlbar = new System.Windows.Forms.ProgressBar();
            this.lbl_xp = new System.Windows.Forms.Label();
            this.lbl_rank = new System.Windows.Forms.Label();
            this.lbl_skillname = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.lbl_craftlvl = new System.Windows.Forms.Label();
            this.lbl_dunglvl = new System.Windows.Forms.Label();
            this.lbl_summlvl = new System.Windows.Forms.Label();
            this.lbl_conlvl = new System.Windows.Forms.Label();
            this.lbl_hunterlvl = new System.Windows.Forms.Label();
            this.lbl_rclvl = new System.Windows.Forms.Label();
            this.lbl_farminglvl = new System.Windows.Forms.Label();
            this.lbl_slayerlvl = new System.Windows.Forms.Label();
            this.lbl_thieflvl = new System.Windows.Forms.Label();
            this.lbl_agilitylvl = new System.Windows.Forms.Label();
            this.lbl_herblvl = new System.Windows.Forms.Label();
            this.lbl_mininglvl = new System.Windows.Forms.Label();
            this.lbl_smithlvl = new System.Windows.Forms.Label();
            this.lbl_fmlvl = new System.Windows.Forms.Label();
            this.lbl_fishinglvl = new System.Windows.Forms.Label();
            this.lbl_cooklvl = new System.Windows.Forms.Label();
            this.lbl_wclvl = new System.Windows.Forms.Label();
            this.lbl_fletchinglvl = new System.Windows.Forms.Label();
            this.lbl_magiclvl = new System.Windows.Forms.Label();
            this.lbl_prayerlvl = new System.Windows.Forms.Label();
            this.lbl_rangelvl = new System.Windows.Forms.Label();
            this.lbl_hplvl = new System.Windows.Forms.Label();
            this.lbl_strengthlvl = new System.Windows.Forms.Label();
            this.lbl_defencelvl = new System.Windows.Forms.Label();
            this.lbl_attacklvl = new System.Windows.Forms.Label();
            this.lbl_overall = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btn_clearembed = new System.Windows.Forms.Button();
            this.btn_processes = new System.Windows.Forms.Button();
            this.btn_embed = new System.Windows.Forms.Button();
            this.lst_exes = new System.Windows.Forms.ListBox();
            this.btn_extremove = new System.Windows.Forms.Button();
            this.grp_extadd = new System.Windows.Forms.GroupBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btn_extfile = new System.Windows.Forms.Button();
            this.txt_extname = new System.Windows.Forms.TextBox();
            this.lst_loc = new System.Windows.Forms.ListBox();
            this.btn_launchext = new System.Windows.Forms.Button();
            this.lst_external = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txt_notes = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.lbl_stopwatch = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txt_natprice = new System.Windows.Forms.TextBox();
            this.txt_alch = new System.Windows.Forms.TextBox();
            this.txt_profit = new System.Windows.Forms.TextBox();
            this.txt_pricelookup = new System.Windows.Forms.TextBox();
            this.txt_item = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.pic_priceitem = new System.Windows.Forms.PictureBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btn_mismanage = new System.Windows.Forms.Button();
            this.btn_prayerdrain = new System.Windows.Forms.Button();
            this.btn_pestcntl = new System.Windows.Forms.Button();
            this.btn_rangemax = new System.Windows.Forms.Button();
            this.btn_meleemax = new System.Windows.Forms.Button();
            this.btn_equiptbonus = new System.Windows.Forms.Button();
            this.btn_Energyrestore = new System.Windows.Forms.Button();
            this.btn_Combatlvl = new System.Windows.Forms.Button();
            this.btn_battlexp = new System.Windows.Forms.Button();
            this.btn_Skill = new System.Windows.Forms.Button();
            this.btn_normcalc = new System.Windows.Forms.Button();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.txt_searchmanga = new System.Windows.Forms.TextBox();
            this.txt_page = new System.Windows.Forms.TextBox();
            this.txt_chapter = new System.Windows.Forms.TextBox();
            this.txt_title = new System.Windows.Forms.TextBox();
            this.btn_randommanga = new System.Windows.Forms.Button();
            this.lst_manga = new System.Windows.Forms.ListBox();
            this.btn_searchmanga = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.btn_nextmanga = new System.Windows.Forms.Button();
            this.btn_peviousmanga = new System.Windows.Forms.Button();
            this.btn_read = new System.Windows.Forms.Button();
            this.lbl_pagenum = new System.Windows.Forms.Label();
            this.lbl_chapter = new System.Windows.Forms.Label();
            this.lbl_title = new System.Windows.Forms.Label();
            this.tabPage17 = new System.Windows.Forms.TabPage();
            this.pb_vidloading = new System.Windows.Forms.ProgressBar();
            this.btn_removefav = new System.Windows.Forms.Button();
            this.btn_addfav = new System.Windows.Forms.Button();
            this.btn_gofav = new System.Windows.Forms.Button();
            this.cmb_youtubechnfavs = new System.Windows.Forms.ComboBox();
            this.btn_mostviews = new System.Windows.Forms.Button();
            this.btn_newrel = new System.Windows.Forms.Button();
            this.btn_latest = new System.Windows.Forms.Button();
            this.btn_featured = new System.Windows.Forms.Button();
            this.webBrowserunshorten = new System.Windows.Forms.WebBrowser();
            this.trv_shows = new System.Windows.Forms.TreeView();
            this.btn_randomanime = new System.Windows.Forms.Button();
            this.pnl_yt = new System.Windows.Forms.Panel();
            this.rad_ytvideosec = new System.Windows.Forms.RadioButton();
            this.rad_ytchannelsec = new System.Windows.Forms.RadioButton();
            this.btn_clearvidplayer = new System.Windows.Forms.Button();
            this.btn_searchsite = new System.Windows.Forms.Button();
            this.grp_sites = new System.Windows.Forms.GroupBox();
            this.rad_moviesearch = new System.Windows.Forms.RadioButton();
            this.rad_animesearch = new System.Windows.Forms.RadioButton();
            this.rad_showsearch = new System.Windows.Forms.RadioButton();
            this.rad_youtubesearch = new System.Windows.Forms.RadioButton();
            this.lst_vids = new System.Windows.Forms.ListBox();
            this.txt_searchvids = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.tabPage18 = new System.Windows.Forms.TabPage();
            this.btn_randgame = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_cleargame = new System.Windows.Forms.Button();
            this.btn_searchgames = new System.Windows.Forms.Button();
            this.txt_game = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.lst_games = new System.Windows.Forms.ListBox();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.btn_go = new System.Windows.Forms.Button();
            this.tmr_geupdates = new System.Windows.Forms.Timer(this.components);
            this.thisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.andThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ohAndThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.test2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.test3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tmr_embed = new System.Windows.Forms.Timer(this.components);
            this.cpuusage = new System.Windows.Forms.Timer(this.components);
            this.tmr_twitter = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tmr_email = new System.Windows.Forms.Timer(this.components);
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rSOldSchoolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rSClassicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentRSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.UsageStrip.SuspendLayout();
            this.maintab.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage21.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_shots)).BeginInit();
            this.tabPage16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_video)).BeginInit();
            this.MANGA0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_manga0)).BeginInit();
            this.tabPage19.SuspendLayout();
            this.tabPage20.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmp_main)).BeginInit();
            this.tbCalc.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl5.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.grp_linknum.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.grp_extadd.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_priceitem)).BeginInit();
            this.tabPage7.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage17.SuspendLayout();
            this.pnl_yt.SuspendLayout();
            this.grp_sites.SuspendLayout();
            this.tabPage18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tmr_stopwatch
            // 
            this.tmr_stopwatch.Interval = 1000;
            this.tmr_stopwatch.Tick += new System.EventHandler(this.tmr_stopwatch_Tick);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(-5, 153);
            this.label28.MinimumSize = new System.Drawing.Size(778, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(778, 16);
            this.label28.TabIndex = 16;
            this.label28.Text = "IRC";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmr_blockads
            // 
            this.tmr_blockads.Enabled = true;
            this.tmr_blockads.Interval = 500;
            this.tmr_blockads.Tick += new System.EventHandler(this.tmr_blockads_Tick);
            // 
            // clearHistoryToolStripMenuItem
            // 
            this.clearHistoryToolStripMenuItem.Name = "clearHistoryToolStripMenuItem";
            this.clearHistoryToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.clearHistoryToolStripMenuItem.Text = "Clear History";
            this.clearHistoryToolStripMenuItem.Click += new System.EventHandler(this.clearHistoryToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.ForeColor = System.Drawing.Color.Black;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.panel5);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel1MinSize = 4;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.UsageStrip);
            this.splitContainer1.Panel2.Controls.Add(this.maintab);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel2MinSize = 4;
            this.splitContainer1.Size = new System.Drawing.Size(1362, 692);
            this.splitContainer1.SplitterDistance = 782;
            this.splitContainer1.TabIndex = 44;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.hideShowToolStripMenuItem,
            this.previousPageToolStripMenuItem,
            this.netxtPageToolStripMenuItem,
            this.showSourcesToolStripMenuItem,
            this.movieSourcesToolStripMenuItem,
            this.keepEmbededToolStripMenuItem,
            this.playToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip2.Size = new System.Drawing.Size(782, 24);
            this.menuStrip2.TabIndex = 50;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAndExitToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.restoreDefaultToolStripMenuItem,
            this.screenShotToolStripMenuItem,
            this.blockInternetExplorerPopupsToolStripMenuItem,
            this.checkForGrandExchangeUpdatesToolStripMenuItem,
            this.swapPanelsToolStripMenuItem,
            this.twitterFollowstrip,
            this.emailToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAndExitToolStripMenuItem
            // 
            this.saveAndExitToolStripMenuItem.Checked = true;
            this.saveAndExitToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveAndExitToolStripMenuItem.Image = global::RS_Client.Properties.Resources.save;
            this.saveAndExitToolStripMenuItem.Name = "saveAndExitToolStripMenuItem";
            this.saveAndExitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveAndExitToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.saveAndExitToolStripMenuItem.Text = "&Save on Exit";
            this.saveAndExitToolStripMenuItem.Click += new System.EventHandler(this.saveAndExitToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::RS_Client.Properties.Resources.exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // restoreDefaultToolStripMenuItem
            // 
            this.restoreDefaultToolStripMenuItem.Image = global::RS_Client.Properties.Resources.restoreiron;
            this.restoreDefaultToolStripMenuItem.Name = "restoreDefaultToolStripMenuItem";
            this.restoreDefaultToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.restoreDefaultToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.restoreDefaultToolStripMenuItem.Text = "&Restore Default";
            this.restoreDefaultToolStripMenuItem.Click += new System.EventHandler(this.restoreDefaultToolStripMenuItem_Click);
            // 
            // screenShotToolStripMenuItem
            // 
            this.screenShotToolStripMenuItem.Image = global::RS_Client.Properties.Resources.screenshot_icon;
            this.screenShotToolStripMenuItem.Name = "screenShotToolStripMenuItem";
            this.screenShotToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.screenShotToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.screenShotToolStripMenuItem.Text = "Screenshot";
            this.screenShotToolStripMenuItem.Click += new System.EventHandler(this.screenShotToolStripMenuItem_Click);
            // 
            // blockInternetExplorerPopupsToolStripMenuItem
            // 
            this.blockInternetExplorerPopupsToolStripMenuItem.Checked = true;
            this.blockInternetExplorerPopupsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.blockInternetExplorerPopupsToolStripMenuItem.Image = global::RS_Client.Properties.Resources.stop;
            this.blockInternetExplorerPopupsToolStripMenuItem.Name = "blockInternetExplorerPopupsToolStripMenuItem";
            this.blockInternetExplorerPopupsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.blockInternetExplorerPopupsToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.blockInternetExplorerPopupsToolStripMenuItem.Text = "Block Internet Explorer Pop-ups";
            this.blockInternetExplorerPopupsToolStripMenuItem.Click += new System.EventHandler(this.blockInternetExplorerPopupsToolStripMenuItem_Click);
            // 
            // checkForGrandExchangeUpdatesToolStripMenuItem
            // 
            this.checkForGrandExchangeUpdatesToolStripMenuItem.Checked = true;
            this.checkForGrandExchangeUpdatesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkForGrandExchangeUpdatesToolStripMenuItem.Image = global::RS_Client.Properties.Resources.Grand_Exchange_icon;
            this.checkForGrandExchangeUpdatesToolStripMenuItem.Name = "checkForGrandExchangeUpdatesToolStripMenuItem";
            this.checkForGrandExchangeUpdatesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.checkForGrandExchangeUpdatesToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.checkForGrandExchangeUpdatesToolStripMenuItem.Text = "Notified on Grand Exchange Updates";
            this.checkForGrandExchangeUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForGrandExchangeUpdatesToolStripMenuItem_Click);
            // 
            // swapPanelsToolStripMenuItem
            // 
            this.swapPanelsToolStripMenuItem.Image = global::RS_Client.Properties.Resources.swap;
            this.swapPanelsToolStripMenuItem.Name = "swapPanelsToolStripMenuItem";
            this.swapPanelsToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.swapPanelsToolStripMenuItem.Text = "Swap Panels";
            this.swapPanelsToolStripMenuItem.Click += new System.EventHandler(this.swapPanelsToolStripMenuItem_Click);
            // 
            // twitterFollowstrip
            // 
            this.twitterFollowstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txttwitteraccount,
            this.startFollowingToolStripMenuItem});
            this.twitterFollowstrip.Image = global::RS_Client.Properties.Resources.twitter;
            this.twitterFollowstrip.Name = "twitterFollowstrip";
            this.twitterFollowstrip.Size = new System.Drawing.Size(310, 22);
            this.twitterFollowstrip.Text = "Follow Twitter Account";
            // 
            // txttwitteraccount
            // 
            this.txttwitteraccount.Name = "txttwitteraccount";
            this.txttwitteraccount.Size = new System.Drawing.Size(100, 23);
            // 
            // startFollowingToolStripMenuItem
            // 
            this.startFollowingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkTimeToolStripMenuItem,
            this.startToolStripMenuItem});
            this.startFollowingToolStripMenuItem.Name = "startFollowingToolStripMenuItem";
            this.startFollowingToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.startFollowingToolStripMenuItem.Text = "Start Following";
            // 
            // checkTimeToolStripMenuItem
            // 
            this.checkTimeToolStripMenuItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.checkTimeToolStripMenuItem.Items.AddRange(new object[] {
            "3 Seconds",
            "5 Seconds",
            "10 Seconds",
            "30 Seconds",
            "1 Minute",
            "5 Minutes",
            "10 Minutes",
            "30 Minutes",
            "1 Hour"});
            this.checkTimeToolStripMenuItem.Name = "checkTimeToolStripMenuItem";
            this.checkTimeToolStripMenuItem.Size = new System.Drawing.Size(152, 23);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // emailToolStripMenuItem
            // 
            this.emailToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accontUsernameToolStripMenuItem,
            this.emailPasswordToolStripMenuItem,
            this.getEmailToolStripMenuItem,
            this.currentEmailNotificationToolStripMenuItem});
            this.emailToolStripMenuItem.Image = global::RS_Client.Properties.Resources.gmail;
            this.emailToolStripMenuItem.Name = "emailToolStripMenuItem";
            this.emailToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.emailToolStripMenuItem.Text = "Gmail";
            // 
            // accontUsernameToolStripMenuItem
            // 
            this.accontUsernameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txt_emailuser});
            this.accontUsernameToolStripMenuItem.Name = "accontUsernameToolStripMenuItem";
            this.accontUsernameToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.accontUsernameToolStripMenuItem.Text = "Gmail Username";
            // 
            // txt_emailuser
            // 
            this.txt_emailuser.Name = "txt_emailuser";
            this.txt_emailuser.Size = new System.Drawing.Size(100, 23);
            // 
            // emailPasswordToolStripMenuItem
            // 
            this.emailPasswordToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txt_emailpass});
            this.emailPasswordToolStripMenuItem.Name = "emailPasswordToolStripMenuItem";
            this.emailPasswordToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.emailPasswordToolStripMenuItem.Text = "Gmail Password";
            // 
            // txt_emailpass
            // 
            this.txt_emailpass.Name = "txt_emailpass";
            this.txt_emailpass.Size = new System.Drawing.Size(100, 23);
            // 
            // getEmailToolStripMenuItem
            // 
            this.getEmailToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmb_emailtime,
            this.rSSEmailToolStripMenuItem});
            this.getEmailToolStripMenuItem.Name = "getEmailToolStripMenuItem";
            this.getEmailToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.getEmailToolStripMenuItem.Text = "Gmail RSS";
            // 
            // cmb_emailtime
            // 
            this.cmb_emailtime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_emailtime.Items.AddRange(new object[] {
            "3 Seconds",
            "5 Seconds",
            "10 Seconds",
            "30 Seconds",
            "1 Minute",
            "5 Minutes",
            "10 Minutes",
            "30 Minutes",
            "1 Hour"});
            this.cmb_emailtime.Name = "cmb_emailtime";
            this.cmb_emailtime.Size = new System.Drawing.Size(121, 23);
            // 
            // rSSEmailToolStripMenuItem
            // 
            this.rSSEmailToolStripMenuItem.Name = "rSSEmailToolStripMenuItem";
            this.rSSEmailToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.rSSEmailToolStripMenuItem.Text = "Start Notification";
            this.rSSEmailToolStripMenuItem.Click += new System.EventHandler(this.rSSEmailToolStripMenuItem_Click);
            // 
            // currentEmailNotificationToolStripMenuItem
            // 
            this.currentEmailNotificationToolStripMenuItem.Name = "currentEmailNotificationToolStripMenuItem";
            this.currentEmailNotificationToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.currentEmailNotificationToolStripMenuItem.Text = "Current Gmail mail";
            this.currentEmailNotificationToolStripMenuItem.Click += new System.EventHandler(this.currentEmailNotificationToolStripMenuItem_Click);
            // 
            // hideShowToolStripMenuItem
            // 
            this.hideShowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideUtilitiesToolStripMenuItem,
            this.iRCToolStripMenuItem});
            this.hideShowToolStripMenuItem.Name = "hideShowToolStripMenuItem";
            this.hideShowToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.hideShowToolStripMenuItem.Text = "Hide/Show";
            // 
            // hideUtilitiesToolStripMenuItem
            // 
            this.hideUtilitiesToolStripMenuItem.Name = "hideUtilitiesToolStripMenuItem";
            this.hideUtilitiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.hideUtilitiesToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.hideUtilitiesToolStripMenuItem.Text = "&Utilities";
            this.hideUtilitiesToolStripMenuItem.Click += new System.EventHandler(this.hideUtilitiesToolStripMenuItem_Click);
            // 
            // iRCToolStripMenuItem
            // 
            this.iRCToolStripMenuItem.Name = "iRCToolStripMenuItem";
            this.iRCToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.iRCToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.iRCToolStripMenuItem.Text = "&IRC";
            this.iRCToolStripMenuItem.Click += new System.EventHandler(this.iRCToolStripMenuItem_Click);
            // 
            // previousPageToolStripMenuItem
            // 
            this.previousPageToolStripMenuItem.Enabled = false;
            this.previousPageToolStripMenuItem.Image = global::RS_Client.Properties.Resources.previous;
            this.previousPageToolStripMenuItem.Name = "previousPageToolStripMenuItem";
            this.previousPageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.previousPageToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.previousPageToolStripMenuItem.Text = "Previous Page";
            this.previousPageToolStripMenuItem.Click += new System.EventHandler(this.previousPageToolStripMenuItem_Click);
            // 
            // netxtPageToolStripMenuItem
            // 
            this.netxtPageToolStripMenuItem.Enabled = false;
            this.netxtPageToolStripMenuItem.Image = global::RS_Client.Properties.Resources.next;
            this.netxtPageToolStripMenuItem.Name = "netxtPageToolStripMenuItem";
            this.netxtPageToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.netxtPageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.netxtPageToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.netxtPageToolStripMenuItem.Text = "Next Page";
            this.netxtPageToolStripMenuItem.Click += new System.EventHandler(this.netxtPageToolStripMenuItem_Click);
            // 
            // showSourcesToolStripMenuItem
            // 
            this.showSourcesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stagevuToolStripMenuItem,
            this.putlockerToolStripMenuItem,
            this.gorillavidToolStripMenuItem,
            this.movshareToolStripMenuItem,
            this.nowvideoToolStripMenuItem,
            this.videoweedToolStripMenuItem1,
            this.uploadcToolStripMenuItem,
            this.vidbullToolStripMenuItem});
            this.showSourcesToolStripMenuItem.Name = "showSourcesToolStripMenuItem";
            this.showSourcesToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.showSourcesToolStripMenuItem.Text = "Show sources";
            // 
            // stagevuToolStripMenuItem
            // 
            this.stagevuToolStripMenuItem.Name = "stagevuToolStripMenuItem";
            this.stagevuToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.stagevuToolStripMenuItem.Text = "stagevu (Requires Divx Plus)";
            this.stagevuToolStripMenuItem.Click += new System.EventHandler(this.stagevuToolStripMenuItem_Click);
            // 
            // putlockerToolStripMenuItem
            // 
            this.putlockerToolStripMenuItem.Checked = true;
            this.putlockerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.putlockerToolStripMenuItem.Name = "putlockerToolStripMenuItem";
            this.putlockerToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.putlockerToolStripMenuItem.Text = "putlocker";
            this.putlockerToolStripMenuItem.Click += new System.EventHandler(this.putlockerToolStripMenuItem_Click);
            // 
            // gorillavidToolStripMenuItem
            // 
            this.gorillavidToolStripMenuItem.Checked = true;
            this.gorillavidToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gorillavidToolStripMenuItem.Name = "gorillavidToolStripMenuItem";
            this.gorillavidToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.gorillavidToolStripMenuItem.Text = "gorillavid";
            this.gorillavidToolStripMenuItem.Click += new System.EventHandler(this.gorillavidToolStripMenuItem_Click);
            // 
            // movshareToolStripMenuItem
            // 
            this.movshareToolStripMenuItem.Checked = true;
            this.movshareToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.movshareToolStripMenuItem.Name = "movshareToolStripMenuItem";
            this.movshareToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.movshareToolStripMenuItem.Text = "movshare";
            this.movshareToolStripMenuItem.Click += new System.EventHandler(this.movshareToolStripMenuItem_Click);
            // 
            // nowvideoToolStripMenuItem
            // 
            this.nowvideoToolStripMenuItem.Checked = true;
            this.nowvideoToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nowvideoToolStripMenuItem.Name = "nowvideoToolStripMenuItem";
            this.nowvideoToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.nowvideoToolStripMenuItem.Text = "nowvideo";
            this.nowvideoToolStripMenuItem.Click += new System.EventHandler(this.nowvideoToolStripMenuItem_Click);
            // 
            // videoweedToolStripMenuItem1
            // 
            this.videoweedToolStripMenuItem1.Checked = true;
            this.videoweedToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.videoweedToolStripMenuItem1.Name = "videoweedToolStripMenuItem1";
            this.videoweedToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
            this.videoweedToolStripMenuItem1.Text = "videoweed";
            this.videoweedToolStripMenuItem1.Click += new System.EventHandler(this.videoweedToolStripMenuItem1_Click);
            // 
            // uploadcToolStripMenuItem
            // 
            this.uploadcToolStripMenuItem.Checked = true;
            this.uploadcToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uploadcToolStripMenuItem.Name = "uploadcToolStripMenuItem";
            this.uploadcToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.uploadcToolStripMenuItem.Text = "uploadc";
            this.uploadcToolStripMenuItem.Click += new System.EventHandler(this.uploadcToolStripMenuItem_Click);
            // 
            // vidbullToolStripMenuItem
            // 
            this.vidbullToolStripMenuItem.Checked = true;
            this.vidbullToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vidbullToolStripMenuItem.Name = "vidbullToolStripMenuItem";
            this.vidbullToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.vidbullToolStripMenuItem.Text = "vidbull";
            this.vidbullToolStripMenuItem.Click += new System.EventHandler(this.vidbullToolStripMenuItem_Click);
            // 
            // movieSourcesToolStripMenuItem
            // 
            this.movieSourcesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.putlockerToolStripMenuItem1,
            this.sockshareToolStripMenuItem,
            this.nowvideoToolStripMenuItem1,
            this.dwnToolStripMenuItem,
            this.videoweedToolStripMenuItem});
            this.movieSourcesToolStripMenuItem.Name = "movieSourcesToolStripMenuItem";
            this.movieSourcesToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.movieSourcesToolStripMenuItem.Text = "Movie sources";
            // 
            // putlockerToolStripMenuItem1
            // 
            this.putlockerToolStripMenuItem1.Checked = true;
            this.putlockerToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.putlockerToolStripMenuItem1.Name = "putlockerToolStripMenuItem1";
            this.putlockerToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.putlockerToolStripMenuItem1.Text = "putlocker";
            this.putlockerToolStripMenuItem1.Click += new System.EventHandler(this.putlockerToolStripMenuItem1_Click);
            // 
            // sockshareToolStripMenuItem
            // 
            this.sockshareToolStripMenuItem.Checked = true;
            this.sockshareToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sockshareToolStripMenuItem.Name = "sockshareToolStripMenuItem";
            this.sockshareToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.sockshareToolStripMenuItem.Text = "sockshare";
            this.sockshareToolStripMenuItem.Click += new System.EventHandler(this.sockshareToolStripMenuItem_Click);
            // 
            // nowvideoToolStripMenuItem1
            // 
            this.nowvideoToolStripMenuItem1.Checked = true;
            this.nowvideoToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nowvideoToolStripMenuItem1.Name = "nowvideoToolStripMenuItem1";
            this.nowvideoToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.nowvideoToolStripMenuItem1.Text = "nowvideo";
            this.nowvideoToolStripMenuItem1.Click += new System.EventHandler(this.nowvideoToolStripMenuItem1_Click);
            // 
            // dwnToolStripMenuItem
            // 
            this.dwnToolStripMenuItem.Checked = true;
            this.dwnToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dwnToolStripMenuItem.Name = "dwnToolStripMenuItem";
            this.dwnToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.dwnToolStripMenuItem.Text = "dwn";
            this.dwnToolStripMenuItem.Click += new System.EventHandler(this.dwnToolStripMenuItem_Click);
            // 
            // videoweedToolStripMenuItem
            // 
            this.videoweedToolStripMenuItem.Checked = true;
            this.videoweedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.videoweedToolStripMenuItem.Name = "videoweedToolStripMenuItem";
            this.videoweedToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.videoweedToolStripMenuItem.Text = "videoweed";
            this.videoweedToolStripMenuItem.Click += new System.EventHandler(this.videoweedToolStripMenuItem_Click);
            // 
            // keepEmbededToolStripMenuItem
            // 
            this.keepEmbededToolStripMenuItem.Name = "keepEmbededToolStripMenuItem";
            this.keepEmbededToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.keepEmbededToolStripMenuItem.Size = new System.Drawing.Size(182, 20);
            this.keepEmbededToolStripMenuItem.Text = "Keep embedded process active";
            this.keepEmbededToolStripMenuItem.Click += new System.EventHandler(this.keepEmbededToolStripMenuItem_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.splitContainer2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(782, 692);
            this.panel5.TabIndex = 48;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txt_news);
            this.splitContainer2.Panel1.Controls.Add(this.btn_p2p);
            this.splitContainer2.Panel1.Controls.Add(this.web_ad);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox2);
            this.splitContainer2.Panel1.Controls.Add(this.webBrowser2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.web_irc);
            this.splitContainer2.Size = new System.Drawing.Size(782, 692);
            this.splitContainer2.SplitterDistance = 529;
            this.splitContainer2.TabIndex = 62;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // txt_news
            // 
            this.txt_news.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_news.BackColor = System.Drawing.Color.BurlyWood;
            this.txt_news.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_news.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_news.ForeColor = System.Drawing.SystemColors.InfoText;
            this.txt_news.Location = new System.Drawing.Point(241, 352);
            this.txt_news.Multiline = true;
            this.txt_news.Name = "txt_news";
            this.txt_news.ReadOnly = true;
            this.txt_news.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_news.Size = new System.Drawing.Size(341, 170);
            this.txt_news.TabIndex = 66;
            // 
            // btn_p2p
            // 
            this.btn_p2p.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_p2p.BackgroundImage = global::RS_Client.Properties.Resources.playnow;
            this.btn_p2p.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_p2p.Location = new System.Drawing.Point(341, 276);
            this.btn_p2p.Name = "btn_p2p";
            this.btn_p2p.Size = new System.Drawing.Size(132, 34);
            this.btn_p2p.TabIndex = 65;
            this.btn_p2p.Click += new System.EventHandler(this.btn_p2p_Click);
            // 
            // web_ad
            // 
            this.web_ad.Location = new System.Drawing.Point(-154, 24);
            this.web_ad.Margin = new System.Windows.Forms.Padding(0);
            this.web_ad.MinimumSize = new System.Drawing.Size(20, 20);
            this.web_ad.Name = "web_ad";
            this.web_ad.ScriptErrorsSuppressed = true;
            this.web_ad.ScrollBarsEnabled = false;
            this.web_ad.Size = new System.Drawing.Size(1078, 91);
            this.web_ad.TabIndex = 64;
            this.web_ad.Url = new System.Uri("http://19ee8df1.linkbucks.com/", System.UriKind.Absolute);
            this.web_ad.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.web_ad.NewWindow += new System.ComponentModel.CancelEventHandler(this.Browser1_NewWindow);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = global::RS_Client.Properties.Resources.backgroundmain;
            this.pictureBox2.Location = new System.Drawing.Point(-1, 118);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(783, 411);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 63;
            this.pictureBox2.TabStop = false;
            // 
            // webBrowser2
            // 
            this.webBrowser2.Location = new System.Drawing.Point(-3, 24);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.ScriptErrorsSuppressed = true;
            this.webBrowser2.Size = new System.Drawing.Size(783, 505);
            this.webBrowser2.TabIndex = 62;
            this.webBrowser2.Visible = false;
            this.webBrowser2.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.webBrowser2.NewWindow += new System.ComponentModel.CancelEventHandler(this.Browser1_NewWindow);
            // 
            // web_irc
            // 
            this.web_irc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web_irc.Location = new System.Drawing.Point(0, 0);
            this.web_irc.MinimumSize = new System.Drawing.Size(20, 20);
            this.web_irc.Name = "web_irc";
            this.web_irc.ScrollBarsEnabled = false;
            this.web_irc.Size = new System.Drawing.Size(782, 159);
            this.web_irc.TabIndex = 0;
            this.web_irc.Url = new System.Uri("http://qwebirc.swiftirc.net/", System.UriKind.Absolute);
            // 
            // UsageStrip
            // 
            this.UsageStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UsageStrip.BackColor = System.Drawing.Color.Black;
            this.UsageStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.UsageStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.UsageStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cpu_strip,
            this.ram_strip,
            this.twitterfeeds,
            this.emailstrip});
            this.UsageStrip.Location = new System.Drawing.Point(465, 670);
            this.UsageStrip.Name = "UsageStrip";
            this.UsageStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.UsageStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.UsageStrip.Size = new System.Drawing.Size(111, 22);
            this.UsageStrip.TabIndex = 45;
            // 
            // cpu_strip
            // 
            this.cpu_strip.ForeColor = System.Drawing.Color.White;
            this.cpu_strip.Name = "cpu_strip";
            this.cpu_strip.Size = new System.Drawing.Size(0, 0);
            // 
            // ram_strip
            // 
            this.ram_strip.ForeColor = System.Drawing.Color.White;
            this.ram_strip.Name = "ram_strip";
            this.ram_strip.Size = new System.Drawing.Size(0, 0);
            // 
            // twitterfeeds
            // 
            this.twitterfeeds.ForeColor = System.Drawing.Color.White;
            this.twitterfeeds.Image = global::RS_Client.Properties.Resources.twitter;
            this.twitterfeeds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.twitterfeeds.Name = "twitterfeeds";
            this.twitterfeeds.Size = new System.Drawing.Size(29, 20);
            // 
            // emailstrip
            // 
            this.emailstrip.ForeColor = System.Drawing.Color.White;
            this.emailstrip.Image = global::RS_Client.Properties.Resources.gmail;
            this.emailstrip.Name = "emailstrip";
            this.emailstrip.Size = new System.Drawing.Size(65, 20);
            this.emailstrip.Text = "Email";
            // 
            // maintab
            // 
            this.maintab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maintab.Controls.Add(this.tabPage11);
            this.maintab.Controls.Add(this.tabPage21);
            this.maintab.Controls.Add(this.tabPage16);
            this.maintab.Controls.Add(this.MANGA0);
            this.maintab.Controls.Add(this.tabPage19);
            this.maintab.Controls.Add(this.tabPage20);
            this.maintab.Controls.Add(this.tabPage12);
            this.maintab.Controls.Add(this.tabPage13);
            this.maintab.Controls.Add(this.tbCalc);
            this.maintab.ImageList = this.maintabimages;
            this.maintab.Location = new System.Drawing.Point(-1, 1);
            this.maintab.Name = "maintab";
            this.maintab.SelectedIndex = 0;
            this.maintab.Size = new System.Drawing.Size(577, 528);
            this.maintab.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.maintab.TabIndex = 45;
            this.maintab.SelectedIndexChanged += new System.EventHandler(this.maintab_SelectedIndexChanged);
            // 
            // tabPage11
            // 
            this.tabPage11.BackColor = System.Drawing.Color.Transparent;
            this.tabPage11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage11.Controls.Add(this.tabControl2);
            this.tabPage11.Controls.Add(this.panel4);
            this.tabPage11.ImageIndex = 0;
            this.tabPage11.Location = new System.Drawing.Point(4, 25);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(569, 499);
            this.tabPage11.TabIndex = 0;
            this.tabPage11.Text = "Websites";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Location = new System.Drawing.Point(2, 23);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(563, 473);
            this.tabControl2.TabIndex = 45;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txt_ur);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(563, 493);
            this.panel4.TabIndex = 5;
            // 
            // txt_ur
            // 
            this.txt_ur.BackColor = System.Drawing.Color.Silver;
            this.txt_ur.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_ur.FormattingEnabled = true;
            this.txt_ur.Location = new System.Drawing.Point(0, 0);
            this.txt_ur.Name = "txt_ur";
            this.txt_ur.Size = new System.Drawing.Size(563, 21);
            this.txt_ur.TabIndex = 6;
            this.txt_ur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_url_KeyPress);
            this.txt_ur.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_ur_KeyUp);
            this.txt_ur.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_url_MouseDoubleClick);
            // 
            // tabPage21
            // 
            this.tabPage21.Controls.Add(this.panel3);
            this.tabPage21.Controls.Add(this.pic_shots);
            this.tabPage21.ImageIndex = 1;
            this.tabPage21.Location = new System.Drawing.Point(4, 25);
            this.tabPage21.Name = "tabPage21";
            this.tabPage21.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage21.Size = new System.Drawing.Size(569, 499);
            this.tabPage21.TabIndex = 6;
            this.tabPage21.Text = "Screenshots";
            this.tabPage21.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_delscreen);
            this.panel3.Controls.Add(this.btn_prevscreen);
            this.panel3.Controls.Add(this.btn_nextscreen);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 449);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(563, 47);
            this.panel3.TabIndex = 4;
            // 
            // btn_delscreen
            // 
            this.btn_delscreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_delscreen.Location = new System.Drawing.Point(167, 0);
            this.btn_delscreen.Name = "btn_delscreen";
            this.btn_delscreen.Size = new System.Drawing.Size(229, 47);
            this.btn_delscreen.TabIndex = 3;
            this.btn_delscreen.Text = "Delete";
            this.btn_delscreen.UseVisualStyleBackColor = true;
            this.btn_delscreen.Click += new System.EventHandler(this.btn_delscreen_Click);
            // 
            // btn_prevscreen
            // 
            this.btn_prevscreen.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_prevscreen.Location = new System.Drawing.Point(0, 0);
            this.btn_prevscreen.Name = "btn_prevscreen";
            this.btn_prevscreen.Size = new System.Drawing.Size(167, 47);
            this.btn_prevscreen.TabIndex = 1;
            this.btn_prevscreen.Text = "<<";
            this.btn_prevscreen.UseVisualStyleBackColor = true;
            this.btn_prevscreen.Click += new System.EventHandler(this.btn_prevscreen_Click);
            // 
            // btn_nextscreen
            // 
            this.btn_nextscreen.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_nextscreen.Location = new System.Drawing.Point(396, 0);
            this.btn_nextscreen.Name = "btn_nextscreen";
            this.btn_nextscreen.Size = new System.Drawing.Size(167, 47);
            this.btn_nextscreen.TabIndex = 2;
            this.btn_nextscreen.Text = ">>";
            this.btn_nextscreen.UseVisualStyleBackColor = true;
            this.btn_nextscreen.Click += new System.EventHandler(this.btn_nextscreen_Click);
            // 
            // pic_shots
            // 
            this.pic_shots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_shots.Location = new System.Drawing.Point(3, 3);
            this.pic_shots.Name = "pic_shots";
            this.pic_shots.Size = new System.Drawing.Size(563, 493);
            this.pic_shots.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_shots.TabIndex = 3;
            this.pic_shots.TabStop = false;
            // 
            // tabPage16
            // 
            this.tabPage16.Controls.Add(this.pic_video);
            this.tabPage16.Controls.Add(this.webvideo);
            this.tabPage16.ImageIndex = 2;
            this.tabPage16.Location = new System.Drawing.Point(4, 25);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage16.Size = new System.Drawing.Size(569, 499);
            this.tabPage16.TabIndex = 3;
            this.tabPage16.Text = "Video";
            this.tabPage16.UseVisualStyleBackColor = true;
            // 
            // pic_video
            // 
            this.pic_video.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_video.Location = new System.Drawing.Point(3, 3);
            this.pic_video.Name = "pic_video";
            this.pic_video.Size = new System.Drawing.Size(563, 493);
            this.pic_video.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_video.TabIndex = 5;
            this.pic_video.TabStop = false;
            this.pic_video.Visible = false;
            // 
            // webvideo
            // 
            this.webvideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webvideo.Location = new System.Drawing.Point(3, 3);
            this.webvideo.MinimumSize = new System.Drawing.Size(20, 20);
            this.webvideo.Name = "webvideo";
            this.webvideo.ScriptErrorsSuppressed = true;
            this.webvideo.ScrollBarsEnabled = false;
            this.webvideo.Size = new System.Drawing.Size(563, 493);
            this.webvideo.TabIndex = 0;
            this.webvideo.Url = new System.Uri("", System.UriKind.Relative);
            this.webvideo.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.webvideo.NewWindow += new System.ComponentModel.CancelEventHandler(this.Browser1_NewWindow);
            // 
            // MANGA0
            // 
            this.MANGA0.Controls.Add(this.pic_manga0);
            this.MANGA0.ImageIndex = 3;
            this.MANGA0.Location = new System.Drawing.Point(4, 25);
            this.MANGA0.Name = "MANGA0";
            this.MANGA0.Padding = new System.Windows.Forms.Padding(3);
            this.MANGA0.Size = new System.Drawing.Size(569, 499);
            this.MANGA0.TabIndex = 1;
            this.MANGA0.Text = "Manga";
            this.MANGA0.UseVisualStyleBackColor = true;
            // 
            // pic_manga0
            // 
            this.pic_manga0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_manga0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_manga0.Location = new System.Drawing.Point(3, 3);
            this.pic_manga0.Name = "pic_manga0";
            this.pic_manga0.Size = new System.Drawing.Size(563, 493);
            this.pic_manga0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_manga0.TabIndex = 0;
            this.pic_manga0.TabStop = false;
            this.pic_manga0.Click += new System.EventHandler(this.pic_manga0_Click);
            // 
            // tabPage19
            // 
            this.tabPage19.Controls.Add(this.web_games);
            this.tabPage19.ImageIndex = 4;
            this.tabPage19.Location = new System.Drawing.Point(4, 25);
            this.tabPage19.Name = "tabPage19";
            this.tabPage19.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage19.Size = new System.Drawing.Size(569, 499);
            this.tabPage19.TabIndex = 4;
            this.tabPage19.Text = "Flash";
            this.tabPage19.UseVisualStyleBackColor = true;
            // 
            // web_games
            // 
            this.web_games.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web_games.Location = new System.Drawing.Point(3, 3);
            this.web_games.MinimumSize = new System.Drawing.Size(20, 20);
            this.web_games.Name = "web_games";
            this.web_games.ScriptErrorsSuppressed = true;
            this.web_games.ScrollBarsEnabled = false;
            this.web_games.Size = new System.Drawing.Size(563, 493);
            this.web_games.TabIndex = 1;
            this.web_games.Url = new System.Uri("", System.UriKind.Relative);
            this.web_games.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.web_games.NewWindow += new System.ComponentModel.CancelEventHandler(this.Browser1_NewWindow);
            // 
            // tabPage20
            // 
            this.tabPage20.Controls.Add(this.pnl_gba);
            this.tabPage20.ImageIndex = 5;
            this.tabPage20.Location = new System.Drawing.Point(4, 25);
            this.tabPage20.Name = "tabPage20";
            this.tabPage20.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage20.Size = new System.Drawing.Size(569, 499);
            this.tabPage20.TabIndex = 5;
            this.tabPage20.Text = "Embed";
            this.tabPage20.UseVisualStyleBackColor = true;
            // 
            // pnl_gba
            // 
            this.pnl_gba.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_gba.Location = new System.Drawing.Point(3, 3);
            this.pnl_gba.Name = "pnl_gba";
            this.pnl_gba.Size = new System.Drawing.Size(563, 493);
            this.pnl_gba.TabIndex = 0;
            this.pnl_gba.Enter += new System.EventHandler(this.pnl_gba_Enter);
            this.pnl_gba.MouseEnter += new System.EventHandler(this.pnl_gba_MouseEnter);
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.trwFileExplorer);
            this.tabPage12.ImageIndex = 6;
            this.tabPage12.Location = new System.Drawing.Point(4, 25);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(569, 499);
            this.tabPage12.TabIndex = 7;
            this.tabPage12.Text = "Launch";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // trwFileExplorer
            // 
            this.trwFileExplorer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trwFileExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trwFileExplorer.ImageIndex = 0;
            this.trwFileExplorer.ImageList = this.imageList1;
            this.trwFileExplorer.Location = new System.Drawing.Point(3, 3);
            this.trwFileExplorer.Name = "trwFileExplorer";
            this.trwFileExplorer.SelectedImageIndex = 0;
            this.trwFileExplorer.Size = new System.Drawing.Size(563, 493);
            this.trwFileExplorer.TabIndex = 0;
            this.trwFileExplorer.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.trwFileExplorer_BeforeExpand);
            this.trwFileExplorer.DoubleClick += new System.EventHandler(this.trwFileExplorer_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "DVDFolderXP.jpg");
            this.imageList1.Images.SetKeyName(2, "DOCL.png");
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.wmp_main);
            this.tabPage13.ImageIndex = 7;
            this.tabPage13.Location = new System.Drawing.Point(4, 25);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Size = new System.Drawing.Size(569, 499);
            this.tabPage13.TabIndex = 8;
            this.tabPage13.Text = "Media";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // wmp_main
            // 
            this.wmp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wmp_main.Enabled = true;
            this.wmp_main.Location = new System.Drawing.Point(0, 0);
            this.wmp_main.Name = "wmp_main";
            this.wmp_main.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmp_main.OcxState")));
            this.wmp_main.Size = new System.Drawing.Size(569, 499);
            this.wmp_main.TabIndex = 0;
            // 
            // tbCalc
            // 
            this.tbCalc.Controls.Add(this.wbCalc);
            this.tbCalc.ImageIndex = 8;
            this.tbCalc.Location = new System.Drawing.Point(4, 25);
            this.tbCalc.Name = "tbCalc";
            this.tbCalc.Size = new System.Drawing.Size(569, 499);
            this.tbCalc.TabIndex = 9;
            this.tbCalc.Text = "Calculator";
            this.tbCalc.UseVisualStyleBackColor = true;
            // 
            // wbCalc
            // 
            this.wbCalc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbCalc.Location = new System.Drawing.Point(0, 0);
            this.wbCalc.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbCalc.Name = "wbCalc";
            this.wbCalc.ScrollBarsEnabled = false;
            this.wbCalc.Size = new System.Drawing.Size(569, 499);
            this.wbCalc.TabIndex = 0;
            this.wbCalc.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.wbCalc.NewWindow += new System.ComponentModel.CancelEventHandler(this.Browser1_NewWindow);
            // 
            // maintabimages
            // 
            this.maintabimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("maintabimages.ImageStream")));
            this.maintabimages.TransparentColor = System.Drawing.Color.Transparent;
            this.maintabimages.Images.SetKeyName(0, "websiteIcon.jpg");
            this.maintabimages.Images.SetKeyName(1, "screenshot-icon_64.png");
            this.maintabimages.Images.SetKeyName(2, "video_icon_full-64x64.jpg");
            this.maintabimages.Images.SetKeyName(3, "icon.40x40.manga.gif");
            this.maintabimages.Images.SetKeyName(4, "Flash-icon-1-1.png");
            this.maintabimages.Images.SetKeyName(5, "g_btn_embed.png");
            this.maintabimages.Images.SetKeyName(6, "Directory.png");
            this.maintabimages.Images.SetKeyName(7, "windows_media_player_50x50.gif");
            this.maintabimages.Images.SetKeyName(8, "calc.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Controls.Add(this.tabPage17);
            this.tabControl1.Controls.Add(this.tabPage18);
            this.tabControl1.Location = new System.Drawing.Point(-1, 535);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(578, 134);
            this.tabControl1.TabIndex = 44;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(570, 108);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Links";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl5
            // 
            this.tabControl5.Controls.Add(this.tabPage14);
            this.tabControl5.Controls.Add(this.tabPage15);
            this.tabControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl5.Location = new System.Drawing.Point(3, 3);
            this.tabControl5.Name = "tabControl5";
            this.tabControl5.SelectedIndex = 0;
            this.tabControl5.Size = new System.Drawing.Size(564, 102);
            this.tabControl5.TabIndex = 11;
            // 
            // tabPage14
            // 
            this.tabPage14.BackgroundImage = global::RS_Client.Properties.Resources.backgrounduntil;
            this.tabPage14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage14.Controls.Add(this.btn_refresh);
            this.tabPage14.Controls.Add(this.chk_lucky);
            this.tabPage14.Controls.Add(this.btn_close_web);
            this.tabPage14.Controls.Add(this.btn_add);
            this.tabPage14.Controls.Add(this.btn_clearurl);
            this.tabPage14.Controls.Add(this.btn_forward);
            this.tabPage14.Controls.Add(this.btn_back);
            this.tabPage14.Controls.Add(this.btn_link2);
            this.tabPage14.Controls.Add(this.btn_link1);
            this.tabPage14.Controls.Add(this.btn_link6);
            this.tabPage14.Controls.Add(this.btn_link4);
            this.tabPage14.Controls.Add(this.btn_link5);
            this.tabPage14.Controls.Add(this.btn_link3);
            this.tabPage14.Location = new System.Drawing.Point(4, 22);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage14.Size = new System.Drawing.Size(556, 76);
            this.tabPage14.TabIndex = 0;
            this.tabPage14.Text = "Links";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_refresh.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_refresh.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_refresh.Image = global::RS_Client.Properties.Resources.refresh;
            this.btn_refresh.Location = new System.Drawing.Point(340, 4);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(73, 71);
            this.btn_refresh.TabIndex = 29;
            this.btn_refresh.UseVisualStyleBackColor = false;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // chk_lucky
            // 
            this.chk_lucky.AutoSize = true;
            this.chk_lucky.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.chk_lucky.Location = new System.Drawing.Point(8, 59);
            this.chk_lucky.Name = "chk_lucky";
            this.chk_lucky.Size = new System.Drawing.Size(108, 17);
            this.chk_lucky.TabIndex = 28;
            this.chk_lucky.Text = "I\'m Feeling Lucky";
            this.chk_lucky.UseVisualStyleBackColor = true;
            this.chk_lucky.Click += new System.EventHandler(this.clearHistoryToolStripMenuItem_Click);
            // 
            // btn_close_web
            // 
            this.btn_close_web.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_close_web.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_close_web.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_close_web.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_web.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close_web.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_close_web.Location = new System.Drawing.Point(416, 44);
            this.btn_close_web.Name = "btn_close_web";
            this.btn_close_web.Size = new System.Drawing.Size(134, 31);
            this.btn_close_web.TabIndex = 27;
            this.btn_close_web.Text = "Close";
            this.btn_close_web.UseVisualStyleBackColor = false;
            this.btn_close_web.Click += new System.EventHandler(this.btn_close_web_Click);
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_add.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_add.Location = new System.Drawing.Point(244, 43);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(93, 31);
            this.btn_add.TabIndex = 26;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_clearurl
            // 
            this.btn_clearurl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_clearurl.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_clearurl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_clearurl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clearurl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clearurl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_clearurl.Location = new System.Drawing.Point(244, 4);
            this.btn_clearurl.Name = "btn_clearurl";
            this.btn_clearurl.Size = new System.Drawing.Size(93, 37);
            this.btn_clearurl.TabIndex = 23;
            this.btn_clearurl.Text = "Clear URL";
            this.btn_clearurl.UseVisualStyleBackColor = false;
            this.btn_clearurl.Click += new System.EventHandler(this.btn_clearurl_Click);
            // 
            // btn_forward
            // 
            this.btn_forward.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_forward.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_forward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_forward.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_forward.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_forward.Image = global::RS_Client.Properties.Resources.forward_dull;
            this.btn_forward.Location = new System.Drawing.Point(486, 4);
            this.btn_forward.Name = "btn_forward";
            this.btn_forward.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_forward.Size = new System.Drawing.Size(64, 37);
            this.btn_forward.TabIndex = 25;
            this.btn_forward.UseVisualStyleBackColor = false;
            this.btn_forward.Click += new System.EventHandler(this.btn_forward_Click);
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_back.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_back.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_back.Image = global::RS_Client.Properties.Resources.backward;
            this.btn_back.Location = new System.Drawing.Point(416, 4);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(64, 37);
            this.btn_back.TabIndex = 24;
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_link2
            // 
            this.btn_link2.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_link2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_link2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_link2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_link2.Location = new System.Drawing.Point(87, 4);
            this.btn_link2.Name = "btn_link2";
            this.btn_link2.Size = new System.Drawing.Size(73, 23);
            this.btn_link2.TabIndex = 22;
            this.btn_link2.Text = "Google";
            this.btn_link2.UseVisualStyleBackColor = true;
            this.btn_link2.Click += new System.EventHandler(this.btn_runehq_Click);
            // 
            // btn_link1
            // 
            this.btn_link1.BackColor = System.Drawing.Color.IndianRed;
            this.btn_link1.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_link1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_link1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_link1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_link1.Location = new System.Drawing.Point(8, 4);
            this.btn_link1.Name = "btn_link1";
            this.btn_link1.Size = new System.Drawing.Size(73, 23);
            this.btn_link1.TabIndex = 17;
            this.btn_link1.Text = "Google";
            this.btn_link1.UseVisualStyleBackColor = false;
            this.btn_link1.Click += new System.EventHandler(this.btn_animecrazy_Click);
            // 
            // btn_link6
            // 
            this.btn_link6.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_link6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_link6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_link6.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_link6.Location = new System.Drawing.Point(166, 33);
            this.btn_link6.Name = "btn_link6";
            this.btn_link6.Size = new System.Drawing.Size(73, 23);
            this.btn_link6.TabIndex = 20;
            this.btn_link6.Text = "Google";
            this.btn_link6.UseVisualStyleBackColor = true;
            this.btn_link6.Click += new System.EventHandler(this.btn_runescape_Click);
            // 
            // btn_link4
            // 
            this.btn_link4.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_link4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_link4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_link4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_link4.Location = new System.Drawing.Point(8, 33);
            this.btn_link4.Name = "btn_link4";
            this.btn_link4.Size = new System.Drawing.Size(73, 23);
            this.btn_link4.TabIndex = 21;
            this.btn_link4.Text = "Google";
            this.btn_link4.UseVisualStyleBackColor = true;
            this.btn_link4.Click += new System.EventHandler(this.btn_megavideo_Click);
            // 
            // btn_link5
            // 
            this.btn_link5.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_link5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_link5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_link5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_link5.Location = new System.Drawing.Point(87, 33);
            this.btn_link5.Name = "btn_link5";
            this.btn_link5.Size = new System.Drawing.Size(73, 23);
            this.btn_link5.TabIndex = 19;
            this.btn_link5.Text = "Google";
            this.btn_link5.UseVisualStyleBackColor = true;
            this.btn_link5.Click += new System.EventHandler(this.btn_google_Click);
            // 
            // btn_link3
            // 
            this.btn_link3.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_link3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_link3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_link3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_link3.Location = new System.Drawing.Point(166, 4);
            this.btn_link3.Name = "btn_link3";
            this.btn_link3.Size = new System.Drawing.Size(73, 23);
            this.btn_link3.TabIndex = 18;
            this.btn_link3.Text = "Google";
            this.btn_link3.UseVisualStyleBackColor = true;
            this.btn_link3.Click += new System.EventHandler(this.btn_youtube_Click);
            // 
            // tabPage15
            // 
            this.tabPage15.BackgroundImage = global::RS_Client.Properties.Resources.backgrounduntil;
            this.tabPage15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage15.Controls.Add(this.btn_setlink);
            this.tabPage15.Controls.Add(this.txt_linkurl);
            this.tabPage15.Controls.Add(this.label32);
            this.tabPage15.Controls.Add(this.txt_linkname);
            this.tabPage15.Controls.Add(this.label31);
            this.tabPage15.Controls.Add(this.grp_linknum);
            this.tabPage15.Location = new System.Drawing.Point(4, 22);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(556, 76);
            this.tabPage15.TabIndex = 1;
            this.tabPage15.Text = "Edit";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // btn_setlink
            // 
            this.btn_setlink.BackgroundImage = global::RS_Client.Properties.Resources.btnset;
            this.btn_setlink.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_setlink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_setlink.ForeColor = System.Drawing.Color.LightCoral;
            this.btn_setlink.Location = new System.Drawing.Point(366, 32);
            this.btn_setlink.Name = "btn_setlink";
            this.btn_setlink.Size = new System.Drawing.Size(71, 20);
            this.btn_setlink.TabIndex = 5;
            this.btn_setlink.UseVisualStyleBackColor = true;
            this.btn_setlink.Click += new System.EventHandler(this.btn_setlink_Click);
            // 
            // txt_linkurl
            // 
            this.txt_linkurl.BackColor = System.Drawing.Color.Silver;
            this.txt_linkurl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_linkurl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_linkurl.Location = new System.Drawing.Point(366, 6);
            this.txt_linkurl.Name = "txt_linkurl";
            this.txt_linkurl.Size = new System.Drawing.Size(187, 14);
            this.txt_linkurl.TabIndex = 4;
            this.txt_linkurl.Text = "http://";
            this.txt_linkurl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_linkurl_KeyPress);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label32.Location = new System.Drawing.Point(309, 9);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(51, 13);
            this.label32.TabIndex = 3;
            this.label32.Text = "Full URL:";
            // 
            // txt_linkname
            // 
            this.txt_linkname.BackColor = System.Drawing.Color.Silver;
            this.txt_linkname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_linkname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_linkname.Location = new System.Drawing.Point(190, 8);
            this.txt_linkname.Name = "txt_linkname";
            this.txt_linkname.Size = new System.Drawing.Size(100, 13);
            this.txt_linkname.TabIndex = 2;
            this.txt_linkname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_linkurl_KeyPress);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label31.Location = new System.Drawing.Point(146, 9);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(38, 13);
            this.label31.TabIndex = 1;
            this.label31.Text = "Name:";
            // 
            // grp_linknum
            // 
            this.grp_linknum.Controls.Add(this.rad_6);
            this.grp_linknum.Controls.Add(this.rad_5);
            this.grp_linknum.Controls.Add(this.rad_4);
            this.grp_linknum.Controls.Add(this.rad_3);
            this.grp_linknum.Controls.Add(this.rad_2);
            this.grp_linknum.Controls.Add(this.rad_1);
            this.grp_linknum.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.grp_linknum.Location = new System.Drawing.Point(6, 6);
            this.grp_linknum.Name = "grp_linknum";
            this.grp_linknum.Size = new System.Drawing.Size(130, 69);
            this.grp_linknum.TabIndex = 0;
            this.grp_linknum.TabStop = false;
            this.grp_linknum.Text = "Number";
            // 
            // rad_6
            // 
            this.rad_6.AutoSize = true;
            this.rad_6.Location = new System.Drawing.Point(80, 42);
            this.rad_6.Name = "rad_6";
            this.rad_6.Size = new System.Drawing.Size(31, 17);
            this.rad_6.TabIndex = 5;
            this.rad_6.TabStop = true;
            this.rad_6.Text = "6";
            this.rad_6.UseVisualStyleBackColor = true;
            // 
            // rad_5
            // 
            this.rad_5.AutoSize = true;
            this.rad_5.Location = new System.Drawing.Point(43, 42);
            this.rad_5.Name = "rad_5";
            this.rad_5.Size = new System.Drawing.Size(31, 17);
            this.rad_5.TabIndex = 4;
            this.rad_5.TabStop = true;
            this.rad_5.Text = "5";
            this.rad_5.UseVisualStyleBackColor = true;
            // 
            // rad_4
            // 
            this.rad_4.AutoSize = true;
            this.rad_4.Location = new System.Drawing.Point(6, 42);
            this.rad_4.Name = "rad_4";
            this.rad_4.Size = new System.Drawing.Size(31, 17);
            this.rad_4.TabIndex = 3;
            this.rad_4.TabStop = true;
            this.rad_4.Text = "4";
            this.rad_4.UseVisualStyleBackColor = true;
            // 
            // rad_3
            // 
            this.rad_3.AutoSize = true;
            this.rad_3.Location = new System.Drawing.Point(80, 19);
            this.rad_3.Name = "rad_3";
            this.rad_3.Size = new System.Drawing.Size(31, 17);
            this.rad_3.TabIndex = 2;
            this.rad_3.TabStop = true;
            this.rad_3.Text = "3";
            this.rad_3.UseVisualStyleBackColor = true;
            // 
            // rad_2
            // 
            this.rad_2.AutoSize = true;
            this.rad_2.Location = new System.Drawing.Point(43, 19);
            this.rad_2.Name = "rad_2";
            this.rad_2.Size = new System.Drawing.Size(31, 17);
            this.rad_2.TabIndex = 1;
            this.rad_2.TabStop = true;
            this.rad_2.Text = "2";
            this.rad_2.UseVisualStyleBackColor = true;
            // 
            // rad_1
            // 
            this.rad_1.AutoSize = true;
            this.rad_1.Location = new System.Drawing.Point(6, 19);
            this.rad_1.Name = "rad_1";
            this.rad_1.Size = new System.Drawing.Size(31, 17);
            this.rad_1.TabIndex = 0;
            this.rad_1.TabStop = true;
            this.rad_1.Text = "1";
            this.rad_1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImage = global::RS_Client.Properties.Resources.backgrounduntil;
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Controls.Add(this.btn_highscoressearch);
            this.tabPage2.Controls.Add(this.xptolvlbar);
            this.tabPage2.Controls.Add(this.lbl_xp);
            this.tabPage2.Controls.Add(this.lbl_rank);
            this.tabPage2.Controls.Add(this.lbl_skillname);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txt_user);
            this.tabPage2.Controls.Add(this.lbl_craftlvl);
            this.tabPage2.Controls.Add(this.lbl_dunglvl);
            this.tabPage2.Controls.Add(this.lbl_summlvl);
            this.tabPage2.Controls.Add(this.lbl_conlvl);
            this.tabPage2.Controls.Add(this.lbl_hunterlvl);
            this.tabPage2.Controls.Add(this.lbl_rclvl);
            this.tabPage2.Controls.Add(this.lbl_farminglvl);
            this.tabPage2.Controls.Add(this.lbl_slayerlvl);
            this.tabPage2.Controls.Add(this.lbl_thieflvl);
            this.tabPage2.Controls.Add(this.lbl_agilitylvl);
            this.tabPage2.Controls.Add(this.lbl_herblvl);
            this.tabPage2.Controls.Add(this.lbl_mininglvl);
            this.tabPage2.Controls.Add(this.lbl_smithlvl);
            this.tabPage2.Controls.Add(this.lbl_fmlvl);
            this.tabPage2.Controls.Add(this.lbl_fishinglvl);
            this.tabPage2.Controls.Add(this.lbl_cooklvl);
            this.tabPage2.Controls.Add(this.lbl_wclvl);
            this.tabPage2.Controls.Add(this.lbl_fletchinglvl);
            this.tabPage2.Controls.Add(this.lbl_magiclvl);
            this.tabPage2.Controls.Add(this.lbl_prayerlvl);
            this.tabPage2.Controls.Add(this.lbl_rangelvl);
            this.tabPage2.Controls.Add(this.lbl_hplvl);
            this.tabPage2.Controls.Add(this.lbl_strengthlvl);
            this.tabPage2.Controls.Add(this.lbl_defencelvl);
            this.tabPage2.Controls.Add(this.lbl_attacklvl);
            this.tabPage2.Controls.Add(this.lbl_overall);
            this.tabPage2.ImageIndex = 0;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(570, 108);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Highscores";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_highscoressearch
            // 
            this.btn_highscoressearch.BackColor = System.Drawing.Color.IndianRed;
            this.btn_highscoressearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_highscoressearch.ForeColor = System.Drawing.Color.LightCoral;
            this.btn_highscoressearch.Image = global::RS_Client.Properties.Resources.searchbtn;
            this.btn_highscoressearch.Location = new System.Drawing.Point(217, 5);
            this.btn_highscoressearch.Name = "btn_highscoressearch";
            this.btn_highscoressearch.Size = new System.Drawing.Size(70, 21);
            this.btn_highscoressearch.TabIndex = 56;
            this.btn_highscoressearch.UseVisualStyleBackColor = false;
            this.btn_highscoressearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // xptolvlbar
            // 
            this.xptolvlbar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.xptolvlbar.ForeColor = System.Drawing.Color.IndianRed;
            this.xptolvlbar.Location = new System.Drawing.Point(452, 70);
            this.xptolvlbar.Name = "xptolvlbar";
            this.xptolvlbar.Size = new System.Drawing.Size(112, 23);
            this.xptolvlbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.xptolvlbar.TabIndex = 55;
            // 
            // lbl_xp
            // 
            this.lbl_xp.AutoSize = true;
            this.lbl_xp.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_xp.Location = new System.Drawing.Point(448, 53);
            this.lbl_xp.Name = "lbl_xp";
            this.lbl_xp.Size = new System.Drawing.Size(24, 13);
            this.lbl_xp.TabIndex = 8;
            this.lbl_xp.Text = "XP:";
            // 
            // lbl_rank
            // 
            this.lbl_rank.AutoSize = true;
            this.lbl_rank.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_rank.Location = new System.Drawing.Point(448, 31);
            this.lbl_rank.Name = "lbl_rank";
            this.lbl_rank.Size = new System.Drawing.Size(36, 13);
            this.lbl_rank.TabIndex = 7;
            this.lbl_rank.Text = "Rank:";
            // 
            // lbl_skillname
            // 
            this.lbl_skillname.AutoSize = true;
            this.lbl_skillname.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_skillname.Location = new System.Drawing.Point(448, 5);
            this.lbl_skillname.Name = "lbl_skillname";
            this.lbl_skillname.Size = new System.Drawing.Size(29, 13);
            this.lbl_skillname.TabIndex = 6;
            this.lbl_skillname.Text = "Skill:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username:";
            // 
            // txt_user
            // 
            this.txt_user.BackColor = System.Drawing.Color.Silver;
            this.txt_user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_user.ForeColor = System.Drawing.Color.Black;
            this.txt_user.Location = new System.Drawing.Point(70, 5);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(141, 19);
            this.txt_user.TabIndex = 3;
            this.txt_user.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_user.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_user_KeyPress);
            this.txt_user.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_user_MouseDoubleClick);
            // 
            // lbl_craftlvl
            // 
            this.lbl_craftlvl.AutoSize = true;
            this.lbl_craftlvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_craftlvl.Image = global::RS_Client.Properties.Resources.crafting;
            this.lbl_craftlvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_craftlvl.Location = new System.Drawing.Point(239, 77);
            this.lbl_craftlvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_craftlvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_craftlvl.Name = "lbl_craftlvl";
            this.lbl_craftlvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_craftlvl.TabIndex = 54;
            this.lbl_craftlvl.Text = "      :";
            this.lbl_craftlvl.MouseEnter += new System.EventHandler(this.lbl_craftlvl_MouseEnter);
            // 
            // lbl_dunglvl
            // 
            this.lbl_dunglvl.AutoSize = true;
            this.lbl_dunglvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_dunglvl.Image = global::RS_Client.Properties.Resources.dung;
            this.lbl_dunglvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_dunglvl.Location = new System.Drawing.Point(405, 74);
            this.lbl_dunglvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_dunglvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_dunglvl.Name = "lbl_dunglvl";
            this.lbl_dunglvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_dunglvl.TabIndex = 53;
            this.lbl_dunglvl.Text = "      :";
            this.lbl_dunglvl.MouseEnter += new System.EventHandler(this.lbl_dunglvl_MouseEnter);
            // 
            // lbl_summlvl
            // 
            this.lbl_summlvl.AutoSize = true;
            this.lbl_summlvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_summlvl.Image = global::RS_Client.Properties.Resources.summoning;
            this.lbl_summlvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_summlvl.Location = new System.Drawing.Point(405, 53);
            this.lbl_summlvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_summlvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_summlvl.Name = "lbl_summlvl";
            this.lbl_summlvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_summlvl.TabIndex = 48;
            this.lbl_summlvl.Text = "      :";
            this.lbl_summlvl.MouseEnter += new System.EventHandler(this.lbl_summlvl_MouseEnter);
            // 
            // lbl_conlvl
            // 
            this.lbl_conlvl.AutoSize = true;
            this.lbl_conlvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_conlvl.Image = global::RS_Client.Properties.Resources.construction;
            this.lbl_conlvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_conlvl.Location = new System.Drawing.Point(405, 31);
            this.lbl_conlvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_conlvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_conlvl.Name = "lbl_conlvl";
            this.lbl_conlvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_conlvl.TabIndex = 47;
            this.lbl_conlvl.Text = "      :";
            this.lbl_conlvl.MouseEnter += new System.EventHandler(this.lbl_conlvl_MouseEnter);
            // 
            // lbl_hunterlvl
            // 
            this.lbl_hunterlvl.AutoSize = true;
            this.lbl_hunterlvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_hunterlvl.Image = global::RS_Client.Properties.Resources.hunter;
            this.lbl_hunterlvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_hunterlvl.Location = new System.Drawing.Point(405, 8);
            this.lbl_hunterlvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_hunterlvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_hunterlvl.Name = "lbl_hunterlvl";
            this.lbl_hunterlvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_hunterlvl.TabIndex = 46;
            this.lbl_hunterlvl.Text = "      :";
            this.lbl_hunterlvl.MouseEnter += new System.EventHandler(this.lbl_hunterlvl_MouseEnter);
            // 
            // lbl_rclvl
            // 
            this.lbl_rclvl.AutoSize = true;
            this.lbl_rclvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_rclvl.Image = global::RS_Client.Properties.Resources.runecrafting;
            this.lbl_rclvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_rclvl.Location = new System.Drawing.Point(351, 74);
            this.lbl_rclvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_rclvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_rclvl.Name = "lbl_rclvl";
            this.lbl_rclvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_rclvl.TabIndex = 45;
            this.lbl_rclvl.Text = "      :";
            this.lbl_rclvl.MouseEnter += new System.EventHandler(this.lbl_rclvl_MouseEnter);
            // 
            // lbl_farminglvl
            // 
            this.lbl_farminglvl.AutoSize = true;
            this.lbl_farminglvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_farminglvl.Image = global::RS_Client.Properties.Resources.farming;
            this.lbl_farminglvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_farminglvl.Location = new System.Drawing.Point(351, 53);
            this.lbl_farminglvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_farminglvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_farminglvl.Name = "lbl_farminglvl";
            this.lbl_farminglvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_farminglvl.TabIndex = 43;
            this.lbl_farminglvl.Text = "      :";
            this.lbl_farminglvl.MouseEnter += new System.EventHandler(this.lbl_farminglvl_MouseEnter);
            // 
            // lbl_slayerlvl
            // 
            this.lbl_slayerlvl.AutoSize = true;
            this.lbl_slayerlvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_slayerlvl.Image = global::RS_Client.Properties.Resources.slayer;
            this.lbl_slayerlvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_slayerlvl.Location = new System.Drawing.Point(351, 31);
            this.lbl_slayerlvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_slayerlvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_slayerlvl.Name = "lbl_slayerlvl";
            this.lbl_slayerlvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_slayerlvl.TabIndex = 41;
            this.lbl_slayerlvl.Text = "      :";
            this.lbl_slayerlvl.MouseEnter += new System.EventHandler(this.lbl_slayerlvl_MouseEnter);
            // 
            // lbl_thieflvl
            // 
            this.lbl_thieflvl.AutoSize = true;
            this.lbl_thieflvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_thieflvl.Image = global::RS_Client.Properties.Resources.thieving;
            this.lbl_thieflvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_thieflvl.Location = new System.Drawing.Point(351, 8);
            this.lbl_thieflvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_thieflvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_thieflvl.Name = "lbl_thieflvl";
            this.lbl_thieflvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_thieflvl.TabIndex = 40;
            this.lbl_thieflvl.Text = "      :";
            this.lbl_thieflvl.MouseEnter += new System.EventHandler(this.lbl_thieflvl_MouseEnter);
            // 
            // lbl_agilitylvl
            // 
            this.lbl_agilitylvl.AutoSize = true;
            this.lbl_agilitylvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_agilitylvl.Image = global::RS_Client.Properties.Resources.agility;
            this.lbl_agilitylvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_agilitylvl.Location = new System.Drawing.Point(298, 77);
            this.lbl_agilitylvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_agilitylvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_agilitylvl.Name = "lbl_agilitylvl";
            this.lbl_agilitylvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_agilitylvl.TabIndex = 36;
            this.lbl_agilitylvl.Text = "      :";
            this.lbl_agilitylvl.MouseEnter += new System.EventHandler(this.lbl_agilitylvl_MouseEnter);
            // 
            // lbl_herblvl
            // 
            this.lbl_herblvl.AutoSize = true;
            this.lbl_herblvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_herblvl.Image = global::RS_Client.Properties.Resources.herb;
            this.lbl_herblvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_herblvl.Location = new System.Drawing.Point(298, 53);
            this.lbl_herblvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_herblvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_herblvl.Name = "lbl_herblvl";
            this.lbl_herblvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_herblvl.TabIndex = 35;
            this.lbl_herblvl.Text = "      :";
            this.lbl_herblvl.MouseEnter += new System.EventHandler(this.lbl_herblvl_MouseEnter);
            // 
            // lbl_mininglvl
            // 
            this.lbl_mininglvl.AutoSize = true;
            this.lbl_mininglvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_mininglvl.Image = global::RS_Client.Properties.Resources.mining;
            this.lbl_mininglvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_mininglvl.Location = new System.Drawing.Point(298, 31);
            this.lbl_mininglvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_mininglvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_mininglvl.Name = "lbl_mininglvl";
            this.lbl_mininglvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_mininglvl.TabIndex = 34;
            this.lbl_mininglvl.Text = "      :";
            this.lbl_mininglvl.MouseEnter += new System.EventHandler(this.lbl_mininglvl_MouseEnter);
            // 
            // lbl_smithlvl
            // 
            this.lbl_smithlvl.AutoSize = true;
            this.lbl_smithlvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_smithlvl.Image = global::RS_Client.Properties.Resources.smithing;
            this.lbl_smithlvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_smithlvl.Location = new System.Drawing.Point(298, 8);
            this.lbl_smithlvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_smithlvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_smithlvl.Name = "lbl_smithlvl";
            this.lbl_smithlvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_smithlvl.TabIndex = 30;
            this.lbl_smithlvl.Text = "      :";
            this.lbl_smithlvl.MouseEnter += new System.EventHandler(this.lbl_smithlvl_MouseEnter);
            // 
            // lbl_fmlvl
            // 
            this.lbl_fmlvl.AutoSize = true;
            this.lbl_fmlvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_fmlvl.Image = global::RS_Client.Properties.Resources.firemaking;
            this.lbl_fmlvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_fmlvl.Location = new System.Drawing.Point(239, 53);
            this.lbl_fmlvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_fmlvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_fmlvl.Name = "lbl_fmlvl";
            this.lbl_fmlvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_fmlvl.TabIndex = 29;
            this.lbl_fmlvl.Text = "      :";
            this.lbl_fmlvl.MouseEnter += new System.EventHandler(this.lbl_fmlvl_MouseEnter);
            // 
            // lbl_fishinglvl
            // 
            this.lbl_fishinglvl.AutoSize = true;
            this.lbl_fishinglvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_fishinglvl.Image = global::RS_Client.Properties.Resources.fishing;
            this.lbl_fishinglvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_fishinglvl.Location = new System.Drawing.Point(239, 31);
            this.lbl_fishinglvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_fishinglvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_fishinglvl.Name = "lbl_fishinglvl";
            this.lbl_fishinglvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_fishinglvl.TabIndex = 27;
            this.lbl_fishinglvl.Text = "      :";
            this.lbl_fishinglvl.MouseEnter += new System.EventHandler(this.lbl_fishinglvl_MouseEnter);
            // 
            // lbl_cooklvl
            // 
            this.lbl_cooklvl.AutoSize = true;
            this.lbl_cooklvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_cooklvl.Image = global::RS_Client.Properties.Resources.cooking;
            this.lbl_cooklvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_cooklvl.Location = new System.Drawing.Point(183, 31);
            this.lbl_cooklvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_cooklvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_cooklvl.Name = "lbl_cooklvl";
            this.lbl_cooklvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_cooklvl.TabIndex = 24;
            this.lbl_cooklvl.Text = "      :";
            this.lbl_cooklvl.MouseEnter += new System.EventHandler(this.lbl_cooklvl_MouseHover);
            // 
            // lbl_wclvl
            // 
            this.lbl_wclvl.AutoSize = true;
            this.lbl_wclvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_wclvl.Image = global::RS_Client.Properties.Resources.woodcutting;
            this.lbl_wclvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_wclvl.Location = new System.Drawing.Point(183, 53);
            this.lbl_wclvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_wclvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_wclvl.Name = "lbl_wclvl";
            this.lbl_wclvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_wclvl.TabIndex = 23;
            this.lbl_wclvl.Text = "      :";
            this.lbl_wclvl.MouseEnter += new System.EventHandler(this.lbl_wclvl_MouseHover);
            // 
            // lbl_fletchinglvl
            // 
            this.lbl_fletchinglvl.AutoSize = true;
            this.lbl_fletchinglvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_fletchinglvl.Image = global::RS_Client.Properties.Resources.fletch;
            this.lbl_fletchinglvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_fletchinglvl.Location = new System.Drawing.Point(183, 77);
            this.lbl_fletchinglvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_fletchinglvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_fletchinglvl.Name = "lbl_fletchinglvl";
            this.lbl_fletchinglvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_fletchinglvl.TabIndex = 20;
            this.lbl_fletchinglvl.Text = "      :";
            this.lbl_fletchinglvl.MouseEnter += new System.EventHandler(this.lbl_fletchinglvl_MouseEnter);
            // 
            // lbl_magiclvl
            // 
            this.lbl_magiclvl.AutoSize = true;
            this.lbl_magiclvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_magiclvl.Image = global::RS_Client.Properties.Resources.magic;
            this.lbl_magiclvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_magiclvl.Location = new System.Drawing.Point(127, 77);
            this.lbl_magiclvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_magiclvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_magiclvl.Name = "lbl_magiclvl";
            this.lbl_magiclvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_magiclvl.TabIndex = 19;
            this.lbl_magiclvl.Text = "      :";
            this.lbl_magiclvl.MouseEnter += new System.EventHandler(this.lbl_magiclvl_MouseHover);
            // 
            // lbl_prayerlvl
            // 
            this.lbl_prayerlvl.AutoSize = true;
            this.lbl_prayerlvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_prayerlvl.Image = global::RS_Client.Properties.Resources.prayer;
            this.lbl_prayerlvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_prayerlvl.Location = new System.Drawing.Point(127, 53);
            this.lbl_prayerlvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_prayerlvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_prayerlvl.Name = "lbl_prayerlvl";
            this.lbl_prayerlvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_prayerlvl.TabIndex = 16;
            this.lbl_prayerlvl.Text = "      :";
            this.lbl_prayerlvl.MouseEnter += new System.EventHandler(this.lbl_prayerlvl_MouseHover);
            // 
            // lbl_rangelvl
            // 
            this.lbl_rangelvl.AutoSize = true;
            this.lbl_rangelvl.BackColor = System.Drawing.Color.Transparent;
            this.lbl_rangelvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_rangelvl.Image = global::RS_Client.Properties.Resources.range;
            this.lbl_rangelvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_rangelvl.Location = new System.Drawing.Point(127, 31);
            this.lbl_rangelvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_rangelvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_rangelvl.Name = "lbl_rangelvl";
            this.lbl_rangelvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_rangelvl.TabIndex = 15;
            this.lbl_rangelvl.Text = "      :";
            this.lbl_rangelvl.MouseEnter += new System.EventHandler(this.lbl_rangelvl_MouseHover);
            // 
            // lbl_hplvl
            // 
            this.lbl_hplvl.AutoSize = true;
            this.lbl_hplvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_hplvl.Image = global::RS_Client.Properties.Resources.hp;
            this.lbl_hplvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_hplvl.Location = new System.Drawing.Point(67, 77);
            this.lbl_hplvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_hplvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_hplvl.Name = "lbl_hplvl";
            this.lbl_hplvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_hplvl.TabIndex = 12;
            this.lbl_hplvl.Text = "      :";
            this.lbl_hplvl.MouseEnter += new System.EventHandler(this.lbl_hplvl_MouseHover);
            // 
            // lbl_strengthlvl
            // 
            this.lbl_strengthlvl.AutoSize = true;
            this.lbl_strengthlvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_strengthlvl.Image = global::RS_Client.Properties.Resources.strength;
            this.lbl_strengthlvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_strengthlvl.Location = new System.Drawing.Point(67, 53);
            this.lbl_strengthlvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_strengthlvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_strengthlvl.Name = "lbl_strengthlvl";
            this.lbl_strengthlvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_strengthlvl.TabIndex = 11;
            this.lbl_strengthlvl.Text = "      :";
            this.lbl_strengthlvl.MouseEnter += new System.EventHandler(this.lbl_strengthlvl_MouseEnter);
            // 
            // lbl_defencelvl
            // 
            this.lbl_defencelvl.AutoSize = true;
            this.lbl_defencelvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_defencelvl.Image = global::RS_Client.Properties.Resources.defence;
            this.lbl_defencelvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_defencelvl.Location = new System.Drawing.Point(6, 74);
            this.lbl_defencelvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_defencelvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_defencelvl.Name = "lbl_defencelvl";
            this.lbl_defencelvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_defencelvl.TabIndex = 10;
            this.lbl_defencelvl.Text = "      :";
            this.lbl_defencelvl.MouseEnter += new System.EventHandler(this.lbl_defencelvl_MouseEnter);
            // 
            // lbl_attacklvl
            // 
            this.lbl_attacklvl.AutoSize = true;
            this.lbl_attacklvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_attacklvl.Image = global::RS_Client.Properties.Resources.attack;
            this.lbl_attacklvl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_attacklvl.Location = new System.Drawing.Point(7, 53);
            this.lbl_attacklvl.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_attacklvl.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_attacklvl.Name = "lbl_attacklvl";
            this.lbl_attacklvl.Size = new System.Drawing.Size(28, 16);
            this.lbl_attacklvl.TabIndex = 9;
            this.lbl_attacklvl.Text = "      :";
            this.lbl_attacklvl.MouseEnter += new System.EventHandler(this.lbl_attacklvl_MouseEnter);
            // 
            // lbl_overall
            // 
            this.lbl_overall.AutoSize = true;
            this.lbl_overall.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_overall.Image = global::RS_Client.Properties.Resources.overall;
            this.lbl_overall.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_overall.Location = new System.Drawing.Point(7, 31);
            this.lbl_overall.MaximumSize = new System.Drawing.Size(0, 16);
            this.lbl_overall.MinimumSize = new System.Drawing.Size(0, 16);
            this.lbl_overall.Name = "lbl_overall";
            this.lbl_overall.Size = new System.Drawing.Size(28, 16);
            this.lbl_overall.TabIndex = 5;
            this.lbl_overall.Text = "      :";
            this.lbl_overall.MouseEnter += new System.EventHandler(this.lbl_overall_MouseEnter);
            // 
            // tabPage3
            // 
            this.tabPage3.BackgroundImage = global::RS_Client.Properties.Resources.backgrounduntil;
            this.tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage3.Controls.Add(this.btn_clearembed);
            this.tabPage3.Controls.Add(this.btn_processes);
            this.tabPage3.Controls.Add(this.btn_embed);
            this.tabPage3.Controls.Add(this.lst_exes);
            this.tabPage3.Controls.Add(this.btn_extremove);
            this.tabPage3.Controls.Add(this.grp_extadd);
            this.tabPage3.Controls.Add(this.btn_launchext);
            this.tabPage3.Controls.Add(this.lst_external);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(570, 108);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "External";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btn_clearembed
            // 
            this.btn_clearembed.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_clearembed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_clearembed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clearembed.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_clearembed.Location = new System.Drawing.Point(432, 74);
            this.btn_clearembed.Name = "btn_clearembed";
            this.btn_clearembed.Size = new System.Drawing.Size(67, 33);
            this.btn_clearembed.TabIndex = 16;
            this.btn_clearembed.Text = "Clear";
            this.btn_clearembed.UseVisualStyleBackColor = true;
            this.btn_clearembed.Click += new System.EventHandler(this.btn_clearembed_Click);
            // 
            // btn_processes
            // 
            this.btn_processes.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_processes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_processes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_processes.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_processes.Location = new System.Drawing.Point(360, 74);
            this.btn_processes.Name = "btn_processes";
            this.btn_processes.Size = new System.Drawing.Size(66, 33);
            this.btn_processes.TabIndex = 15;
            this.btn_processes.Text = "Processes";
            this.btn_processes.UseVisualStyleBackColor = true;
            this.btn_processes.Click += new System.EventHandler(this.btn_processes_Click);
            // 
            // btn_embed
            // 
            this.btn_embed.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_embed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_embed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_embed.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_embed.Location = new System.Drawing.Point(505, 74);
            this.btn_embed.Name = "btn_embed";
            this.btn_embed.Size = new System.Drawing.Size(62, 33);
            this.btn_embed.TabIndex = 14;
            this.btn_embed.Text = "Embed";
            this.btn_embed.UseVisualStyleBackColor = true;
            this.btn_embed.Click += new System.EventHandler(this.btn_embed_Click);
            // 
            // lst_exes
            // 
            this.lst_exes.FormattingEnabled = true;
            this.lst_exes.Location = new System.Drawing.Point(342, 3);
            this.lst_exes.Name = "lst_exes";
            this.lst_exes.Size = new System.Drawing.Size(222, 69);
            this.lst_exes.TabIndex = 13;
            // 
            // btn_extremove
            // 
            this.btn_extremove.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_extremove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_extremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_extremove.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_extremove.Location = new System.Drawing.Point(277, 81);
            this.btn_extremove.Name = "btn_extremove";
            this.btn_extremove.Size = new System.Drawing.Size(71, 30);
            this.btn_extremove.TabIndex = 11;
            this.btn_extremove.Text = "Remove";
            this.btn_extremove.UseVisualStyleBackColor = true;
            this.btn_extremove.Click += new System.EventHandler(this.btn_extremove_Click);
            // 
            // grp_extadd
            // 
            this.grp_extadd.Controls.Add(this.label33);
            this.grp_extadd.Controls.Add(this.btn_extfile);
            this.grp_extadd.Controls.Add(this.txt_extname);
            this.grp_extadd.Controls.Add(this.lst_loc);
            this.grp_extadd.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.grp_extadd.Location = new System.Drawing.Point(200, 6);
            this.grp_extadd.Name = "grp_extadd";
            this.grp_extadd.Size = new System.Drawing.Size(136, 72);
            this.grp_extadd.TabIndex = 10;
            this.grp_extadd.TabStop = false;
            this.grp_extadd.Text = "Add External Link";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 23);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(38, 13);
            this.label33.TabIndex = 8;
            this.label33.Text = "Name:";
            // 
            // btn_extfile
            // 
            this.btn_extfile.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_extfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_extfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_extfile.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_extfile.Location = new System.Drawing.Point(9, 46);
            this.btn_extfile.Name = "btn_extfile";
            this.btn_extfile.Size = new System.Drawing.Size(75, 23);
            this.btn_extfile.TabIndex = 7;
            this.btn_extfile.Text = "File Path";
            this.btn_extfile.UseVisualStyleBackColor = true;
            this.btn_extfile.Click += new System.EventHandler(this.btn_extfile_Click);
            // 
            // txt_extname
            // 
            this.txt_extname.BackColor = System.Drawing.Color.Silver;
            this.txt_extname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_extname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_extname.Location = new System.Drawing.Point(50, 23);
            this.txt_extname.Name = "txt_extname";
            this.txt_extname.Size = new System.Drawing.Size(70, 17);
            this.txt_extname.TabIndex = 9;
            this.txt_extname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_extname_KeyPress);
            // 
            // lst_loc
            // 
            this.lst_loc.FormattingEnabled = true;
            this.lst_loc.Location = new System.Drawing.Point(0, -6);
            this.lst_loc.Name = "lst_loc";
            this.lst_loc.Size = new System.Drawing.Size(68, 56);
            this.lst_loc.TabIndex = 12;
            this.lst_loc.Visible = false;
            // 
            // btn_launchext
            // 
            this.btn_launchext.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_launchext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_launchext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_launchext.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_launchext.Location = new System.Drawing.Point(200, 81);
            this.btn_launchext.Name = "btn_launchext";
            this.btn_launchext.Size = new System.Drawing.Size(71, 30);
            this.btn_launchext.TabIndex = 6;
            this.btn_launchext.Text = "Launch";
            this.btn_launchext.UseVisualStyleBackColor = true;
            this.btn_launchext.Click += new System.EventHandler(this.btn_launchext_Click);
            // 
            // lst_external
            // 
            this.lst_external.BackColor = System.Drawing.Color.Silver;
            this.lst_external.FormattingEnabled = true;
            this.lst_external.Location = new System.Drawing.Point(3, 3);
            this.lst_external.Name = "lst_external";
            this.lst_external.Size = new System.Drawing.Size(191, 108);
            this.lst_external.TabIndex = 5;
            this.lst_external.DoubleClick += new System.EventHandler(this.lst_external_DoubleClick);
            this.lst_external.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lst_external_KeyPress);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txt_notes);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(570, 108);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Notes";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txt_notes
            // 
            this.txt_notes.BackColor = System.Drawing.Color.Silver;
            this.txt_notes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_notes.Location = new System.Drawing.Point(3, 3);
            this.txt_notes.Multiline = true;
            this.txt_notes.Name = "txt_notes";
            this.txt_notes.Size = new System.Drawing.Size(564, 102);
            this.txt_notes.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.BackgroundImage = global::RS_Client.Properties.Resources.backgrounduntil;
            this.tabPage5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage5.Controls.Add(this.btn_stop);
            this.tabPage5.Controls.Add(this.btn_reset);
            this.tabPage5.Controls.Add(this.btn_start);
            this.tabPage5.Controls.Add(this.lbl_stopwatch);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(570, 108);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Stopwatch";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btn_stop
            // 
            this.btn_stop.BackgroundImage = global::RS_Client.Properties.Resources.btnstop;
            this.btn_stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_stop.ForeColor = System.Drawing.Color.IndianRed;
            this.btn_stop.Location = new System.Drawing.Point(486, 46);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(79, 44);
            this.btn_stop.TabIndex = 5;
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.BackgroundImage = global::RS_Client.Properties.Resources.btnreset;
            this.btn_reset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_reset.ForeColor = System.Drawing.Color.IndianRed;
            this.btn_reset.Location = new System.Drawing.Point(245, 46);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(79, 44);
            this.btn_reset.TabIndex = 3;
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_start
            // 
            this.btn_start.BackgroundImage = global::RS_Client.Properties.Resources.btnstart;
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start.ForeColor = System.Drawing.Color.IndianRed;
            this.btn_start.Location = new System.Drawing.Point(5, 46);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(79, 44);
            this.btn_start.TabIndex = 1;
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // lbl_stopwatch
            // 
            this.lbl_stopwatch.AutoSize = true;
            this.lbl_stopwatch.BackColor = System.Drawing.Color.Transparent;
            this.lbl_stopwatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_stopwatch.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_stopwatch.Location = new System.Drawing.Point(58, 3);
            this.lbl_stopwatch.MinimumSize = new System.Drawing.Size(450, 40);
            this.lbl_stopwatch.Name = "lbl_stopwatch";
            this.lbl_stopwatch.Size = new System.Drawing.Size(450, 40);
            this.lbl_stopwatch.TabIndex = 0;
            this.lbl_stopwatch.Text = "00:00:00";
            this.lbl_stopwatch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage6
            // 
            this.tabPage6.BackgroundImage = global::RS_Client.Properties.Resources.backgrounduntil;
            this.tabPage6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage6.Controls.Add(this.txt_natprice);
            this.tabPage6.Controls.Add(this.txt_alch);
            this.tabPage6.Controls.Add(this.txt_profit);
            this.tabPage6.Controls.Add(this.txt_pricelookup);
            this.tabPage6.Controls.Add(this.txt_item);
            this.tabPage6.Controls.Add(this.label38);
            this.tabPage6.Controls.Add(this.label4);
            this.tabPage6.Controls.Add(this.label5);
            this.tabPage6.Controls.Add(this.label3);
            this.tabPage6.Controls.Add(this.label2);
            this.tabPage6.Controls.Add(this.btn_search);
            this.tabPage6.Controls.Add(this.pic_priceitem);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(570, 108);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Prices";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txt_natprice
            // 
            this.txt_natprice.BackColor = System.Drawing.Color.Silver;
            this.txt_natprice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_natprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_natprice.Location = new System.Drawing.Point(82, 58);
            this.txt_natprice.Name = "txt_natprice";
            this.txt_natprice.ReadOnly = true;
            this.txt_natprice.Size = new System.Drawing.Size(160, 17);
            this.txt_natprice.TabIndex = 14;
            // 
            // txt_alch
            // 
            this.txt_alch.BackColor = System.Drawing.Color.Silver;
            this.txt_alch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_alch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_alch.Location = new System.Drawing.Point(82, 32);
            this.txt_alch.Name = "txt_alch";
            this.txt_alch.ReadOnly = true;
            this.txt_alch.Size = new System.Drawing.Size(160, 17);
            this.txt_alch.TabIndex = 10;
            // 
            // txt_profit
            // 
            this.txt_profit.BackColor = System.Drawing.Color.Silver;
            this.txt_profit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_profit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_profit.Location = new System.Drawing.Point(82, 87);
            this.txt_profit.Name = "txt_profit";
            this.txt_profit.ReadOnly = true;
            this.txt_profit.Size = new System.Drawing.Size(160, 17);
            this.txt_profit.TabIndex = 8;
            // 
            // txt_pricelookup
            // 
            this.txt_pricelookup.BackColor = System.Drawing.Color.Silver;
            this.txt_pricelookup.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_pricelookup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pricelookup.Location = new System.Drawing.Point(82, 6);
            this.txt_pricelookup.Name = "txt_pricelookup";
            this.txt_pricelookup.ReadOnly = true;
            this.txt_pricelookup.Size = new System.Drawing.Size(160, 17);
            this.txt_pricelookup.TabIndex = 4;
            // 
            // txt_item
            // 
            this.txt_item.BackColor = System.Drawing.Color.Silver;
            this.txt_item.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_item.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_item.Location = new System.Drawing.Point(285, 6);
            this.txt_item.Name = "txt_item";
            this.txt_item.Size = new System.Drawing.Size(120, 17);
            this.txt_item.TabIndex = 2;
            this.txt_item.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_item_KeyPress);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label38.Location = new System.Drawing.Point(6, 61);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(54, 13);
            this.label38.TabIndex = 13;
            this.label38.Text = "Nat Price:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(6, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Alch Price:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Location = new System.Drawing.Point(6, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Alch Profit:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Market Price:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(249, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Item:";
            // 
            // btn_search
            // 
            this.btn_search.BackgroundImage = global::RS_Client.Properties.Resources.btnpricelook;
            this.btn_search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.ForeColor = System.Drawing.Color.IndianRed;
            this.btn_search.Location = new System.Drawing.Point(346, 32);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(58, 43);
            this.btn_search.TabIndex = 0;
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // pic_priceitem
            // 
            this.pic_priceitem.Location = new System.Drawing.Point(252, 32);
            this.pic_priceitem.Name = "pic_priceitem";
            this.pic_priceitem.Size = new System.Drawing.Size(88, 78);
            this.pic_priceitem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_priceitem.TabIndex = 11;
            this.pic_priceitem.TabStop = false;
            // 
            // tabPage7
            // 
            this.tabPage7.BackgroundImage = global::RS_Client.Properties.Resources.backgrounduntil;
            this.tabPage7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage7.Controls.Add(this.btn_mismanage);
            this.tabPage7.Controls.Add(this.btn_prayerdrain);
            this.tabPage7.Controls.Add(this.btn_pestcntl);
            this.tabPage7.Controls.Add(this.btn_rangemax);
            this.tabPage7.Controls.Add(this.btn_meleemax);
            this.tabPage7.Controls.Add(this.btn_equiptbonus);
            this.tabPage7.Controls.Add(this.btn_Energyrestore);
            this.tabPage7.Controls.Add(this.btn_Combatlvl);
            this.tabPage7.Controls.Add(this.btn_battlexp);
            this.tabPage7.Controls.Add(this.btn_Skill);
            this.tabPage7.Controls.Add(this.btn_normcalc);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(570, 108);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Calculators";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btn_mismanage
            // 
            this.btn_mismanage.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_mismanage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_mismanage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_mismanage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_mismanage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_mismanage.Location = new System.Drawing.Point(133, 47);
            this.btn_mismanage.Name = "btn_mismanage";
            this.btn_mismanage.Size = new System.Drawing.Size(99, 60);
            this.btn_mismanage.TabIndex = 17;
            this.btn_mismanage.Text = "Miscellania Manage";
            this.btn_mismanage.UseVisualStyleBackColor = true;
            this.btn_mismanage.Click += new System.EventHandler(this.btn_mismanage_Click);
            // 
            // btn_prayerdrain
            // 
            this.btn_prayerdrain.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_prayerdrain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_prayerdrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_prayerdrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_prayerdrain.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_prayerdrain.Location = new System.Drawing.Point(238, 47);
            this.btn_prayerdrain.Name = "btn_prayerdrain";
            this.btn_prayerdrain.Size = new System.Drawing.Size(99, 60);
            this.btn_prayerdrain.TabIndex = 16;
            this.btn_prayerdrain.Text = "Prayer Drain";
            this.btn_prayerdrain.UseVisualStyleBackColor = true;
            this.btn_prayerdrain.Click += new System.EventHandler(this.btn_prayerdrain_Click);
            // 
            // btn_pestcntl
            // 
            this.btn_pestcntl.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_pestcntl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_pestcntl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pestcntl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pestcntl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_pestcntl.Location = new System.Drawing.Point(343, 47);
            this.btn_pestcntl.Name = "btn_pestcntl";
            this.btn_pestcntl.Size = new System.Drawing.Size(99, 60);
            this.btn_pestcntl.TabIndex = 15;
            this.btn_pestcntl.Text = "Pest Control";
            this.btn_pestcntl.UseVisualStyleBackColor = true;
            this.btn_pestcntl.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // btn_rangemax
            // 
            this.btn_rangemax.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_rangemax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_rangemax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_rangemax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_rangemax.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_rangemax.Location = new System.Drawing.Point(448, 47);
            this.btn_rangemax.Name = "btn_rangemax";
            this.btn_rangemax.Size = new System.Drawing.Size(99, 60);
            this.btn_rangemax.TabIndex = 14;
            this.btn_rangemax.Text = "Range Max";
            this.btn_rangemax.UseVisualStyleBackColor = true;
            this.btn_rangemax.Click += new System.EventHandler(this.btn_rangemax_Click);
            // 
            // btn_meleemax
            // 
            this.btn_meleemax.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_meleemax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_meleemax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_meleemax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_meleemax.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_meleemax.Location = new System.Drawing.Point(462, 6);
            this.btn_meleemax.Name = "btn_meleemax";
            this.btn_meleemax.Size = new System.Drawing.Size(99, 35);
            this.btn_meleemax.TabIndex = 13;
            this.btn_meleemax.Text = "Melee Max";
            this.btn_meleemax.UseVisualStyleBackColor = true;
            this.btn_meleemax.Click += new System.EventHandler(this.btn_meleemax_Click);
            // 
            // btn_equiptbonus
            // 
            this.btn_equiptbonus.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_equiptbonus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_equiptbonus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_equiptbonus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_equiptbonus.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_equiptbonus.Location = new System.Drawing.Point(28, 47);
            this.btn_equiptbonus.Name = "btn_equiptbonus";
            this.btn_equiptbonus.Size = new System.Drawing.Size(99, 60);
            this.btn_equiptbonus.TabIndex = 12;
            this.btn_equiptbonus.Text = "Equipment Bonus";
            this.btn_equiptbonus.UseVisualStyleBackColor = true;
            this.btn_equiptbonus.Click += new System.EventHandler(this.btn_equiptbonus_Click);
            // 
            // btn_Energyrestore
            // 
            this.btn_Energyrestore.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_Energyrestore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Energyrestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Energyrestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Energyrestore.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Energyrestore.Location = new System.Drawing.Point(381, 6);
            this.btn_Energyrestore.Name = "btn_Energyrestore";
            this.btn_Energyrestore.Size = new System.Drawing.Size(75, 35);
            this.btn_Energyrestore.TabIndex = 11;
            this.btn_Energyrestore.Text = "Energy";
            this.btn_Energyrestore.UseVisualStyleBackColor = true;
            this.btn_Energyrestore.Click += new System.EventHandler(this.btn_Energyrestore_Click);
            // 
            // btn_Combatlvl
            // 
            this.btn_Combatlvl.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_Combatlvl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Combatlvl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Combatlvl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Combatlvl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Combatlvl.Location = new System.Drawing.Point(276, 6);
            this.btn_Combatlvl.Name = "btn_Combatlvl";
            this.btn_Combatlvl.Size = new System.Drawing.Size(99, 35);
            this.btn_Combatlvl.TabIndex = 10;
            this.btn_Combatlvl.Text = "Combat Lvl";
            this.btn_Combatlvl.UseVisualStyleBackColor = true;
            this.btn_Combatlvl.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_battlexp
            // 
            this.btn_battlexp.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_battlexp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_battlexp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_battlexp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_battlexp.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_battlexp.Location = new System.Drawing.Point(168, 6);
            this.btn_battlexp.Name = "btn_battlexp";
            this.btn_battlexp.Size = new System.Drawing.Size(102, 35);
            this.btn_battlexp.TabIndex = 9;
            this.btn_battlexp.Text = "Combat XP";
            this.btn_battlexp.UseVisualStyleBackColor = true;
            this.btn_battlexp.Click += new System.EventHandler(this.btn_battlexp_Click);
            // 
            // btn_Skill
            // 
            this.btn_Skill.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_Skill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Skill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Skill.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Skill.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Skill.Location = new System.Drawing.Point(87, 6);
            this.btn_Skill.Name = "btn_Skill";
            this.btn_Skill.Size = new System.Drawing.Size(75, 35);
            this.btn_Skill.TabIndex = 8;
            this.btn_Skill.Text = "Skill";
            this.btn_Skill.UseVisualStyleBackColor = true;
            this.btn_Skill.Click += new System.EventHandler(this.btn_Skill_Click);
            // 
            // btn_normcalc
            // 
            this.btn_normcalc.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_normcalc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_normcalc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_normcalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_normcalc.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_normcalc.Location = new System.Drawing.Point(6, 6);
            this.btn_normcalc.Name = "btn_normcalc";
            this.btn_normcalc.Size = new System.Drawing.Size(75, 35);
            this.btn_normcalc.TabIndex = 7;
            this.btn_normcalc.Text = "Normal";
            this.btn_normcalc.UseVisualStyleBackColor = true;
            this.btn_normcalc.Click += new System.EventHandler(this.btn_normcalc_Click);
            // 
            // tabPage10
            // 
            this.tabPage10.BackgroundImage = global::RS_Client.Properties.Resources.backgrounduntil;
            this.tabPage10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage10.Controls.Add(this.txt_searchmanga);
            this.tabPage10.Controls.Add(this.txt_page);
            this.tabPage10.Controls.Add(this.txt_chapter);
            this.tabPage10.Controls.Add(this.txt_title);
            this.tabPage10.Controls.Add(this.btn_randommanga);
            this.tabPage10.Controls.Add(this.lst_manga);
            this.tabPage10.Controls.Add(this.btn_searchmanga);
            this.tabPage10.Controls.Add(this.label26);
            this.tabPage10.Controls.Add(this.btn_nextmanga);
            this.tabPage10.Controls.Add(this.btn_peviousmanga);
            this.tabPage10.Controls.Add(this.btn_read);
            this.tabPage10.Controls.Add(this.lbl_pagenum);
            this.tabPage10.Controls.Add(this.lbl_chapter);
            this.tabPage10.Controls.Add(this.lbl_title);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(570, 108);
            this.tabPage10.TabIndex = 7;
            this.tabPage10.Text = "Manga";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // txt_searchmanga
            // 
            this.txt_searchmanga.BackColor = System.Drawing.Color.Silver;
            this.txt_searchmanga.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_searchmanga.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_searchmanga.Location = new System.Drawing.Point(230, 6);
            this.txt_searchmanga.Name = "txt_searchmanga";
            this.txt_searchmanga.Size = new System.Drawing.Size(100, 17);
            this.txt_searchmanga.TabIndex = 1;
            this.txt_searchmanga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_searchmanga_KeyPress);
            // 
            // txt_page
            // 
            this.txt_page.BackColor = System.Drawing.Color.Silver;
            this.txt_page.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_page.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_page.Location = new System.Drawing.Point(72, 58);
            this.txt_page.Name = "txt_page";
            this.txt_page.Size = new System.Drawing.Size(100, 17);
            this.txt_page.TabIndex = 8;
            this.txt_page.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_title_KeyPress);
            // 
            // txt_chapter
            // 
            this.txt_chapter.BackColor = System.Drawing.Color.Silver;
            this.txt_chapter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_chapter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chapter.Location = new System.Drawing.Point(72, 32);
            this.txt_chapter.Name = "txt_chapter";
            this.txt_chapter.Size = new System.Drawing.Size(100, 17);
            this.txt_chapter.TabIndex = 7;
            this.txt_chapter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_title_KeyPress);
            // 
            // txt_title
            // 
            this.txt_title.BackColor = System.Drawing.Color.Silver;
            this.txt_title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_title.Location = new System.Drawing.Point(72, 6);
            this.txt_title.Name = "txt_title";
            this.txt_title.ReadOnly = true;
            this.txt_title.Size = new System.Drawing.Size(100, 17);
            this.txt_title.TabIndex = 10;
            this.txt_title.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_title_KeyPress);
            // 
            // btn_randommanga
            // 
            this.btn_randommanga.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_randommanga.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_randommanga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_randommanga.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_randommanga.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_randommanga.Location = new System.Drawing.Point(20, 78);
            this.btn_randommanga.Name = "btn_randommanga";
            this.btn_randommanga.Size = new System.Drawing.Size(152, 27);
            this.btn_randommanga.TabIndex = 11;
            this.btn_randommanga.Text = "Random";
            this.btn_randommanga.UseVisualStyleBackColor = true;
            this.btn_randommanga.Click += new System.EventHandler(this.btn_randommanga_Click);
            // 
            // lst_manga
            // 
            this.lst_manga.BackColor = System.Drawing.Color.Silver;
            this.lst_manga.FormattingEnabled = true;
            this.lst_manga.Location = new System.Drawing.Point(332, 8);
            this.lst_manga.Margin = new System.Windows.Forms.Padding(0);
            this.lst_manga.Name = "lst_manga";
            this.lst_manga.Size = new System.Drawing.Size(234, 95);
            this.lst_manga.TabIndex = 2;
            this.lst_manga.SelectedIndexChanged += new System.EventHandler(this.lst_manga_SelectedIndexChanged);
            this.lst_manga.DoubleClick += new System.EventHandler(this.lst_manga_DoubleClick);
            this.lst_manga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lst_manga_KeyPress);
            // 
            // btn_searchmanga
            // 
            this.btn_searchmanga.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_searchmanga.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_searchmanga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_searchmanga.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_searchmanga.Location = new System.Drawing.Point(255, 35);
            this.btn_searchmanga.Name = "btn_searchmanga";
            this.btn_searchmanga.Size = new System.Drawing.Size(75, 27);
            this.btn_searchmanga.TabIndex = 4;
            this.btn_searchmanga.Text = "Search";
            this.btn_searchmanga.UseVisualStyleBackColor = true;
            this.btn_searchmanga.Click += new System.EventHandler(this.btn_searchmanga_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label26.Location = new System.Drawing.Point(175, 10);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(44, 13);
            this.label26.TabIndex = 9;
            this.label26.Text = "Search:";
            // 
            // btn_nextmanga
            // 
            this.btn_nextmanga.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_nextmanga.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_nextmanga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_nextmanga.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_nextmanga.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_nextmanga.Location = new System.Drawing.Point(255, 68);
            this.btn_nextmanga.Name = "btn_nextmanga";
            this.btn_nextmanga.Size = new System.Drawing.Size(75, 35);
            this.btn_nextmanga.TabIndex = 6;
            this.btn_nextmanga.Text = ">>";
            this.btn_nextmanga.UseVisualStyleBackColor = true;
            this.btn_nextmanga.Visible = false;
            this.btn_nextmanga.Click += new System.EventHandler(this.btn_nextmanga_Click);
            // 
            // btn_peviousmanga
            // 
            this.btn_peviousmanga.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_peviousmanga.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_peviousmanga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_peviousmanga.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_peviousmanga.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_peviousmanga.Location = new System.Drawing.Point(178, 68);
            this.btn_peviousmanga.Name = "btn_peviousmanga";
            this.btn_peviousmanga.Size = new System.Drawing.Size(75, 35);
            this.btn_peviousmanga.TabIndex = 5;
            this.btn_peviousmanga.Text = "<<";
            this.btn_peviousmanga.UseVisualStyleBackColor = true;
            this.btn_peviousmanga.Visible = false;
            this.btn_peviousmanga.Click += new System.EventHandler(this.btn_peviousmanga_Click);
            // 
            // btn_read
            // 
            this.btn_read.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_read.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_read.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_read.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_read.Location = new System.Drawing.Point(178, 35);
            this.btn_read.Name = "btn_read";
            this.btn_read.Size = new System.Drawing.Size(75, 27);
            this.btn_read.TabIndex = 3;
            this.btn_read.Text = "Read";
            this.btn_read.UseVisualStyleBackColor = true;
            this.btn_read.Click += new System.EventHandler(this.btn_read_Click);
            // 
            // lbl_pagenum
            // 
            this.lbl_pagenum.AutoSize = true;
            this.lbl_pagenum.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_pagenum.Location = new System.Drawing.Point(0, 62);
            this.lbl_pagenum.Name = "lbl_pagenum";
            this.lbl_pagenum.Size = new System.Drawing.Size(35, 13);
            this.lbl_pagenum.TabIndex = 4;
            this.lbl_pagenum.Text = "Page:";
            // 
            // lbl_chapter
            // 
            this.lbl_chapter.AutoSize = true;
            this.lbl_chapter.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_chapter.Location = new System.Drawing.Point(0, 36);
            this.lbl_chapter.Name = "lbl_chapter";
            this.lbl_chapter.Size = new System.Drawing.Size(47, 13);
            this.lbl_chapter.TabIndex = 2;
            this.lbl_chapter.Text = "Chapter:";
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_title.Location = new System.Drawing.Point(0, 10);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(30, 13);
            this.lbl_title.TabIndex = 0;
            this.lbl_title.Text = "Title:";
            // 
            // tabPage17
            // 
            this.tabPage17.BackgroundImage = global::RS_Client.Properties.Resources.backgrounduntil;
            this.tabPage17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage17.Controls.Add(this.pb_vidloading);
            this.tabPage17.Controls.Add(this.btn_removefav);
            this.tabPage17.Controls.Add(this.btn_addfav);
            this.tabPage17.Controls.Add(this.btn_gofav);
            this.tabPage17.Controls.Add(this.cmb_youtubechnfavs);
            this.tabPage17.Controls.Add(this.btn_mostviews);
            this.tabPage17.Controls.Add(this.btn_newrel);
            this.tabPage17.Controls.Add(this.btn_latest);
            this.tabPage17.Controls.Add(this.btn_featured);
            this.tabPage17.Controls.Add(this.webBrowserunshorten);
            this.tabPage17.Controls.Add(this.trv_shows);
            this.tabPage17.Controls.Add(this.btn_randomanime);
            this.tabPage17.Controls.Add(this.pnl_yt);
            this.tabPage17.Controls.Add(this.btn_clearvidplayer);
            this.tabPage17.Controls.Add(this.btn_searchsite);
            this.tabPage17.Controls.Add(this.grp_sites);
            this.tabPage17.Controls.Add(this.lst_vids);
            this.tabPage17.Controls.Add(this.txt_searchvids);
            this.tabPage17.Controls.Add(this.label36);
            this.tabPage17.Location = new System.Drawing.Point(4, 22);
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage17.Size = new System.Drawing.Size(570, 108);
            this.tabPage17.TabIndex = 9;
            this.tabPage17.Text = "Video Player";
            this.tabPage17.UseVisualStyleBackColor = true;
            // 
            // pb_vidloading
            // 
            this.pb_vidloading.Location = new System.Drawing.Point(466, 4);
            this.pb_vidloading.Name = "pb_vidloading";
            this.pb_vidloading.Size = new System.Drawing.Size(100, 23);
            this.pb_vidloading.TabIndex = 6;
            this.pb_vidloading.Visible = false;
            // 
            // btn_removefav
            // 
            this.btn_removefav.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_removefav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_removefav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_removefav.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_removefav.Location = new System.Drawing.Point(225, 77);
            this.btn_removefav.Name = "btn_removefav";
            this.btn_removefav.Size = new System.Drawing.Size(64, 23);
            this.btn_removefav.TabIndex = 34;
            this.btn_removefav.Text = "Remove";
            this.btn_removefav.UseVisualStyleBackColor = true;
            this.btn_removefav.Visible = false;
            this.btn_removefav.Click += new System.EventHandler(this.btn_removefav_Click);
            // 
            // btn_addfav
            // 
            this.btn_addfav.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_addfav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_addfav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_addfav.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_addfav.Location = new System.Drawing.Point(159, 77);
            this.btn_addfav.Name = "btn_addfav";
            this.btn_addfav.Size = new System.Drawing.Size(64, 23);
            this.btn_addfav.TabIndex = 33;
            this.btn_addfav.Text = "Add";
            this.btn_addfav.UseVisualStyleBackColor = true;
            this.btn_addfav.Visible = false;
            this.btn_addfav.Click += new System.EventHandler(this.btn_addfav_Click);
            // 
            // btn_gofav
            // 
            this.btn_gofav.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_gofav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_gofav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gofav.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_gofav.Location = new System.Drawing.Point(258, 49);
            this.btn_gofav.Name = "btn_gofav";
            this.btn_gofav.Size = new System.Drawing.Size(38, 23);
            this.btn_gofav.TabIndex = 32;
            this.btn_gofav.Text = "Go";
            this.btn_gofav.UseVisualStyleBackColor = true;
            this.btn_gofav.Visible = false;
            this.btn_gofav.Click += new System.EventHandler(this.btn_gofav_Click);
            // 
            // cmb_youtubechnfavs
            // 
            this.cmb_youtubechnfavs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_youtubechnfavs.FormattingEnabled = true;
            this.cmb_youtubechnfavs.Location = new System.Drawing.Point(159, 49);
            this.cmb_youtubechnfavs.Name = "cmb_youtubechnfavs";
            this.cmb_youtubechnfavs.Size = new System.Drawing.Size(93, 21);
            this.cmb_youtubechnfavs.TabIndex = 31;
            this.cmb_youtubechnfavs.Visible = false;
            // 
            // btn_mostviews
            // 
            this.btn_mostviews.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_mostviews.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_mostviews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_mostviews.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_mostviews.Location = new System.Drawing.Point(213, 85);
            this.btn_mostviews.Name = "btn_mostviews";
            this.btn_mostviews.Size = new System.Drawing.Size(83, 22);
            this.btn_mostviews.TabIndex = 30;
            this.btn_mostviews.Text = "Most views";
            this.btn_mostviews.UseVisualStyleBackColor = true;
            this.btn_mostviews.Visible = false;
            this.btn_mostviews.Click += new System.EventHandler(this.btn_mostviews_Click);
            // 
            // btn_newrel
            // 
            this.btn_newrel.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_newrel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_newrel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_newrel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_newrel.Location = new System.Drawing.Point(228, 49);
            this.btn_newrel.Name = "btn_newrel";
            this.btn_newrel.Size = new System.Drawing.Size(63, 22);
            this.btn_newrel.TabIndex = 29;
            this.btn_newrel.Text = "New";
            this.btn_newrel.UseVisualStyleBackColor = true;
            this.btn_newrel.Visible = false;
            this.btn_newrel.Click += new System.EventHandler(this.btn_newrel_Click);
            // 
            // btn_latest
            // 
            this.btn_latest.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_latest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_latest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_latest.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_latest.Location = new System.Drawing.Point(159, 85);
            this.btn_latest.Name = "btn_latest";
            this.btn_latest.Size = new System.Drawing.Size(48, 22);
            this.btn_latest.TabIndex = 28;
            this.btn_latest.Text = "Latest";
            this.btn_latest.UseVisualStyleBackColor = true;
            this.btn_latest.Visible = false;
            this.btn_latest.Click += new System.EventHandler(this.btn_latest_Click);
            // 
            // btn_featured
            // 
            this.btn_featured.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_featured.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_featured.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_featured.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_featured.Location = new System.Drawing.Point(159, 49);
            this.btn_featured.Name = "btn_featured";
            this.btn_featured.Size = new System.Drawing.Size(63, 22);
            this.btn_featured.TabIndex = 27;
            this.btn_featured.Text = "Featured";
            this.btn_featured.UseVisualStyleBackColor = true;
            this.btn_featured.Visible = false;
            this.btn_featured.Click += new System.EventHandler(this.btn_featured_Click);
            // 
            // webBrowserunshorten
            // 
            this.webBrowserunshorten.Location = new System.Drawing.Point(302, 33);
            this.webBrowserunshorten.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserunshorten.Name = "webBrowserunshorten";
            this.webBrowserunshorten.ScriptErrorsSuppressed = true;
            this.webBrowserunshorten.Size = new System.Drawing.Size(265, 74);
            this.webBrowserunshorten.TabIndex = 26;
            this.webBrowserunshorten.Visible = false;
            this.webBrowserunshorten.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.webBrowserunshorten.NewWindow += new System.ComponentModel.CancelEventHandler(this.Browser1_NewWindow);
            // 
            // trv_shows
            // 
            this.trv_shows.Location = new System.Drawing.Point(152, 27);
            this.trv_shows.Name = "trv_shows";
            this.trv_shows.Size = new System.Drawing.Size(144, 80);
            this.trv_shows.TabIndex = 25;
            this.trv_shows.Visible = false;
            this.trv_shows.DoubleClick += new System.EventHandler(this.trv_shows_DoubleClick);
            // 
            // btn_randomanime
            // 
            this.btn_randomanime.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_randomanime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_randomanime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_randomanime.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_randomanime.Location = new System.Drawing.Point(159, 49);
            this.btn_randomanime.Name = "btn_randomanime";
            this.btn_randomanime.Size = new System.Drawing.Size(121, 49);
            this.btn_randomanime.TabIndex = 24;
            this.btn_randomanime.Text = "Random";
            this.btn_randomanime.UseVisualStyleBackColor = true;
            this.btn_randomanime.Visible = false;
            this.btn_randomanime.Click += new System.EventHandler(this.btn_randomanime_Click);
            // 
            // pnl_yt
            // 
            this.pnl_yt.Controls.Add(this.rad_ytvideosec);
            this.pnl_yt.Controls.Add(this.rad_ytchannelsec);
            this.pnl_yt.Location = new System.Drawing.Point(156, 30);
            this.pnl_yt.Name = "pnl_yt";
            this.pnl_yt.Size = new System.Drawing.Size(133, 20);
            this.pnl_yt.TabIndex = 22;
            this.pnl_yt.Visible = false;
            // 
            // rad_ytvideosec
            // 
            this.rad_ytvideosec.AutoSize = true;
            this.rad_ytvideosec.Checked = true;
            this.rad_ytvideosec.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_ytvideosec.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rad_ytvideosec.Location = new System.Drawing.Point(3, 3);
            this.rad_ytvideosec.Name = "rad_ytvideosec";
            this.rad_ytvideosec.Size = new System.Drawing.Size(52, 16);
            this.rad_ytvideosec.TabIndex = 20;
            this.rad_ytvideosec.TabStop = true;
            this.rad_ytvideosec.Text = "Videos";
            this.rad_ytvideosec.UseVisualStyleBackColor = true;
            // 
            // rad_ytchannelsec
            // 
            this.rad_ytchannelsec.AutoSize = true;
            this.rad_ytchannelsec.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_ytchannelsec.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rad_ytchannelsec.Location = new System.Drawing.Point(61, 3);
            this.rad_ytchannelsec.Name = "rad_ytchannelsec";
            this.rad_ytchannelsec.Size = new System.Drawing.Size(62, 16);
            this.rad_ytchannelsec.TabIndex = 21;
            this.rad_ytchannelsec.Text = "Channels";
            this.rad_ytchannelsec.UseVisualStyleBackColor = true;
            this.rad_ytchannelsec.CheckedChanged += new System.EventHandler(this.rad_ytchannelsec_CheckedChanged);
            // 
            // btn_clearvidplayer
            // 
            this.btn_clearvidplayer.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_clearvidplayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_clearvidplayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clearvidplayer.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_clearvidplayer.Location = new System.Drawing.Point(225, 3);
            this.btn_clearvidplayer.Name = "btn_clearvidplayer";
            this.btn_clearvidplayer.Size = new System.Drawing.Size(64, 22);
            this.btn_clearvidplayer.TabIndex = 9;
            this.btn_clearvidplayer.Text = "Clear";
            this.btn_clearvidplayer.UseVisualStyleBackColor = true;
            this.btn_clearvidplayer.Click += new System.EventHandler(this.btn_clearvidplayer_Click);
            // 
            // btn_searchsite
            // 
            this.btn_searchsite.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_searchsite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_searchsite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_searchsite.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_searchsite.Location = new System.Drawing.Point(156, 3);
            this.btn_searchsite.Name = "btn_searchsite";
            this.btn_searchsite.Size = new System.Drawing.Size(63, 22);
            this.btn_searchsite.TabIndex = 7;
            this.btn_searchsite.Text = "Search";
            this.btn_searchsite.UseVisualStyleBackColor = true;
            this.btn_searchsite.Click += new System.EventHandler(this.btn_searchsite_Click);
            // 
            // grp_sites
            // 
            this.grp_sites.Controls.Add(this.rad_moviesearch);
            this.grp_sites.Controls.Add(this.rad_animesearch);
            this.grp_sites.Controls.Add(this.rad_showsearch);
            this.grp_sites.Controls.Add(this.rad_youtubesearch);
            this.grp_sites.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.grp_sites.Location = new System.Drawing.Point(10, 30);
            this.grp_sites.Name = "grp_sites";
            this.grp_sites.Size = new System.Drawing.Size(140, 68);
            this.grp_sites.TabIndex = 6;
            this.grp_sites.TabStop = false;
            this.grp_sites.Text = "Search Site";
            // 
            // rad_moviesearch
            // 
            this.rad_moviesearch.AutoSize = true;
            this.rad_moviesearch.Location = new System.Drawing.Point(77, 42);
            this.rad_moviesearch.Name = "rad_moviesearch";
            this.rad_moviesearch.Size = new System.Drawing.Size(59, 17);
            this.rad_moviesearch.TabIndex = 3;
            this.rad_moviesearch.Text = "Movies";
            this.rad_moviesearch.UseVisualStyleBackColor = true;
            this.rad_moviesearch.CheckedChanged += new System.EventHandler(this.rad_moviesearch_CheckedChanged);
            // 
            // rad_animesearch
            // 
            this.rad_animesearch.AutoSize = true;
            this.rad_animesearch.Location = new System.Drawing.Point(6, 42);
            this.rad_animesearch.Name = "rad_animesearch";
            this.rad_animesearch.Size = new System.Drawing.Size(54, 17);
            this.rad_animesearch.TabIndex = 2;
            this.rad_animesearch.Text = "Anime";
            this.rad_animesearch.UseVisualStyleBackColor = true;
            this.rad_animesearch.CheckedChanged += new System.EventHandler(this.rad_anime44search_CheckedChanged);
            // 
            // rad_showsearch
            // 
            this.rad_showsearch.AutoSize = true;
            this.rad_showsearch.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rad_showsearch.Location = new System.Drawing.Point(77, 19);
            this.rad_showsearch.Name = "rad_showsearch";
            this.rad_showsearch.Size = new System.Drawing.Size(57, 17);
            this.rad_showsearch.TabIndex = 1;
            this.rad_showsearch.Text = "Shows";
            this.rad_showsearch.UseVisualStyleBackColor = true;
            // 
            // rad_youtubesearch
            // 
            this.rad_youtubesearch.AutoSize = true;
            this.rad_youtubesearch.Checked = true;
            this.rad_youtubesearch.Location = new System.Drawing.Point(6, 19);
            this.rad_youtubesearch.Name = "rad_youtubesearch";
            this.rad_youtubesearch.Size = new System.Drawing.Size(65, 17);
            this.rad_youtubesearch.TabIndex = 0;
            this.rad_youtubesearch.TabStop = true;
            this.rad_youtubesearch.Text = "Youtube";
            this.rad_youtubesearch.UseVisualStyleBackColor = true;
            this.rad_youtubesearch.CheckedChanged += new System.EventHandler(this.rad_youtubesearch_CheckedChanged);
            // 
            // lst_vids
            // 
            this.lst_vids.BackColor = System.Drawing.Color.Silver;
            this.lst_vids.FormattingEnabled = true;
            this.lst_vids.Location = new System.Drawing.Point(299, 3);
            this.lst_vids.Margin = new System.Windows.Forms.Padding(0);
            this.lst_vids.Name = "lst_vids";
            this.lst_vids.Size = new System.Drawing.Size(268, 108);
            this.lst_vids.TabIndex = 5;
            this.lst_vids.SelectedIndexChanged += new System.EventHandler(this.lst_vids_SelectedIndexChanged);
            this.lst_vids.DoubleClick += new System.EventHandler(this.lst_vids_DoubleClick);
            this.lst_vids.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lst_vids_KeyPress);
            // 
            // txt_searchvids
            // 
            this.txt_searchvids.BackColor = System.Drawing.Color.Silver;
            this.txt_searchvids.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_searchvids.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_searchvids.Location = new System.Drawing.Point(56, 7);
            this.txt_searchvids.Name = "txt_searchvids";
            this.txt_searchvids.Size = new System.Drawing.Size(94, 17);
            this.txt_searchvids.TabIndex = 4;
            this.txt_searchvids.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_searchvids_KeyPress);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label36.Location = new System.Drawing.Point(6, 10);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(44, 13);
            this.label36.TabIndex = 3;
            this.label36.Text = "Search:";
            // 
            // tabPage18
            // 
            this.tabPage18.BackgroundImage = global::RS_Client.Properties.Resources.backgrounduntil;
            this.tabPage18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage18.Controls.Add(this.btn_randgame);
            this.tabPage18.Controls.Add(this.button3);
            this.tabPage18.Controls.Add(this.button2);
            this.tabPage18.Controls.Add(this.button1);
            this.tabPage18.Controls.Add(this.btn_cleargame);
            this.tabPage18.Controls.Add(this.btn_searchgames);
            this.tabPage18.Controls.Add(this.txt_game);
            this.tabPage18.Controls.Add(this.label35);
            this.tabPage18.Controls.Add(this.lst_games);
            this.tabPage18.Location = new System.Drawing.Point(4, 22);
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage18.Size = new System.Drawing.Size(570, 108);
            this.tabPage18.TabIndex = 10;
            this.tabPage18.Text = "Flash";
            this.tabPage18.UseVisualStyleBackColor = true;
            // 
            // btn_randgame
            // 
            this.btn_randgame.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_randgame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_randgame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_randgame.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_randgame.Location = new System.Drawing.Point(10, 71);
            this.btn_randgame.Name = "btn_randgame";
            this.btn_randgame.Size = new System.Drawing.Size(60, 36);
            this.btn_randgame.TabIndex = 8;
            this.btn_randgame.Text = "Random";
            this.btn_randgame.UseVisualStyleBackColor = true;
            this.btn_randgame.Click += new System.EventHandler(this.btn_randgame_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button3.Location = new System.Drawing.Point(76, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 36);
            this.button3.TabIndex = 7;
            this.button3.Text = "Most Played";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Location = new System.Drawing.Point(208, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 36);
            this.button2.TabIndex = 6;
            this.button2.Text = "Top Rated";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(142, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 36);
            this.button1.TabIndex = 5;
            this.button1.Text = "Latest";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_cleargame
            // 
            this.btn_cleargame.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_cleargame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cleargame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cleargame.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_cleargame.Location = new System.Drawing.Point(99, 32);
            this.btn_cleargame.Name = "btn_cleargame";
            this.btn_cleargame.Size = new System.Drawing.Size(75, 33);
            this.btn_cleargame.TabIndex = 1;
            this.btn_cleargame.Text = "Clear";
            this.btn_cleargame.UseVisualStyleBackColor = true;
            this.btn_cleargame.Click += new System.EventHandler(this.btn_cleargame_Click);
            // 
            // btn_searchgames
            // 
            this.btn_searchgames.BackgroundImage = global::RS_Client.Properties.Resources.custbtn;
            this.btn_searchgames.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_searchgames.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_searchgames.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_searchgames.Location = new System.Drawing.Point(180, 32);
            this.btn_searchgames.Name = "btn_searchgames";
            this.btn_searchgames.Size = new System.Drawing.Size(88, 33);
            this.btn_searchgames.TabIndex = 2;
            this.btn_searchgames.Text = "Search";
            this.btn_searchgames.UseVisualStyleBackColor = true;
            this.btn_searchgames.Click += new System.EventHandler(this.btn_searchgames_Click);
            // 
            // txt_game
            // 
            this.txt_game.BackColor = System.Drawing.Color.Silver;
            this.txt_game.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_game.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_game.Location = new System.Drawing.Point(58, 6);
            this.txt_game.Name = "txt_game";
            this.txt_game.Size = new System.Drawing.Size(210, 17);
            this.txt_game.TabIndex = 0;
            this.txt_game.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_game_KeyPress);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label35.Location = new System.Drawing.Point(8, 9);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(44, 13);
            this.label35.TabIndex = 4;
            this.label35.Text = "Search:";
            // 
            // lst_games
            // 
            this.lst_games.BackColor = System.Drawing.Color.Silver;
            this.lst_games.FormattingEnabled = true;
            this.lst_games.Location = new System.Drawing.Point(278, 3);
            this.lst_games.Margin = new System.Windows.Forms.Padding(0);
            this.lst_games.Name = "lst_games";
            this.lst_games.Size = new System.Drawing.Size(289, 108);
            this.lst_games.TabIndex = 3;
            this.lst_games.SelectedIndexChanged += new System.EventHandler(this.lst_games_SelectedIndexChanged);
            this.lst_games.DoubleClick += new System.EventHandler(this.lst_games_DoubleClick);
            this.lst_games.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lst_games_KeyPress);
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(0, 0);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(100, 20);
            this.txt_url.TabIndex = 0;
            // 
            // btn_go
            // 
            this.btn_go.Location = new System.Drawing.Point(0, 0);
            this.btn_go.Name = "btn_go";
            this.btn_go.Size = new System.Drawing.Size(75, 23);
            this.btn_go.TabIndex = 0;
            // 
            // tmr_geupdates
            // 
            this.tmr_geupdates.Enabled = true;
            this.tmr_geupdates.Interval = 600000;
            this.tmr_geupdates.Tick += new System.EventHandler(this.tmr_geupdates_Tick);
            // 
            // thisToolStripMenuItem
            // 
            this.thisToolStripMenuItem.Name = "thisToolStripMenuItem";
            this.thisToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // andThisToolStripMenuItem
            // 
            this.andThisToolStripMenuItem.Name = "andThisToolStripMenuItem";
            this.andThisToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // ohAndThisToolStripMenuItem
            // 
            this.ohAndThisToolStripMenuItem.Name = "ohAndThisToolStripMenuItem";
            this.ohAndThisToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // test2ToolStripMenuItem
            // 
            this.test2ToolStripMenuItem.Name = "test2ToolStripMenuItem";
            this.test2ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // testToolStripMenuItem1
            // 
            this.testToolStripMenuItem1.Name = "testToolStripMenuItem1";
            this.testToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // test2ToolStripMenuItem1
            // 
            this.test2ToolStripMenuItem1.Name = "test2ToolStripMenuItem1";
            this.test2ToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // test3ToolStripMenuItem
            // 
            this.test3ToolStripMenuItem.Name = "test3ToolStripMenuItem";
            this.test3ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(32, 19);
            // 
            // tmr_embed
            // 
            this.tmr_embed.Interval = 70;
            this.tmr_embed.Tick += new System.EventHandler(this.tmr_embed_Tick);
            // 
            // cpuusage
            // 
            this.cpuusage.Enabled = true;
            this.cpuusage.Interval = 5000;
            this.cpuusage.Tick += new System.EventHandler(this.cpuusage_Tick);
            // 
            // tmr_twitter
            // 
            this.tmr_twitter.Interval = 3000;
            this.tmr_twitter.Tick += new System.EventHandler(this.tmr_twitter_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tmr_email
            // 
            this.tmr_email.Tick += new System.EventHandler(this.tmr_email_Tick);
            // 
            // bw
            // 
            this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rSOldSchoolToolStripMenuItem,
            this.rSClassicToolStripMenuItem,
            this.currentRSToolStripMenuItem});
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.playToolStripMenuItem.Text = "Play";
            // 
            // rSOldSchoolToolStripMenuItem
            // 
            this.rSOldSchoolToolStripMenuItem.Name = "rSOldSchoolToolStripMenuItem";
            this.rSOldSchoolToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rSOldSchoolToolStripMenuItem.Text = "RS Old School";
            this.rSOldSchoolToolStripMenuItem.Click += new System.EventHandler(this.rSOldSchoolToolStripMenuItem_Click);
            // 
            // rSClassicToolStripMenuItem
            // 
            this.rSClassicToolStripMenuItem.Name = "rSClassicToolStripMenuItem";
            this.rSClassicToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rSClassicToolStripMenuItem.Text = "RS Classic";
            this.rSClassicToolStripMenuItem.Click += new System.EventHandler(this.rSClassicToolStripMenuItem_Click);
            // 
            // currentRSToolStripMenuItem
            // 
            this.currentRSToolStripMenuItem.Name = "currentRSToolStripMenuItem";
            this.currentRSToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.currentRSToolStripMenuItem.Text = "Current RS";
            this.currentRSToolStripMenuItem.Click += new System.EventHandler(this.currentRSToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1362, 692);
            this.Controls.Add(this.splitContainer1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "RuneScape - Randy\'s RS Client";
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.UsageStrip.ResumeLayout(false);
            this.UsageStrip.PerformLayout();
            this.maintab.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabPage21.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_shots)).EndInit();
            this.tabPage16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_video)).EndInit();
            this.MANGA0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_manga0)).EndInit();
            this.tabPage19.ResumeLayout(false);
            this.tabPage20.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wmp_main)).EndInit();
            this.tbCalc.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl5.ResumeLayout(false);
            this.tabPage14.ResumeLayout(false);
            this.tabPage14.PerformLayout();
            this.tabPage15.ResumeLayout(false);
            this.tabPage15.PerformLayout();
            this.grp_linknum.ResumeLayout(false);
            this.grp_linknum.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.grp_extadd.ResumeLayout(false);
            this.grp_extadd.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_priceitem)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabPage10.PerformLayout();
            this.tabPage17.ResumeLayout(false);
            this.tabPage17.PerformLayout();
            this.pnl_yt.ResumeLayout(false);
            this.pnl_yt.PerformLayout();
            this.grp_sites.ResumeLayout(false);
            this.grp_sites.PerformLayout();
            this.tabPage18.ResumeLayout(false);
            this.tabPage18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmr_stopwatch;
        private System.Windows.Forms.Label label28;
        private Timer tmr_blockads;
        private SplitContainer splitContainer1;
        private TabControl maintab;
        private TabPage tabPage11;
        private TabPage tabPage16;
        private WebBrowser webvideo;
        private TabPage MANGA0;
        private PictureBox pic_manga0;
        private TabPage tabPage19;
        private WebBrowser web_games;
        private TabPage tabPage20;
        private TabPage tabPage21;
        private Panel panel3;
        private Button btn_prevscreen;
        private Button btn_nextscreen;
        private PictureBox pic_shots;
        private Panel panel4;
        private TextBox txt_url;
        private Button btn_go;
        private ToolStripMenuItem clearHistoryToolStripMenuItem;
        private TabControl tabControl2;
        private ComboBox txt_ur;
        private Button btn_delscreen;
        private ImageList maintabimages;
        private PictureBox pictureBox1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveAndExitToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem restoreDefaultToolStripMenuItem;
        private ToolStripMenuItem screenShotToolStripMenuItem;
        private ToolStripMenuItem blockInternetExplorerPopupsToolStripMenuItem;
        private ToolStripMenuItem hideShowToolStripMenuItem;
        private ToolStripMenuItem hideUtilitiesToolStripMenuItem;
        private ToolStripMenuItem previousPageToolStripMenuItem;
        private ToolStripMenuItem netxtPageToolStripMenuItem;
        private Panel panel5;
        private ToolStripMenuItem checkForGrandExchangeUpdatesToolStripMenuItem;
        private Timer tmr_geupdates;
        private MenuStrip menuStrip2;
        private Panel pnl_gba;
        private ToolStripMenuItem swapPanelsToolStripMenuItem;
        private ToolStripMenuItem thisToolStripMenuItem;
        private ToolStripMenuItem andThisToolStripMenuItem;
        private ToolStripMenuItem ohAndThisToolStripMenuItem;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripMenuItem test2ToolStripMenuItem;
        private ToolStripMenuItem testToolStripMenuItem1;
        private ToolStripMenuItem test2ToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem test3ToolStripMenuItem;
        private SplitContainer splitContainer2;
        private TextBox txt_news;
        private Panel btn_p2p;
        private WebBrowser web_ad;
        private PictureBox pictureBox2;
        private WebBrowser webBrowser2;
        private WebBrowser web_irc;
        private PictureBox pic_video;
        private ToolStripMenuItem iRCToolStripMenuItem;
        private TabPage tabPage12;
        private TreeView trwFileExplorer;
        private ImageList imageList1;
        private TabPage tabPage13;
        private AxWMPLib.AxWindowsMediaPlayer wmp_main;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabControl tabControl5;
        private TabPage tabPage14;
        private Button btn_refresh;
        private CheckBox chk_lucky;
        private Button btn_close_web;
        private Button btn_add;
        private Button btn_clearurl;
        private Button btn_forward;
        private Button btn_back;
        private Button btn_link2;
        private Button btn_link1;
        private Button btn_link6;
        private Button btn_link4;
        private Button btn_link5;
        private Button btn_link3;
        private TabPage tabPage15;
        private Button btn_setlink;
        private TextBox txt_linkurl;
        private Label label32;
        private TextBox txt_linkname;
        private Label label31;
        private GroupBox grp_linknum;
        private RadioButton rad_6;
        private RadioButton rad_5;
        private RadioButton rad_4;
        private RadioButton rad_3;
        private RadioButton rad_2;
        private RadioButton rad_1;
        private TabPage tabPage2;
        private Button btn_highscoressearch;
        private ProgressBar xptolvlbar;
        private Label lbl_xp;
        private Label lbl_rank;
        private Label lbl_skillname;
        private Label label1;
        private TextBox txt_user;
        private Label lbl_craftlvl;
        private Label lbl_dunglvl;
        private Label lbl_summlvl;
        private Label lbl_conlvl;
        private Label lbl_hunterlvl;
        private Label lbl_rclvl;
        private Label lbl_farminglvl;
        private Label lbl_slayerlvl;
        private Label lbl_thieflvl;
        private Label lbl_agilitylvl;
        private Label lbl_herblvl;
        private Label lbl_mininglvl;
        private Label lbl_smithlvl;
        private Label lbl_fmlvl;
        private Label lbl_fishinglvl;
        private Label lbl_cooklvl;
        private Label lbl_wclvl;
        private Label lbl_fletchinglvl;
        private Label lbl_magiclvl;
        private Label lbl_prayerlvl;
        private Label lbl_rangelvl;
        private Label lbl_hplvl;
        private Label lbl_strengthlvl;
        private Label lbl_defencelvl;
        private Label lbl_attacklvl;
        private Label lbl_overall;
        private TabPage tabPage3;
        private Button btn_clearembed;
        private Button btn_processes;
        private Button btn_embed;
        private ListBox lst_exes;
        private Button btn_extremove;
        private GroupBox grp_extadd;
        private Label label33;
        private Button btn_extfile;
        private TextBox txt_extname;
        private ListBox lst_loc;
        private Button btn_launchext;
        private ListBox lst_external;
        private TabPage tabPage4;
        private TextBox txt_notes;
        private TabPage tabPage5;
        private Button btn_stop;
        private Button btn_reset;
        private Button btn_start;
        private Label lbl_stopwatch;
        private TabPage tabPage6;
        private TextBox txt_natprice;
        private TextBox txt_alch;
        private TextBox txt_profit;
        private TextBox txt_pricelookup;
        private TextBox txt_item;
        private Label label38;
        private Label label4;
        private Label label5;
        private Label label3;
        private Label label2;
        private Button btn_search;
        private PictureBox pic_priceitem;
        private TabPage tabPage7;
        private TabPage tabPage10;
        private TextBox txt_searchmanga;
        private TextBox txt_page;
        private TextBox txt_chapter;
        private TextBox txt_title;
        private Button btn_randommanga;
        private ListBox lst_manga;
        private Button btn_searchmanga;
        private Label label26;
        private Button btn_nextmanga;
        private Button btn_peviousmanga;
        private Button btn_read;
        private Label lbl_pagenum;
        private Label lbl_chapter;
        private Label lbl_title;
        private TabPage tabPage17;
        private TreeView trv_shows;
        private Button btn_randomanime;
        private Panel pnl_yt;
        private RadioButton rad_ytvideosec;
        private RadioButton rad_ytchannelsec;
        private Button btn_clearvidplayer;
        private Button btn_searchsite;
        private GroupBox grp_sites;
        private RadioButton rad_moviesearch;
        private RadioButton rad_animesearch;
        private RadioButton rad_showsearch;
        private RadioButton rad_youtubesearch;
        private ListBox lst_vids;
        private TextBox txt_searchvids;
        private Label label36;
        private TabPage tabPage18;
        private Button button2;
        private Button button1;
        private Button btn_cleargame;
        private Button btn_searchgames;
        private TextBox txt_game;
        private Label label35;
        private ListBox lst_games;
        private WebBrowser webBrowserunshorten;
        private ToolStripMenuItem showSourcesToolStripMenuItem;
        private ToolStripMenuItem stagevuToolStripMenuItem;
        private ToolStripMenuItem putlockerToolStripMenuItem;
        private ToolStripMenuItem gorillavidToolStripMenuItem;
        private ToolStripMenuItem movshareToolStripMenuItem;
        private ToolStripMenuItem nowvideoToolStripMenuItem;
        private ToolStripMenuItem movieSourcesToolStripMenuItem;
        private ToolStripMenuItem putlockerToolStripMenuItem1;
        private ToolStripMenuItem sockshareToolStripMenuItem;
        private ToolStripMenuItem nowvideoToolStripMenuItem1;
        private ToolStripMenuItem dwnToolStripMenuItem;
        private ToolStripMenuItem videoweedToolStripMenuItem;
        private Button btn_randgame;
        private Button button3;
        private TabPage tbCalc;
        private Button btn_normcalc;
        private WebBrowser wbCalc;
        private Button btn_Skill;
        private Button btn_battlexp;
        private Button btn_Combatlvl;
        private Button btn_Energyrestore;
        private Button btn_equiptbonus;
        private Button btn_meleemax;
        private Button btn_rangemax;
        private Button btn_pestcntl;
        private Button btn_prayerdrain;
        private Button btn_mismanage;
        private Button btn_latest;
        private Button btn_featured;
        private Button btn_newrel;
        private Button btn_mostviews;
        private ToolStripMenuItem videoweedToolStripMenuItem1;
        private ToolStripMenuItem uploadcToolStripMenuItem;
        private ComboBox cmb_youtubechnfavs;
        private Button btn_removefav;
        private Button btn_addfav;
        private Button btn_gofav;
        private Timer tmr_embed;
        private ToolStripMenuItem keepEmbededToolStripMenuItem;
        private StatusStrip UsageStrip;
        private ToolStripStatusLabel cpu_strip;
        private Timer cpuusage;
        private ToolStripStatusLabel ram_strip;
        private ToolStripDropDownButton twitterfeeds;
        private ToolStripMenuItem twitterFollowstrip;
        private ToolStripTextBox txttwitteraccount;
        private ToolStripMenuItem startFollowingToolStripMenuItem;
        private ToolStripComboBox checkTimeToolStripMenuItem;
        private Timer tmr_twitter;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem emailToolStripMenuItem;
        private ToolStripMenuItem accontUsernameToolStripMenuItem;
        private ToolStripTextBox txt_emailuser;
        private ToolStripMenuItem emailPasswordToolStripMenuItem;
        private ToolStripTextBox txt_emailpass;
        private ToolStripMenuItem getEmailToolStripMenuItem;
        private ToolStripComboBox cmb_emailtime;
        private ToolStripMenuItem rSSEmailToolStripMenuItem;
        private ToolStripMenuItem currentEmailNotificationToolStripMenuItem;
        private Timer tmr_email;
        private System.ComponentModel.BackgroundWorker bw;
        private ToolStripDropDownButton emailstrip;
        private ToolStripMenuItem vidbullToolStripMenuItem;
        private ProgressBar pb_vidloading;
        private ToolStripMenuItem playToolStripMenuItem;
        private ToolStripMenuItem rSOldSchoolToolStripMenuItem;
        private ToolStripMenuItem rSClassicToolStripMenuItem;
        private ToolStripMenuItem currentRSToolStripMenuItem;
    }
}

