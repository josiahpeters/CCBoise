using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CCBoise.iOSApp.MonoTouchUI
{
    public class WebViewCell : UITableViewCell
    {
        string title;
        string htmlContent;

        UIWebView webView;

        public WebViewCell(string title, string htmlContent, UIWebView webView)
        {
            this.title = title;
            this.htmlContent = htmlContent;

            this.webView = webView;//new UIWebView();
            
            //ContentView.Add(webView);
        }

        public void Update()
        {
            SetNeedsDisplay();
            webView.SetNeedsDisplay();
        }
        
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            webView.Frame = ContentView.Bounds;
            //webView.Frame
            //if (pageSize == null)
            //    webView.Frame = new RectangleF(0, 0, ContentView.Bounds.Width, ContentView.Bounds.Height * 4);
            //else
            //webView.Frame = (RectangleF)pageSize;

            //webView.SetNeedsDisplay();
        }

        
    }
}
