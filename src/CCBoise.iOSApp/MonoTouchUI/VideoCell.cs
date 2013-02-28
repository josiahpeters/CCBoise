using MonoTouch.Dialog.Utilities;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CCBoise.iOSApp
{
    public class DetailImageData
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public Uri ImageUri { get; set; }
    }
    public class VideoCell : UITableViewCell
    {
        VideoCellView videoCellView;

        DetailImageData data;

        public VideoCell(string title, string subTitle, string imageUri)
        {
            data = new DetailImageData { 
                Title = title,
                SubTitle = subTitle,
                ImageUri = new Uri(imageUri)
            };

            videoCellView = new VideoCellView(data);
            UpdateCell(data);
            ContentView.Add(videoCellView);
        }

        public void UpdateCell(DetailImageData data)
        {
            videoCellView.Update(data);
            SetNeedsDisplay();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            videoCellView.Frame = ContentView.Bounds;
            videoCellView.SetNeedsDisplay();
        }

        public class VideoCellView : UIView, IImageUpdated
        {
            DetailImageData data;

            static UIFont titleFont = UIFont.BoldSystemFontOfSize(15);
            static UIFont subTitleFont = UIFont.BoldSystemFontOfSize(12);

            UILineBreakMode LineBreakMode = UILineBreakMode.TailTruncation;


            UIImage image;
            Uri imageUri;

            public UIImage Image
            {
                get
                {
                    return image;
                }
                set
                {
                    image = value;
                    imageUri = null;
                }
            }
            public Uri ImageUri
            {
                get
                {
                    return imageUri;
                }
                set
                {
                    imageUri = value;
                    image = null;
                }
            }

            public VideoCellView(DetailImageData data)
            {
                this.data = data;
            }

            public void Update(DetailImageData newData)
            {
                //imgView.Frame = new RectangleF(0, 0, 50, 35);
                ImageUri = newData.ImageUri;

                if (ImageUri != null)
                    image = ImageLoader.DefaultRequestImage(ImageUri, this);
                else if (Image != null)
                    image = Image;
                else
                    image = null;                

                SetNeedsDisplay();
            }

            public override void Draw(RectangleF rect)
            {
                bool highlighted = (Superview.Superview as UITableViewCell).Highlighted;

                float padding = 10;

                float imageWidthPercentage = .30f;

                float width = Bounds.Width - padding * 2;
                float height = Bounds.Height - padding * 2;

                float imageWidth = 120;

                float imageX = width - imageWidth - padding;
                float contentWidth = width - imageWidth - padding * 3;

                var context = UIGraphics.GetCurrentContext();

                UIColor titleTextColor;
                UIColor subTitleTextColor;

                if (highlighted)
                {
                    UIColor.FromRGB(4, 0x79, 0xef).SetColor();
                    context.FillRect(Bounds);
                    titleTextColor = UIColor.White;
                    subTitleTextColor = UIColor.LightGray;
                }
                else
                {
                    UIColor.White.SetColor();
                    context.FillRect(Bounds);
                    titleTextColor = UIColor.Black;
                    subTitleTextColor = UIColor.DarkGray;
                }

                titleTextColor.SetColor();

                var maxTitleHeight = StringSize("A\nA", titleFont, contentWidth, LineBreakMode);

                var computedTitleSize = StringSize(data.Title, titleFont, contentWidth, LineBreakMode);
                
                float titleHeight = maxTitleHeight.Height;

                if (titleHeight > computedTitleSize.Height)
                    titleHeight = computedTitleSize.Height;

                titleTextColor.SetColor();

                DrawString(data.Title, new RectangleF(padding, padding, contentWidth, titleHeight), titleFont);

                subTitleTextColor.SetColor();
                DrawString(data.SubTitle, new RectangleF(padding, padding + titleHeight, contentWidth, height - (titleHeight + padding)), subTitleFont);

                if (image != null)
                {
                    float ratio = image.Size.Height / image.Size.Width;
                    float imageHeight = ratio * imageWidth;

                    image.Draw(new RectangleF(imageX, padding, imageWidth, imageHeight));

                }

                //context.DrawLinearGradient(myGradient, start, end, 0);
            }

            public void UpdatedImage(Uri uri)
            {
                if (uri == null || ImageUri == null)
                    return;

                SetNeedsDisplay(); 
            }
        }
    }

    /*public class MyDataCell : 
    {
        MyDataView myDataView;

        public MyDataCell(MyData myData, NSString identKey)
            : base(UITableViewCellStyle.Default, identKey)
        {
            // Configure your cell here: selection style, colors, properties
            myDataView = new MyDataView(myData);
            ContentView.Add(myDataView);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            myDataView.Frame = ContentView.Bounds;
            myDataView.SetNeedsDisplay();
        }

        // Called by our client code when we get new data.
        public void UpdateCell(MyData newData)
        {
            myDataView.Update(newData);
        }
    }*/
}
