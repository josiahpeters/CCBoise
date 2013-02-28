using System;
using System.Collections.Generic;
using System.Linq;
using System.Json;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace CCBoise.iOSApp
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UINavigationController navigationController;

		DialogViewController rootDvc;
		
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

            JsonElement.RegisterElementMapping("htmlstring", (json, data) => {

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

            var jsonElement = JsonElement.FromFile ("Json/CCBoise.js");
            var videoElements = JsonElement.FromFile("Json/video.js");
			rootDvc = new DialogViewController (jsonElement);
			
			navigationController = new UINavigationController(rootDvc);

			window.RootViewController = navigationController;

            //sections elements sections elements
            var videos = jsonElement["videos"] as RootElement;

            var jsonVideo = jsonElement["videos"] as JsonElement;

            videos.Add(videoElements);

			// If you have defined a view, add it here:
			//window.AddSubview (navigationController.View);
			
			// make the window visible
			window.MakeKeyAndVisible ();
			
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

