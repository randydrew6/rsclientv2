﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using RS_Client.Properties;
using System.Reflection;
using System.ComponentModel;
using FileExplorer_TreeView;
using System.Text.RegularExpressions;
using AE.Net.Mail;

namespace RS_Client
{
    public partial class Form1 : Form
    {
        private string mainuser;

        public string Mainuser
        {
            get
            {
                return mainuser;
            }
            set
            {
                mainuser = value;
            }
        }

        List<string> MangaURL = new List<string>();
        List<string> MangaTitle = new List<string>();
        List<string> AnimeTitle = new List<string>();
        List<string> AnimeUrl = new List<string>();
        List<string> VidTitle = new List<string>();
        List<string> VidUrl = new List<string>();
        List<string> VidPic = new List<string>();
        MatchCollection rtnSeason;
        List<string> VidSeasonlist = new List<string>();
        MatchCollection rtnEpisode;
        List<string> VidEpisodelist = new List<string>();
        List<string> GameTitle = new List<string>();
        List<string> GameEmbedURL = new List<string>();
        List<string> GameURL = new List<string>();
        List<string> GamePreview = new List<string>();
        List<string> ytFavTitle = new List<string>();
        List<string> ytFavURL = new List<string>();
        string currentytchannel = string.Empty;
        string currentytchannelname = string.Empty;
        string[] ScreenShot;
        Process p = null;
        public static int GWL_STYLE = -16;
        public static int WS_CAPTION = 0x00800000 | 0x00400000; //window with a title bar

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        //Sets window attributes
        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        //Gets window attributes
        [DllImport("USER32.DLL")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport(@"user32.dll", EntryPoint = "SetWindowPos", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


        public static void SetPositionSize(IntPtr hWind, int x, int y, int width, int height)
        {
            SetWindowPos(hWind, (IntPtr)null, x, y, width, height, 0u);
        }

        string playtype = "";
        bool ytthrochannel = false;
        int tries = 0;
        int style = -1;
        string showhelp = "";
        string switch_complete = "";
        int lstvidposition = 0;
        string VidType = "";
        string emailuser = string.Empty;
        string emailpass = string.Empty;
        string latestemail = string.Empty;
        string twitteruser = string.Empty;
        string latesttweet = string.Empty;
        string mangapicurl = "";
        int mangachappages = 0;
        string priceitem = "";
        int natprice = 0;
        int Screenshots = 0;
        int Currentshot = 0;
        int Extnums = 0;
        int seconds = 0;
        int minutes = 0;
        int hours = 0;
        int page_number = 0;
        string page = "1";
        string chapter = "1";
        string originaltitle = "";
        string title = "";
        string link1 = "http://google.com/";
        string link1_string = "Google";
        string link2 = "http://google.com/";
        string link2_string = "Google";
        string link3 = "http://google.com/";
        string link3_string = "Google";
        string link4 = "http://google.com/";
        string link4_string = "Google";
        string link5 = "http://google.com/";
        string link5_string = "Google";
        string link6 = "http://google.com/";
        string link6_string = "Google";

        Player tLocalPlayer = null;

        FileExplorer fe = new FileExplorer();
        public Form1()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClosingEventCancle_Closing);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Visible = false;
            loading();
            fe.CreateTree(this.trwFileExplorer);
            this.Visible = true;
        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
            HideScriptErrors(((WebBrowser)sender), true);
        }

        private void browser_DocumentComplete(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Erro);
        }

        private void Window_Erro(object sender, HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box.
            e.Handled = false;
        }

