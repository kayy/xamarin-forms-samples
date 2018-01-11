using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using AVFoundation;
using AVKit;
using CoreMedia;
using Foundation;
using UIKit;

using MediaHelpers;
using MediaHelpers.iOS;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(VideoPlayerRenderer))]

namespace MediaHelpers.iOS
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        AVPlayerViewController _playerViewController;       // solely for ViewController property

        public override UIViewController ViewController
        {
            get
            {
                return _playerViewController;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (Control == null)
            {
                _playerViewController = new AVPlayerViewController();
                AVPlayer player = new AVPlayer();
                _playerViewController.Player = player;
                SetNativeControl(_playerViewController.View);




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
            AVPlayerItem playerItem = null;

            if (Element.Source != null)
            {
                AVAsset asset = null;

                if (Element.Source is UriVideoSource)
                {
                    string uriString = (Element.Source as UriVideoSource).Uri;
                    asset = AVAsset.FromUrl(new NSUrl(uriString));
                }
                else
                {
                    // TODO for file sources
                }

                if (asset != null)
                {
                    playerItem = new AVPlayerItem(asset);

                    ((IVideoController)Element).Duration = TimeSpan.FromMilliseconds(playerItem.Duration.Value);

                    observer = playerItem.AddObserver("duration", NSKeyValueObservingOptions.Initial, 
                        

                        (sender) =>
                                        {
                                            ((IVideoController)Element).Duration = TimeSpan.FromMilliseconds(playerItem.Duration.Value);

                                        });
                    
                }
            }
            
            ((AVPlayerViewController)ViewController).Player.ReplaceCurrentItemWithPlayerItem(playerItem);

            // TODO: Is there an AutoPlay property?

            if (playerItem != null && Element.AutoPlay)
            {
                ((AVPlayerViewController)ViewController).Player.Play();
            }
        }

        private IDisposable observer;
/*
        public void Observer(NSObservedChange nsObservedChange)
        {
            ((IVideoController)Element).Duration = TimeSpan.FromMilliseconds(Control.playerItem.Duration.Value);
        }
*/
        void SetAreTransportControlsEnabled()
        {
            ((AVPlayerViewController)ViewController).ShowsPlaybackControls = Element.AreTransportControlsEnabled;
        }

        // Event handlers to implement methods
        void OnPlayRequested(object sender, EventArgs args)
        {
            ((AVPlayerViewController)ViewController).Player.Play();
        }

        void OnPauseRequested(object sender, EventArgs args)
        {
            ((AVPlayerViewController)ViewController).Player.Pause();
        }

        void OnStopRequested(object sender, EventArgs args)
        {
            ((AVPlayerViewController)ViewController).Player.Pause();
            ((AVPlayerViewController)ViewController).Player.Seek(new CMTime(0, 0));
        }
    }
}