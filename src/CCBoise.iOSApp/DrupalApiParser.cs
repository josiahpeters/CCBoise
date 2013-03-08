using CCBoise.Core;
using MonoTouch.Dialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;

namespace CCBoise.iOSApp
{
    public class DrupalApiParser
    {
        public static List<ApiNode> ParseJsonStream(Stream jsonStream)
        {
            var sr = new StreamReader(jsonStream);

            var dt = sr.ReadToEnd();

            List<ApiNode> apiNodes = new List<ApiNode>();

            var json = JsonValue.Parse(dt) as JsonObject;

            var nodes = json["nodes"] as JsonArray;

            foreach (JsonObject node in nodes)
            {
                var element = node["node"] as JsonObject;

                apiNodes.Add(FromJsonObject(element));

            }
            return apiNodes;
        }
        public static ApiNode FromJsonObject(JsonObject json)
        {
            var apiNode = new ApiNode()
            {
                Id = GetString(json, "id")
            };

            foreach (var key in json.Keys)
            {
                apiNode[key] = GetString(json, key);
            }

            return apiNode;
        }
        public static string GetString(JsonValue obj, string key)
        {
            if (obj.ContainsKey(key))
                if (obj[key].JsonType == JsonType.String)
                    return (string)obj[key];
            return null;
        }
    }
}
