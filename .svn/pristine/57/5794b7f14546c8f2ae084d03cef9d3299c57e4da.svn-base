using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace RS_Client
{
    public class RandysStringFunctions
    {
        public static string MakeStringAlphaNumeric(string pDirtyString)
        {
            return Regex.Replace(pDirtyString, @"[^\w\s]", "");
        }

        public static string Removetags(string pDirtyString)
        {
            string rturn = pDirtyString;
            rturn = Regex.Replace(rturn, "<.*?>", string.Empty);
            rturn = rturn.Replace("&quot;", @"""");
            rturn = rturn.Replace("&amp;", "&");
            rturn = rturn.Replace("&#8217;", "'");
            rturn = rturn.Replace("&#13;&#10;", "");
            rturn = rturn.Replace("&#39;s","'");
            rturn = rturn.Replace("&apos;", "'");
            rturn = rturn.Replace("&#8211;", "-");
            rturn = rturn.Replace("&#8216;", "'");
            rturn = rturn.Replace("&#39;", "'");
            rturn = rturn.Replace("Ã©","e");
            return rturn;
        }

        public static string GetHtmlContent(string url)
        {
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(url);
                return htmlCode;
            }
        }
    }
}