        private void btn_animecrazy_Click(object sender, EventArgs e)
        {
            try
            {
                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri(link1));
            }
            catch { }
        }

        private void btn_youtube_Click(object sender, EventArgs e)
        {
            try
            {
                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri(link3));
            }
            catch { }
        }

        private void btn_google_Click(object sender, EventArgs e)
        {
            try
            {
                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri(link5));
            }
            catch { }
        }

        private void btn_runescape_Click(object sender, EventArgs e)
        {
            try
            {
                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri(link6));
            }
            catch { }
        }

        private void btn_megavideo_Click(object sender, EventArgs e)
        {
            try
            {
                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri(link4));
            }
            catch { }
        }

        private void btn_runehq_Click(object sender, EventArgs e)
        {
            try
            {
                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri(link2));
            }
            catch { }
        }

        private void btn_clearurl_Click(object sender, EventArgs e)
        {
            txt_ur.Text = "";
            txt_ur.Focus();
            maintab.SelectedIndex = 0;
        }

        private void highscoreslookup(string p)
        {
            tLocalPlayer = new Player(p);
            if (tLocalPlayer != null)
            {
                try
                {
                    using (WebClient stats = new WebClient())
                    {
                        Stream statstream = stats.OpenRead("http://hiscore.runescape.com/index_lite.ws?player=" + tLocalPlayer.PlayerName);
                        List<string> lststats = new List<string>();
                        string[] tempseperate = new string[3];
                        using (StreamReader statread = new StreamReader(statstream))
                        {
                            string sockread = null;
                            int runs = 0;
                            double points = 0;
                            double output = 0;
                            double minlevel = 30; // first level to display
                            double maxlevel = 127; // last level to display
                            double returnxptolvl = -1;
                            double remover = 0;
                            Int32 tempval = 0;
                            while ((sockread = statread.ReadLine()) != null)
                            {
                                lststats.Add(sockread);
                                tempseperate = lststats[runs].ToString().Split(',');

                                points = 0;
                                output = 0;
                                returnxptolvl = -1;
                                tempval = 0;
                                remover = 0;
                                if (runs > 0)
                                {
                                    for (double lvl = 1; lvl <= maxlevel; lvl++)
                                    {
                                        points += Math.Floor(lvl + 300 * Math.Pow(2, lvl / 7));
                                        if (lvl >= minlevel)
                                        {
                                            output = Math.Floor(points / 4);
                                            try
                                            {
                                                if (output < Convert.ToDouble(tempseperate[2]))
                                                {
                                                    tempseperate[1] = (lvl + 1).ToString();
                                                    returnxptolvl = lvl + 1;
                                                    remover = output;
                                                }
                                                else if (returnxptolvl == lvl)
                                                {
                                                    tempval = Convert.ToInt32(100 * Convert.ToDecimal((Convert.ToDouble(tempseperate[2]) - remover) / (output - remover)));
                                                }
                                            }
                                            catch
                                            {
                                                tempseperate[1] = (lvl + 1).ToString();
                                                returnxptolvl = lvl + 1;
                                                remover = output;
                                                break;
                                            }
                                        }
                                    }
                                }
                                switch (runs)
                                {
                                    case 0:
                                        lbl_overall.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Overall.SkillXp = tempseperate[2];
                                        tLocalPlayer.Overall.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Overall.SkillRank = tempseperate[0];
                                        break;
                                    case 1:
                                        lbl_attacklvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Attack.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Attack.SkillRank = tempseperate[0];
                                        tLocalPlayer.Attack.SkillXp = tempseperate[2];
                                        tLocalPlayer.Attack.SkillXpValue = tempval;
                                        break;
                                    case 2:
                                        lbl_defencelvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Defence.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Defence.SkillRank = tempseperate[0];
                                        tLocalPlayer.Defence.SkillXp = tempseperate[2];
                                        tLocalPlayer.Defence.SkillXpValue = tempval;
                                        break;
                                    case 3:
                                        lbl_strengthlvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Strength.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Strength.SkillRank = tempseperate[0];
                                        tLocalPlayer.Strength.SkillXp = tempseperate[2];
                                        tLocalPlayer.Strength.SkillXpValue = tempval;
                                        break;
                                    case 4:
                                        lbl_hplvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Constitution.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Constitution.SkillRank = tempseperate[0];
                                        tLocalPlayer.Constitution.SkillXp = tempseperate[2];
                                        tLocalPlayer.Constitution.SkillXpValue = tempval;
                                        break;
                                    case 5:
                                        lbl_rangelvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Ranged.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Ranged.SkillRank = tempseperate[0];
                                        tLocalPlayer.Ranged.SkillXp = tempseperate[2];
                                        tLocalPlayer.Ranged.SkillXpValue = tempval;
                                        break;
                                    case 6:
                                        lbl_prayerlvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Prayer.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Prayer.SkillRank = tempseperate[0];
                                        tLocalPlayer.Prayer.SkillXp = tempseperate[2];
                                        tLocalPlayer.Prayer.SkillXpValue = tempval;
                                        break;
                                    case 7:
                                        lbl_magiclvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Magic.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Magic.SkillRank = tempseperate[0];
                                        tLocalPlayer.Magic.SkillXp = tempseperate[2];
                                        tLocalPlayer.Magic.SkillXpValue = tempval;
                                        break;
                                    case 8:
                                        lbl_cooklvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Cooking.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Cooking.SkillRank = tempseperate[0];
                                        tLocalPlayer.Cooking.SkillXp = tempseperate[2];
                                        tLocalPlayer.Cooking.SkillXpValue = tempval;
                                        break;
                                    case 9:
                                        lbl_wclvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Woodcutting.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Woodcutting.SkillRank = tempseperate[0];
                                        tLocalPlayer.Woodcutting.SkillXp = tempseperate[2];
                                        tLocalPlayer.Woodcutting.SkillXpValue = tempval;
                                        break;
                                    case 10:
                                        lbl_fletchinglvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Fletching.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Fletching.SkillRank = tempseperate[0];
                                        tLocalPlayer.Fletching.SkillXp = tempseperate[2];
                                        tLocalPlayer.Fletching.SkillXpValue = tempval;
                                        break;
                                    case 11:
                                        lbl_fishinglvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Fishing.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Fishing.SkillRank = tempseperate[0];
                                        tLocalPlayer.Fishing.SkillXp = tempseperate[2];
                                        tLocalPlayer.Fishing.SkillXpValue = tempval;
                                        break;
                                    case 12:
                                        lbl_fmlvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Firemaking.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Firemaking.SkillRank = tempseperate[0];
                                        tLocalPlayer.Firemaking.SkillXp = tempseperate[2];
                                        tLocalPlayer.Firemaking.SkillXpValue = tempval;
                                        break;
                                    case 13:
                                        lbl_craftlvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Crafting.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Crafting.SkillRank = tempseperate[0];
                                        tLocalPlayer.Crafting.SkillXp = tempseperate[2];
                                        tLocalPlayer.Crafting.SkillXpValue = tempval;
                                        break;
                                    case 14:
                                        lbl_smithlvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Smithing.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Smithing.SkillRank = tempseperate[0];
                                        tLocalPlayer.Smithing.SkillXp = tempseperate[2];
                                        tLocalPlayer.Smithing.SkillXpValue = tempval;
                                        break;
                                    case 15:
                                        lbl_mininglvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Mining.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Mining.SkillRank = tempseperate[0];
                                        tLocalPlayer.Mining.SkillXp = tempseperate[2];
                                        tLocalPlayer.Mining.SkillXpValue = tempval;
                                        break;
                                    case 16:
                                        lbl_herblvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Herblore.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Herblore.SkillRank = tempseperate[0];
                                        tLocalPlayer.Herblore.SkillXp = tempseperate[2];
                                        tLocalPlayer.Herblore.SkillXpValue = tempval;
                                        break;
                                    case 17:
                                        lbl_agilitylvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Agility.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Agility.SkillRank = tempseperate[0];
                                        tLocalPlayer.Agility.SkillXp = tempseperate[2];
                                        tLocalPlayer.Agility.SkillXpValue = tempval;
                                        break;
                                    case 18:
                                        lbl_thieflvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Thieving.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Thieving.SkillRank = tempseperate[0];
                                        tLocalPlayer.Thieving.SkillXp = tempseperate[2];
                                        tLocalPlayer.Thieving.SkillXpValue = tempval;
                                        break;
                                    case 19:
                                        lbl_slayerlvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Slayer.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Slayer.SkillRank = tempseperate[0];
                                        tLocalPlayer.Slayer.SkillXp = tempseperate[2];
                                        tLocalPlayer.Slayer.SkillXpValue = tempval;
                                        break;
                                    case 20:
                                        lbl_farminglvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Farming.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Farming.SkillRank = tempseperate[0];
                                        tLocalPlayer.Farming.SkillXp = tempseperate[2];
                                        tLocalPlayer.Farming.SkillXpValue = tempval;
                                        break;
                                    case 21:
                                        lbl_rclvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Runecrafting.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Runecrafting.SkillRank = tempseperate[0];
                                        tLocalPlayer.Runecrafting.SkillXp = tempseperate[2];
                                        tLocalPlayer.Runecrafting.SkillXpValue = tempval;
                                        break;
                                    case 22:
                                        lbl_hunterlvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Hunter.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Hunter.SkillRank = tempseperate[0];
                                        tLocalPlayer.Hunter.SkillXp = tempseperate[2];
                                        tLocalPlayer.Hunter.SkillXpValue = tempval;
                                        break;
                                    case 23:
                                        lbl_conlvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Construction.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Construction.SkillRank = tempseperate[0];
                                        tLocalPlayer.Construction.SkillXp = tempseperate[2];
                                        tLocalPlayer.Construction.SkillXpValue = tempval;
                                        break;
                                    case 24:
                                        lbl_summlvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Summoning.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Summoning.SkillRank = tempseperate[0];
                                        tLocalPlayer.Summoning.SkillXp = tempseperate[2];
                                        tLocalPlayer.Summoning.SkillXpValue = tempval;
                                        break;
                                    case 25:
                                        lbl_dunglvl.Text = "      : " + tempseperate[1];
                                        tLocalPlayer.Dungeoneering.SkillLevel = tempseperate[1];
                                        tLocalPlayer.Dungeoneering.SkillRank = tempseperate[0];
                                        tLocalPlayer.Dungeoneering.SkillXp = tempseperate[2];
                                        tLocalPlayer.Dungeoneering.SkillXpValue = tempval;
                                        break;
                                }
                                runs++;
                            }
                            statstream.Close();
                            sockread = null;
                        }
                    }
                }
                catch { }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            btn_back.Enabled = false;
            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).GoBack();
            maintab.SelectedIndex = 0;
            btn_back.Enabled = true;
        }

        private void btn_forward_Click(object sender, EventArgs e)
        {
            btn_forward.Enabled = false;
            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).GoForward();
            maintab.SelectedIndex = 0;
            btn_forward.Enabled = true;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            btn_add.Enabled = false;

            WebBrowser Browser = new WebBrowser();
            tabControl2.TabPages.Add("New Page");
            tabControl2.SelectTab(page_number);
            Browser.Name = "Web Browser";
            Browser.Dock = DockStyle.Fill;
            Browser.ScriptErrorsSuppressed = true;
            Browser.Navigated += new WebBrowserNavigatedEventHandler(Navigated);
            Browser.NewWindow += new System.ComponentModel.CancelEventHandler(this.Browser1_NewWindow);
            Browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(Done);
            tabControl2.SelectedTab.Controls.Add(Browser);
            page_number++;
            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).GoHome();
            btn_add.Enabled = true;
        }

        private void btn_close_web_Click(object sender, EventArgs e)
        {
            btn_close_web.Enabled = false;
            if (!(tabControl2.TabPages.Count == 1))
            {
                tabControl2.TabPages.RemoveAt(tabControl2.SelectedIndex);
                tabControl2.SelectTab(tabControl2.TabPages.Count - 1);
                page_number--;
            }
            btn_close_web.Enabled = true;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            btn_start.Enabled = false;
            tmr_stopwatch.Enabled = true;
            btn_start.Enabled = true;
        }

        private void tmr_stopwatch_Tick(object sender, EventArgs e)
        {
            string secs = null;
            string mins = null;
            string hrs = null;

            seconds++;
            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
            }
            if (minutes >= 60)
            {
                hours++;
                minutes = 0;
            }

            if (seconds < 10)
            {
                secs = "0" + Convert.ToString(seconds);
            }
            else
            {
                secs = Convert.ToString(seconds);
            }

            if (minutes < 10)
            {
                mins = "0" + Convert.ToString(minutes);
            }
            else
            {
                mins = Convert.ToString(minutes);
            }

            if (hours < 10)
            {
                hrs = "0" + Convert.ToString(hours);
            }
            else
            {
                hrs = Convert.ToString(hours);
            }

            lbl_stopwatch.Text = Convert.ToString(hrs) + ":" + Convert.ToString(mins) + ":" + Convert.ToString(secs);
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            btn_stop.Enabled = false;
            tmr_stopwatch.Enabled = false;
            btn_stop.Enabled = true;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            btn_reset.Enabled = false;
            seconds = 0;
            minutes = 0;
            hours = 0;

            string secs = null;
            string mins = null;
            string hrs = null;

            if (seconds < 10)
            {
                secs = "0" + Convert.ToString(seconds);
            }
            else
            {
                secs = Convert.ToString(seconds);
            }

            if (minutes < 10)
            {
                mins = "0" + Convert.ToString(minutes);
            }
            else
            {
                mins = Convert.ToString(minutes);
            }

            if (hours < 10)
            {
                hrs = "0" + Convert.ToString(hours);
            }
            else
            {
                hrs = Convert.ToString(hours);
            }

            lbl_stopwatch.Text = Convert.ToString(hrs) + ":" + Convert.ToString(mins) + ":" + Convert.ToString(secs);
            secs = null;
            mins = null;
            hrs = null;
            btn_reset.Enabled = true;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            searchprice();
        }

        private void searchprice()
        {
            try
            {
                string item = txt_item.Text;
                item = item.Replace("  ", " ");
                item = item.Replace(" ", "_");
                item = item.Replace("'", "%27");

                string html = RandysStringFunctions.GetHtmlContent("http://runescape.wikia.com/wiki/" + item);
                Match rtnmarketprice = Regex.Match(html, @"(?<=GEItem.*span>).*[0-9]", RegexOptions.IgnoreCase);
                Match rtnalchprice = Regex.Match(html, @"(?<=<td>.*)[0-9,]+(?=.*160;coins)", RegexOptions.IgnoreCase);
                Match rtnitemimage = Regex.Match(html, @"(?<=noscript><img.*A detailed image of.*src="")http://images.*(?="" class=)", RegexOptions.IgnoreCase);

                string marketprice = rtnmarketprice.ToString();
                txt_pricelookup.Text = marketprice;
                marketprice = marketprice.Replace(",", "");
                int itemprice = Convert.ToInt32(marketprice);

                int alchprice = 0;
                txt_alch.Text = rtnalchprice.ToString();
                string stralchprice = rtnalchprice.ToString();
                stralchprice = stralchprice.Replace(",", "");
                alchprice = Convert.ToInt32(stralchprice);

                WebRequest requestPic = WebRequest.Create(rtnitemimage.ToString());
                WebResponse responsePic = requestPic.GetResponse();
                Image image = Image.FromStream(responsePic.GetResponseStream());
                pic_priceitem.Image = image;
                priceitem = rtnitemimage.ToString();


                if (natprice == 0)
                {
                    html = RandysStringFunctions.GetHtmlContent("http://services.runescape.com/m=itemdb_rs/Nature_rune/viewitem.ws?obj=561");
                    Match rtnnatprice = Regex.Match(html, @"(?<=<td>)[0-9]+");
                    natprice = Convert.ToInt16(rtnnatprice.ToString());
                }

                txt_natprice.Text = natprice.ToString("#,##0");
                string strprofit = ((alchprice - itemprice) - natprice).ToString("#,##0");
                txt_profit.Text = strprofit;
            }
            catch { }
        }

        private void btn_read_Click(object sender, EventArgs e)
        {
            readmanga();
        }

        private void readmanga()
        {
            try
            {
                try
                {
                    chapter = txt_chapter.Text;
                    page = txt_page.Text;
                    switch_complete = "Read Manga";
                    bw.RunWorkerAsync("Read Manga");
                    btn_peviousmanga.Visible = true;
                    btn_nextmanga.Visible = true;
                    netxtPageToolStripMenuItem.Enabled = true;
                    previousPageToolStripMenuItem.Enabled = true;
                }
                catch
                {
                    pic_manga0.Image = null;
                    title = "";
                    originaltitle = "";
                    chapter = "1";
                    page = "1";
                    txt_title.Text = "";
                    txt_chapter.Text = "";
                    txt_page.Text = "";
                    btn_read.Enabled = true;
                    btn_peviousmanga.Visible = false;
                    btn_nextmanga.Visible = false;
                    MessageBox.Show("Error Loading manga.. Sorry");
                }
            }
            catch { }
        }

        private void manga_page()
        {
            try
            {
                Match rtnstr = Regex.Match(RandysStringFunctions.GetHtmlContent("http://manga.animea.net" + title + "-chapter-" + chapter + "-page-1.html"), @"(?<=Page 1 of )[0-9]+", RegexOptions.Compiled);
                mangachappages = Convert.ToInt16(rtnstr.Value);
                if (Convert.ToInt32(page) > mangachappages)
                {
                    chapter = Convert.ToString(Convert.ToInt32(chapter) + 1);
                    page = "1";
                }
                Match tmp = Regex.Match(RandysStringFunctions.GetHtmlContent("http://manga.animea.net" + title + "-chapter-" + chapter + "-page-" + page + ".html"), @"(?<=-chapter-.*-page-.*><img.*src="".*onerror=.*src=').*(?='"" class=""mangaimg)", RegexOptions.Compiled);
                mangapicurl = tmp.ToString();
            }
            catch { }
        }

        private void btn_peviousmanga_Click(object sender, EventArgs e)
        {
            try
            {
                txt_page.Text = Convert.ToString(Convert.ToInt16(page) - 1);
                page = Convert.ToString(Convert.ToInt32(page) - 1);

                if (Convert.ToInt32(page) < 1)
                {
                    page = "1";
                    txt_page.Text = "1";
                }
                else
                {
                    switch_complete = "Read Manga";
                    bw.RunWorkerAsync("Read Manga");
                }
            }
            catch { }
        }

        private void btn_nextmanga_Click(object sender, EventArgs e)
        {
            try
            {
                txt_page.Text = Convert.ToString(Convert.ToInt16(page) + 1);
                page = Convert.ToString(Convert.ToInt16(page) + 1);

                switch_complete = "Read Manga";
                bw.RunWorkerAsync("Read Manga");
            }
            catch { }
        }

        private void btn_searchmanga_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Manga";
                bw.RunWorkerAsync("Manga");
            }
            catch { }
        }

        private void searchmanga()
        {
            try
            {
                MangaURL.Clear();
                MangaTitle.Clear();
                string searchhtml = RandysStringFunctions.GetHtmlContent("http://manga.animea.net/search.html?title=" + RandysStringFunctions.MakeStringAlphaNumeric(txt_searchmanga.Text) + "&completed=0&yor_range=0&yor=&type=any&author=&artist=&genre%5BAction%5D=0&genre%5BAdventure%5D=0&genre%5BComedy%5D=0&genre%5BDoujinshi%5D=0&genre%5BDrama%5D=0&genre%5BEcchi%5D=0&genre%5BFantasy%5D=0&genre%5BGender_Bender%5D=0&genre%5BHarem%5D=0&genre%5BHistorical%5D=0&genre%5BHorror%5D=0&genre%5BJosei%5D=0&genre%5BMartial_Arts%5D=0&genre%5BMature%5D=0&genre%5BMecha%5D=0&genre%5BMystery%5D=0&genre%5BPsychological%5D=0&genre%5BRomance%5D=0&genre%5BSchool_Life%5D=0&genre%5BSci-fi%5D=0&genre%5BSeinen%5D=0&genre%5BShotacon%5D=0&genre%5BShoujo%5D=0&genre%5BShoujo_Ai%5D=0&genre%5BShounen%5D=0&genre%5BShounen_Ai%5D=0&genre%5BSlice_of_Life%5D=0&genre%5BSmut%5D=0&genre%5BSports%5D=0&genre%5BSupernatural%5D=0&genre%5BTragedy%5D=0&genre%5BYaoi%5D=0&genre%5BYuri%5D=0#results");
                MatchCollection rtnstr = Regex.Matches(searchhtml, @"(?<=<a href="").*(?=[.]html""><img.*src=.*manga_img)");
                MatchCollection rtntitle = Regex.Matches(searchhtml, @"(?<=<a href=.*class=.*manga_title.*"">).*(?=</a>)");
                for (int runs = 0; runs < rtnstr.Count; runs++)
                {
                    MangaURL.Add(rtnstr[runs].Value);
                    MangaTitle.Add(RandysStringFunctions.Removetags(rtntitle[runs].Value));
                }
            }
            catch
            {
                MessageBox.Show("Error loading search information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_url_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                gotourl();
            }
        }

        private void gotourl()
        {
            char Flag = ' ';
            WebClient checkurl = new WebClient();

            try
            {
                if ((txt_ur.Text).Contains("http://") == true)
                {
                    string temp = "";
                    temp = txt_ur.Text;
                    Flag = (temp)[(temp).Length - 1];
                    if (Flag == '/')
                    {
                        try
                        {
                            string stream = checkurl.DownloadString(txt_ur.Text);
                            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(txt_ur.Text);
                            string save = txt_ur.Text;
                            save = save.Replace("http://", "");
                            save = save.Replace("www.", "");
                            save = save.Replace("/", "");
                            StreamWriter hw = File.AppendText(Config.Historyfile);
                            hw.WriteLine(save);
                            hw.Close();
                            if (!txt_ur.Items.Contains(save))
                            {
                                txt_ur.Items.Add(save);
                            }
                            maintab.SelectedIndex = 0;
                        }
                        catch
                        {
                            string chang = txt_ur.Text;
                            chang = chang.Replace("  ", " ");
                            chang = chang.Replace(" ", "+");
                            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri("http://www.google.com/webhp?hl=en#sclient=psy&hl=en&site=webhp&source=hp&q=" + chang + "&aq=f&aqi=g-e1g4&aql=&oq=&pbx=1&bav=on.2,or.r_gc.r_pw.&fp=2be2441b25e78025"));
                            maintab.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            string stream = checkurl.DownloadString(txt_ur.Text + "/");
                            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(txt_ur.Text + "/");
                            string save = txt_ur.Text;
                            save = save.Replace("http://", "");
                            save = save.Replace("www.", "");
                            save = save.Replace("/", "");
                            StreamWriter hw = File.AppendText(Config.Historyfile);
                            hw.WriteLine(save);
                            hw.Close();
                            if (!txt_ur.Items.Contains(save))
                            {
                                txt_ur.Items.Add(save);
                            }
                            maintab.SelectedIndex = 0;
                        }
                        catch
                        {
                            string chang = txt_ur.Text;
                            chang = chang.Replace("  ", " ");
                            chang = chang.Replace(" ", "+");
                            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri("http://www.google.com/webhp?hl=en#sclient=psy&hl=en&site=webhp&source=hp&q=" + chang + "&aq=f&aqi=g-e1g4&aql=&oq=&pbx=1&bav=on.2,or.r_gc.r_pw.&fp=2be2441b25e78025"));
                            maintab.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    string temp = "";
                    temp = "http://" + txt_ur.Text;
                    Flag = (temp)[(temp).Length - 1];
                    if (Flag == '/')
                    {
                        try
                        {
                            string stream = checkurl.DownloadString("http://" + txt_ur.Text);
                            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate("http://" + txt_ur.Text);
                            string save = txt_ur.Text;
                            save = save.Replace("http://", "");
                            save = save.Replace("www.", "");
                            save = save.Replace("/", "");
                            StreamWriter hw = File.AppendText(Config.Historyfile);
                            hw.WriteLine(save);
                            hw.Close();
                            if (!txt_ur.Items.Contains(save))
                            {
                                txt_ur.Items.Add(save);
                            }
                            maintab.SelectedIndex = 0;
                        }
                        catch
                        {
                            string chang = txt_ur.Text;
                            chang = chang.Replace("  ", " ");
                            chang = chang.Replace(" ", "+");
                            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri("http://www.google.com/webhp?hl=en#sclient=psy&hl=en&site=webhp&source=hp&q=" + chang + "&aq=f&aqi=g-e1g4&aql=&oq=&pbx=1&bav=on.2,or.r_gc.r_pw.&fp=2be2441b25e78025"));
                            maintab.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            string stream = checkurl.DownloadString("http://" + txt_ur.Text + "/");
                            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate("http://" + txt_ur.Text + "/");
                            string save = txt_ur.Text;
                            save = save.Replace("http://", "");
                            save = save.Replace("www.", "");
                            save = save.Replace("/", "");
                            StreamWriter hw = File.AppendText(Config.Historyfile);
                            hw.WriteLine(save);
                            hw.Close();
                            if (!txt_ur.Items.Contains(save))
                            {
                                txt_ur.Items.Add(save);
                            }
                            maintab.SelectedIndex = 0;
                        }
                        catch
                        {
                            if (chk_lucky.Checked == false)
                            {
                                string chang = txt_ur.Text;
                                chang = chang.Replace("  ", " ");
                                chang = chang.Replace(" ", "+");
                                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri("http://www.google.com/webhp?hl=en#sclient=psy&hl=en&site=webhp&source=hp&q=" + chang + "&aq=f&aqi=g-e1g4&aql=&oq=&pbx=1&bav=on.2,or.r_gc.r_pw.&fp=2be2441b25e78025"));
                                maintab.SelectedIndex = 0;
                            }
                            else
                            {
                                string chang = txt_ur.Text;
                                chang = chang.Replace("  ", " ");
                                chang = chang.Replace(" ", "+");

                                Match rtnstr = Regex.Match(RandysStringFunctions.GetHtmlContent("http://www.gigablast.com/search?k1l=573404&q=" + chang), @"(?<=<a href=).*(?=><font)", RegexOptions.Compiled);
                                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri(rtnstr.Value));
                            }
                        }
                    }
                }
            }
            catch
            {
                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri(((WebBrowser)tabControl2.SelectedTab.Controls[0]).Url.ToString()));
                StreamWriter sw = File.CreateText(Config.Browserfile);
                maintab.SelectedIndex = 0;
            }
        }

        private void txt_user_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                highscoreslookup(txt_user.Text);
            }
        }

        private void txt_item_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                searchprice();
            }
        }

        private void txt_title_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                readmanga();
            }
        }

        private void txt_searchmanga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && bw.IsBusy != true)
            {
                switch_complete = "Manga";
                bw.RunWorkerAsync("Manga");
            }
        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                txt_page.Text = Convert.ToString(Convert.ToInt16(page) - 1);
                page = Convert.ToString(Convert.ToInt16(page) - 1);
                if (Convert.ToInt64(page) < 1)
                {
                    page = "1";
                    txt_page.Text = "1";
                }

                manga_page();
            }
            else
                if (e.KeyCode == Keys.Right)
                {
                    txt_page.Text = Convert.ToString(Convert.ToInt16(page) + 1);
                    page = Convert.ToString(Convert.ToInt16(page) + 1);

                    manga_page();
                }
        }

        private void btn_p2p_Click(object sender, EventArgs e)
        {
            if (playtype == "" || playtype == "http://www.runescape.com/game.ws?j=1")
            {
                playtype = "http://www.runescape.com/game.ws?j=1";
                play_game(playtype, 22);
            }
            else
            {
                play_game(playtype, 0);
            }

        }

        private void play_game(string play, int p)
        {
            int dis = splitContainer1.SplitterDistance;
            Size websize = webBrowser2.Size;
            splitContainer1.SplitterDistance = websize.Width;
            webBrowser2.Navigate(play);
            webBrowser2.Size = new System.Drawing.Size(dis + p, websize.Height + 27);
            if (!txt_news.IsDisposed)
            {
                txt_news.Dispose();
                btn_p2p.Dispose();
                web_ad.Dispose();
                pictureBox2.Dispose();
                tmr_blockads.Enabled = false;
                webBrowser2.Visible = true;
            }
            splitContainer1.SplitterDistance = dis;
        }

        private void lbl_overall_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Overall";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Overall.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Overall.SkillXp).ToString("#,##0");
            }
            catch { }
        }

        private void lbl_attacklvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Attack";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Attack.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Attack.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Attack.SkillXpValue;
            }
            catch { }
        }

        private void lbl_defencelvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Defence";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Defence.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Defence.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Defence.SkillXpValue;
            }
            catch { }
        }

        private void lbl_strengthlvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Strength";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Strength.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Strength.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Strength.SkillXpValue;
            }
            catch { }
        }

        private void lbl_hplvl_MouseHover(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Constitution";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Constitution.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Constitution.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Constitution.SkillXpValue;
            }
            catch { }
        }

        private void lbl_rangelvl_MouseHover(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Ranged";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Ranged.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Ranged.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Ranged.SkillXpValue;
            }
            catch { }
        }

        private void lbl_prayerlvl_MouseHover(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Prayer";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Prayer.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Prayer.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Prayer.SkillXpValue;
            }
            catch { }
        }

        private void lbl_magiclvl_MouseHover(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Magic";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Magic.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Magic.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Magic.SkillXpValue;
            }
            catch { }
        }

        private void lbl_cooklvl_MouseHover(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Cooking";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Cooking.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Cooking.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Cooking.SkillXpValue;
            }
            catch { }
        }

        private void lbl_wclvl_MouseHover(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Woodcutting";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Woodcutting.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Woodcutting.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Woodcutting.SkillXpValue;
            }
            catch { }
        }

        private void lbl_fletchinglvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Fletching";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Fletching.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Fletching.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Fletching.SkillXpValue;
            }
            catch { }
        }

        private void lbl_fishinglvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Fishing";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Fishing.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Fishing.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Fishing.SkillXpValue;
            }
            catch { }
        }

        private void lbl_fmlvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Firemaking";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Firemaking.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Firemaking.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Firemaking.SkillXpValue;
            }
            catch { }
        }

        private void lbl_craftlvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Crafting";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Crafting.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Crafting.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Crafting.SkillXpValue;
            }
            catch { }
        }

        private void lbl_smithlvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Smithing";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Smithing.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Smithing.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Smithing.SkillXpValue;
            }
            catch { }
        }

        private void lbl_mininglvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Mining";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Mining.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Mining.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Mining.SkillXpValue;
            }
            catch { }
        }

        private void lbl_herblvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Herblore";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Herblore.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Herblore.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Herblore.SkillXpValue;
            }
            catch { }
        }

        private void lbl_agilitylvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Agility";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Agility.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Agility.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Agility.SkillXpValue;
            }
            catch { }
        }

        private void lbl_thieflvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Thieving";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Thieving.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Thieving.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Thieving.SkillXpValue;
            }
            catch { }
        }

        private void lbl_slayerlvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Slayer";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Slayer.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Slayer.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Slayer.SkillXpValue;
            }
            catch { }
        }

        private void lbl_farminglvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Farming";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Farming.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Farming.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Farming.SkillXpValue;
            }
            catch { }
        }

        private void lbl_rclvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Runecrafting";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Runecrafting.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Runecrafting.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Runecrafting.SkillXpValue;
            }
            catch { }
        }

        private void lbl_hunterlvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Hunter";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Hunter.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Hunter.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Hunter.SkillXpValue;
            }
            catch { }
        }

        private void lbl_conlvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Construction";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Construction.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Construction.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Construction.SkillXpValue;
            }
            catch { }
        }

        private void lbl_summlvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Summoning";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Summoning.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Summoning.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Summoning.SkillXpValue;
            }
            catch { }
        }

        private void lbl_dunglvl_MouseEnter(object sender, EventArgs e)
        {
            lbl_skillname.Text = "Skill: Dungeoneering";
            try
            {
                lbl_rank.Text = "Rank: " + Convert.ToInt32(tLocalPlayer.Dungeoneering.SkillRank).ToString("#,##0");
                lbl_xp.Text = "Experience:" + Convert.ToInt32(tLocalPlayer.Dungeoneering.SkillXp).ToString("#,##0");
                xptolvlbar.Value = tLocalPlayer.Dungeoneering.SkillXpValue;
            }
            catch { }
        }

        private void lst_manga_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_title.Text = lst_manga.Items[lst_manga.SelectedIndex].ToString();
                originaltitle = lst_manga.Items[lst_manga.SelectedIndex].ToString();
                title = MangaURL[lst_manga.SelectedIndex];
            }
            catch { }
        }

        private void btn_setlink_Click(object sender, EventArgs e)
        {
            linkurl();
        }

        private void btn_launchext_Click(object sender, EventArgs e)
        {
            externallaunch();
        }

        private void btn_extfile_Click(object sender, EventArgs e)
        {
            extfile();
        }

        private void extfile()
        {
            if (txt_extname.Text != "")
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                string filepath = ofd.FileName;
                lst_external.Items.Add(txt_extname.Text);
                lst_loc.Items.Add(filepath);
                Extnums++;
            }
        }

        private void txt_extname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                extfile();
            }
        }

        private void btn_extremove_Click(object sender, EventArgs e)
        {
            if (lst_external.Items.Count != 0 && lst_external.SelectedIndex > -1)
            {
                int ii = lst_external.SelectedIndex;
                lst_external.Items.RemoveAt(ii);
                lst_loc.Items.RemoveAt(ii);
                if (Extnums != 0)
                {
                    Extnums--;
                }
            }
        }

        private void btn_searchsite_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Video";
                bw.RunWorkerAsync("Video");
            }
            catch { }
        }

        private void setupvid(string url, string titleregex, string urlregex, string imageregex, string[] strRemove)
        {
            string html = RandysStringFunctions.GetHtmlContent(url);
            foreach (string str in strRemove)
            {
                html = html.Replace(str, "");
            }
            MatchCollection rtntitle = Regex.Matches(html, titleregex, RegexOptions.IgnoreCase);
            MatchCollection rtnpath = Regex.Matches(html, urlregex, RegexOptions.IgnoreCase);
            MatchCollection rtnimage = Regex.Matches(html, imageregex, RegexOptions.IgnoreCase);
            VidUrl.Clear();
            VidPic.Clear();
            VidTitle.Clear();
            int[] numbers = new int[] { rtntitle.Count, rtnpath.Count, rtnimage.Count };
            int minNumber = numbers.Min();
            for (int runs = 0; runs < minNumber; runs++)
            {
                VidTitle.Add(RandysStringFunctions.Removetags(rtntitle[runs].ToString()));
                VidUrl.Add(rtnpath[runs].ToString());
                if (!rtnimage[runs].ToString().Contains("http://"))
                {
                    VidPic.Add("http://" + rtnimage[runs].ToString());
                }
                else
                {
                    VidPic.Add(rtnimage[runs].ToString());
                }
            }
        }

        private void setupvid(string url, string titleregex, string urlregex)
        {
            string html = RandysStringFunctions.GetHtmlContent(url);
            MatchCollection rtntitle = Regex.Matches(html, titleregex, RegexOptions.IgnoreCase);
            MatchCollection rtnpath = Regex.Matches(html, urlregex, RegexOptions.IgnoreCase);
            VidUrl.Clear();
            VidPic.Clear();
            VidTitle.Clear();
            int[] numbers = new int[] { rtntitle.Count, rtnpath.Count };
            int minNumber = numbers.Min();
            for (int runs = 0; runs < minNumber; runs++)
            {
                VidTitle.Add(RandysStringFunctions.Removetags(rtntitle[runs].ToString()));
                VidUrl.Add(rtnpath[runs].ToString());
            }
        }

        private void setupvid(string url, string titleregex, string urlregex, string imageregex)
        {
            string html = RandysStringFunctions.GetHtmlContent(url);
            MatchCollection rtntitle = Regex.Matches(html, titleregex, RegexOptions.IgnoreCase);
            MatchCollection rtnpath = Regex.Matches(html, urlregex, RegexOptions.IgnoreCase);
            MatchCollection rtnimage = Regex.Matches(html, imageregex, RegexOptions.IgnoreCase);
            VidUrl.Clear();
            VidPic.Clear();
            VidTitle.Clear();
            int[] numbers = new int[] { rtntitle.Count, rtnpath.Count, rtnimage.Count };
            int minNumber = numbers.Min();
            for (int runs = 0; runs < minNumber; runs++)
            {
                VidTitle.Add(RandysStringFunctions.Removetags(rtntitle[runs].ToString()));
                VidUrl.Add(rtnpath[runs].ToString());
                if (!rtnimage[runs].ToString().Contains("http://"))
                {
                    VidPic.Add("http://" + rtnimage[runs].ToString());
                }
                else
                {
                    VidPic.Add(rtnimage[runs].ToString());
                }
            }
        }

        private void searchsite()
        {
            if (rad_youtubesearch.Checked == true)
            {
                if (rad_ytvideosec.Checked == true)
                {
                    string search = txt_searchvids.Text;
                    search = search.Replace("  ", "+");
                    search = search.Replace(" ", "+");
                    setupvid("http://www.youtube.com/results?search_query=" + search, @"(?<=data-context-item-title="")[^.*""]+", @"(?<=item-id="")[^.*""]+", @"(?<=.*video-thumb ux-thumb yt-thumb-default-.*//)i.*[.]jpg(?=.*video-time)");
                    VidType = "Youtube";
                }
                else
                {
                    if (rad_ytchannelsec.Checked == true)
                    {
                        string search = txt_searchvids.Text;
                        search = search.Replace("  ", "+");
                        search = search.Replace(" ", "+");
                        setupvid("http://www.youtube.com/results?search_type=search_users&search_query=" + search + "&uni=3", @"(?<=/user/).*(?="" class=""ux-thumb-wrap)", @"/user/.*(?="" class=""ux-thumb-wrap)", @"(?<=thumb.*img src=""//).*[.][a-z]+");
                        VidType = "YoutubeChannel";
                    }
                }
            }
            else
                if (rad_showsearch.Checked == true)
                {
                    string search = txt_searchvids.Text;
                    search = search.Replace("  ", "%20");
                    search = search.Replace(" ", "%20");
                    setupvid("http://watchseries.eu/search/" + search, @"(?<=watch serie )[a-zA-Z0-9-:/. _]+(?=.*<img src=.*(.jpg))", @"(?<=<a href="")[a-zA-Z0-9-:/. _]+(?=.*<img src=.*(.jpg))", @"(?<=<img src="").*(.jpg)");
                    VidType = "Showp1";
                    int totalshows = VidUrl.Count;
                    int vidnums = 0;
                    VidSeasonlist.Clear();
                    VidEpisodelist.Clear();
                    while (totalshows > 0)
                    {
                        string html = RandysStringFunctions.GetHtmlContent(VidUrl[vidnums]);

                        rtnSeason = Regex.Matches(html, @"(?<=Season )[0-9]+", RegexOptions.Compiled);
                        string _season = "";
                        for (int i = 1; i < rtnSeason.Count; i++)
                        {
                            _season += rtnSeason[i].Value + ',';
                        }
                        _season = _season.Substring(0, _season.Length - 1);
                        VidSeasonlist.Add(_season);
                        string _episode = "";
                        rtnEpisode = Regex.Matches(html, @"(?<=Season .*)[0-9]+(?= episode)", RegexOptions.Compiled);
                        for (int i = 0; i < rtnEpisode.Count; i++)
                        {
                            _episode += rtnEpisode[i].Value + ',';
                        }
                        _episode = _episode.Substring(0, _episode.Length - 1);
                        VidEpisodelist.Add(_episode);
                        totalshows--;
                        vidnums++;
                    }
                }
                else if (rad_animesearch.Checked == true)
                {
                    string search = txt_searchvids.Text;
                    search = search.Replace("  ", "+");
                    search = search.Replace(" ", "+");
                    string[] replacestr = new string[2];
                    replacestr[0] = "</b>";
                    replacestr[1] = "<b>";
                    setupvid("http://www.lovemyanime.net/search.php?searchquery=" + search, @"(?<=search-page_in_box_mid_link.*>)[a-zA-Z0-9:/.! '-()-]+(?=<)", @"(?<=search-page_in_box_mid_link.*href="")[a-zA-Z0-9:/.! '-]+", @"(?<=><object data="")[a-zA-Z0-9:/.-]+", replacestr);
                    VidType = "Anime";
                }
                else if (rad_moviesearch.Checked == true)
                {
                    string search = txt_searchvids.Text;
                    search = search.Replace("  ", "+");
                    search = search.Replace(" ", "+");
                    string html = RandysStringFunctions.GetHtmlContent("http://movie25.com/search.php?key=" + search + "&submit=");
                    MatchCollection rtntitle = Regex.Matches(html, @"(?<=                  ).*(?=                  </a></h1>)", RegexOptions.Compiled);
                    MatchCollection rtnpath = Regex.Matches(html, @"(?<=<h1><a href="").*(?="" )", RegexOptions.Compiled);
                    MatchCollection rtnimage = Regex.Matches(html, @"(?<= <img src="").*jpg", RegexOptions.Compiled);
                    Match rtnrealpath;
                    Size lSize = webvideo.Size;
                    string tmp = "";
                    VidUrl.Clear();
                    VidTitle.Clear();
                    VidPic.Clear();
                    int[] numbers = new int[] { rtntitle.Count, rtnpath.Count, rtnimage.Count };
                    int minNumber = numbers.Min();
                    for (int runs = 0; runs < minNumber; runs++)
                    {
                        try
                        {
                            tmp = RandysStringFunctions.GetHtmlContent("http://movie25.com/" + rtnpath[runs].ToString());
                            if ((rtnrealpath = Regex.Match(tmp, @"(?<=url=http://www.putlocker.com/file/).*(?=','newwin','fullscreen=yes)", RegexOptions.Compiled)).Success && putlockerToolStripMenuItem1.CheckState == CheckState.Checked)
                            {
                                VidUrl.Add("http://www.putlocker.com/embed/" + rtnrealpath.Value);
                                VidTitle.Add(rtntitle[runs].ToString());
                                VidPic.Add(rtnimage[runs].ToString());
                            }
                            else if ((rtnrealpath = Regex.Match(tmp, @"(?<=url=http://www.sockshare.com/file/).*(?=','newwin','fullscreen=yes)", RegexOptions.Compiled)).Success && sockshareToolStripMenuItem.CheckState == CheckState.Checked)
                            {
                                VidUrl.Add("http://www.sockshare.com/embed/" + rtnrealpath.Value);
                                VidTitle.Add(rtntitle[runs].ToString());
                                VidPic.Add(rtnimage[runs].ToString());
                            }
                            else if ((rtnrealpath = Regex.Match(tmp, @"(?<=url=http://www.nowvideo.eu/video/).*(?=','newwin','fullscreen=yes)", RegexOptions.Compiled)).Success && nowvideoToolStripMenuItem1.CheckState == CheckState.Checked)
                            {
                                VidUrl.Add("http://embed.nowvideo.eu/embed.php?v=" + rtnrealpath.Value + "&width=" + lSize.Width.ToString() + "&height=" + lSize.Height.ToString());
                                VidTitle.Add(rtntitle[runs].ToString());
                                VidPic.Add(rtnimage[runs].ToString());
                            }
                            else if ((rtnrealpath = Regex.Match(tmp, @"(?<=url=http://dwn.so/v/).*(?=','newwin','fullscreen=yes)", RegexOptions.Compiled)).Success && dwnToolStripMenuItem.CheckState == CheckState.Checked)
                            {
                                VidUrl.Add("http://dwn.so/player/embed.php?v=" + rtnrealpath.Value + "&width=" + lSize.Width.ToString() + "&height=" + lSize.Height.ToString());
                                VidTitle.Add(rtntitle[runs].ToString());
                                VidPic.Add(rtnimage[runs].ToString());
                            }
                            else if ((rtnrealpath = Regex.Match(tmp, @"(?<=url=http://www.videoweed.es/file/).*(?=','newwin','fullscreen=yes)", RegexOptions.Compiled)).Success && videoweedToolStripMenuItem.CheckState == CheckState.Checked)
                            {
                                VidUrl.Add("http://embed.videoweed.es/embed.php?v=" + rtnrealpath.Value + "&width=" + lSize.Width.ToString() + "&height=" + lSize.Height.ToString());
                                VidTitle.Add(rtntitle[runs].ToString());
                                VidPic.Add(rtnimage[runs].ToString());
                            }
                        }
                        catch { }
                    }
                    if (minNumber > 0)
                    {
                        VidType = "Movie";
                    }
                }
        }

        private void btn_clearvidplayer_Click(object sender, EventArgs e)
        {
            webvideo.Url = null;
            pb_vidloading.Visible = false;
        }

        private void txt_searchvids_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    switch_complete = "Video";
                    bw.RunWorkerAsync("Video");
                }
                catch { }
            }
        }

        private void txt_linkurl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                linkurl();
            }
        }

        private void linkurl()
        {
            btn_setlink.Enabled = false;
            if (txt_linkurl.Text.Length > 7)
            {
                if (txt_linkurl.Text.Substring(0, 7) != "http://")
                {
                    txt_linkurl.Text = "http://" + txt_linkurl.Text;
                }
                if (rad_1.Checked == true)
                {
                    link1 = txt_linkurl.Text;
                    link1_string = txt_linkname.Text;
                    btn_link1.Text = txt_linkname.Text;
                }
                else
                    if (rad_2.Checked == true)
                    {
                        link2 = txt_linkurl.Text;
                        link2_string = txt_linkname.Text;
                        btn_link2.Text = txt_linkname.Text;
                    }
                    else
                        if (rad_3.Checked == true)
                        {
                            link3 = txt_linkurl.Text;
                            link3_string = txt_linkname.Text;
                            btn_link3.Text = txt_linkname.Text;
                        }
                        else
                            if (rad_4.Checked == true)
                            {
                                link4 = txt_linkurl.Text;
                                link4_string = txt_linkname.Text;
                                btn_link4.Text = txt_linkname.Text;
                            }
                            else
                                if (rad_5.Checked == true)
                                {
                                    link5 = txt_linkurl.Text;
                                    link5_string = txt_linkname.Text;
                                    btn_link5.Text = txt_linkname.Text;
                                }
                                else
                                    if (rad_6.Checked == true)
                                    {
                                        link6 = txt_linkurl.Text;
                                        link6_string = txt_linkname.Text;
                                        btn_link6.Text = txt_linkname.Text;
                                    }
            }
            btn_setlink.Enabled = true;
        }

        private void txt_user_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txt_user.Text = mainuser;
        }

        private void tmr_blockads_Tick(object sender, EventArgs e)
        {
            try
            {
                if (web_ad.Url.ToString() != "http://19ee8df1.linkbucks.com/")
                {
                    tmr_blockads.Enabled = false;
                    web_ad.Dispose();
                }
            }
            catch { }
        }

        private void txt_url_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txt_ur.Text = "";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void restoreDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter w = File.CreateText(Config.Historyfile);
            w.Close();

            w = File.CreateText(Config.Flashfile);
            w.Close();

            w = File.CreateText(Config.Vidfile);
            w.Close();

            w = File.CreateText(Config.Extfile);
            w.Close();

            w = File.CreateText(Config.Browserfile);
            w.WriteLine("1");
            w.Close();

            w = File.CreateText(Config.Favlinksfile);
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

            if (File.Exists(Config.Otherfile))
            {
                w = File.CreateText(Config.Otherfile);
                w.Close();
            }
            if (File.Exists(Config.Mangafile))
            {
                w = File.CreateText(Config.Mangafile);
                w.Close();
            }
            if (File.Exists(Config.Itemfile))
            {
                w = File.CreateText(Config.Itemfile);
                w.Close();
            }
            if (File.Exists(Config.Notesfile))
            {
                w = File.CreateText(Config.Notesfile);
                w.Close();
            }
        }

        private void hideUtilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hideUtilitiesToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                tabControl1.Visible = false;
                Size mainSize = maintab.Size;
                maintab.Size = new System.Drawing.Size(mainSize.Width, System.Windows.Forms.Form.ActiveForm.Size.Height - 62);
                try
                {
                    SetPositionSize(p.MainWindowHandle, 0, 0, pnl_gba.Width, pnl_gba.Height);
                    ShowWindow(p.MainWindowHandle.ToInt32(), SW_SHOWMAXIMIZED);
                }
                catch { }
                hideUtilitiesToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                tabControl1.Visible = true;
                Size mainSize = maintab.Size;
                maintab.Size = new System.Drawing.Size(mainSize.Width, System.Windows.Forms.Form.ActiveForm.Size.Height - 202);
                hideUtilitiesToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void netxtPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txt_page.Text = Convert.ToString(Convert.ToInt16(page) + 1);
                page = Convert.ToString(Convert.ToInt16(page) + 1);

                switch_complete = "Read Manga";
                bw.RunWorkerAsync("Read Manga");
            }
            catch { }
        }

        private void previousPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txt_page.Text = Convert.ToString(Convert.ToInt16(page) - 1);
                page = Convert.ToString(Convert.ToInt16(page) - 1);
                if (Convert.ToInt64(page) < 1)
                {
                    page = "1";
                    txt_page.Text = "1";
                }
                else
                {
                    switch_complete = "Read Manga";
                    bw.RunWorkerAsync("Read Manga");
                }
            }
            catch { }
        }

        private void loading()
        {
            string[] ScreenShot = Directory.GetFiles(Config.ScreenshotFolder);
            Screenshots = ScreenShot.Count();

            using (StreamReader read = new StreamReader(Config.Historyfile))
            {
                string hist = "";
                while ((hist = read.ReadLine()) != null)
                {
                    if (!txt_ur.Items.Contains(hist))
                    {
                        txt_ur.Items.Add(hist);
                    }
                }
            }
            string html = RandysStringFunctions.GetHtmlContent("http://services.runescape.com/m=news/");
            MatchCollection rtntitle = Regex.Matches(html, @"(?<=FlatHeader"">).*(?=</h4>)", RegexOptions.Compiled);
            MatchCollection rtnDetails = Regex.Matches(html, @"(?<=<div class=""Content"">(\n)).*", RegexOptions.Compiled);
            for (int runs = 0; runs < rtnDetails.Count; runs++)
            {
                txt_news.Text += RandysStringFunctions.Removetags(rtntitle[runs].Value) + Environment.NewLine;
                txt_news.Text += RandysStringFunctions.Removetags(rtnDetails[runs].Value) + Environment.NewLine + Environment.NewLine;
            }
            html = null;
            rtntitle = null;
            rtnDetails = null;

            using (StreamReader webvidr = new StreamReader(Config.Vidfile))
            {
                try
                {
                    webvideo.Navigate(new Uri(webvidr.ReadLine()));
                    txt_searchvids.Text = webvidr.ReadLine();
                    if (webvidr.ReadLine().ToLower() == "true")
                    {
                        rad_youtubesearch.Checked = true;
                        pnl_yt.Visible = true;
                    }
                    if (webvidr.ReadLine().ToLower() == "true")
                    {
                        rad_showsearch.Checked = true;
                        pnl_yt.Visible = false;
                    }
                    if (webvidr.ReadLine().ToLower() == "true")
                    {
                        rad_animesearch.Checked = true;
                        pnl_yt.Visible = false;
                    }
                    if (webvidr.ReadLine().ToLower() == "true")
                    {
                        rad_moviesearch.Checked = true;
                        pnl_yt.Visible = false;
                    }
                    VidType = webvidr.ReadLine();
                    if (VidType == "YoutubeChannel")
                    {
                        cmb_youtubechnfavs.Visible = true;
                        btn_gofav.Visible = true;
                        rad_ytchannelsec.Checked = true;
                    }
                    else
                    {
                        rad_ytvideosec.Checked = true;
                    }
                    if (webvidr.ReadLine().ToLower() == "true")
                    {
                        cmb_youtubechnfavs.Visible = true;
                        btn_gofav.Visible = true;
                    }
                    int total = Convert.ToInt16(webvidr.ReadLine()) - 1;
                    for (int i = 0; i <= total; i++)
                    {
                        ytFavTitle.Add(webvidr.ReadLine());
                        ytFavURL.Add(webvidr.ReadLine());
                        cmb_youtubechnfavs.Items.Add(ytFavTitle[i]);
                    }
                    string tempread = webvidr.ReadLine();
                    if (tempread != "True" && tempread != "False")
                    {
                        while (tempread != null)
                        {
                            lst_vids.Items.Add(tempread);
                            VidTitle.Add(tempread);
                            tempread = webvidr.ReadLine();
                            VidUrl.Add(tempread);
                            tempread = webvidr.ReadLine();
                            VidPic.Add(tempread);
                            tempread = webvidr.ReadLine();
                            if (tempread == "True" || tempread == "False")
                            {
                                break;
                            }
                        }
                    }
                    if (tempread == "True")
                    {
                        tempread = webvidr.ReadLine();
                        while (tempread != null)
                        {
                            VidSeasonlist.Add(tempread);
                            tempread = webvidr.ReadLine();
                            VidEpisodelist.Add(tempread);
                            tempread = webvidr.ReadLine();
                        }
                    }
                }
                catch { }
                webvidr.Close();
            }

            using (StreamReader webgamer = new StreamReader(Config.Flashfile))
            {
                try
                {
                    web_games.Navigate(new Uri(webgamer.ReadLine()));
                    txt_game.Text = webgamer.ReadLine();
                    string tempread = webgamer.ReadLine();
                    int countn = 0;
                    while (tempread != null)
                    {
                        GameTitle.Add(tempread);
                        tempread = webgamer.ReadLine();
                        GameEmbedURL.Add(tempread);
                        tempread = webgamer.ReadLine();
                        GameURL.Add(tempread);
                        tempread = webgamer.ReadLine();
                        GamePreview.Add(tempread);
                        lst_games.Items.Add(GameTitle[countn]);
                        tempread = webgamer.ReadLine();
                        countn++;
                    }
                }
                catch { }
                webgamer.Close();
            }

            string _tempread = "";
            using (StreamReader extr = new StreamReader(Config.Extfile))
            {
                while ((_tempread = extr.ReadLine()) != null)
                {
                    lst_external.Items.Add(_tempread);
                    _tempread = extr.ReadLine();
                    lst_loc.Items.Add(_tempread);
                    Extnums++;
                }
                extr.Close();
            }

            using (StreamReader linkr = new StreamReader(Config.Favlinksfile))
            {
                if (linkr.ReadLine() == "true")
                {
                    chk_lucky.Checked = true;
                }
                else
                {
                    chk_lucky.Checked = false;
                }
                link1 = linkr.ReadLine();
                link1_string = linkr.ReadLine();
                btn_link1.Text = link1_string;
                link2 = linkr.ReadLine();
                link2_string = linkr.ReadLine();
                btn_link2.Text = link2_string;
                link3 = linkr.ReadLine();
                link3_string = linkr.ReadLine();
                btn_link3.Text = link3_string;
                link4 = linkr.ReadLine();
                link4_string = linkr.ReadLine();
                btn_link4.Text = link4_string;
                link5 = linkr.ReadLine();
                link5_string = linkr.ReadLine();
                btn_link5.Text = link5_string;
                link6 = linkr.ReadLine();
                link6_string = linkr.ReadLine();
                btn_link6.Text = link6_string;
                linkr.Close();
            }

            using (StreamReader mangar = new StreamReader(Config.Mangafile))
            {
                mangapicurl = mangar.ReadLine();
                if (mangapicurl != null && mangapicurl != "")
                {
                    Control[] controls = this.Controls.Find("pic_manga0", true);
                    if (controls.Length == 1) // 0 means not found, more - there are several controls with the same name
                    {
                        PictureBox control = controls[0] as PictureBox;

                        if (control != null)
                        {
                            try
                            {
                                WebRequest requestPic = WebRequest.Create(mangapicurl);

                                WebResponse responsePic = requestPic.GetResponse();

                                Image img = Image.FromStream(responsePic.GetResponseStream());
                                control.Image = img;
                            }
                            catch { }
                        }
                    }
                    txt_title.Text = mangar.ReadLine();
                    originaltitle = txt_title.Text;
                    title = mangar.ReadLine();
                    txt_chapter.Text = mangar.ReadLine();
                    chapter = txt_chapter.Text;
                    txt_page.Text = mangar.ReadLine();
                    page = txt_page.Text;
                    if (page != "")
                    {
                        netxtPageToolStripMenuItem.Enabled = true;
                        previousPageToolStripMenuItem.Enabled = true;
                        btn_nextmanga.Visible = true;
                        btn_peviousmanga.Visible = true;
                    }
                }
                mangar.Close();
            }

            try
            {
                using (StreamReader nread = new StreamReader(Config.Notesfile))
                {
                    string t = "";
                    while ((t = nread.ReadLine()) != null)
                    {
                        txt_notes.Text += t + Environment.NewLine;
                    }
                    nread.Close();
                }
            }
            catch
            {
                txt_notes.Text = "";
            }

            try
            {
                using (StreamReader swread = new StreamReader(Config.Otherfile))
                {
                    string stopwatchtext = swread.ReadLine();
                    playtype = swread.ReadLine();
                    txt_user.Text = swread.ReadLine();
                    highscoreslookup(txt_user.Text);
                    if (swread.ReadLine() == "Yes")
                    {
                        splitContainer1.RightToLeft = RightToLeft.Yes;
                        menuStrip2.RightToLeft = RightToLeft.No;
                        maintab.RightToLeft = RightToLeft.No;
                        tabControl1.RightToLeft = RightToLeft.No;
                        txt_news.RightToLeft = RightToLeft.No;
                    }
                    try
                    {
                        int dis = Convert.ToInt32(swread.ReadLine());
                        if (dis != 0)
                        {
                            splitContainer1.SplitterDistance = dis;
                        }
                    }
                    catch { }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            stagevuToolStripMenuItem.Checked = true;
                            break;
                        case "False":
                            stagevuToolStripMenuItem.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            putlockerToolStripMenuItem.Checked = true;
                            break;
                        case "False":
                            putlockerToolStripMenuItem.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            gorillavidToolStripMenuItem.Checked = true;
                            break;
                        case "False":
                            gorillavidToolStripMenuItem.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            movshareToolStripMenuItem.Checked = true;
                            break;
                        case "False":
                            movshareToolStripMenuItem.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            nowvideoToolStripMenuItem.Checked = true;
                            break;
                        case "False":
                            nowvideoToolStripMenuItem.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            videoweedToolStripMenuItem1.Checked = true;
                            break;
                        case "False":
                            videoweedToolStripMenuItem1.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            uploadcToolStripMenuItem.Checked = true;
                            break;
                        case "False":
                            uploadcToolStripMenuItem.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            vidbullToolStripMenuItem.Checked = true;
                            break;
                        case "False":
                            vidbullToolStripMenuItem.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            iRCToolStripMenuItem.Checked = true;
                            splitContainer2.Panel2Collapsed = true;
                            break;
                        case "False":
                            iRCToolStripMenuItem.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            putlockerToolStripMenuItem1.Checked = true;
                            break;
                        case "False":
                            putlockerToolStripMenuItem1.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            sockshareToolStripMenuItem.Checked = true;
                            break;
                        case "False":
                            sockshareToolStripMenuItem.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            nowvideoToolStripMenuItem1.Checked = true;
                            break;
                        case "False":
                            nowvideoToolStripMenuItem1.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            dwnToolStripMenuItem.Checked = true;
                            break;
                        case "False":
                            dwnToolStripMenuItem.Checked = false;
                            break;
                    }

                    switch (swread.ReadLine().ToString())
                    {
                        case "True":
                            videoweedToolStripMenuItem.Checked = true;
                            break;
                        case "False":
                            videoweedToolStripMenuItem.Checked = false;
                            break;
                    }

                    twitteruser = swread.ReadLine().ToString();
                    txttwitteraccount.Text = twitteruser;

                    emailuser = swread.ReadLine().ToString();
                    txt_emailuser.Text = emailuser;
                    emailpass = swread.ReadLine().ToString();
                    txt_emailpass.Text = emailpass;

                    if (stopwatchtext != null)
                    {
                        lbl_stopwatch.Text = stopwatchtext;
                    }
                    else
                    {
                        lbl_stopwatch.Text = "00:00:00";
                    }
                    string[] arr_time = new string[3];
                    arr_time = (lbl_stopwatch.Text).Split(":".ToCharArray());
                    swread.Close();
                    if (arr_time[0].Contains("00"))
                    {
                        hours = 0;
                    }
                    else
                    {
                        if (arr_time[0].Contains("0"))
                        {
                            hours = Convert.ToInt16(arr_time[0].Replace("0", ""));
                        }
                        else
                        {
                            hours = Convert.ToInt16(arr_time[0]);
                        }
                    }

                    if (arr_time[1].Contains("00"))
                    {
                        minutes = 0;
                    }
                    else
                    {
                        if (arr_time[1].Contains("0"))
                        {
                            minutes = Convert.ToInt16(arr_time[1].Replace("0", ""));
                        }
                        else
                        {
                            minutes = Convert.ToInt16(arr_time[1]);
                        }
                    }

                    if (arr_time[2].Contains("00"))
                    {
                        seconds = 0;
                    }
                    else
                    {
                        if (arr_time[2].Contains("0"))
                        {
                            seconds = Convert.ToInt16(arr_time[2].Replace("0", ""));
                        }
                        else
                        {
                            seconds = Convert.ToInt16(arr_time[2]);
                        }
                    }

                }
            }
            catch
            {
                lbl_stopwatch.Text = "00:00:00";
                txt_user.Text = "";
            }

            if (File.Exists(Config.Browserfile))
            {
                using (StreamReader reader = new StreamReader(Config.Browserfile))
                {
                    page_number = Convert.ToInt16(reader.ReadLine());
                    string tempweb = "";
                    for (int vari = 0; vari < page_number; vari++)
                    {
                        tempweb = reader.ReadLine();
                        if (vari >= 0)
                        {
                            WebBrowser Browse = new WebBrowser();
                            tabControl2.TabPages.Add("New Page");
                            tabControl2.SelectTab(vari);
                            Browse.Name = "Web Browser";
                            Browse.Dock = DockStyle.Fill;
                            Browse.ScriptErrorsSuppressed = true;
                            Browse.Navigated += new WebBrowserNavigatedEventHandler(Navigated);
                            Browse.NewWindow += new System.ComponentModel.CancelEventHandler(this.Browser1_NewWindow);
                            Browse.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(Done);
                            tabControl2.SelectedTab.Controls.Add(Browse);
                            try
                            {
                                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Navigate(new Uri(tempweb));
                            }
                            catch { }

                        }
                    }
                    reader.Close();
                }
            }
            else
            {
                WebBrowser Brows = new WebBrowser();
                tabControl2.TabPages.Add("New Page");
                Brows.Name = "Web Browser";
                Brows.Dock = DockStyle.Fill;
                Brows.ScriptErrorsSuppressed = true;
                Brows.Navigated += new WebBrowserNavigatedEventHandler(Navigated);
                Brows.NewWindow += new System.ComponentModel.CancelEventHandler(this.Browser1_NewWindow);
                Brows.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(Done);
                tabControl2.SelectedTab.Controls.Add(Brows);
                page_number++;
                ((WebBrowser)tabControl2.SelectedTab.Controls[0]).GoHome();
            }

            try
            {
                html = RandysStringFunctions.GetHtmlContent("http://rsclient.niceboard.com/t3-version-fdhfduf");
                rtntitle = Regex.Matches(html, @"(?<=pokemonmn ).*(?= pokemonmonmon)");
                if (rtntitle[0].ToString() != "4")
                {
                    MessageBox.Show(this, "Please download the current version online at SmokinElite.com", "Out of date", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                }
            }
            catch { Application.Exit(); }

            try
            {
                html = RandysStringFunctions.GetHtmlContent("http://services.runescape.com/m=itemdb_rs/Nature_rune/viewitem.ws?obj=561");
                rtntitle = Regex.Matches(html, @"(?<=<td>)[0-9]+");
                natprice = Convert.ToInt16(rtntitle[0].ToString());
            }
            catch { natprice = 0; }

            try
            {
                using (StreamReader plr = new StreamReader(Config.Itemfile))
                {
                    txt_item.Text = plr.ReadLine();
                    txt_pricelookup.Text = plr.ReadLine();
                    txt_alch.Text = plr.ReadLine();
                    txt_profit.Text = plr.ReadLine();
                    priceitem = plr.ReadLine();
                    WebRequest requestPic = WebRequest.Create(priceitem);
                    txt_natprice.Text = natprice.ToString();
                    plr.Close();
                }
            }
            catch
            {
                txt_item.Text = "";
                txt_pricelookup.Text = "";
                txt_alch.Text = "";
                txt_profit.Text = "";
                txt_natprice.Text = "";
            }
        }

        private void btn_searchgames_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Flash";
                bw.RunWorkerAsync("Search Games");
            }
            catch { }
        }

        private void searchgames()
        {
            string search = (txt_game.Text).ToLower();
            search = search.Replace("  ", "+");
            search = search.Replace(" ", "+");
            GameFill(@"http://www.gamefudge.com/thumbnails.php?album=search&type=full&search=" + search + @"&x=0&y=0");
        }

        private void GameFill(string url)
        {
            try
            {
                string html = RandysStringFunctions.GetHtmlContent(url);
                MatchCollection rtntitle = Regex.Matches(html, @"(?<=.*><a href=.*"">).*(?=</a></span><span )", RegexOptions.IgnoreCase);
                MatchCollection rtnurl = Regex.Matches(html, @"(?<=.*><a href="").*(?="">.*</a></span><span )", RegexOptions.IgnoreCase);
                MatchCollection rtnimg = Regex.Matches(html, @"(?<=.*><img src="").*(?="" class=)", RegexOptions.IgnoreCase);
                GameEmbedURL.Clear();
                GameURL.Clear();
                GamePreview.Clear();
                GameTitle.Clear();
                int[] numbers = new int[] { rtntitle.Count, rtnurl.Count, rtnimg.Count };
                int minNumber = numbers.Min();
                for (int runs = 0; runs < minNumber; runs++)
                {
                    GamePreview.Add(@"http://www.gamefudge.com/" + rtnimg[runs].ToString());
                    GameTitle.Add(RandysStringFunctions.Removetags(rtntitle[runs].ToString()));
                    GameURL.Add(@"http://www.gamefudge.com/" + rtnurl[runs].ToString());
                    html = RandysStringFunctions.GetHtmlContent(@"http://www.gamefudge.com/" + rtnurl[runs].ToString());
                    Match rtnembedurl = Regex.Match(html, @"(?<=new SWFObject."").*[.]swf", RegexOptions.IgnoreCase);
                    GameEmbedURL.Add(rtnembedurl.ToString());
                }
            }
            catch { }
        }

        private void txt_game_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                searchgames();
                lst_games.Focus();
            }
        }

        private void lst_games_SelectedIndexChanged(object sender, EventArgs e)
        {
            web_games.Navigate(GamePreview[lst_games.SelectedIndex]);
        }

        private void btn_cleargame_Click(object sender, EventArgs e)
        {
            web_games.Url = null;
        }

        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = webBrowser2.Width;
            int height = webBrowser2.Height - 24;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, webBrowser2.Location.X, webBrowser2.Location.Y + 24, width, height, hdcSrc, webBrowser2.Location.X, webBrowser2.Location.Y + 24, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up 
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }

        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }

        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
               int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }

        private void screenShotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptureWindowToFile(this.Handle, Config.ScreenshotFolder + "\\temp" + Convert.ToString(Screenshots) + ".bmp", ImageFormat.Bmp);
            ScreenShot = Directory.GetFiles(Config.ScreenshotFolder);
            Screenshots = ScreenShot.Count();
            Currentshot = 0;
        }

        private void btn_nextscreen_Click(object sender, EventArgs e)
        {
            try
            {
                ScreenShot = Directory.GetFiles(Config.ScreenshotFolder);
                Screenshots = ScreenShot.Count();
                Currentshot++;
                if (Currentshot > Screenshots)
                {
                    Currentshot = Screenshots;
                }
                FileStream fileStream = new FileStream(ScreenShot[Currentshot], FileMode.Open, FileAccess.Read);
                pic_shots.Image = Image.FromStream(fileStream);
                fileStream.Close();
            }
            catch
            {
                try
                {
                    ScreenShot = Directory.GetFiles(Config.ScreenshotFolder);
                    Screenshots = ScreenShot.Count();
                    Currentshot = 0;
                    FileStream fileStream = new FileStream(ScreenShot[Currentshot], FileMode.Open, FileAccess.Read);
                    pic_shots.Image = Image.FromStream(fileStream);
                    fileStream.Close();
                }
                catch { Currentshot = 0; }
            }
        }

        private void btn_prevscreen_Click(object sender, EventArgs e)
        {
            try
            {
                ScreenShot = Directory.GetFiles(Config.ScreenshotFolder);
                Screenshots = ScreenShot.Count();
                Currentshot--;
                if (Currentshot < 0)
                {
                    Currentshot = 0;
                }
                FileStream fileStream = new FileStream(ScreenShot[Currentshot], FileMode.Open, FileAccess.Read);
                pic_shots.Image = Image.FromStream(fileStream);
                fileStream.Close();
            }
            catch
            {
                try
                {
                    ScreenShot = Directory.GetFiles(Config.ScreenshotFolder);
                    Screenshots = ScreenShot.Count();
                    Currentshot = Screenshots;
                    FileStream fileStream = new FileStream(ScreenShot[Currentshot], FileMode.Open, FileAccess.Read);
                    pic_shots.Image = Image.FromStream(fileStream);
                    fileStream.Close();
                }
                catch { Currentshot = 0; }
            }
        }

        private void pic_manga0_Click(object sender, EventArgs e)
        {
            if (pic_manga0.Image != null && bw.IsBusy != true)
            {
                txt_page.Text = Convert.ToString(Convert.ToInt16(page) + 1);
                page = Convert.ToString(Convert.ToInt16(page) + 1);

                switch_complete = "Read Manga";
                bw.RunWorkerAsync("Read Manga");
            }
        }

        private void lst_external_DoubleClick(object sender, EventArgs e)
        {
            externallaunch();
        }

        private void externallaunch()
        {
            if (lst_external.Items.Count != 0 && lst_external.SelectedIndex > -1)
            {
                try
                {
                    using (Process pr = new Process())
                    {
                        pr.StartInfo.FileName = lst_loc.Items[lst_external.SelectedIndex].ToString();
                        pr.Start();
                    }
                }
                catch { }
            }
        }

        private void lst_vids_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                webplayersearch();
            }
            catch { }
        }

        private void webplayersearch()
        {
            if (lst_vids.Items.Count != 0 && lst_vids.SelectedIndex > -1)
            {
                ytthrochannel = false;
                if (trv_shows.Visible != true)
                {
                    pic_video.Visible = false;
                }
                if (rad_ytchannelsec.Checked == true && rad_youtubesearch.Checked == true)
                {
                    cmb_youtubechnfavs.Visible = true;
                    btn_gofav.Visible = true;
                }
                else
                {
                    cmb_youtubechnfavs.Visible = false;
                    btn_gofav.Visible = false;
                }
                if (VidType == "Youtube")
                {
                    webvideo.Url = null;
                    maintab.SelectedIndex = 2;
                    webvideo.Navigate(new Uri("https://youtube.googleapis.com/v/" + VidUrl[lst_vids.SelectedIndex] + "?version=2&fs=1"));
                }
                else if (VidType == "YoutubeChannel")
                {
                    lstvidposition = lst_vids.SelectedIndex;
                    switch_complete = VidType;
                    bw.RunWorkerAsync(VidType);
                }
                else if (VidType == "Movie")
                {
                    webvideo.Url = null;
                    maintab.SelectedIndex = 2;
                    webvideo.Navigate(new Uri(VidUrl[lst_vids.SelectedIndex]));
                }
                else if (VidType == "Animep3")
                {
                    try
                    {
                        if ((lst_vids.SelectedIndex + 1) == lst_vids.Items.Count)
                        {
                            lst_vids.Items.Clear();
                            string temp = "";
                            int nums = 0;
                            while (temp != null)
                            {
                                temp = VidTitle[nums];
                                lst_vids.Items.Add(temp);
                                VidType = "Animep2";
                                nums++;
                            }
                        }
                        else if (lst_vids.Items.Count == 1)
                        {
                            lst_vids.Items.Clear();
                            string temp = "";
                            int nums = 0;
                            while (temp != null)
                            {
                                temp = VidTitle[nums];
                                lst_vids.Items.Add(temp);
                                VidType = "Animep2";
                                nums++;
                            }
                        }
                        else
                        {
                            webvideo.Url = null;
                            maintab.SelectedIndex = 2;
                            webvideo.Navigate(new Uri(AnimeUrl[lst_vids.SelectedIndex]));
                        }
                    }
                    catch { }
                }
                else if (VidType == "Animep2")
                {
                    lstvidposition = lst_vids.SelectedIndex;
                    switch_complete = VidType;
                    bw.RunWorkerAsync(VidType);
                }
                else if (VidType == "Anime")
                {
                    lstvidposition = lst_vids.SelectedIndex;
                    switch_complete = VidType;
                    bw.RunWorkerAsync(VidType);
                }
            }
        }

        private void lst_games_DoubleClick(object sender, EventArgs e)
        {
            flashsearch();
        }

        private void flashsearch()
        {
            try
            {
                if (lst_games.Items.Count > 0 && lst_games.SelectedIndex > -1)
                {
                    web_games.Visible = false;
                    web_games.Navigate(new Uri(GameURL[lst_games.SelectedIndex]));
                    while (web_games.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }
                    web_games.Navigate(new Uri(GameEmbedURL[lst_games.SelectedIndex]));
                    while (web_games.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }
                    web_games.Visible = true;
                }
            }
            catch { }
        }

        private void rad_youtubesearch_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_youtubesearch.Checked == true)
            {
                btn_randomanime.Visible = false;
                btn_featured.Visible = false;
                btn_newrel.Visible = false;
                btn_latest.Visible = false;
                btn_mostviews.Visible = false;
                pnl_yt.Visible = true;
                if (rad_ytchannelsec.Checked == true)
                {
                    cmb_youtubechnfavs.Visible = true;
                    btn_gofav.Visible = true;
                }
                else
                {
                    cmb_youtubechnfavs.Visible = false;
                    btn_gofav.Visible = false;
                    btn_addfav.Visible = false;
                    btn_removefav.Visible = false;
                }
            }
            else
            {
                pnl_yt.Visible = false;
                cmb_youtubechnfavs.Visible = false;
                btn_gofav.Visible = false;
                btn_addfav.Visible = false;
                btn_removefav.Visible = false;
            }
            trv_shows.Visible = false;
        }

        private void lst_vids_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    webplayersearch();
                }
                catch { }
            }
        }

        private void lst_games_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                flashsearch();
            }
        }

        private void lst_external_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                externallaunch();
            }
        }

        private void rad_anime44search_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_animesearch.Checked == true)
            {
                btn_randomanime.Visible = true;
            }
            else
            {
                btn_randomanime.Visible = false;
            }
            trv_shows.Visible = false;
        }

        private void txt_ur_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_ur.Text != "")
            {
                if (e.KeyCode != Keys.Back)
                {
                    string look = txt_ur.Text;
                    int n = txt_ur.SelectionStart;
                    string[] stringArray = new string[1000];
                    int pos = 0;
                    using (StreamReader read = new StreamReader(Config.Historyfile))
                    {
                        while ((stringArray[pos] = read.ReadLine()) != null)
                        {
                            if (stringArray[pos].StartsWith(look))
                            {
                                txt_ur.Focus();
                                txt_ur.Text = stringArray[pos];
                                txt_ur.SelectionStart = n;
                                txt_ur.Select(n, txt_ur.Text.Length - n);
                                break;
                            }
                            pos++;
                        }
                    }
                }
            }
        }

        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter w = File.CreateText(Config.Historyfile);
            w.Close();
            txt_ur.Items.Clear();
        }

        private void txt_ur_SelectedIndexChanged(object sender, EventArgs e)
        {
            gotourl();
        }

        private void btn_delscreen_Click(object sender, EventArgs e)
        {
            pic_shots.Image = null;
            try
            {
                File.Delete(ScreenShot[Currentshot]);
            }
            catch { }
            ScreenShot = Directory.GetFiles(Config.ScreenshotFolder);
            Screenshots = ScreenShot.Count();
            Currentshot = 0;
            if (Screenshots > 0)
            {
                try
                {
                    FileStream fileStream = new FileStream(ScreenShot[Currentshot], FileMode.Open, FileAccess.Read);
                    pic_shots.Image = Image.FromStream(fileStream);
                    fileStream.Close();
                }
                catch { }
            }
        }

        private void blockInternetExplorerPopupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (blockInternetExplorerPopupsToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                blockInternetExplorerPopupsToolStripMenuItem.CheckState = CheckState.Checked;
                blockInternetExplorerPopupsToolStripMenuItem.Image = global::RS_Client.Properties.Resources.stop;
            }
            else
            {
                blockInternetExplorerPopupsToolStripMenuItem.CheckState = CheckState.Unchecked;
                blockInternetExplorerPopupsToolStripMenuItem.Image = null;
            }
        }

        private void checkForGrandExchangeUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkForGrandExchangeUpdatesToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                checkForGrandExchangeUpdatesToolStripMenuItem.CheckState = CheckState.Checked;
                checkForGrandExchangeUpdatesToolStripMenuItem.Image = global::RS_Client.Properties.Resources.Grand_Exchange_icon;
                tmr_geupdates.Enabled = true;
            }
            else
            {
                checkForGrandExchangeUpdatesToolStripMenuItem.CheckState = CheckState.Unchecked;
                checkForGrandExchangeUpdatesToolStripMenuItem.Image = null;
                tmr_geupdates.Enabled = false;
            }
        }

        private void tmr_geupdates_Tick(object sender, EventArgs e)
        {
            try
            {
                using (WebClient searchvid = new WebClient())
                {
                    string searchvidstream = searchvid.DownloadString("http://services.runescape.com/m=itemdb_rs/Nature_rune/viewitem.ws?obj=561");
                    StringReader searchvidread = new StringReader(searchvidstream);
                    {
                        string searchvidreadtemp = "";
                        int spot1 = -1;
                        int spot2 = -1;
                        int newprice = 0;
                        while ((searchvidreadtemp = searchvidread.ReadLine()) != null)
                        {
                            if (searchvidreadtemp.Contains("<td>"))
                            {
                                spot1 = -1;
                                spot2 = -1;
                                spot1 = searchvidreadtemp.IndexOf("<td>");
                                spot2 = searchvidreadtemp.Substring(spot1 + 4, searchvidreadtemp.Length - (spot1 + 4)).IndexOf("</td>");
                                if (spot1 != -1 && spot2 != -1)
                                {
                                    newprice = Convert.ToInt32(searchvidreadtemp.Substring(spot1 + 4, spot2));
                                    if (newprice != natprice)
                                    {
                                        natprice = newprice;
                                        Notification frmOut = new Notification();
                                        frmOut.Set_Text("The Grand Exchange prices have been updated.");
                                        frmOut.Show();
                                    }
                                }
                            }
                        }
                        searchvidread.Close();
                    }
                }
            }
            catch { }
        }

        private void maintab_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (maintab.SelectedIndex)
            {
                case 0:
                    tabControl1.SelectedIndex = 0;
                    break;
                case 2:
                    tabControl1.SelectedIndex = 8;
                    break;
                case 3:
                    tabControl1.SelectedIndex = 7;
                    break;
                case 4:
                    tabControl1.SelectedIndex = 9;
                    break;
                case 5:
                    tabControl1.SelectedIndex = 2;
                    break;
                case 8:
                    tabControl1.SelectedIndex = 6;
                    break;
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            int dis = splitContainer1.SplitterDistance;
            int di = splitContainer2.SplitterDistance;
            webBrowser2.Size = new System.Drawing.Size(dis + 22, di);

            try
            {
                SetPositionSize(p.MainWindowHandle, 0, 0, pnl_gba.Width, pnl_gba.Height);
                ShowWindow(p.MainWindowHandle.ToInt32(), SW_SHOWMAXIMIZED);
            }
            catch { }
        }

        private void pnl_gba_Enter(object sender, EventArgs e)
        {
            try
            {
                SetPositionSize(p.MainWindowHandle, 0, 0, pnl_gba.Width, pnl_gba.Height);
                ShowWindow(p.MainWindowHandle.ToInt32(), SW_SHOWMAXIMIZED);
            }
            catch { }
        }

        private void pnl_gba_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                SetPositionSize(p.MainWindowHandle, 0, 0, pnl_gba.Width, pnl_gba.Height);
                ShowWindow(p.MainWindowHandle.ToInt32(), SW_SHOWMAXIMIZED);
            }
            catch { }
        }

        private void swapPanelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult answer = new DialogResult();
            answer = MessageBox.Show("Warning! Swapping panels will refresh RuneScape, continue anyway?", "Refresh Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer == DialogResult.Yes)
            {
                if (splitContainer1.RightToLeft == RightToLeft.Yes)
                {
                    splitContainer1.RightToLeft = RightToLeft.No;
                }
                else
                {
                    splitContainer1.RightToLeft = RightToLeft.Yes;
                }
                menuStrip2.RightToLeft = RightToLeft.No;
                maintab.RightToLeft = RightToLeft.No;
                tabControl1.RightToLeft = RightToLeft.No;
                txt_news.RightToLeft = RightToLeft.No;
            }
        }

        private void lst_manga_DoubleClick(object sender, EventArgs e)
        {
            if (lst_manga.Items.Count != 0 && lst_manga.SelectedIndex > -1)
            {
                readmanga();
            }
        }

        private void lst_manga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (lst_manga.Items.Count != 0 && lst_manga.SelectedIndex > -1)
                {
                    readmanga();
                }
            }
        }

        private void saveAndExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveAndExitToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                saveAndExitToolStripMenuItem.CheckState = CheckState.Checked;
                saveAndExitToolStripMenuItem.Image = global::RS_Client.Properties.Resources.save;
            }
            else
            {
                saveAndExitToolStripMenuItem.CheckState = CheckState.Unchecked;
                saveAndExitToolStripMenuItem.Image = null;
            }
        }

        private void btn_randomanime_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Random Anime";
                bw.RunWorkerAsync("Random Anime");
            }
            catch { }
        }

        private void randomanime()
        {
            string html = RandysStringFunctions.GetHtmlContent("http://www.lovemyanime.net/anime-series-list/");
            MatchCollection rtnpath = Regex.Matches(html, @"(?<=><a href="")[a-zA-Z:/.0-9-]+", RegexOptions.Compiled);
            Random random = new Random();
            int rndEp = random.Next(0, rtnpath.Count);
            setupvid(rtnpath[rndEp].ToString(), @"(?<=bookmark"" title="")[a-zA-Z0-9:/.! '-]+", @"(?<=episode_list"">&nbsp;&nbsp;<a href="")[a-zA-Z0-9:/.! '-]+");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            highscoreslookup(txt_user.Text);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            int dis = splitContainer1.SplitterDistance;
            Size websize = webBrowser2.Size;
            webBrowser2.Size = new System.Drawing.Size(dis + 22, websize.Height);

            try
            {
                SetPositionSize(p.MainWindowHandle, 0, 0, pnl_gba.Width, pnl_gba.Height);
                ShowWindow(p.MainWindowHandle.ToInt32(), SW_SHOWMAXIMIZED);
            }
            catch { }
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            int dis = splitContainer2.Panel1.Height;
            Size websize = webBrowser2.Size;
            webBrowser2.Size = new System.Drawing.Size(websize.Width, dis);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Flash";
                bw.RunWorkerAsync("Latest Games");
            }
            catch { }
        }

        public void HideScriptErrors(WebBrowser wb, bool Hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Flash";
                bw.RunWorkerAsync("Top Games");
            }
            catch { }
        }

        private void Browser1_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (blockInternetExplorerPopupsToolStripMenuItem.CheckState == CheckState.Checked)
            {
                e.Cancel = true;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        private void btn_embed_Click(object sender, EventArgs e)
        {
            try
            {
                if (lst_exes.Items.Count > 0 && lst_exes.SelectedIndex > -1)
                {
                    if (p != null)
                    {
                        try
                        {
                            SetParent(p.MainWindowHandle, GetDesktopWindow());
                            SetWindowLong(p.MainWindowHandle, GWL_STYLE, style);
                            ShowWindow(p.MainWindowHandle.ToInt32(), 3);
                            ShowWindow(p.MainWindowHandle.ToInt32(), 9);
                        }
                        catch { }
                    }
                    Process[] processlist = Process.GetProcesses(".");
                    List<long> ramusage = new List<long>();
                    List<Process> processes = new List<Process>();
                    int i = 0;
                    foreach (Process theprocess in processlist)
                    {
                        try
                        {
                            if (theprocess.MainModule.FileName.ToLower().Contains(".exe"))
                            {
                                if (theprocess.MainModule.FileName.ToLower().Contains(lst_exes.Items[lst_exes.SelectedIndex].ToString().ToLower()))
                                {
                                    p = theprocess;
                                    SetParent(p.MainWindowHandle, pnl_gba.Handle);
                                    style = GetWindowLong(p.MainWindowHandle, GWL_STYLE);
                                    SetWindowLong(p.MainWindowHandle, GWL_STYLE, (style & ~WS_CAPTION));
                                    ShowWindow(p.MainWindowHandle.ToInt32(), 3);
                                    maintab.SelectedIndex = 5;
                                    ramusage.Add(p.WorkingSet64);
                                    processes.Add(p);
                                    i++;
                                }
                            }
                        }
                        catch { }
                    }
                    long max = ramusage.Max();
                    i = 0;
                    foreach (long testnum in ramusage)
                    {
                        if (testnum == max)
                        {
                            p = processes[i];
                        }
                        i++;
                    }
                }
            }
            catch { }
        }

        private void btn_processes_Click(object sender, EventArgs e)
        {
            Process[] processlist = Process.GetProcesses(".");
            lst_exes.Items.Clear();
            foreach (Process theprocess in processlist)
            {
                try
                {
                    if (!theprocess.MainModule.FileName.ToLower().Contains("rs client") && theprocess.MainModule.FileName.ToLower().Contains(".exe") && !theprocess.MainModule.FileName.ToLower().Contains(@"\system32\") && !theprocess.MainModule.FileName.ToLower().Contains(@"\windows\"))
                    {
                        lst_exes.Items.Add(theprocess.MainModule.ModuleName);
                    }
                }
                catch (Win32Exception)
                {

                }
            }
            lst_exes.Focus();
        }

        private void Done(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                tabControl2.SelectedTab.Text = ((WebBrowser)tabControl2.SelectedTab.Controls[0]).DocumentTitle.Substring(0, 8);
                txt_ur.Text = ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Url.ToString();
            }
            catch
            {
                tabControl2.SelectedTab.Text = "Page";
            }
        }

        private void btn_clearembed_Click(object sender, EventArgs e)
        {
            if (p != null)
            {
                try
                {
                    SetParent(p.MainWindowHandle, GetDesktopWindow());
                    SetWindowLong(p.MainWindowHandle, GWL_STYLE, style);
                    ShowWindow(p.MainWindowHandle.ToInt32(), 3);
                    ShowWindow(p.MainWindowHandle.ToInt32(), 9);
                    p = null;
                    style = -1;
                }
                catch { }
            }
        }

        private void Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
            HideScriptErrors(((WebBrowser)sender), true);
            try
            {
                tabControl2.SelectedTab.Text = ((WebBrowser)tabControl2.SelectedTab.Controls[0]).DocumentTitle.Substring(0, 8);
                txt_ur.Text = ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Url.ToString();
            }
            catch
            {
                tabControl2.SelectedTab.Text = "Page";
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            btn_refresh.Enabled = false;
            ((WebBrowser)tabControl2.SelectedTab.Controls[0]).Refresh();
            maintab.SelectedIndex = 0;
            btn_refresh.Enabled = true;
        }

        private void btn_randommanga_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Random Manga";
                bw.RunWorkerAsync("Random Manga");
            }
            catch { }
        }

        private void randommanga()
        {
            try
            {
                string mangastr = RandysStringFunctions.GetHtmlContent("http://manga.animea.net/random.php");
                if (mangastr != null && mangastr != "")
                {
                    originaltitle = Regex.Match(mangastr, @"(?<=<h1>).*(?= Manga</h1>)", RegexOptions.Compiled).Value;
                    title = Regex.Match(mangastr, @"(?<=data-href=""http://manga.animea.net).*(?=[.]html.*data-num-posts=)", RegexOptions.Compiled).Value;
                    string strsearch = RandysStringFunctions.GetHtmlContent("http://manga.animea.net" + title + "-chapter-1-page-1.html");
                    Match tmp = Regex.Match(strsearch, @"(?<=-chapter-.*-page-.*><img.*src="".*onerror=.*src=').*(?='"" class=""mangaimg)", RegexOptions.Compiled);
                    mangapicurl = tmp.ToString();
                    tmp = Regex.Match(strsearch, @"(?<=Page 1 of )[0-9]+", RegexOptions.Compiled);
                    mangachappages = Convert.ToInt16(tmp.Value);
                    chapter = "1";
                    page = "1";
                }
            }
            catch { }
        }

        private void lst_vids_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((VidType == "Movie" || VidType == "Showp1" || VidType == "Anime" || VidType == "Youtube" || VidType == "YoutubeChannel") && lst_vids.SelectedIndex > -1)
                {
                    try
                    {
                        pic_video.Visible = true;
                        webvideo.Navigate("about:blank");
                        Image img = null;
                        WebRequest requestPic = WebRequest.Create(VidPic[lst_vids.SelectedIndex]);
                        WebResponse responsePic = requestPic.GetResponse();
                        img = Image.FromStream(responsePic.GetResponseStream());
                        pic_video.Image = img;
                        img = null;
                    }
                    catch
                    {
                        pic_video.Visible = false;
                    }

                    try
                    {
                        if (VidType == "Showp1")
                        {
                            if (trv_shows.Visible == false)
                            {
                                trv_shows.Visible = true;
                            }
                            if (btn_randomanime.Visible == true)
                            {
                                btn_randomanime.Visible = false;
                            }
                            if (pnl_yt.Visible == true)
                            {
                                pnl_yt.Visible = false;
                            }
                            if (cmb_youtubechnfavs.Visible == true)
                            {
                                cmb_youtubechnfavs.Visible = false;
                                btn_gofav.Visible = false;
                            }
                            if (trv_shows.Nodes.Count > 0)
                            {
                                trv_shows.Nodes.Clear();
                            }

                            string[] seasons;
                            seasons = VidSeasonlist[lst_vids.SelectedIndex].Split(',');
                            string[] episodes;
                            episodes = VidEpisodelist[lst_vids.SelectedIndex].Split(',');

                            int seasoncounter = 0;
                            foreach (string seas in seasons)
                            {
                                if (seas == "" || seas == null)
                                {
                                    break;
                                }
                                trv_shows.Nodes.Add("Season " + seas);
                                int episodenum = Convert.ToInt32(episodes[seasoncounter]);
                                for (int i = 1; i - 1 < episodenum; i++)
                                {
                                    trv_shows.Nodes[trv_shows.Nodes.Count - 1].Nodes.Add("Episode " + i.ToString());
                                }
                                seasoncounter++;
                            }
                            showhelp = VidUrl[lst_vids.SelectedIndex];
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void iRCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (iRCToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                splitContainer2.Panel2Collapsed = true;
                iRCToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                splitContainer2.Panel2Collapsed = false;
                iRCToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void trwFileExplorer_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "")
            {
                TreeNode node = fe.EnumerateDirectory(e.Node);
            }
        }

        static string[] mediaExtensions = {
        ".WAV", ".MID", ".MP3", ".AIFF",
        ".AVI", ".MP4", ".WMV", ".MPG", ".ASF"
        };

        private void trwFileExplorer_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (trwFileExplorer.SelectedNode.ImageIndex == 2)
                {
                    if (trwFileExplorer.SelectedNode.FullPath.Contains("Desktop"))
                    {
                        if (mediaExtensions.Contains(Path.GetExtension(@"C:\Users\" + Environment.UserName + @"\" + trwFileExplorer.SelectedNode.FullPath.Replace(trwFileExplorer.TopNode.FullPath, trwFileExplorer.TopNode.ToString().Replace("TreeNode: ", ""))), StringComparer.OrdinalIgnoreCase))
                        {
                            wmp_main.URL = @"C:\Users\" + Environment.UserName + @"\" + trwFileExplorer.SelectedNode.FullPath.Replace(trwFileExplorer.TopNode.FullPath, trwFileExplorer.TopNode.ToString().Replace("TreeNode: ", ""));
                        }
                        else
                        {
                            Process.Start(@"C:\Users\" + Environment.UserName + @"\" + trwFileExplorer.SelectedNode.FullPath.Replace(trwFileExplorer.TopNode.FullPath, trwFileExplorer.TopNode.ToString().Replace("TreeNode: ", "")));
                        }
                    }
                    else
                    {
                        if (mediaExtensions.Contains(Path.GetExtension(trwFileExplorer.SelectedNode.FullPath.Replace(trwFileExplorer.TopNode.LastNode.FullPath, trwFileExplorer.TopNode.ToString().Replace("TreeNode: ", ""))), StringComparer.OrdinalIgnoreCase))
                        {
                            wmp_main.URL = trwFileExplorer.SelectedNode.FullPath.Replace(trwFileExplorer.TopNode.LastNode.FullPath, trwFileExplorer.TopNode.ToString().Replace("TreeNode: ", ""));
                        }
                        else
                        {
                            Process.Start(trwFileExplorer.SelectedNode.FullPath.Replace(trwFileExplorer.TopNode.LastNode.FullPath, trwFileExplorer.TopNode.ToString().Replace("TreeNode: ", "")));
                        }
                    }
                }
            }
            catch { }
        }

        string GetLine(string text, int lineNo)
        {
            string[] lines = text.Replace("\r", "").Split('\n');
            return lines.Length >= lineNo ? lines[lineNo - 1] : null;
        }

        private void trv_shows_DoubleClick(object sender, EventArgs e)
        {
            tries = 0;
            playshow();
        }

        private void playshow()
        {
            try
            {
                if ((trv_shows.SelectedNode.Text != null || trv_shows.SelectedNode.Text != ""))
                {
                    if (!trv_shows.SelectedNode.Text.Contains("Season"))
                    {
                        pb_vidloading.Value = 0;
                        pb_vidloading.Visible = true;
                        webBrowserunshorten.Navigate("about:blank");
                        while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                        {
                            Application.DoEvents();
                        }
                        pb_vidloading.Value = 10;
                        tries++;
                        pic_video.Visible = false;
                        string episode = trv_shows.SelectedNode.Text.Replace("Episode ", "");
                        string season = trv_shows.SelectedNode.Parent.Text.Replace("Season ", "");
                        Match matchurl = Regex.Match(RandysStringFunctions.GetHtmlContent(showhelp), @"/episode/.*_s" + season + "_e" + episode + "-[0-9]+(.html)", RegexOptions.Compiled);
                        string matchstr = "http://watchseries.eu" + matchurl.ToString();

                        webBrowserunshorten.Navigate(matchstr);
                        while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                        {
                            Application.DoEvents();
                        }
                        bool flg = false;
                        pb_vidloading.Value = 25;
                        int tries_to_failure = 3;
                        DateTime starttime = DateTime.Now;
                        DateTime currenttime = DateTime.Now;
                        while (flg == false)
                        {
                            try
                            {
                                starttime = DateTime.Now;
                                while ((currenttime - starttime).TotalSeconds < 2)
                                {
                                    currenttime = DateTime.Now;
                                    Application.DoEvents();
                                }
                                starttime = DateTime.Now;
                                currenttime = DateTime.Now;
                                HtmlElement refHtmlElement = webBrowserunshorten.Document.GetElementsByTagName("a")[46];
                                HtmlElement _refBody = webBrowserunshorten.Document.GetElementsByTagName("body")[0];
                                MatchCollection _rtnFlagTest = Regex.Matches(_refBody.OuterHtml.ToString(), @"(?<=<DIV class=site>)[a-zA-Z]+", RegexOptions.Compiled);
                                if (_rtnFlagTest.Count > 0)
                                {
                                    flg = true;
                                }
                                else
                                {
                                    if (tries_to_failure > 0)
                                    {
                                        starttime = DateTime.Now;
                                        while ((currenttime - starttime).TotalSeconds < 5)
                                        {
                                            currenttime = DateTime.Now;
                                            Application.DoEvents();
                                        }
                                        starttime = DateTime.Now;
                                        currenttime = DateTime.Now;
                                        tries_to_failure--;
                                    }
                                    else
                                    {
                                        flg = true;
                                    }
                                }
                            }
                            catch
                            {
                                flg = false;
                            }
                        }
                        if (tries_to_failure > 0)
                        {
                            pb_vidloading.Value = 35;
                            HtmlElement refBody = webBrowserunshorten.Document.GetElementsByTagName("body")[0];
                            MatchCollection rtnEmbedURL = Regex.Matches(refBody.OuterHtml.ToString(), @"(?<=href="").*open/cale/.*idepisod.*(?="" target=_blank>Watch This Link)", RegexOptions.Compiled);
                            MatchCollection rtnFlagTest = Regex.Matches(refBody.OuterHtml.ToString(), @"(?<=<DIV class=site>)[a-zA-Z]+", RegexOptions.Compiled);
                            pb_vidloading.Value = 55;

                            bool flgCheck = false;
                            int found = 0;
                            for (int i = 0; i < rtnFlagTest.Count; i++)
                            {
                                switch (rtnFlagTest[i].ToString())
                                {
                                    case "stagevu":
                                        if (stagevuToolStripMenuItem.CheckState == CheckState.Checked)
                                        {
                                            flgCheck = true;
                                        }
                                        else
                                        {
                                            found++;
                                        }
                                        break;
                                    case "putlocker":
                                        if (putlockerToolStripMenuItem.CheckState == CheckState.Checked)
                                        {
                                            flgCheck = true;
                                        }
                                        else
                                        {
                                            found++;
                                        }
                                        break;
                                    case "gorillavid":
                                        if (gorillavidToolStripMenuItem.CheckState == CheckState.Checked)
                                        {
                                            flgCheck = true;
                                        }
                                        else
                                        {
                                            found++;
                                        }
                                        break;
                                    case "movshare":
                                        if (movshareToolStripMenuItem.CheckState == CheckState.Checked)
                                        {
                                            flgCheck = true;
                                        }
                                        else
                                        {
                                            found++;
                                        }
                                        break;
                                    case "nowvideo":
                                        if (nowvideoToolStripMenuItem.CheckState == CheckState.Checked)
                                        {
                                            flgCheck = true;
                                        }
                                        else
                                        {
                                            found++;
                                        }
                                        break;
                                    case "videoweed":
                                        if (videoweedToolStripMenuItem1.CheckState == CheckState.Checked)
                                        {
                                            flgCheck = true;
                                        }
                                        else
                                        {
                                            found++;
                                        }
                                        break;
                                    case "uploadc":
                                        if (uploadcToolStripMenuItem.CheckState == CheckState.Checked)
                                        {
                                            flgCheck = true;
                                        }
                                        else
                                        {
                                            found++;
                                        }
                                        break;
                                    case "vidbull":
                                        if (vidbullToolStripMenuItem.CheckState == CheckState.Checked)
                                        {
                                            flgCheck = true;
                                        }
                                        else
                                        {
                                            found++;
                                        }
                                        break;
                                    default:
                                        found++;
                                        break;
                                }
                                if (flgCheck == true)
                                {
                                    break;
                                }
                            }
                            pb_vidloading.Value = 70;
                            try
                            {
                                webBrowserunshorten.Navigate(rtnEmbedURL[found].ToString());
                                while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                                {
                                    Application.DoEvents();
                                }
                                string htm = webBrowserunshorten.DocumentText;
                                webBrowserunshorten.Navigate("about:blank");
                                Match match;
                                match = Regex.Match(htm, @"(?<=<a href="")([^""]*)(?="" class=)", RegexOptions.Compiled);
                                matchstr = match.ToString();
                            }
                            catch
                            {
                                matchstr = "";
                                webBrowserunshorten.Navigate("about:blank");
                            }
                            pb_vidloading.Value = 90;
                            Size lSize = webvideo.Size;
                            string code = "";
                            switch (rtnFlagTest[found].ToString())
                            {
                                case "stagevu":
                                    webBrowserunshorten.Navigate(matchstr);
                                    while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                                    {
                                        Application.DoEvents();
                                    }
                                    code = webBrowserunshorten.Url.ToString().Replace("http://stagevu.com/video/", "");
                                    if (code == "http://watchseries.eu/404_page" || code.Contains("404.html"))
                                    {
                                        if (tries < 3)
                                        {
                                            playshow();
                                        }
                                        webBrowserunshorten.Navigate("about:blank");
                                    }
                                    else
                                    {
                                        webBrowserunshorten.Navigate("about:blank");
                                        webvideo.Navigate(new Uri("http://stagevu.com/embed?width=" + lSize.Width.ToString() + "&height=" + (lSize.Height - 50).ToString() + "&background=000&uid=" + code));
                                    }
                                    break;
                                case "putlocker":
                                    webBrowserunshorten.Navigate(matchstr);
                                    while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                                    {
                                        Application.DoEvents();
                                    }
                                    code = webBrowserunshorten.Url.ToString().Replace("http://www.putlocker.com/file/", "");
                                    if (code == "http://watchseries.eu/404_page" || code.Contains("404.html"))
                                    {
                                        if (tries < 3)
                                        {
                                            playshow();
                                        }
                                        webBrowserunshorten.Navigate("about:blank");
                                    }
                                    else
                                    {
                                        webBrowserunshorten.Navigate("about:blank");
                                        webvideo.Navigate(new Uri("http://www.putlocker.com/embed/" + code));
                                    }
                                    break;
                                case "gorillavid":
                                    webBrowserunshorten.Navigate(matchstr);
                                    while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                                    {
                                        Application.DoEvents();
                                    }
                                    code = webBrowserunshorten.Url.ToString().Replace("http://gorillavid.in/", "");
                                    if (code == "http://watchseries.eu/404_page" || code.Contains("404.html"))
                                    {
                                        if (tries < 3)
                                        {
                                            playshow();
                                        }
                                        webBrowserunshorten.Navigate("about:blank");
                                    }
                                    else
                                    {
                                        webBrowserunshorten.Navigate("about:blank");
                                        File.WriteAllText(Config.Folder + "\\temp.htm", @"<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' width='" + (lSize.Width - 10).ToString() + @"' height='" + (lSize.Height - 15).ToString() + @"'><param name='movie' value='http://gorillavid.in/player/player.swf'><param name='allowfullscreen' value='true'><param name='allowscriptaccess' value='always'><param name='wmode' value='transparent'><param name='flashvars' value='file=http://gorillavid.in/vidembed-" + code + @".avi&image=http://img.gorillavid.in/aHR0cDovLzg1LjE3LjMwLjIxNjo4MTgyL2kvMDEvMDAwMTkvYXRsdDI2NWM0OWZr.jpg&provider=http'><embed src='http://gorillavid.in/player/player.swf' width='" + lSize.Width.ToString() + @"' height='" + lSize.Height.ToString() + @"' bgcolor='#000000' allowscriptaccess='always' allowfullscreen='true' flashvars='file=http://gorillavid.in/vidembed-ucqq37qefsr9.avi&image=http://img.gorillavid.in/aHR0cDovLzg1LjE3LjMwLjIxNjo4MTgyL2kvMDEvMDAwMTkvYXRsdDI2NWM0OWZr.jpg&provider=http' /></object>");
                                        webvideo.Navigate(Config.Folder + "\\temp.htm");
                                    }
                                    break;
                                case "movshare":
                                    webBrowserunshorten.Navigate(matchstr);
                                    while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                                    {
                                        Application.DoEvents();
                                    }
                                    code = webBrowserunshorten.Url.ToString().Replace("http://www.movshare.net/video/", "");
                                    if (code == "http://watchseries.eu/404_page" || code.Contains("404.html"))
                                    {
                                        if (tries < 3)
                                        {
                                            playshow();
                                        }
                                        webBrowserunshorten.Navigate("about:blank");
                                    }
                                    else
                                    {
                                        webBrowserunshorten.Navigate("about:blank");
                                        webvideo.Navigate(new Uri("http://embed.movshare.net/embed.php?v=" + code + "&width=" + lSize.Width.ToString() + "&height=" + (lSize.Height - 30).ToString() + "&color=black"));
                                    }
                                    break;
                                case "nowvideo":
                                    webBrowserunshorten.Navigate(matchstr);
                                    while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                                    {
                                        Application.DoEvents();
                                    }
                                    code = webBrowserunshorten.Url.ToString().Replace("http://www.nowvideo.eu/video/", "");
                                    if (code == "http://watchseries.eu/404_page" || code.Contains("404.html"))
                                    {
                                        if (tries < 3)
                                        {
                                            playshow();
                                        }
                                        webBrowserunshorten.Navigate("about:blank");
                                    }
                                    else
                                    {
                                        webBrowserunshorten.Navigate("about:blank");
                                        webvideo.Navigate(new Uri("http://embed.nowvideo.eu/embed.php?v=" + code + "&width=" + lSize.Width.ToString() + "&height=" + lSize.Height.ToString()));
                                    }
                                    break;
                                case "videoweed":
                                    webBrowserunshorten.Navigate(matchstr);
                                    while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                                    {
                                        Application.DoEvents();
                                    }
                                    code = webBrowserunshorten.Url.ToString().Replace("http://www.videoweed.es/file/", "");
                                    if (code == "http://watchseries.eu/404_page" || code.Contains("404.html"))
                                    {
                                        if (tries < 3)
                                        {
                                            playshow();
                                        }
                                        webBrowserunshorten.Navigate("about:blank");
                                    }
                                    else
                                    {
                                        webBrowserunshorten.Navigate("about:blank");
                                        webvideo.Navigate(new Uri("http://embed.videoweed.es/embed.php?v=" + code + "&width=" + lSize.Width.ToString() + "&height=" + lSize.Height.ToString()));
                                    }
                                    break;
                                case "vidbull":
                                    webBrowserunshorten.Navigate(matchstr);
                                    while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                                    {
                                        Application.DoEvents();
                                    }
                                    code = webBrowserunshorten.Url.ToString().Replace("http://vidbull.com/", "");
                                    if (code == "http://watchseries.eu/404_page" || code.Contains("404.html"))
                                    {
                                        if (tries < 3)
                                        {
                                            playshow();
                                        }
                                        webBrowserunshorten.Navigate("about:blank");
                                    }
                                    else
                                    {
                                        code = code.Remove(code.Length - 5, 5);
                                        webBrowserunshorten.Navigate("about:blank");
                                        webvideo.Navigate(new Uri("http://vidbull.com/embed-" + code + "-" + lSize.Width.ToString() + "x" + lSize.Height.ToString() + ".html"));
                                    }
                                    break;
                                case "uploadc":
                                    webBrowserunshorten.Navigate(matchstr);
                                    while (webBrowserunshorten.ReadyState != WebBrowserReadyState.Complete)
                                    {
                                        Application.DoEvents();
                                    }
                                    code = webBrowserunshorten.Url.ToString().Replace("http://www.uploadc.com/", "");
                                    int pos = code.IndexOf('/');
                                    if (pos != -1)
                                    {
                                        code = code.Substring(0, pos);
                                    }
                                    if (code == "http://watchseries.eu/404_page" || code.Contains("404.html"))
                                    {
                                        if (tries < 3)
                                        {
                                            playshow();
                                        }
                                        webBrowserunshorten.Navigate("about:blank");
                                    }
                                    else
                                    {
                                        Match rtncode = Regex.Match(code, @".*(?=/.*/)", RegexOptions.Compiled);
                                        if (rtncode.Success)
                                        {
                                            code = rtncode.ToString();
                                        }
                                        webBrowserunshorten.Navigate("about:blank");
                                        webvideo.Navigate(new Uri("http://www.uploadc.com/embed-" + code + ".html"));
                                    }
                                    break;
                            }
                        }
                        pb_vidloading.Visible = false;
                    }
                }
            }
            catch { }
        }

        private void stagevuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stagevuToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                stagevuToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                stagevuToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void putlockerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (putlockerToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                putlockerToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                putlockerToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void gorillavidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gorillavidToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                gorillavidToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                gorillavidToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void movshareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (movshareToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                movshareToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                movshareToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void nowvideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowvideoToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                nowvideoToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                nowvideoToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void putlockerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (putlockerToolStripMenuItem1.CheckState == CheckState.Unchecked)
            {
                putlockerToolStripMenuItem1.CheckState = CheckState.Checked;
            }
            else
            {
                putlockerToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }
        }

        private void sockshareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sockshareToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                sockshareToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                sockshareToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void nowvideoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (nowvideoToolStripMenuItem1.CheckState == CheckState.Unchecked)
            {
                nowvideoToolStripMenuItem1.CheckState = CheckState.Checked;
            }
            else
            {
                nowvideoToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }
        }

        private void dwnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dwnToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                dwnToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                dwnToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void videoweedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (videoweedToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                videoweedToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                videoweedToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Flash";
                bw.RunWorkerAsync("Most Played");
            }
            catch { }
        }

        private void btn_randgame_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Flash";
                bw.RunWorkerAsync("Random Flash");
            }
            catch { }
        }

        private void btn_normcalc_Click(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.online-calculator.com/swf/online-calculator.swf");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 6:
                    maintab.SelectedIndex = 8;
                    break;
            }
        }

        private void btn_Skill_Click(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.runehq.com/calculators/rs2/c.swf?calc=SkillsCalculator");
        }

        private void btn_battlexp_Click(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.runehq.com/calculators/rs2/CombatExperienceCalculator.swf");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.global-rs.com/calculators/combat/popup/");
        }

        private void btn_Energyrestore_Click(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.runehq.com/calculators/rs2/EnergyRestorationCalculator.swf");
        }

        private void btn_equiptbonus_Click(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.runehq.com/calculators/rs2/eC.swf");
        }

        private void btn_meleemax_Click(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.runehq.com/calculators/rs2/WarriorMaxHitCalculator.swf");
        }

        private void btn_rangemax_Click(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.runehq.com/calculators/rs2/RangedMaxHitCalculator.swf");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.runehq.com/calculators/rs2/PestControlCalculator.swf");
        }

        private void btn_prayerdrain_Click(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.runehq.com/calculators/rs2/PrayerDrainCalculator.swf");
        }

        private void btn_mismanage_Click(object sender, EventArgs e)
        {
            wbCalc.Navigate("http://www.runehq.com/calculators/rs2/MiscellaniaManagementCalculator.swf");
        }

        private void btn_featured_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Movie";
                bw.RunWorkerAsync("Featured Movie");
            }
            catch { }
        }

        private void Moviefill(string url)
        {
            string html = RandysStringFunctions.GetHtmlContent(url);
            MatchCollection rtntitle = Regex.Matches(html, @"(?<="" >).*(?=</a></h1>)", RegexOptions.Compiled);
            MatchCollection rtnpath = Regex.Matches(html, @"(?<=movie_pic""><a href="").*(?="" ><img src=)", RegexOptions.Compiled);
            MatchCollection rtnimage = Regex.Matches(html, @"(?<=><img src="").*[jpg](?="" width=)", RegexOptions.Compiled);
            Match rtnrealpath;
            Size lSize = webvideo.Size;
            string tmp = "";
            VidUrl.Clear();
            VidPic.Clear();
            VidTitle.Clear();
            for (int runs = 0; runs < rtntitle.Count; runs++)
            {
                try
                {
                    tmp = RandysStringFunctions.GetHtmlContent(rtnpath[runs].ToString());
                    if ((rtnrealpath = Regex.Match(tmp, @"(?<=url=http://www.putlocker.com/file/).*(?=','newwin','fullscreen=yes)", RegexOptions.Compiled)).Success && putlockerToolStripMenuItem1.CheckState == CheckState.Checked)
                    {
                        VidUrl.Add("http://www.putlocker.com/embed/" + rtnrealpath.Value);
                        VidTitle.Add(rtntitle[runs].ToString());
                        VidPic.Add(rtnimage[runs].ToString());
                        VidType = "Movie";
                    }
                    else if ((rtnrealpath = Regex.Match(tmp, @"(?<=url=http://www.sockshare.com/file/).*(?=','newwin','fullscreen=yes)", RegexOptions.Compiled)).Success && sockshareToolStripMenuItem.CheckState == CheckState.Checked)
                    {
                        VidUrl.Add("http://www.sockshare.com/embed/" + rtnrealpath.Value);
                        VidTitle.Add(rtntitle[runs].ToString());
                        VidPic.Add(rtnimage[runs].ToString());
                    }
                    else if ((rtnrealpath = Regex.Match(tmp, @"(?<=url=http://www.nowvideo.eu/video/).*(?=','newwin','fullscreen=yes)", RegexOptions.Compiled)).Success && nowvideoToolStripMenuItem1.CheckState == CheckState.Checked)
                    {
                        VidUrl.Add("http://embed.nowvideo.eu/embed.php?v=" + rtnrealpath.Value + "&width=" + lSize.Width.ToString() + "&height=" + lSize.Height.ToString());
                        VidTitle.Add(rtntitle[runs].ToString());
                        VidPic.Add(rtnimage[runs].ToString());
                    }
                    else if ((rtnrealpath = Regex.Match(tmp, @"(?<=url=http://dwn.so/v/).*(?=','newwin','fullscreen=yes)", RegexOptions.Compiled)).Success && dwnToolStripMenuItem.CheckState == CheckState.Checked)
                    {
                        VidUrl.Add("http://dwn.so/player/embed.php?v=" + rtnrealpath.Value + "&width=" + lSize.Width.ToString() + "&height=" + lSize.Height.ToString());
                        VidTitle.Add(rtntitle[runs].ToString());
                        VidPic.Add(rtnimage[runs].ToString());
                    }
                    else if ((rtnrealpath = Regex.Match(tmp, @"(?<=url=http://www.videoweed.es/file/).*(?=','newwin','fullscreen=yes)", RegexOptions.Compiled)).Success && videoweedToolStripMenuItem.CheckState == CheckState.Checked)
                    {
                        VidUrl.Add("http://embed.videoweed.es/embed.php?v=" + rtnrealpath.Value + "&width=" + lSize.Width.ToString() + "&height=" + lSize.Height.ToString());
                        VidTitle.Add(rtntitle[runs].ToString());
                        VidPic.Add(rtnimage[runs].ToString());
                    }
                }
                catch { }
            }
        }

        private void btn_latest_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Movie";
                bw.RunWorkerAsync("Latest Movie");
            }
            catch { }
        }

        private void btn_newrel_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Movie";
                bw.RunWorkerAsync("Newest Movie");
            }
            catch { }
        }

        private void btn_mostviews_Click(object sender, EventArgs e)
        {
            try
            {
                switch_complete = "Movie";
                bw.RunWorkerAsync("Most Viewed Movie");
            }
            catch { }
        }

        private void rad_moviesearch_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_moviesearch.Checked == true)
            {
                btn_featured.Visible = true;
                btn_newrel.Visible = true;
                btn_latest.Visible = true;
                btn_mostviews.Visible = true;
            }
            else
            {
                btn_featured.Visible = false;
                btn_newrel.Visible = false;
                btn_latest.Visible = false;
                btn_mostviews.Visible = false;
            }
            trv_shows.Visible = false;
        }

        private void videoweedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (videoweedToolStripMenuItem1.CheckState == CheckState.Unchecked)
            {
                videoweedToolStripMenuItem1.CheckState = CheckState.Checked;
            }
            else
            {
                videoweedToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }
        }

        private void uploadcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uploadcToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                uploadcToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                uploadcToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void btn_addfav_Click(object sender, EventArgs e)
        {
            if (!cmb_youtubechnfavs.Items.Contains(currentytchannelname))
            {
                cmb_youtubechnfavs.Items.Add(currentytchannelname);
                ytFavURL.Add(currentytchannel);
                ytFavTitle.Add(currentytchannelname);
                btn_addfav.Visible = false;
                btn_removefav.Visible = true;
            }
        }

        private void btn_gofav_Click(object sender, EventArgs e)
        {
            if (cmb_youtubechnfavs.Items.Count > 0 && cmb_youtubechnfavs.SelectedIndex > -1 && ytFavTitle.Count > 0 && bw.IsBusy != true)
            {
                lstvidposition = cmb_youtubechnfavs.SelectedIndex;
                switch_complete = "FavChannel";
                bw.RunWorkerAsync("FavChannel");
            }
        }

        private void btn_removefav_Click(object sender, EventArgs e)
        {
            if (cmb_youtubechnfavs.Items.Contains(currentytchannelname))
            {
                int i = cmb_youtubechnfavs.Items.IndexOf(currentytchannelname);
                cmb_youtubechnfavs.Items.Remove(currentytchannelname);
                ytFavTitle.RemoveAt(i);
                ytFavURL.RemoveAt(i);
                btn_addfav.Visible = true;
                btn_removefav.Visible = false;
            }
        }

        private void rad_ytchannelsec_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_ytchannelsec.Checked == true)
            {
                cmb_youtubechnfavs.Visible = true;
                btn_gofav.Visible = true;
            }
            else
            {
                cmb_youtubechnfavs.Visible = false;
                btn_gofav.Visible = false;
                btn_addfav.Visible = false;
                btn_removefav.Visible = false;
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        private void tmr_embed_Tick(object sender, EventArgs e)
        {
            try
            {
                if (p != null)
                {
                    if (Process.GetProcessesByName(p.ProcessName).Length > 0)
                    {
                        if (keepEmbededToolStripMenuItem.CheckState == CheckState.Checked)
                        {
                            int processID;
                            GetWindowThreadProcessId(GetForegroundWindow(), out processID);
                            Process processToCheck = Process.GetProcessById(processID);
                            if (p.Id != processToCheck.Id)
                            {
                                SetForegroundWindow(p.MainWindowHandle);
                            }
                        }
                        else
                        {
                            keepEmbededToolStripMenuItem.CheckState = CheckState.Unchecked;
                            keepEmbededToolStripMenuItem.Image = null;
                            tmr_embed.Enabled = false;
                        }
                    }
                    else
                    {
                        p = null;
                        keepEmbededToolStripMenuItem.CheckState = CheckState.Unchecked;
                        keepEmbededToolStripMenuItem.Image = null;
                        tmr_embed.Enabled = false;
                    }
                }
                else
                {
                    keepEmbededToolStripMenuItem.CheckState = CheckState.Unchecked;
                    keepEmbededToolStripMenuItem.Image = null;
                    tmr_embed.Enabled = false;
                }
            }
            catch
            {
                keepEmbededToolStripMenuItem.CheckState = CheckState.Unchecked;
                keepEmbededToolStripMenuItem.Image = null;
                tmr_embed.Enabled = false;
            }
        }

        private void keepEmbededToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (keepEmbededToolStripMenuItem.CheckState == CheckState.Unchecked && p != null)
            {
                keepEmbededToolStripMenuItem.CheckState = CheckState.Checked;
                keepEmbededToolStripMenuItem.Image = global::RS_Client.Properties.Resources.externalproc;
                tmr_embed.Enabled = true;
            }
            else
            {
                keepEmbededToolStripMenuItem.CheckState = CheckState.Unchecked;
                keepEmbededToolStripMenuItem.Image = null;
                tmr_embed.Enabled = false;
            }
        }

        public void cpuusage_Tick(object sender, EventArgs e)
        {
            Double cpuUsage = Processor.GetUsage();
            if (cpuUsage < 0)
            {
                cpu_strip.Text = "CPU: " + ((int)(cpuUsage) * -1) + "%";
            }
            else
            {
                cpu_strip.Text = "CPU: " + (int)(cpuUsage) + "%";
            }

            Process proc = Process.GetCurrentProcess();
            ram_strip.Text = "RAM: " + (proc.PrivateMemorySize64 / 1048576).ToString() + "MB";
        }

        private void tmr_twitter_Tick(object sender, EventArgs e)
        {
            try
            {
                var fromTwitter = Twitter.Parse(twitteruser);
                if (fromTwitter[(fromTwitter.Count - 1)].Message != latesttweet)
                {
                    twitterfeeds.DropDownItems.Add(fromTwitter[(fromTwitter.Count - 1)].Message);
                    latesttweet = fromTwitter[(fromTwitter.Count - 1)].Message;
                    Notification frmOut = new Notification();
                    frmOut.Set_Text(fromTwitter[(fromTwitter.Count - 1)].Message);
                    frmOut.Show();
                }
            }
            catch { }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (startFollowingToolStripMenuItem.Checked == false)
            {
                twitterfeeds.DropDownItems.Clear();
                try
                {
                    var fromTwitter = Twitter.Parse(txttwitteraccount.Text);
                    twitteruser = txttwitteraccount.Text;
                    foreach (Twitter m in fromTwitter)
                    {
                        twitterfeeds.DropDownItems.Add(m.Message);
                    }
                    latesttweet = fromTwitter[(fromTwitter.Count - 1)].Message;
                    twitterfeeds.Text = twitteruser;
                    try
                    {
                        switch (checkTimeToolStripMenuItem.SelectedItem.ToString())
                        {
                            case "3 Seconds":
                                tmr_twitter.Interval = 3000;
                                tmr_twitter.Enabled = true;
                                break;

                            case "5 Seconds":
                                tmr_twitter.Interval = 5000;
                                tmr_twitter.Enabled = true;
                                break;

                            case "10 Seconds":
                                tmr_twitter.Interval = 10000;
                                tmr_twitter.Enabled = true;
                                break;

                            case "30 Seconds":
                                tmr_twitter.Interval = 30000;
                                tmr_twitter.Enabled = true;
                                break;

                            case "1 Minute":
                                tmr_twitter.Interval = 60000;
                                tmr_twitter.Enabled = true;
                                break;

                            case "5 Minutes":
                                tmr_twitter.Interval = 300000;
                                tmr_twitter.Enabled = true;
                                break;

                            case "10 Minutes":
                                tmr_twitter.Interval = 600000;
                                tmr_twitter.Enabled = true;
                                break;

                            case "30 Minutes":
                                tmr_twitter.Interval = 1800000;
                                tmr_twitter.Enabled = true;
                                break;

                            case "1 Hour":
                                tmr_twitter.Interval = 3600000;
                                tmr_twitter.Enabled = true;
                                break;

                            default:
                                tmr_twitter.Interval = 10000;
                                tmr_twitter.Enabled = true;
                                break;
                        }
                    }
                    catch
                    {
                        tmr_twitter.Interval = 10000;
                        tmr_twitter.Enabled = true;
                    }
                    startFollowingToolStripMenuItem.Checked = true;
                }
                catch { }
            }
            else
            {
                tmr_twitter.Enabled = false;
                startFollowingToolStripMenuItem.Checked = false;
            }
        }

        private void rSSEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rSSEmailToolStripMenuItem.Checked == false)
            {
                try
                {
                    emailuser = txt_emailuser.Text;
                    emailpass = txt_emailpass.Text;
                    ImapClient ic = new ImapClient("imap.gmail.com", emailuser, emailpass,
                                    ImapClient.AuthMethods.Login, 993, true);
                    ic.SelectMailbox("INBOX");
                    MailMessage[] mm = ic.GetMessages(0, ic.GetMessageCount());
                    latestemail = mm[ic.GetMessageCount() - 1].Subject;
                    ic.Dispose();

                    try
                    {
                        switch (cmb_emailtime.SelectedItem.ToString())
                        {
                            case "3 Seconds":
                                tmr_email.Interval = 3000;
                                tmr_email.Enabled = true;
                                break;

                            case "5 Seconds":
                                tmr_email.Interval = 5000;
                                tmr_email.Enabled = true;
                                break;

                            case "10 Seconds":
                                tmr_email.Interval = 10000;
                                tmr_email.Enabled = true;
                                break;

                            case "30 Seconds":
                                tmr_email.Interval = 30000;
                                tmr_email.Enabled = true;
                                break;

                            case "1 Minute":
                                tmr_email.Interval = 60000;
                                tmr_email.Enabled = true;
                                break;

                            case "5 Minutes":
                                tmr_email.Interval = 300000;
                                tmr_email.Enabled = true;
                                break;

                            case "10 Minutes":
                                tmr_email.Interval = 600000;
                                tmr_email.Enabled = true;
                                break;

                            case "30 Minutes":
                                tmr_email.Interval = 1800000;
                                tmr_email.Enabled = true;
                                break;

                            case "1 Hour":
                                tmr_email.Interval = 3600000;
                                tmr_email.Enabled = true;
                                break;

                            default:
                                tmr_email.Interval = 10000;
                                tmr_email.Enabled = true;
                                break;
                        }
                    }
                    catch
                    {
                        tmr_email.Interval = 10000;
                        tmr_email.Enabled = true;
                    }

                    rSSEmailToolStripMenuItem.Checked = true;
                }
                catch { }
            }
            else
            {
                tmr_email.Enabled = false;
                rSSEmailToolStripMenuItem.Checked = false;
            }
        }

        private void currentEmailNotificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                emailuser = txt_emailuser.Text;
                emailpass = txt_emailpass.Text;
                emailstrip.DropDownItems.Clear();
                ImapClient ic = new ImapClient("imap.gmail.com", emailuser, emailpass,
                                ImapClient.AuthMethods.Login, 993, true);
                ic.SelectMailbox("INBOX");
                MailMessage[] mm = ic.GetMessages(0, ic.GetMessageCount());
                foreach (MailMessage m in mm)
                {
                    emailstrip.DropDownItems.Add(m.Subject);
                }
                ic.Dispose();
            }
            catch { }
        }

        private void tmr_email_Tick(object sender, EventArgs e)
        {
            try
            {
                ImapClient ic = new ImapClient("imap.gmail.com", emailuser, emailpass,
                                    ImapClient.AuthMethods.Login, 993, true);
                ic.SelectMailbox("INBOX");
                MailMessage[] mm = ic.GetMessages(0, ic.GetMessageCount());
                if (mm[ic.GetMessageCount() - 1].Subject != latestemail)
                {
                    latestemail = mm[ic.GetMessageCount() - 1].Subject;
                    emailstrip.DropDownItems.Add(latestemail);
                    Notification frmOut = new Notification();
                    frmOut.Set_Text(mm[ic.GetMessageCount() - 1].Subject);
                    frmOut.Show();
                }

                ic.Dispose();
            }
            catch { }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (e.Argument.ToString())
            {
                case "Read Manga":
                    manga_page();
                    break;

                case "Top Games":
                    GameFill("http://www.gamefudge.com/toprated");
                    break;

                case "Latest Games":
                    GameFill("http://www.gamefudge.com/latest");
                    break;

                case "Most Played":
                    GameFill("http://www.gamefudge.com/mostplayed");
                    break;

                case "Random Flash":
                    string hml = RandysStringFunctions.GetHtmlContent("http://www.gamefudge.com/");
                    int intStart = hml.IndexOf("Random Games");
                    hml = hml.Substring(intStart, hml.Length - intStart);
                    MatchCollection rtntitle = Regex.Matches(hml, @"(?<=.*><a href=.*"">).*(?=</a></span><span )", RegexOptions.IgnoreCase);
                    MatchCollection rtnurl = Regex.Matches(hml, @"(?<=.*><a href="").*(?="">.*</a></span><span )", RegexOptions.IgnoreCase);
                    MatchCollection rtnimg = Regex.Matches(hml, @"(?<=.*><img src="").*(?="" class=)", RegexOptions.IgnoreCase);
                    GameEmbedURL.Clear();
                    GameURL.Clear();
                    GamePreview.Clear();
                    GameTitle.Clear();
                    int[] numbers = new int[] { rtntitle.Count, rtnurl.Count, rtnimg.Count };
                    int minNumber = numbers.Min();
                    for (int runs = 0; runs < minNumber; runs++)
                    {
                        GamePreview.Add(@"http://www.gamefudge.com/" + rtnimg[runs].ToString());
                        GameTitle.Add(RandysStringFunctions.Removetags(rtntitle[runs].ToString()));
                        GameURL.Add(@"http://www.gamefudge.com/" + rtnurl[runs].ToString());
                        hml = RandysStringFunctions.GetHtmlContent(@"http://www.gamefudge.com/" + rtnurl[runs].ToString());
                        Match rtnembedurl = Regex.Match(hml, @"(?<=new SWFObject."").*[.]swf", RegexOptions.IgnoreCase);
                        GameEmbedURL.Add(rtnembedurl.ToString());
                    }
                    break;

                case "Search Games":
                    searchgames();
                    break;

                case "Manga":
                    searchmanga();
                    break;

                case "Random Manga":
                    randommanga();
                    break;

                case "Random Anime":
                    randomanime();
                    break;

                case "Featured Movie":
                    Moviefill("http://movie25.com/movies/featured-movies/");
                    break;

                case "Most Viewed Movie":
                    Moviefill("http://movie25.com/movies/most-viewed/");
                    break;

                case "Newest Movie":
                    Moviefill("http://movie25.com/movies/new-releases/");
                    break;

                case "Latest Movie":
                    Moviefill("http://movie25.com/movies/latest-added/");
                    break;

                case "YoutubeChannel":
                    currentytchannel = "http://www.youtube.com" + VidUrl[lstvidposition] + "/videos?view=0";
                    currentytchannel = currentytchannel.Replace("//videos?", "/videos?");
                    currentytchannelname = VidTitle[lstvidposition];
                    setupvid(currentytchannel, @"(?<=watch[?]v=.*(\n)[\t\n\v\f\r \u00a0\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u200b\u2028\u2029\u3000]+)[A-Z0-9a-z!@#$]+.*", @"(?<=<a href=""/watch[?]v=)[a-zA-Z0-9-_]+(?=.*title)", @"(?<=src=""//)i[0-9]+.*ytimg.*mqdefault.jpg");
                    break;

                case "FavChannel":
                    currentytchannelname = ytFavTitle[lstvidposition];
                    currentytchannel = ytFavURL[lstvidposition];
                    setupvid(currentytchannel, @"(?<=watch[?]v=.*(\n)[\t\n\v\f\r \u00a0\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u200b\u2028\u2029\u3000]+)[A-Z0-9a-z!@#$]+.*", @"(?<=<a href=""/watch[?]v=)[a-zA-Z0-9-_]+(?=.*title)", @"(?<=src=""//)i[0-9]+.*ytimg.*mqdefault.jpg");
                    break;

                case "Video":
                    searchsite();
                    break;

                case "Anime":
                    setupvid(VidUrl[lstvidposition], @"(?<=bookmark"" title="")[a-zA-Z0-9:/.! '-]+", @"(?<=episode_list"">&nbsp;&nbsp;<a href="")[a-zA-Z0-9:/.! '-]+");
                    VidType = "Animep2";
                    break;

                case "Animep2":
                    string _html = RandysStringFunctions.GetHtmlContent(VidUrl[lstvidposition]);
                    _html = _html.Replace(@"""http://lovemyanimechat.chatango.com/group", "");
                    MatchCollection _rtntitle = Regex.Matches(_html, @"(?<=.*embed.*=*""http://)[a-zA-Z0-9.]+", RegexOptions.Compiled);
                    MatchCollection _rtnpath = Regex.Matches(_html, @"(?<=.*embed.*=*"")http[a-zA-Z0-9.:/-?-&-]+", RegexOptions.Compiled);
                    Match rtnCoords;
                    AnimeUrl.Clear();
                    AnimeTitle.Clear();
                    int[] _numbers = new int[] { _rtntitle.Count, _rtnpath.Count };
                    int _minNumber = _numbers.Min();
                    string path;
                    Size lSize = webvideo.Size;
                    for (int runs = 0; runs + 1 < _minNumber; runs++)
                    {
                        path = _rtnpath[runs].ToString();
                        if ((rtnCoords = Regex.Match(path, @"w=[0-9]+", RegexOptions.Compiled)).Success)
                        {
                            path = path.Replace(rtnCoords.Value, "w=" + lSize.Width.ToString());
                            rtnCoords = Regex.Match(path, @"h=[0-9]+", RegexOptions.Compiled);
                            path = path.Replace(rtnCoords.Value, "h=" + lSize.Height.ToString());
                        }
                        else if ((rtnCoords = Regex.Match(path, @"width=[0-9]+", RegexOptions.Compiled)).Success)
                        {
                            path = path.Replace(rtnCoords.ToString(), "width=" + lSize.Width.ToString());
                            rtnCoords = Regex.Match(path, @"height=[0-9]+", RegexOptions.Compiled);
                            path = path.Replace(rtnCoords.ToString(), "height=" + lSize.Height.ToString());
                        }
                        AnimeTitle.Add(_rtntitle[runs].ToString());
                        AnimeUrl.Add(path);
                    }
                    if (_rtntitle.Count > 0)
                    {
                        VidType = "Animep3";
                    }
                    break;
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (switch_complete)
            {
                case "Read Manga":
                    try
                    {
                        txt_page.Text = page;
                        txt_chapter.Text = chapter;
                        WebRequest requesPic = WebRequest.Create(mangapicurl);
                        WebResponse responePic = requesPic.GetResponse();
                        pic_manga0.Image = Image.FromStream(responePic.GetResponseStream());
                        maintab.SelectedIndex = 3;
                    }
                    catch { }
                    break;

                case "Flash":
                    lst_games.Focus();
                    lst_games.Items.Clear();
                    lst_games.BeginUpdate();
                    lst_games.Items.AddRange(GameTitle.ToArray());
                    lst_games.EndUpdate();
                    break;

                case "Manga":
                    lst_manga.Items.Clear();
                    lst_manga.Focus();
                    lst_manga.BeginUpdate();
                    lst_manga.Items.AddRange(MangaTitle.ToArray());
                    lst_manga.EndUpdate();
                    break;

                case "Random Manga":
                    try
                    {
                        lst_manga.Items.Clear();
                        WebRequest requestPic = WebRequest.Create(mangapicurl);
                        WebResponse responsePic = requestPic.GetResponse();
                        Image img = Image.FromStream(responsePic.GetResponseStream());
                        pic_manga0.Image = img;
                        maintab.SelectedIndex = 3;
                        txt_chapter.Text = "1";
                        txt_page.Text = "1";
                        txt_title.Text = originaltitle;
                    }
                    catch { }
                    break;

                case "Random Anime":
                    lst_vids.Items.Clear();
                    if (VidTitle.Count != 0)
                    {
                        lst_vids.BeginUpdate();
                        lst_vids.Items.AddRange(VidTitle.ToArray());
                        lst_vids.EndUpdate();
                        lst_vids.Focus();
                        VidType = "Animep2";
                    }
                    else
                    {
                        bw.RunWorkerAsync("Random Anime");
                    }
                    break;

                case "Movie":
                    lst_vids.Items.Clear();
                    if (VidUrl.Count > 0)
                    {
                        VidType = "Movie";
                        lst_vids.BeginUpdate();
                        lst_vids.Items.AddRange(VidTitle.ToArray());
                        lst_vids.EndUpdate();
                        lst_vids.Focus();
                    }
                    break;

                case "YoutubeChannel":
                    lst_vids.Items.Clear();
                    if (trv_shows.Visible == true)
                    {
                        trv_shows.Visible = false;
                    }
                    if (!cmb_youtubechnfavs.Items.Contains(currentytchannelname))
                    {
                        if (btn_addfav.Visible == false)
                        {
                            btn_addfav.Visible = true;
                        }
                        if (btn_removefav.Visible == true)
                        {
                            btn_removefav.Visible = false;
                        }
                    }
                    else
                    {
                        if (btn_addfav.Visible == false)
                        {
                            btn_addfav.Visible = true;
                        }
                        if (btn_removefav.Visible == true)
                        {
                            btn_removefav.Visible = false;
                        }
                    }

                    cmb_youtubechnfavs.Visible = true;
                    btn_gofav.Visible = true;

                    VidType = "Youtube";
                    lst_vids.BeginUpdate();
                    lst_vids.Items.AddRange(VidTitle.ToArray());
                    lst_vids.EndUpdate();
                    lst_vids.Focus();
                    break;

                case "FavChannel":
                    lst_vids.Items.Clear();
                    if (btn_addfav.Visible == true)
                    {
                        btn_addfav.Visible = false;
                    }
                    if (btn_removefav.Visible == false)
                    {
                        btn_removefav.Visible = true;
                    }
                    if (lst_vids.Items.Count > 0)
                    {
                        VidType = "Youtube";
                    }
                    if (trv_shows.Visible == true)
                    {
                        trv_shows.Visible = false;
                    }
                    if (rad_ytchannelsec.Checked == true && rad_youtubesearch.Checked == true)
                    {
                        cmb_youtubechnfavs.Visible = true;
                        btn_gofav.Visible = true;
                    }
                    else
                    {
                        cmb_youtubechnfavs.Visible = false;
                        btn_gofav.Visible = false;
                    }

                    VidType = "Youtube";
                    lst_vids.BeginUpdate();
                    lst_vids.Items.AddRange(VidTitle.ToArray());
                    lst_vids.EndUpdate();
                    lst_vids.Focus();
                    break;

                case "Video":
                    lst_vids.Items.Clear();
                    if (trv_shows.Visible == true)
                    {
                        trv_shows.Visible = false;
                    }
                    if (btn_addfav.Visible == true)
                    {
                        btn_addfav.Visible = false;
                    }
                    if (btn_removefav.Visible == true)
                    {
                        btn_removefav.Visible = false;
                    }
                    cmb_youtubechnfavs.Visible = false;
                    btn_gofav.Visible = false;

                    lst_vids.BeginUpdate();
                    lst_vids.Items.AddRange(VidTitle.ToArray());
                    lst_vids.EndUpdate();
                    lst_vids.Focus();
                    break;

                case "Anime":
                    lst_vids.Items.Clear();
                    lst_vids.BeginUpdate();
                    lst_vids.Items.AddRange(VidTitle.ToArray());
                    lst_vids.EndUpdate();
                    lst_vids.Focus();
                    break;

                case "Animep2":
                    lst_vids.Items.Clear();
                    lst_vids.BeginUpdate();
                    lst_vids.Items.AddRange(AnimeTitle.ToArray());
                    lst_vids.EndUpdate();
                    lst_vids.Items.Add("<< Go back to episodes");
                    break;
            }
        }

        private void vidbullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vidbullToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                vidbullToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                vidbullToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void rSOldSchoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playtype = "http://oldschool37.runescape.com/j1";
            play_game(playtype, 0);
        }

        private void rSClassicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playtype = "http://runescape.com/classicapplet/classicgame.ws?f=3&j=1";
            play_game(playtype, 0);
        }

        private void currentRSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playtype = "http://www.runescape.com/game.ws?j=1";
            play_game(playtype, 22);

        }
    }
}