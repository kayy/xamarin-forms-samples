using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using AVFoundation;
using AVKit;
using Foundation;
using UIKit;

using MediaHelpers;
using MediaHelpers.iOS;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(VideoPlayerRenderer))]

namespace MediaHelpers.iOS
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        AVPlayerViewController playerViewController;

        public override UIViewController ViewController
        {
            get
            {
                return playerViewController;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (Control == null)
            {
                playerViewController = new AVPlayerViewController();
                AVPlayer player = new AVPlayer();
                playerViewController.Player = player;
                SetNativeControl(playerViewController.View);

                // TODO: This code suppresses the transport UI. Move into property setting
                // playerViewController.ShowsPlaybackControls = false;

                // TODO: Move this stuff into the Source property setting
                AVAsset asset = AVAsset.FromUrl(new NSUrl("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4"));
                AVPlayerItem playerItem = new AVPlayerItem(asset);
                playerViewController.Player.ReplaceCurrentItemWithPlayerItem(playerItem);

                playerViewController.Player.Play();
            }

            if (args.OldElement != null)
            {

            }

            if (args.NewElement != null)
            {

            }
        }
    }
}