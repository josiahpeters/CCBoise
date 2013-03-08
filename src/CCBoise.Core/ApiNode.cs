using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCBoise.Core
{
    public class ApiNode
    {
        public string Id { get; set; }

        public string Title
        {
            get
            { return this["title"]; }
            set
            {
                this["title"] = value;
            }
        }

        public string ApiUrl
        {
            get
            { return this["apiUrl"]; }
            set
            {
                this["apiUrl"] = value;
            }
        }

        public string ApiItemUrl
        {
            get
            { return this["apiItemUrl"]; }
            set
            {
                this["apiItemUrl"] = value;
            }
        }

        public string Description
        {
            get
            { return this["description"]; }
            set
            {
                this["description"] = value;
            }
        }

        public string ImageUrl
        {
            get
            { return this["imageUrl"]; }
            set
            {
                this["imageUrl"] = value;
            }
        }

        public List<ApiElement> Children { get; set; }

        private Dictionary<string, string> items;

        public List<string> Values()
        {
            return items.Values.ToList();
        }
        public List<string> Keys()
        {
            return items.Keys.ToList();
        }

        public string this[string key]
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

        public ApiNode()
        {
            items = new Dictionary<string, string>();
            Children = new List<ApiElement>();
        }        
    }
}
