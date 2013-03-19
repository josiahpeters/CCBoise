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

        UIImage image;
        Uri imageUri;

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

        public DetailedImageElement(ApiElement apiElement)
            : base(apiElement.Title)
        {
            //this.imgUrl = apiElement["thumbnailSml"].ToString();
            //this.title = apiElement.Title;
            //this.detail = apiElement.Description;
            //this.apiElement = apiElement;
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
                cell = new DetailedImageCell(detailImageData)
                {
                    SelectionStyle = UITableViewCellSelectionStyle.Blue
                };
            }

            PrepareCell(cell);

            return cell;
        }

        protected void PrepareCell(UITableViewCell cell)
        {
            var detailCell = cell as DetailedImageCell;

            if (detailImageData.Image == null)
            {
                detailImageData.Image = ImageLoader.DefaultRequestImage(detailImageData.ImageUri, this);

                detailCell.UpdateCell(detailImageData);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public float GetHeight(UITableView tableView, NSIndexPath indexPath)
        {
            return 110f;
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
