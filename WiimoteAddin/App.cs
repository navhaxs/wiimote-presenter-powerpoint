using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiimoteAddin
{
    class App
    {

        internal static AppWindow ui;                                            
        internal static WiimotePairingWorker wiimoteManager = new WiimotePairingWorker();
        private static WiimoteListener wiimoteListner;

        // helpers for wiimoteListener
        internal static void restartwiimoteListner() {
            if (wiimoteListner != null)
            {
                wiimoteListner.Dispose();
            }
            wiimoteListner = new WiimoteListener();
        }

        internal static void stopwiimoteListner()
        {
            if (wiimoteListner != null)
            {
                wiimoteListner.Dispose();
            }
        }

        // ui data
        internal static MainScreenViewModel MainScreenData = new MainScreenViewModel();

    }
}
