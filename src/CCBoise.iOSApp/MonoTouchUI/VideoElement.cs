using CCBoise.Core;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.MediaPlayer;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCBoise.iOSApp.MonoTouchUI
{
    public class VideoElement : Element
    {
        ApiElement apiElement;

        static NSString skey = new NSString("VideoElement");

        public VideoElement(ApiElement apiElement)
            : base(apiElement.Title)
        {
            this.apiElement = apiElement;
            this.Caption = apiElement.Title;
        }

        public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath path)
        {
            var url = apiElement.SiteUrl;

            if (apiElement["videoSrc2"] != null)
                url = apiElement["videoSrc2"].ToString();

            var videoController = new UIVideoController(url);

            dvc.ActivateController(videoController);
        }
    }
}
