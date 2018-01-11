using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHelpers
{ 
    public interface IVideoController
    {
        // bool CanPause { set; get; }


            TimeSpan Duration { set; get; }
    

        event EventHandler PlayRequested;       // ???? See IWebViewController

    }
}
