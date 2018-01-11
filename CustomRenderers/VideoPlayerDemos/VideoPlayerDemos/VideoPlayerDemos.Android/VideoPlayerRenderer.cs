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
using System.ComponentModel;

[assembly: ExportRenderer(typeof(MediaHelpers.VideoPlayer),
                          typeof(MediaHelpers.Droid.VideoPlayerRenderer))]

namespace MediaHelpers.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, VideoView>
    {
        // Used to display transport controls
        MediaController mediaController;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (Control == null)
            {
                VideoView videoView = new VideoView(Context);

                // TODO: Fix aspect ratio. This is not helping!
                // Might have to override GetDesiredSize!
                videoView.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

                SetNativeControl(videoView);
            }

            if (args.OldElement != null)
            {
                args.OldElement.PlayRequested -= OnPlayRequested;
                args.OldElement.PauseRequested -= OnPauseRequested;
                args.OldElement.StopRequested -= OnStopRequested;
            }

            if (args.NewElement != null)
            {
                SetSource();
                SetAreTransportControlsEnabled();

                args.NewElement.PlayRequested += OnPlayRequested;
                args.NewElement.PauseRequested += OnPauseRequested;
                args.NewElement.StopRequested += OnStopRequested;
            }
        }

        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            SizeRequest size = base.GetDesiredSize(widthConstraint, heightConstraint);
            return size;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == VideoPlayer.SourceProperty.PropertyName)
            {
                SetSource();
            }
            else if (args.PropertyName == VideoPlayer.AreTransportControlsEnabledProperty.PropertyName)
            {
                SetAreTransportControlsEnabled();
            }
        }

        void SetSource()
        {
            Android.Net.Uri uri = null;

            if (Element.Source != null)
            {
                if (Element.Source is UriVideoSource)
                {
                    string uriString = (Element.Source as UriVideoSource).Uri;
                    uri = Android.Net.Uri.Parse(uriString);
                }
                else
                {
                    // TODO for file sources
                }
            }

            Control.SetVideoURI(uri);

            // TODO: Is there an AutoPlay property?
            if (uri != null && Element.AutoPlay)
            {
                Control.Start();
            }
        }

        void SetAreTransportControlsEnabled()
        {
            if (Element.AreTransportControlsEnabled)
            {
                mediaController = new MediaController(Context);
                // SetAnchorView and SetMediaPlayer seem to have the same effect 
                //     mediaController.SetAnchorView(videoView);
                mediaController.SetMediaPlayer(Control);
                Control.SetMediaController(mediaController);
            }
            else
            {
                Control.SetMediaController(null);

                if (mediaController != null)
                {
                    mediaController.SetMediaPlayer(null);
                    mediaController = null;
                }
            }
        }

        // Event handlers to implement methods
        void OnPlayRequested(object sender, EventArgs args)
        {
            Control.Start();
        }

        void OnPauseRequested(object sender, EventArgs args)
        {
            Control.Pause();
        }

        void OnStopRequested(object sender, EventArgs args)
        {
            Control.StopPlayback();
        }
    }
}