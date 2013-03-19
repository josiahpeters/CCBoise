using CCBoise.Core;
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
    public class DetailedImageElement : Element, IElementSizing, IImageUpdated
    {
        static NSString ckey = new NSString("detailedImageKey");

        DetailImageData detailImageData;

        ApiNode apiNode;

        Func<ApiNode, UIViewController> createOnSelected;

        public DetailedImageElement(string imageUri, string title, string detail)
            : base(title)
        {
            detailImageData = new DetailImageData()
            {
                ImageUri = new Uri(imageUri),
                Title = title,
                SubTitle = detail
            };
        }

        public DetailedImageElement(ApiNode apiNode, Func<ApiNode, UIViewController> createOnSelected)
            : base(apiNode.Title)
        {
            detailImageData = new DetailImageData()
            {
                ImageUri = new Uri(apiNode["thumbnailSml"]),
                Title = apiNode.Title,
                SubTitle = apiNode.Description
            };

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
                cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            }

            cell.TextLabel.Text = detailImageData.Title;
            cell.DetailTextLabel.Text = detailImageData.SubTitle;
            cell.DetailTextLabel.LineBreakMode = UILineBreakMode.WordWrap;
            cell.DetailTextLabel.Lines = 2;
            
            // to show an image on the right instead of the left. Just set the accessory view to that of a UIImageView.
            if (cell.AccessoryView == null)
            {
                var img = ImageLoader.DefaultRequestImage(detailImageData.ImageUri, this);

                if (img != null)
                {
                    var imgView = new UIImageView(img);
                    imgView.Frame = new RectangleF(0,0,75,65);
                    cell.AccessoryView = imgView;
                }
            }
            return cell;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public float GetHeight(UITableView tableView, NSIndexPath indexPath)
        {
            return 85f;
        }

        public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath path)
        {
            var viewController = createOnSelected(apiNode);

            dvc.ActivateController(viewController);
        }

        public void UpdatedImage(Uri uri)
        {
            if (uri == null || detailImageData.ImageUri == null)
                return;

            var root = GetImmediateRootElement();
            if (root == null || root.TableView == null)
                return;
            root.TableView.ReloadRows(new NSIndexPath[] { IndexPath }, UITableViewRowAnimation.None);
        }
    }
}
