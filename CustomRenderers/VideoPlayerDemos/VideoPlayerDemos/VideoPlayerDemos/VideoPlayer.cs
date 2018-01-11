using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaHelpers
{
    public class VideoPlayer : View, IVideoController
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create("Source", typeof(VideoSource), typeof(VideoPlayer), null);

        public static readonly BindableProperty AutoPlayProperty =
            BindableProperty.Create("AutoPlay", typeof(bool), typeof(VideoPlayer), true);

        public static readonly BindableProperty AreTransportControlsEnabledProperty =
            BindableProperty.Create("AreTransportControlsEnabled", typeof(bool), typeof(VideoPlayer), true);

        public VideoSource Source
        {
            set { SetValue(SourceProperty, value); }
            get { return (VideoSource)GetValue(SourceProperty); }
        }

        public bool AutoPlay
        {
            set { SetValue(AutoPlayProperty, value); }
            get { return (bool)GetValue(AutoPlayProperty); }
        }

        public bool AreTransportControlsEnabled
        {
            set { SetValue(AreTransportControlsEnabledProperty, value); }
            get { return (bool)GetValue(AreTransportControlsEnabledProperty); }
        }

        private static readonly BindablePropertyKey DurationPropertyKey =
            BindableProperty.CreateReadOnly("Duration", typeof(TimeSpan), typeof(VideoPlayer), new TimeSpan());

        public static readonly BindableProperty DurationProperty = DurationPropertyKey.BindableProperty;

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
        }

        TimeSpan IVideoController.Duration
        {
            set { SetValue(DurationPropertyKey, value); }
            get { return Duration; }
        }

        // Methods handled by renderers
        public event EventHandler PlayRequested;

        public void Play()
        {
            PlayRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler PauseRequested;

        public void Pause()
        {
            PauseRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler StopRequested;

        public void Stop()
        {
            StopRequested?.Invoke(this, EventArgs.Empty);
        }



    }
}
