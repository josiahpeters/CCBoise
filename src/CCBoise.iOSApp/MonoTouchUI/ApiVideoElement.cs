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

            if (html == null && apiNode["contentNode"] != null)
            {
                html = apiNode[apiNode["contentNode"]];
            }


            if (!html.Contains("<html"))
            {
                html = String.Format("<html><head><link href=\"main-style.css\" rel=\"stylesheet\" type=\"text/css\" /></head><body><h1>{0}</h1>{1}</body><html>", apiNode.Title, html);
            }

            webView.LoadHtmlString(html, new NSUrl("HtmlContent/", true));


            vc.NavigationItem.Title = apiNode.Title;

            vc.View.AutosizesSubviews = true;
            vc.View.AddSubview(webView);

            return vc;
        }

        public static UIViewController VideoSelected(ApiNode apiNode)
        {
            var url = apiNode["videoSrc2"];

            var videoController = new UIVideoController(url);

            videoController.NavigationItem.Title = apiNode.Title;

            return videoController;
        }
    }
}
