using System;
using System.Collections.Generic;
using System.Linq;
using System.Json;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.IO;
using CCBoise.Data;

namespace CCBoise.iOSApp
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        UINavigationController navigationController;

        DialogViewController rootDvc;

        UITabBarController navigation;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            WebApi api = new WebApi(new iOSWebRequest(), new Helper());

            JsonElement.RegisterElementMapping("htmlstring", (json, data) =>
            {
                var caption = GetString(json, "caption");
                var html = GetString(json, "html");

                return new HtmlStringElement(caption, html);
            });

            JsonElement.RegisterElementMapping("detailedImage", (json, data) =>
            {
                var imageUri = GetString(json, "imageUri");
                var title = GetString(json, "title");
                var detail = GetString(json, "detail");

                return new DetailedImageElement(imageUri, title, detail);
            });


            window = new UIWindow(UIScreen.MainScreen.Bounds);

            JsonObject rootJson;
            using (var reader = File.OpenRead("Json/CCBoise.js"))
            {
                rootJson = JsonObject.Load(reader) as JsonObject;
            }

            var videoElements = JsonElement.FromFile("Json/video.js");


            var sections = rootJson["tabs"] as JsonArray;

            var tabs = new List<UIViewController>();

            RootElement videoElement = null;

            foreach (JsonObject section in sections)
            {
                var tab = new UINavigationController();

                string title = (string)section["title"];
                string icon = (string)section["icon"];

                tab.TabBarItem = new UITabBarItem(title, UIImage.FromFile(icon), 1);

                var jsonElement = JsonElement.FromJson(section);

                var videos = jsonElement["videos"] as RootElement;
                if(videos != null)
                    videoElement = videos;
                //    videos.Add(videoElements);


                tab.PushViewController(new DialogViewController(jsonElement), false);

                tabs.Add(tab);
            }

            api.GetElements("video", (data) => 
            {
                var videoSections = new Section ("");
                videoElement.Add(videoSections);
                foreach (var video in data)
                {
                    videoSections.Add(new DetailedImageElement(video["thumbnailSml"].ToString(), video.Title, video.Description));
                }
            });

            navigation = new UITabBarController();
            navigation.ViewControllers = tabs.ToArray();
            
            navigation.CustomizableViewControllers = new UIViewController[0];

            window.RootViewController = navigation;

            window.MakeKeyAndVisible();

            return true;
        }

        static string GetString(JsonValue obj, string key)
        {
            if (obj.ContainsKey(key))
                if (obj[key].JsonType == JsonType.String)
                    return (string)obj[key];
            return null;
        }
    }

    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
    /*
    static Element LoadImageTitleDetailElement(JsonObject json)
        {
            var imageurl = GetString(json, "imageUri");
            var title = GetString(json, "title");
            var detail = GetString(json, "detail");

            return new ImageTitleDescriptionElement(imageurl, title, detail);
        }

        static Element LoadHtmlStringElement(JsonObject json)
        {
            var caption = GetString(json, "caption");
            var html = GetString(json, "html");

            return new HtmlStringElement(caption, html);
        }
     */

}

