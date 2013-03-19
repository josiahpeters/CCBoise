using CCBoise.Core;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using CCBoise.iOSApp;
using System.Drawing;

namespace CCBoise.iOSApp
{
    public class CustomRootElement : RootElement
    {
        JsonObject data;
        ApiNode apiNode;

        bool loading = false;
        const int CSIZE = 16;
        const int SPINNER_TAG = 1000;

        public CustomRootElement(string title, JsonObject data)
            : base(title)
        {
            this.data = data;
            apiNode = DrupalApiParser.FromJsonObject(data);
            //apiNode = ApiNode.
        }

        private Element CreateElement(ApiNode element, ApiNode parentNode)
        {
            string childType = parentNode["childType"];
            string childAction = parentNode["childAction"];

            // if there is an item call for the api, build the url here
            if (parentNode.ApiItemUrl != null)
                element.ApiUrl = String.Format(parentNode.ApiItemUrl, element.Id);

            if (parentNode["contentNode"] != null)
                element["contentNode"] = parentNode["contentNode"];

            Func<ApiNode, UIViewController> selectedAction = null;

            if (element.Title.Contains("&#039;"))
                element.Title = element.Title.Replace("&#039;","'");


            switch(childAction)
            {
                case "video":
                    selectedAction = ApiVideoElement.VideoSelected;
                    break;
                case "audio":
                    selectedAction = ApiVideoElement.AudioSelected;
                    break;
                default:
                    selectedAction = ApiVideoElement.HtmlSelected;
                    break;
            }

            switch (childType)
            {
                case "detailedImage":

                    return new DetailedImageElement(element, selectedAction);

                case "htmlContent":

                    // try to combine date and reference for a description
                    if(element.Description == null)
                        element.Description = String.Format("{0} - {1}", element["date"], element["reference"]);                   

                    return new ApiDetailElement(element, selectedAction);

                default:
                    var strElement = new MultilineElement(element.Title, element["description"]);

                    //strElement.
                    return strElement;
            }
        }

        public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath path)
        {
            tableView.DeselectRow(path, false);
            if (loading)
                return;
            var cell = GetActiveCell();
            var spinner = StartSpinner(cell);
            loading = true;

            var request = new NSUrlRequest(new NSUrl(apiNode.ApiUrl), NSUrlRequestCachePolicy.UseProtocolCachePolicy, 60);

            var connection = new NSUrlConnection(request, new ConnectionDelegate((data, error) =>
            {
                var apiNodes = DrupalApiParser.ParseJsonStream(data);

                loading = false;
                spinner.StopAnimating();
                spinner.RemoveFromSuperview();

                string childType = apiNode["childType"];

                if (this.Count == 0)
                    Add(new Section(""));
                else
                    this[0].Clear();

                foreach (var element in apiNodes)
                {
                    this[0].Add(CreateElement(element, apiNode));
                }

                var newDvc = new DialogViewController(this, true)
                {
                    Autorotate = true
                };
                PrepareDialogViewController(newDvc);
                dvc.ActivateController(newDvc);

                return;
            }));
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
    }
}
