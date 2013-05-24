using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RS_Client
{
    public class Twitter
    {
        public string Message { get; set; }

        public static List<Twitter> Parse(string User)
        {
            var rv = new List<Twitter>();
            var url = "http://twitter.com/statuses/user_timeline/" + User + ".rss";

            var element = XElement.Load(url);
            foreach (var node in element.Element("channel").Elements("item"))
            {
                var twit = new Twitter();
                var message = node.Element("description").Value;

                //remove username information
                twit.Message = message.Replace(User + ": ", string.Empty);
                rv.Add(twit);
            }

            return rv;
        }
    }
}
