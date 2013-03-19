using System;
using System.Collections.Generic;
using System.Linq;
using System.Json;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.IO;
using CCBoise.Data;
using CCBoise.Core;
using CCBoise.iOSApp.MonoTouchUI;

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

            //JsonElement.RegisterElementMapping("htmlstring", (json, data) =>
            //{
            //    var caption = GetString(json, "caption");
            //    var html = GetString(json, "html");

            //    return new HtmlStringElement(caption, html);
            //});

            //JsonElement.RegisterElementMapping("detailedImage", (json, data) =>
            //{
            //    var imageUri = GetString(json, "imageUri");
            //    var title = GetString(json, "title");
            //    var detail = GetString(json, "detail");

            //    return new DetailedImageElement(imageUri, title, detail);
            //});

            JsonElement.RegisterElementMapping("customRoot", (json, data) =>
            {
                var title = GetString(json, "title");

                return new CustomRootElement(title, json);
            });


            window = new UIWindow(UIScreen.MainScreen.Bounds);

            JsonObject rootJson;
            //http://boise.alertsense.com/CCBoise.js
            using (var reader = File.OpenRead("Json/CCBoise.js"))
            {
                rootJson = JsonObject.Load(reader) as JsonObject;
            }

            var sections = rootJson["tabs"] as JsonArray;

            var tabs = new List<UIViewController>();

            int selectedTabIndex = 0;

            for(int i = 0; i < sections.Count; i++)
            {
                JsonObject section = sections[i] as JsonObject;

                var tab = new UINavigationController();

                // determine if the current tab should be loaded first
                if (section.ContainsKey("selectedTab") && (bool)section["selectedTab"])
                    selectedTabIndex = i;

                string title = (string)section["title"];
                string icon = (string)section["icon"];

                tab.TabBarItem = new UITabBarItem(title, UIImage.FromFile(icon), 1);

                var jsonElement = JsonElement.FromJson(section);

                tab.PushViewController(new DialogViewController(jsonElement), false);

                tabs.Add(tab);
                
            }
            
            navigation = new UITabBarController();
            navigation.ViewControllers = tabs.ToArray();

            navigation.CustomizableViewControllers = new UIViewController[0];

            // set the selected index for which tab to show first
            navigation.SelectedIndex = selectedTabIndex;
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

