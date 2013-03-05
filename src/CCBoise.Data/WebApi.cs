using CCBoise.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Net;
using System.IO;
using System.Json;

namespace CCBoise.Data
{
    public class WebApi : IApiSource
    {
        private Dictionary<string, WebApiEndpoint> endpoints;

        private IWebRequest webRequest;
        private IHelper helper;

        public void GetElements(string name, Action<List<ApiElement>> callback)
        {
            webRequest.GetUrl(endpoints["video"].Url, (data, error) =>
            {
                List<ApiElement> elements = new List<ApiElement>();

                var sr = new StreamReader(data);

                var dt = sr.ReadToEnd();

                var json = JsonValue.Parse(dt) as JsonObject;

                var nodes = json["nodes"] as JsonArray;

                foreach(JsonObject node in nodes)
                {
                    var element = node["node"] as JsonObject;

                    var apiElement = new ApiElement()
                    {
                        Id = helper.GetString(element, "id"),
                        Title = helper.GetString(element, "title"),
                        Description = helper.GetString(element, "description"),
                        SiteUrl = helper.GetString(element, "siteURL")
                    };

                    foreach (var key in element.Keys)
                    {
                        apiElement[key] = helper.GetString(element, key);
                    }

                    elements.Add(apiElement);
                }

                callback(elements);
                // nodes
                    // [0].node
                        // id
            });
        }

        

        public void GetElementDetail(string name, string identifier, Action<List<ApiElement>> callback)
        {
            throw new NotImplementedException();
        }

        public WebApi(IWebRequest webRequest, IHelper helper)
        {
            this.webRequest = webRequest;
            this.helper = helper;

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
