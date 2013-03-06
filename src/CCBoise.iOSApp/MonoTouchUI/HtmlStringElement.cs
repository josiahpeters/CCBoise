using CCBoise.iOSApp.MonoTouchUI;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace CCBoise.iOSApp
{
    /// <summary>
    ///  Used to display a cell that will launch a web browser when selected.
    /// </summary>
    public class HtmlStringElement : Element, IElementSizing
    {
        string html;
        static NSString hkey = new NSString("HtmlStringElement");

        UIWebView webView;
        int height = 480;

        WebViewCell currentCell = null;

        public HtmlStringElement(string caption, string html, UIWebView webView)
            : base(caption)
        {
            Html = html;

            this.webView = webView;
            //webView = new UIWebView();
            //webView.LoadFinished += LoadingFinished;
            //webView.LoadHtmlString(html, new NSUrl("HtmlContent/", true));
        }

        public void LoadingFinished(object source, EventArgs e)
        {
            var heightStr = webView.EvaluateJavascript("document.documentElement.scrollHeight");
            int.TryParse(heightStr, out height);

            //webView.Frame = new RectangleF(0, 0, webView.Frame.Width, height);
            //webView.SetNeedsDisplay();
            if (currentCell != null)
            {
                float diff = currentCell.Frame.Width - currentCell.Bounds.Width;
                currentCell.Frame = new RectangleF(0, 0, currentCell.Frame.Width, height);
                currentCell.Bounds = new RectangleF(0, 0, currentCell.Bounds.Width, height - diff);
                //currentCell.SetNeedsDisplay();
                currentCell.ContentView.Add(webView);

                currentCell.Update();
            }
        }

        public float GetHeight(UITableView tableView, NSIndexPath indexPath)
        {
            return height;
        }

        protected override NSString CellKey
        {
            get
            {
                return hkey;
            }
        }
        public string Html
        {
            get
            {
                return html;
            }
            set
            {
                html = value;
            }
        }

        public override UITableViewCell GetCell(UITableView tv)
        {
            var cell = tv.DequeueReusableCell(CellKey);

            if (cell == null)
            {
                cell = new WebViewCell(Caption, html, webView);// UITableViewCell(UITableViewCellStyle.Default, CellKey);
                //cell.ContentMode = UIViewContentMode.Redraw;
                //cell.SelectionStyle = UITableViewCellSelectionStyle.Blue;
            }
            //cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            currentCell = cell as WebViewCell;
            //cell.TextLabel.Text = Caption;
            return cell;
        }

        static bool NetworkActivity
        {
            set
            {
                UIApplication.SharedApplication.NetworkActivityIndicatorVisible = value;
            }
        }

        // We use this class to dispose the web control when it is not
        // in use, as it could be a bit of a pig, and we do not want to
        // wait for the GC to kick-in.
        class WebViewController : UIViewController
        {
            HtmlStringElement container;

            public WebViewController(HtmlStringElement container)
                : base()
            {
                this.container = container;
            }

            public bool Autorotate { get; set; }

            public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
            {
                return Autorotate;
            }
        }

        public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath path)
        {
            int i = 0;
            var vc = new WebViewController(this)
            {
                Autorotate = dvc.Autorotate
            };

            //web = new UIWebView(UIScreen.MainScreen.Bounds)
            //{
            //    BackgroundColor = UIColor.White,
            //    ScalesPageToFit = true,
            //    AutoresizingMask = UIViewAutoresizing.All
            //};

            //web.LoadError += (webview, args) =>
            //{
            //    NetworkActivity = false;
            //    vc.NavigationItem.RightBarButtonItem = null;
            //    if (web != null)
            //        web.LoadHtmlString(
            //            String.Format("<html><center><font size=+5 color='red'>{0}:<br>{1}</font></center></html>", "An error occurred.", "Unable to load content"), null);
            //};
            //vc.NavigationItem.Title = Caption;

            //vc.View.AutosizesSubviews = true;
            //vc.View.AddSubview(web);

            //dvc.ActivateController(vc);
            //// load the html string
            //web.LoadHtmlString(html, new NSUrl("HtmlContent/", true));
        }
    }
}
