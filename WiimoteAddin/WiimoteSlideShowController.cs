using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WiimoteLib;

namespace WiimoteAddin
{
    class WiimoteSlideShowController
    {
        
        public void UpdateWiimoteChanged(object sender, WiimoteLib.WiimoteChangedEventArgs args)
        {
            
            WiimoteState ws = args.WiimoteState;

            // TODO: handle button state changes
            // (on button press)
            
        }

    }
}
