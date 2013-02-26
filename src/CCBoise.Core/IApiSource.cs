using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCBoise.Core
{
    public interface IApiSource
    {
        List<ApiElement> GetElements(string name);
        List<ApiElement> GetElementDetail(string name, string identifier);
    }

    public class ApiElement
    {
        public string Id { get; set; }
        public string Title { get; set; }
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
