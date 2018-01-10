using System;
using System.Collections.Generic;
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
                // NOTE: MediaPlayerElement rather than MediaElement is recommended for Windows 10 Build 1607 and later

                MediaElement mediaElement = new MediaElement();
                SetNativeControl(mediaElement);

                // TODO: This property enables or suppresses the transport UI. Move into property setting
                mediaElement.AreTransportControlsEnabled = true;

                // TODO: Move into Source property setting
                mediaElement.Source = new Uri("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4");
                mediaElement.Play();
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