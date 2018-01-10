using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: ExportRenderer(typeof(MediaHelpers.VideoPlayer),
                          typeof(MediaHelpers.Droid.VideoPlayerRenderer))]

namespace MediaHelpers.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, VideoView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (Control == null)
            {
                VideoView videoView = new VideoView(Context);
                SetNativeControl(videoView);

                // TODO: This code shows the transport UI. Move into property setting
                MediaController mediaController = new MediaController(Context);
                // SetAnchorView and SetMediaPlayer seem to have the same effect 
                //     mediaController.SetAnchorView(videoView);
                mediaController.SetMediaPlayer(videoView);
                videoView.SetMediaController(mediaController);

                // TODO: Move into Source property setting
                var uri = Android.Net.Uri.Parse("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4");
                videoView.SetVideoURI(uri);
                videoView.Start();
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