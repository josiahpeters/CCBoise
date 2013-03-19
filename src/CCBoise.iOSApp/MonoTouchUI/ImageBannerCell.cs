using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CCBoise.iOSApp.MonoTouchUI
{
    public class ImageBannerCell : UITableViewCell
    {
        UIImageView imageView;

        public ImageBannerCell(UIImage image)
        {
            UpdateCell(image);
            imageView = new UIImageView();

            ContentView.Add(imageView);
        }

        public void UpdateCell(UIImage image)
        {
            if (image != null)
            {
                imageView.Image = image;
                imageView.Frame = new RectangleF(0, 0, image.Size.Width, image.Size.Height);
                SetNeedsDisplay();
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            float margin = 5;

            imageView.Frame = new RectangleF(margin,margin, ContentView.Bounds.Width-margin*2, ContentView.Bounds.Height-margin*2);

            imageView.SetNeedsDisplay();
        }
    }
}
