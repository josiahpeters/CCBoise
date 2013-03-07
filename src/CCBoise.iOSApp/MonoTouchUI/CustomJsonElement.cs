using CCBoise.Core;
using CCBoise.Data;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading;

namespace CCBoise.iOSApp.MonoTouchUI
{
    public class CustomJsonElement : RootElement
    {
        ApiElement apiElement;

        bool loading = false;
        const int CSIZE = 16;
        const int SPINNER_TAG = 1000;

        public CustomJsonElement(ApiElement apiElement)
            : base(apiElement.Title)
        {
            this.apiElement = apiElement;
            this.Caption = apiElement.Title;
            this.UnevenRows = true;
        }

        public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath path)
        {
            if (apiElement.SiteUrl == null)
            {
                base.Selected(dvc, tableView, path);
                return;
            }

            tableView.DeselectRow(path, false);
            if (loading)
                return;
            var cell = GetActiveCell();
            var spinner = StartSpinner(cell);
            loading = true;

            var request = new NSUrlRequest(new NSUrl(apiElement.SiteUrl), NSUrlRequestCachePolicy.UseProtocolCachePolicy, 60);

            var connection = new NSUrlConnection(request, new ConnectionDelegate((data, error) =>
            {
                var apiElements = DrupalApiParser.ParseJsonStream(data, apiElement);

                var root = new RootElement(apiElement.Title);

                var section = new Section("");

                loading = false;
                spinner.StopAnimating();
                spinner.RemoveFromSuperview();



                if (apiElements.Count == 1)
                {
                    var element = apiElements.First();

                    if (element["content"] != null)
                    {
                        var html = element["content"].ToString();
                        UIWebView webView = new UIWebView(UIScreen.MainScreen.Bounds)
                        {
                            BackgroundColor = UIColor.Clear,// UIColor.White,
                            ScalesPageToFit = false,
                            AutoresizingMask = UIViewAutoresizing.All
                        };

                        if (!html.Contains("<html"))
                        {
                            html = String.Format("<html><head><link href=\"main.css\" rel=\"stylesheet\" type=\"text/css\" /></head><body><h1>{0}</h1>{1}</body><html>", element.Title, html);
                        }

                        webView.LoadHtmlString(html, new NSUrl("HtmlContent/", true));


                        var wc = new UIViewController();

                        wc.NavigationItem.Title = Caption;

                        wc.View.AutosizesSubviews = true;
                        wc.View.AddSubview(webView);

                        PrepareDialogViewController(wc);
                        dvc.ActivateController(wc);

                        return;
                    }
                }

                foreach (var element in apiElements)
                {
                    section.Add(BuildElement(element));
                }

                root.Add(section);

                var newDvc = new DialogViewController(root, true)
                {
                    Autorotate = true
                };
                PrepareDialogViewController(newDvc);
                dvc.ActivateController(newDvc);

                return;
            }));
        }

        Element BuildElement(ApiElement element)
        {

            if (element["videoSrc1"] != null)
            {
                return new DetailedImageElement(element);
            }
            if (element["content"] != null || element.DetailJsonUrl != null)
            {
                return new HtmlStringElement(element.Id, element["content"].ToString());
            }

            var root = new CustomJsonElement(element);
            var section = new Section("");

            root.UnevenRows = true;

            foreach (var child in element.Children)
            {
                section.Add(BuildElement(child));
            }
            root.Add(section);

            return root; 
        }

        UIActivityIndicatorView StartSpinner(UITableViewCell cell)
        {
            var cvb = cell.ContentView.Bounds;

            var spinner = new UIActivityIndicatorView(new RectangleF(cvb.Width - CSIZE / 2, (cvb.Height - CSIZE) / 2, CSIZE, CSIZE))
            {
                Tag = SPINNER_TAG,
                ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray,
            };
            cell.ContentView.AddSubview(spinner);
            spinner.StartAnimating();
            cell.Accessory = UITableViewCellAccessory.None;

            return spinner;
        }
    }

    public class DrupalApiParser
    {
        public static List<ApiElement> ParseJsonStream(Stream jsonStream, ApiElement parent)
        {
            List<ApiElement> elements = new List<ApiElement>();

            var sr = new StreamReader(jsonStream);

            var dt = sr.ReadToEnd();

            var json = JsonValue.Parse(dt) as JsonObject;

            var nodes = json["nodes"] as JsonArray;

            foreach (JsonObject node in nodes)
            {
                var element = node["node"] as JsonObject;

                var apiElement = new ApiElement()
                {
                    Id = GetString(element, "id"),
                    Title = GetString(element, "title"),
                    Description = GetString(element, "description"),
                    SiteUrl = GetString(element, "siteURL")
                };

                if (parent.DetailJsonUrl != null)
                    apiElement.SiteUrl = String.Format(parent.DetailJsonUrl, apiElement.Id);

                foreach (var key in element.Keys)
                {
                    apiElement[key] = GetString(element, key);
                }

                elements.Add(apiElement);
            }

            return elements;
        }
        public static string GetString(JsonValue obj, string key)
        {
            if (obj.ContainsKey(key))
                if (obj[key].JsonType == JsonType.String)
                    return (string)obj[key];
            return null;
        }
    }

    class ConnectionDelegate : NSUrlConnectionDelegate
    {
        Action<Stream, NSError> callback;
        NSMutableData buffer;

        public ConnectionDelegate(Action<Stream, NSError> callback)
        {
            this.callback = callback;
            buffer = new NSMutableData();
        }

        public override void ReceivedResponse(NSUrlConnection connection, NSUrlResponse response)
        {
            buffer.SetLength(0);
        }

        public override void FailedWithError(NSUrlConnection connection, NSError error)
        {
            callback(null, error);
        }

        public override void ReceivedData(NSUrlConnection connection, NSData data)
        {
            buffer.AppendData(data);
        }

        public override void FinishedLoading(NSUrlConnection connection)
        {
            callback(buffer.AsStream(), null);
        }
    }
}
