using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;

namespace CCBoise.Core
{
    public interface IApiSource
    {
        void GetElements(string name, Action<List<ApiElement>> callback);
        void GetElementDetail(string name, string identifier, Action<List<ApiElement>> callback);
    }
    public interface IWebRequest
    {
        void GetUrl(string url, Action<Stream, Exception> callback);
    }
    public interface IHelper
    {
        string GetString(JsonValue obj, string key);
    }

    public class ApiElement
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DetailJsonUrl { get; set; }
        public string SiteUrl { get; set; }
        public string Type { get; set; }

        public List<ApiElement> Children { get; set; }

        private Dictionary<string, object> items;

        public List<object> Values()
        {
            return items.Values.ToList();
        }
        public List<string> Keys()
        {
            return items.Keys.ToList();
        }

        public object this[string key]
        {
            get
            {
                if (items.ContainsKey(key))
                    return items[key];
                else
                    return null;
            }
            set
            {
                items[key] = value;
            }
        }

        public ApiElement()
        {
            items = new Dictionary<string, object>();
            Children = new List<ApiElement>();
        }
    }
}
