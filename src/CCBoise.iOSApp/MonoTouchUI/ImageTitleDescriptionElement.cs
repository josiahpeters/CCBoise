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
    public class ImageTitleDescriptionElement : Element, IElementSizing, IImageUpdated
    {
        static NSString ckey = new NSString("imageTitleDescriptionKey");
        public event NSAction Tapped;
        public UILineBreakMode LineBreakMode = UILineBreakMode.TailTruncation;
        public UIViewContentMode ContentMode = UIViewContentMode.Left;
        public int Lines = 1;

        public UITableViewCellAccessory Accessory = UITableViewCellAccessory.DisclosureIndicator;

        UIFont titleFont = UIFont.FromName("Helvetica", 16f);
        UIFont detailFont = UIFont.FromName("Helvetica", 12f);
        UIImage image;
        Uri imageUri;

        string imgUrl = "";

        string title;
        string detail;

        public ImageTitleDescriptionElement(string imageUri, string title, string detail) // badgeImage, string cellText, NSAction tapped)
            : base(title)
        {
            this.ImageUri = new Uri(imageUri);
            this.imgUrl = imageUri;
            this.title = title;
            this.detail = detail;
        }

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

        // Loads the image from the specified uri (use this or Image)
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

            //cell.Accessory = Accessory;

            //var titleLabel = cell.TextLabel;
            //titleLabel.Text = title;
            //titleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            //titleLabel.Lines = 2;
            //titleLabel.Font = titleFont;
            //titleLabel.ContentMode = UIViewContentMode.Left;

            //var detailLabel = cell.DetailTextLabel;
            //detailLabel.Text = detail;
            //detailLabel.Font = detailFont;
            //detailLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            //detailLabel.Lines = 3;

            //// get existing image if there is one
            //var imgView = cell.ImageView;
            //// temporarily hold the image
            //UIImage img;

            //if (imgView != null)
            //{
            //    imgView.Frame = new RectangleF(0, 0, 50, 35);

            //    if (ImageUri != null)
            //        img = ImageLoader.DefaultRequestImage(ImageUri, this);
            //    else if (Image != null)
            //        img = Image;
            //    else
            //        img = null;

            //    imgView.Image = img;
            //}


            ////imgView.Image.Scale(new SizeF(50, 50));

            //cell.SetNeedsDisplay();

            return cell;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public float GetHeight(UITableView tableView, NSIndexPath indexPath)
        {
            SizeF size = new SizeF(tableView.Bounds.Width - 40, float.MaxValue);
            float height = 0;
            height += tableView.StringSize(title, titleFont, size, LineBreakMode).Height + 10;
            height += tableView.StringSize(detail, detailFont, size, LineBreakMode).Height + 10;
            //float height = tableView.StringSize(title, font, size, LineBreakMode).Height + 10;

            // Image is 57 pixels tall, add some padding
            return 110f;// Math.Max(height, 75);
        }

        public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath path)
        {
            if (Tapped != null)
                Tapped();
            tableView.DeselectRow(path, true);
        }

        public static UIImage MakeCalendarBadge(UIImage template, string smallText, string bigText)
        {
            using (var cs = CGColorSpace.CreateDeviceRGB())
            {
                using (var context = new CGBitmapContext(IntPtr.Zero, 57, 57, 8, 57 * 4, cs, CGImageAlphaInfo.PremultipliedLast))
                {
                    //context.ScaleCTM (0.5f, -1);
                    context.TranslateCTM(0, 0);
                    context.DrawImage(new RectangleF(0, 0, 57, 57), template.CGImage);
                    context.SetFillColor(1, 1, 1, 1);

                    context.SelectFont("Helvetica", 10f, CGTextEncoding.MacRoman);

                    // Pretty lame way of measuring strings, as documented:
                    var start = context.TextPosition.X;
                    context.SetTextDrawingMode(CGTextDrawingMode.Invisible);
                    context.ShowText(smallText);
                    var width = context.TextPosition.X - start;

                    context.SetTextDrawingMode(CGTextDrawingMode.Fill);
                    context.ShowTextAtPoint((57 - width) / 2, 46, smallText);

                    // The big string
                    context.SelectFont("Helvetica-Bold", 32, CGTextEncoding.MacRoman);
                    start = context.TextPosition.X;
                    context.SetTextDrawingMode(CGTextDrawingMode.Invisible);
                    context.ShowText(bigText);
                    width = context.TextPosition.X - start;

                    context.SetFillColor(0, 0, 0, 1);
                    context.SetTextDrawingMode(CGTextDrawingMode.Fill);
                    context.ShowTextAtPoint((57 - width) / 2, 9, bigText);

                    context.StrokePath();

                    return UIImage.FromImage(context.ToImage());
                }
            }
        }

        public void UpdatedImage(Uri uri)
        {
            if (uri == null || ImageUri == null)
                return;
            var root = GetImmediateRootElement();
            if (root == null || root.TableView == null)
                return;
            root.TableView.ReloadRows(new NSIndexPath[] { IndexPath }, UITableViewRowAnimation.None);
        }
    }
}
