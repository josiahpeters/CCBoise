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
    public class HtmlStringElement : Element//, IElementSizing
    {
        static NSString hkey = new NSString("HtmlStringElement");

        string html;

        UIWebView webView;

        public HtmlStringElement(string caption, string html)
            : base(caption)
        {
            if (!html.Contains("<html"))
            {
                this.html = String.Format("<html><head><link href=\"main.css\" rel=\"stylesheet\" type=\"text/css\" /></head><body><h1>{0}</h1>{1}</body><html>", caption, html);
            }
            else
                this.html = html;
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
        public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath path)
        {
            int i = 0;
            var vc = new UIViewController()
            {
                //Autorotate = dvc.Autorotate
            };
            

            webView = new UIWebView(UIScreen.MainScreen.Bounds)
            {
                BackgroundColor = UIColor.White,
                ScalesPageToFit = true,
                AutoresizingMask = UIViewAutoresizing.All
            };

            webView.LoadHtmlString(html, new NSUrl("HtmlContent/", true));

            vc.NavigationItem.Title = Caption;

            vc.View.AutosizesSubviews = true;
            vc.View.AddSubview(webView);

            dvc.ActivateController(vc);
        }
    }
}
