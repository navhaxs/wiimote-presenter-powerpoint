using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WiimoteLib;

namespace WiimoteAddin
{
    class WiimoteListener : IDisposable
    {
        ButtonState state = new ButtonState();
        PowerPointApi pptController = new PowerPointApi(WiimoteAddin.Connect.thisPowerPoint);

        public WiimoteListener()
        {
            if (App.wiimoteManager.WiimoteCount > 0)
            {
                state.ButtonStateChanged += OnButtonStateChanged;
                try
                {
                    App.wiimoteManager.wiimoteCollection.FindAllWiimotes();
                    int i = 0;
                    foreach (WiimoteLib.Wiimote wm in App.wiimoteManager.wiimoteCollection)
                    {
                        wm.Connect();
                    
                        wm.SetReportType(InputReport.Buttons, true);

                        if (i == 0)
                        {
                            Debug.Print("Added WiimoteChanged event to Wiimote 1.");
                            wm.WiimoteChanged += UpdateWiimoteChanged;
                        }


                        i++;
                    }          
                }
            catch (WiimoteLib.WiimoteNotFoundException)
            {
                // ignore this case
                Debug.Print("No Wiimotes were found at this time.");
            }
            catch (Exception exception)
            {
                Debug.Print(exception.Message);
            }

            }

        }

        private void UpdateWiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            state.Update(e.WiimoteState);
        }

        void OnButtonStateChanged(object sender, ButtonStateChangedArgs e)
        {
            if (e.buttonDown)
            {
                if (e.button == Button.Right || e.button == Button.Down || e.button == Button.A)
                {
                    pptController.NextSlide();
                }
                if (e.button == Button.Left || e.button == Button.Up || e.button == Button.B)
                {
                    pptController.PrevSlide();
                }
            }
            
        }

        public void Dispose()
        {
            foreach (WiimoteLib.Wiimote wm in App.wiimoteManager.wiimoteCollection)
            {
                wm.SetLEDs(false, false, false, false);
                wm.WiimoteChanged -= UpdateWiimoteChanged;
                wm.SetReportType(InputReport.Buttons, false);
                wm.Disconnect(); // release wiimote
            }
        }

    }
}
