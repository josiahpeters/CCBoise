﻿using CCBoise.Core;
using CCBoise.iOSApp.MonoTouchUI;
using MonoTouch.CoreGraphics;
using MonoTouch.Dialog;
using MonoTouch.Dialog.Utilities;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CCBoise.iOSApp
{

    public class ApiDetailElement : Element
    {
        static NSString ckey = new NSString("ApiElement");

        ApiNode apiNode;

        string title;
        string detail;

        Func<ApiNode, UIViewController> createOnSelected;

        public event NSAction Tapped;

        bool loading = false;
        const int CSIZE = 16;
        const int SPINNER_TAG = 1000;

        public ApiDetailElement(ApiNode apiNode, Func<ApiNode, UIViewController> createOnSelected)
            : base(apiNode.Title)
        {
            this.title = apiNode.Title;
            this.detail = apiNode.Description;
            this.apiNode = apiNode;
            this.createOnSelected = createOnSelected;
        }

        public override UITableViewCell GetCell(UITableView tv)
        {
            var cell = tv.DequeueReusableCell(ckey);
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, ckey);
                cell.SelectionStyle = UITableViewCellSelectionStyle.Blue;
            }
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            cell.TextLabel.Text = title;
            cell.DetailTextLabel.Text = detail;

            return cell;
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath path)
        {
            tableView.DeselectRow(path, false);

            // prevent us from loading twice if someone is impatient
            if (loading)
                return;

            var cell = GetActiveCell();
            var spinner = StartSpinner(cell);
            loading = true;

            if (apiNode.ApiUrl != null)
            {
                var request = new NSUrlRequest(new NSUrl(apiNode.ApiUrl), NSUrlRequestCachePolicy.UseProtocolCachePolicy, 60);

                var connection = new NSUrlConnection(request, new ConnectionDelegate((data, error) =>
                {
                    var apiNodes = DrupalApiParser.ParseJsonStream(data);

                    loading = false;
                    spinner.StopAnimating();
                    spinner.RemoveFromSuperview();

                    var contentApiNode = apiNodes.First();

                    // make sure that we get the content mapping from the parent node, so we know which field to map content to if its not "content"
                    if (apiNode["contentNode"] != null)
                        contentApiNode["contentNode"] = apiNode["contentNode"];

                    dvc.ActivateController(createOnSelected(contentApiNode));

                    return;
                }));
            }
            else
            {
                loading = false;
                dvc.ActivateController(createOnSelected(apiNode));
            }
        }
    }
}