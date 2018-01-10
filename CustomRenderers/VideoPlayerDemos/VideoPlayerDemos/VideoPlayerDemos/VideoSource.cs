using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaHelpers
{
    public class VideoSource : Element
    {
        public static VideoSource FromUri(Uri uri)
        {
            return new UriVideoSource { Uri = uri };
        }
    }
}
