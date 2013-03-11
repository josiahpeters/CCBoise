using CCBoise.Core;
using CCBoise.iOSApp.MonoTouchUI;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.MediaPlayer;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCBoise.iOSApp
{
    public class ApiVideoElement
    {
        public static UIViewController HtmlSelected(ApiNode apiNode)
        {
            var vc = new UIViewController();

            var webView = new UIWebView(UIScreen.MainScreen.Bounds)
            {
                BackgroundColor = UIColor.White,
                ScalesPageToFit = false,
                AutoresizingMask = UIViewAutoresizing.All
            };

            var html = apiNode["content"];

            if (apiNode["contentNode"] != null)
            {
                html = apiNode[apiNode["contentNode"]];
            }


            if (html != null && !html.Contains("<html"))
            {
                html = String.Format("<html><head><link href=\"main-style.css\" rel=\"stylesheet\" type=\"text/css\" /></head><body><h1>{0}</h1>{1}</body><html>", apiNode.Title, html);
            }

            if (html != null)
                webView.LoadHtmlString(html, new NSUrl("HtmlContent/", true));


            vc.NavigationItem.Title = apiNode.Title;

            vc.View.AutosizesSubviews = true;
            vc.View.AddSubview(webView);

            return vc;
        }

        public static UIViewController VideoSelected(ApiNode apiNode)
        {
            var url = apiNode["videoSrc1"];

            if (apiNode["contentNode"] != null)
            {
                url = apiNode[apiNode["contentNode"]];
            }

            //var videoController = new UIVideoController(url);

            //videoController.NavigationItem.Title = apiNode.Title;

            //return videoController;


            var vc = new UIViewController();


            var webView = new UIWebView(UIScreen.MainScreen.Bounds)
            {
                BackgroundColor = UIColor.White,
                ScalesPageToFit = true,
                AutoresizingMask = UIViewAutoresizing.All
            };

            var request = new NSUrlRequest(new NSUrl(url), NSUrlRequestCachePolicy.UseProtocolCachePolicy, 60);

            webView.LoadRequest(request);

            vc.NavigationItem.Title = apiNode.Title;

            vc.View.AutosizesSubviews = true;
            vc.View.AddSubview(webView);

            return vc;
        }

        public static UIViewController AudioSelected(ApiNode apiNode)
        {
            var url = apiNode["videoSrc1"];

            if (apiNode["contentNode"] != null)
            {
                url = apiNode[apiNode["contentNode"]];
            }

            //var videoController = new UIVideoController(url);

            //videoController.NavigationItem.Title = apiNode.Title;

            //return videoController;


            var vc = new UIViewController();


            var webView = new UIWebView(UIScreen.MainScreen.Bounds)
            {
                BackgroundColor = UIColor.White,
                ScalesPageToFit = true,
                AutoresizingMask = UIViewAutoresizing.All
            };

            var request = new NSUrlRequest(new NSUrl(url), NSUrlRequestCachePolicy.UseProtocolCachePolicy, 60);

            webView.LoadRequest(request);

            vc.NavigationItem.Title = apiNode.Title;

            vc.View.AutosizesSubviews = true;
            vc.View.AddSubview(webView);

            return vc;
        }
    }
}
