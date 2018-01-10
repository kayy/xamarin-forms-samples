using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaHelpers
{
    public class VideoPlayer : View
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create("Source", typeof(VideoSource), typeof(VideoPlayer));

        public VideoPlayer()
        {
            ;
        }

        public VideoSource Source
        {
            set { SetValue(SourceProperty, value); }
            get { return (VideoSource)GetValue(SourceProperty); }
        }
    }
}
