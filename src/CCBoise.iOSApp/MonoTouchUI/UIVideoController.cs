using MonoTouch.Foundation;
using MonoTouch.MediaPlayer;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCBoise.iOSApp.MonoTouchUI
{
    public class UIVideoController : UIViewController
    {
        MPMoviePlayerController moviePlayer;

        string url;

        public UIVideoController(string url)
        {
            this.url = url;
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            playMedia();
        }
        void playMedia()
        {
            moviePlayer = new MPMoviePlayerController(NSUrl.FromString(url));
            View.AddSubview(moviePlayer.View);
            moviePlayer.View.Frame = View.Frame;
            moviePlayer.SetFullscreen(true, true);
            moviePlayer.PrepareToPlay();
            moviePlayer.Play();
        }
    }
}
