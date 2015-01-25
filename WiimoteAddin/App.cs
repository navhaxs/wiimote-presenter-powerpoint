using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiimoteAddin
{
    class App
    {

        public static AppWindow wnd;

        public static WiimotePairingWorker PairingWorker = new WiimotePairingWorker();


        public static WiimoteLib.WiimoteCollection Wiimote_Collection = new WiimoteLib.WiimoteCollection();
        public static int WiimoteCount = 0;

        public static MainScreenViewModel MainScreenData = new MainScreenViewModel();

        public static WiimoteSlideShowController WiimoteController = new WiimoteSlideShowController();

        public static bool NoWiimotesActive()
        {
            if (WiimoteCount == 0)
            {
                return true;
            } else {
                return false;
            }
        }

    }
}
