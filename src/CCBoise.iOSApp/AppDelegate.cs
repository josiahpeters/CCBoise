using System;
using System.Collections.Generic;
using System.Linq;

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
		UIViewController viewController;

		RootElement rootElement;
		DialogViewController rootDvc;
		UIBarButtonItem addButton;

		int n = 0;
		
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
			
			rootElement = new RootElement ("To Do List"){new Section ()};

            var jsonElement = JsonElement.FromFile ("Json/CCBoise.json");
            var videoElements = JsonElement.FromFile("Json/video.json");
			rootDvc = new DialogViewController (jsonElement);
			
			navigationController = new UINavigationController(rootDvc);
			//navigationController.PushViewController (viewController, false);
			window.RootViewController = navigationController;

            //sections elements sections elements

            var videos = jsonElement["videos"] as RootElement;

            var jsonVideo = jsonElement["videos"] as JsonElement;

            //json

            //jsonElement["videos"].Get
            videos.Add(videoElements);

			rootDvc.NavigationItem.RightBarButtonItem = addButton;
			
			// If you have defined a view, add it here:
			//window.AddSubview (navigationController.View);
			
			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}

		public class Task 
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public DateTime DueDate { get; set; }
		}
}

