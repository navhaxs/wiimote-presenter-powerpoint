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

                        wm.WiimoteDisconnect += wm_WiimoteDisconnect;

                        if (i == 0) // recieve events only from the first wiimote
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

        void wm_WiimoteDisconnect(object sender, EventArgs e)
        {
            // todo

        }

        private void UpdateWiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            state.Update(e.WiimoteState);
        }

        void OnButtonStateChanged(object sender, ButtonStateChangedArgs e)
        {
            if (e.buttonDown)
            {
                if (WiimoteAddin.Win32Api.isPowerPointSlideShowActive())
                {
                    if (e.button == Button.Up)
                    {
                        App.ui.DoSendKey("{UP}");
                    }
                    if (e.button == Button.Down)
                    {
                        App.ui.DoSendKey("{DOWN}");
                    }
                    if (e.button == Button.Left)
                    {
                        App.ui.DoSendKey("{LEFT}");
                    }
                    if (e.button == Button.Right)
                    {
                        App.ui.DoSendKey("{RIGHT}");
                    }
                    if (e.button == Button.A)
                    {
                        App.ui.DoSendKey("{ENTER}");
                    }
                    if (e.button == Button.B)
                    {
                        App.ui.DoSendKey("{BKSP}");
                    }
                    if (e.button == Button.One)
                    {
                        pptController.blankBlack();
                    }
                    if (e.button == Button.Two)
                    {
                        pptController.blankWhite();
                    }
                    if (e.button == Button.Plus)
                    {
                        pptController.zoomIn();
                    }
                    if (e.button == Button.Minus)
                    {
                        pptController.zoomOut();
                    }
                    if (e.button == Button.Home)
                    {
                        App.ui.DoSendKey("{ESC}");
                    }
                } else {

                    if (e.button == Button.Right || e.button == Button.Down || e.button == Button.A)
                    {
                        pptController.NextSlide();
                    }
                    if (e.button == Button.Left || e.button == Button.Up || e.button == Button.B)
                    {
                        pptController.PrevSlide();
                    }
                    if (e.button == Button.One)
                    {
                        pptController.blankBlack();
                    }
                    if (e.button == Button.Two)
                    {
                        pptController.blankWhite();
                    }
                    if (e.button == Button.Plus)
                    {
                        pptController.zoomIn();
                    }
                    if (e.button == Button.Minus)
                    {
                        pptController.zoomOut();
                    }
                    if (e.button == Button.Home)
                    {
                        pptController.slideshowStart();
                    }
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
