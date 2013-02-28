using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCBoise.iOSApp
{
    /// <summary>
    ///  Used to display a cell that will launch a web browser when selected.
    /// </summary>
    public class HtmlStringElement : Element
    {
        string html;
        static NSString hkey = new NSString("HtmlStringElement");
        UIWebView web;

        public HtmlStringElement(string caption, string html)
            : base(caption)
        {
            Html = html;
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
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellKey);
                cell.SelectionStyle = UITableViewCellSelectionStyle.Blue;
            }
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            cell.TextLabel.Text = Caption;
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

            web = new UIWebView(UIScreen.MainScreen.Bounds)
            {
                BackgroundColor = UIColor.White,
                ScalesPageToFit = true,
                AutoresizingMask = UIViewAutoresizing.All
            };

            web.LoadError += (webview, args) =>
            {
                NetworkActivity = false;
                vc.NavigationItem.RightBarButtonItem = null;
                if (web != null)
                    web.LoadHtmlString(
                        String.Format("<html><center><font size=+5 color='red'>{0}:<br>{1}</font></center></html>", "An error occurred.", "Unable to load content"), null);
            };
            vc.NavigationItem.Title = Caption;

            vc.View.AutosizesSubviews = true;
            vc.View.AddSubview(web);

            dvc.ActivateController(vc);
            // load the html string
            web.LoadHtmlString(html, new NSUrl("HtmlContent/", true));
        }
    }
}
