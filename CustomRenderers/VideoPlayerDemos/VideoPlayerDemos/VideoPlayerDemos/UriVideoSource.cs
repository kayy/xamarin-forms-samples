using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaHelpers
{
    public class UriVideoSource : VideoSource
    {
        public static readonly BindableProperty UriProperty =
            BindableProperty.Create("Uri", typeof(Uri), typeof(UriVideoSource));

        public Uri Uri
        {
            set { SetValue(UriProperty, value); }
            get { return (Uri)GetValue(UriProperty); }
        }
    }
}
