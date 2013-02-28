using CCBoise.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Net;
using System.IO;

namespace CCBoise.Data
{
    public class WebApi : IApiSource
    {
        private Dictionary<string, WebApiEndpoint> endpoints;

        public List<ApiElement> GetElements(string name)
        {
            throw new NotImplementedException();
        }

        public List<ApiElement> GetElementDetail(string name, string identifier)
        {
            throw new NotImplementedException();
        }

        public WebApi()
        {
            endpoints = new Dictionary<string, WebApiEndpoint>()
            {
                { "video", new WebApiEndpoint { Name = "video", Url = "http://www.ccboise.org/api/messages/video" } },
                { "audio", new WebApiEndpoint { Name = "audio", Url = "http://www.ccboise.org/api/messages/audio" } },
                { "devotional", new WebApiEndpoint { Name = "devotional", Url = "http://www.ccboise.org/api/daily/devotionals", ItemUrl = "http://www.ccboise.org/api/daily/devotional/{ItemId}" } },
                { "prayer", new WebApiEndpoint { Name = "prayer", Url = "http://www.ccboise.org/api/daily/prayer" } },
                { "events", new WebApiEndpoint { Name = "events", Url = "http://www.ccboise.org/api/connect/events" } },
                { "calendar", new WebApiEndpoint { Name = "calendar", Url = "http://www.ccboise.org/api/connect/calendar", ItemUrl = "http://www.ccboise.org/api/connect/calendar-event/{ItemId}" } },
            };
        }

        private string getUrlContent(string url)
        {
            //var webClient = new WebClient();
            //var response = webClient.DownloadString (new Uri (url));
            //return response;
            return "";
        }
    }

    internal class WebApiEndpoint
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string ItemUrl { get; set; }
        public ApiElement Element { get; set; }
        public List<ApiElement> ChildElements { get; set; }
    }
}
