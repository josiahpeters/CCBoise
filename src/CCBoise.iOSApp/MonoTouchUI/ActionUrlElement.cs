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
    public class ActionUrlElement : Element, IElementSizing
    {
        static NSString hkey = new NSString("ActionUrlElement");

        UILineBreakMode LineBreakMode = UILineBreakMode.WordWrap;
        string url;

        public ActionUrlElement(string caption, string url)
            : base(caption)
        {
            this.url = url;
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
            cell.TextLabel.LineBreakMode = LineBreakMode;
            cell.TextLabel.Lines = 4;

            return cell;
        }
        public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath path)
        {
            //NSUrl url = new NSUrl(this.url);
            //var test = UIApplication.SharedApplication;
            //var result = test.CanOpenUrl(url);

            //var vc = new UIViewController();

            //var webView = new UIWebView(UIScreen.MainScreen.Bounds)
            //{
            //    BackgroundColor = UIColor.White,
            //    ScalesPageToFit = true,
            //    AutoresizingMask = UIViewAutoresizing.All
            //};

            //var request = new NSUrlRequest(new NSUrl(url));

            //webView.LoadRequest(request);

            //vc.NavigationItem.Title = "Phone";// apiNode.Title;

            //vc.View.AutosizesSubviews = true;
            //vc.View.AddSubview(webView);
            //dvc.ActivateController(vc);


            if (!UIApplication.SharedApplication.OpenUrl(new NSUrl(url)))
            {
                var av = new UIAlertView("Not supported"
                    , "Sorry, this action is not supported on this device."
                    , null
                    , "Ok thanks"
                    , null);
                av.Show();
            }
            tableView.DeselectRow(path, false);
        }

        public float GetHeight(UITableView tableView, NSIndexPath indexPath)
        {
            float margin = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone ? 40f : 110f;
            SizeF maxSize = new SizeF(tableView.Bounds.Width - margin, float.MaxValue);

            maxSize.Width -= 20;

            var captionFont = UIFont.BoldSystemFontOfSize(17);
            float height = tableView.StringSize(Caption, captionFont, maxSize, LineBreakMode).Height;

            return height + 10;
        }
    }
}
