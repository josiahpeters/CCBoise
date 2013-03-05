﻿using System;
using System.Collections.Generic;
using System.IO;
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

    public class ApiElement
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SiteUrl { get; set; }

        private Dictionary<string, object> items;

        public object this[string key]
        {
            get
            {
                return items[key];
            }
        }
    }
}
