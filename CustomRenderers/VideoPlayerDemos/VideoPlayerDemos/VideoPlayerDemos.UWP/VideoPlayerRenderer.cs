using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MediaHelpers.VideoPlayer),
                          typeof(MediaHelpers.UWP.VideoPlayerRenderer))]

namespace MediaHelpers.UWP
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, MediaElement>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (Control == null)
            {
                // NOTE: MediaPlayerElement rather than MediaElement is recommended for Windows 10 Build 1607 and later,

                MediaElement mediaElement = new MediaElement();

                mediaElement.MediaOpened += (sender, e) =>
                {
                    ((IVideoController)Element).Duration = mediaElement.NaturalDuration.TimeSpan;
                };

                mediaElement.CurrentStateChanged += (sender, e) =>
                {

                };

                // MediaEnded, MediaFailed


                SetNativeControl(mediaElement);
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
                SetAutoPlay();
                SetAreTransportControlsEnabled();

                

                args.NewElement.PlayRequested += OnPlayRequested;
                args.NewElement.PauseRequested += OnPauseRequested;
                args.NewElement.StopRequested += OnStopRequested;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == VideoPlayer.SourceProperty.PropertyName)
            {
                SetSource();
            }
            else if (args.PropertyName == VideoPlayer.AutoPlayProperty.PropertyName)
            {
                SetAutoPlay();
            }
            else if (args.PropertyName == VideoPlayer.AreTransportControlsEnabledProperty.PropertyName)
            {
                SetAreTransportControlsEnabled();
            }
        }

        void SetSource()
        {
            Uri uri = null;

            if (Element.Source != null)
            {
                if (Element.Source is UriVideoSource)
                {
                    string uriString = (Element.Source as UriVideoSource).Uri;
                    uri = new Uri(uriString);
                }
                else
                {
                    // TODO for file sources
                }
            }

            Control.Source = uri;
        }

        void SetAutoPlay()
        {
            Control.AutoPlay = Element.AutoPlay;
        }

        void SetAreTransportControlsEnabled()
        {
            Control.AreTransportControlsEnabled = Element.AreTransportControlsEnabled;
        }

        // Event handlers to implement methods
        void OnPlayRequested(object sender, EventArgs args)
        {
            Control.Play();
        }

        void OnPauseRequested(object sender, EventArgs args)
        {
            Control.Pause();
        }

        void OnStopRequested(object sender, EventArgs args)
        {
            Control.Stop();
        }
    }
}