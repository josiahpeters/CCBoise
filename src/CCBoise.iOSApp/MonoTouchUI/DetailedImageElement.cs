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
    public class DetailedImageElement : Element, IElementSizing
    {
        static NSString ckey = new NSString("detailedImageKey");
        
        string imgUrl = "";
        string title;
        string detail;

        ApiElement apiElement;

        public event NSAction Tapped;

        public DetailedImageElement(string imageUri, string title, string detail)
            : base(title)
        {
            this.imgUrl = imageUri;
            this.title = title;
            this.detail = detail;
        }

        public DetailedImageElement(ApiElement apiElement)
            : base(apiElement.Title)
        {
            this.imgUrl = apiElement["thumbnailSml"].ToString();
            this.title = apiElement.Title;
            this.detail = apiElement.Description;
            this.apiElement = apiElement;
        }

        public override UITableViewCell GetCell(UITableView tv)
        {
            var cell = tv.DequeueReusableCell(ckey);
            if (cell == null)
            {
                cell = new DetailedImageCell(title, detail, imgUrl)
                {
                    SelectionStyle = UITableViewCellSelectionStyle.Blue
                };
            }

            return cell;
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
            if (Tapped != null)
                Tapped();
            tableView.DeselectRow(path, true);

            //MPMoviePlayerController 
            var url = apiElement.SiteUrl;

            if (apiElement["videoSrc2"] != null)
                url = apiElement["videoSrc2"].ToString();

            var videoController = new UIVideoController(url);

            dvc.ActivateController(videoController);
        }
    }
}
