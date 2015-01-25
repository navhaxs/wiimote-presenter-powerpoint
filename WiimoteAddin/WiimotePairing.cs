using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Diagnostics; //TODO: remove this ref, remove side effects
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WiimoteLib;

namespace WiimoteAddin
{
    class WiimotePairingWorker
    {
        private System.ComponentModel.BackgroundWorker pairWorker = new System.ComponentModel.BackgroundWorker();
        private System.ComponentModel.BackgroundWorker unpairWorker = new System.ComponentModel.BackgroundWorker();
        
        public WiimotePairingWorker()
        {
            pairWorker.DoWork += pairWorker_DoWork;
            pairWorker.RunWorkerCompleted += PairWorkerCompleted;       
            unpairWorker.DoWork += unpairWorker_DoWork;
            unpairWorker.RunWorkerCompleted += UnpairWorkerCompleted;       
        }

        private void PairWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            mutex = false;
            App.MainScreenData.UpdateUI();
        }

        private void UnpairWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            mutex = false;
            App.MainScreenData.UpdateUI();

            // pause to show how many were disconnected

            System.Threading.Thread.Sleep(5);

            // restart background scanning no more wiimotes connected

            if (App.WiimoteCount == 0)
            {
                startPairWorker(true);
            }
        }

        class pairResult
        {
            int newWiimotes;
            bool success;

            pairResultErrorType errorType;
        }
        enum pairResultErrorType
        {
            FailNoBluetooth,
            FailUnknown
        };

        bool mutex = false;

        public void startPairWorker(bool isContinousScanning)
        {
            if (!pairWorker.IsBusy && !mutex)
            {
                mutex = true;
                pairWorker.WorkerSupportsCancellation = true;
                pairWorker.RunWorkerAsync(isContinousScanning);
            }
        }

        //public void startBackgroundPairWorker()
        //{
        //    if (!pairWorker.IsBusy && !mutex)
        //    {
        //        mutex = true;
        //        pairWorker.RunWorkerAsync();
        //    }
        //}

        public void startUnpairWorker()
        {
            
            //stop pairing
            if (pairWorker.IsBusy)
            {
                pairWorker.CancelAsync();
                //while (mutex == true)
                //{

                //}

            }


         

            if (!unpairWorker.IsBusy && !mutex)
            {
                mutex = true;
                unpairWorker.RunWorkerAsync();
            }
        }

        void pairWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool loopUntilFound = (bool)e.Argument;

            App.MainScreenData.StatusLabel = "Scanning for Wiimotes...";


            // do while
            if (loopUntilFound)
            {
                while (App.WiimoteCount == 0 && !pairWorker.CancellationPending)
                {
                    _pair_();
                }

            }
            else
            {
                _pair_();

            }

            

            // Output results
            if (App.WiimoteCount == 0)
            {
                App.MainScreenData.StatusLabel = "No Wiimotes found, try again.";
            } else if (App.WiimoteCount == 1)
            {
                App.MainScreenData.StatusLabel = "Ready to present! Save your work, then start the slideshow";
            } else if (App.WiimoteCount > 0)
            {
                App.MainScreenData.StatusLabel = "Connected " + App.WiimoteCount + " Wiimotes.";
            }
    
        }

        private void _pair_()
        {

            // TODO: Huh isn't this global?
            App.Wiimote_Collection.Clear();

            int WiimoteCount_New = new int();
            int WiimoteCount_Existing = new int();
            //int WiimoteCount_Connected = new int();

            // Check radio
            if (BluetoothRadio.PrimaryRadio == null)
            {
                //e.Result = pairResult.FailNoBluetooth;
                return;
            }
            else if (BluetoothRadio.PrimaryRadio.Mode == RadioMode.PowerOff)
            {
                BluetoothRadio.PrimaryRadio.Mode = RadioMode.Connectable; // turn ON radio if off
            }
            InTheHand.Net.Sockets.BluetoothClient bluetoothClient = new InTheHand.Net.Sockets.BluetoothClient(); // init bt object in our code

            // Check existing wiimote pairings
            bluetoothClient.InquiryLength = new TimeSpan(0, 0, 1); // 1 second scan interval
            foreach (BluetoothDeviceInfo currentRememberedDevice in bluetoothClient.DiscoverDevices(255, false, true, true))
            {
                // max devices   255
                // authenticated false
                // remembered    true   (ie. existing pairings on this bt adapter)
                // unknown       true
                //currentRememberedDevice.Refresh(); //?
                if (isWiimote(currentRememberedDevice))
                {
                    if (currentRememberedDevice.Connected == false)
                    {
                        doPairWithDevice(currentRememberedDevice);
                    }
                }
            }

            // Scan for unpaired wiimotes in range
            foreach (BluetoothDeviceInfo currentFoundDevice in bluetoothClient.DiscoverDevices(255, false, false, true))
            {
                // max devices   255
                // authenticated false
                // remembered    false   (ie. new devices to this bt adapter)
                // unknown       true
                if (isWiimote(currentFoundDevice))
                {
                    doPairWithDevice(currentFoundDevice);
                }
            }

            // Connect to all available Wiimotes
            try
            {
                App.Wiimote_Collection.FindAllWiimotes();
                foreach (WiimoteLib.Wiimote wm in App.Wiimote_Collection)
                {
                    wm.Connect();
                    wm.SetLEDs(true, true, true, true);
                    System.Threading.Thread.Sleep(200);
                    wm.SetRumble(true);
                    System.Threading.Thread.Sleep(200);
                    wm.SetRumble(false);
                    //wm.Disconnect();
                    WiimoteCount_Existing = WiimoteCount_Existing + 1;

                    wm.SetReportType(InputReport.Buttons, true);
                    wm.WiimoteChanged += App.WiimoteController.UpdateWiimoteChanged;
                    wm.Disconnect(); // release hook for now
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

            App.WiimoteCount = WiimoteCount_New + WiimoteCount_Existing;

        }

        void unpairWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            App.MainScreenData.StatusLabel = "Disconnecting Wiimotes...";
            
            App.Wiimote_Collection.Clear();

            int WiimoteCount_Existing = new int();
            //int WiimoteCount_Connected = new int();

            // Check radio
            if (BluetoothRadio.PrimaryRadio == null)
            {
                //e.Result = pairResult.FailNoBluetooth;
                return;
            }
            else if (BluetoothRadio.PrimaryRadio.Mode == RadioMode.PowerOff)
            {
                BluetoothRadio.PrimaryRadio.Mode = RadioMode.Connectable; // turn ON radio if off
            }
            InTheHand.Net.Sockets.BluetoothClient bluetoothClient = new InTheHand.Net.Sockets.BluetoothClient(); // init bt object in our code

            // Disconnect Wiimotes
            try
            {
                App.Wiimote_Collection.FindAllWiimotes();
                foreach (WiimoteLib.Wiimote wm in App.Wiimote_Collection)
                {
                    wm.WiimoteChanged -= App.WiimoteController.UpdateWiimoteChanged;
                    wm.SetLEDs(true, false, false, true);
                    wm.Disconnect();
                }
            }
            catch (WiimoteLib.WiimoteNotFoundException exception)
            {
                // ignore this case
                Debug.Print("No Wiimotes available.");
            }
            catch (Exception exception)
            {
                Debug.Print(exception.Message);
            }

            // Remove wiimote pairings
            bluetoothClient.InquiryLength = new TimeSpan(0, 0, 1); // 1 second scan interval
            foreach (BluetoothDeviceInfo currentRememberedDevice in bluetoothClient.DiscoverDevices(255, false, true, true))
            {
                // max devices   255
                // authenticated false
                // remembered    true   (ie. existing pairings on this bt adapter)
                // unknown       true

                //BluetoothDeviceInfo newDevices = currentRememberedDevice;
                currentRememberedDevice.Refresh(); // ??
                if (isWiimote(currentRememberedDevice))
                {
                    Debug.Print("Device: " + currentRememberedDevice.DeviceName);
                    Debug.Print("   BT address: " + currentRememberedDevice.DeviceAddress);
                    Debug.Print("   Connected: " + currentRememberedDevice.Connected);
                    
                    //currentRememberedDevice.SetServiceState(BluetoothService.HumanInterfaceDevice, false, false);
                    InTheHand.Net.Bluetooth.BluetoothSecurity.RemoveDevice(currentRememberedDevice.DeviceAddress);
                    
                    WiimoteCount_Existing++;
                }
            }

            App.MainScreenData.StatusLabel = WiimoteCount_Existing + " Wiimotes removed.";

            // Update counter
            App.WiimoteCount = 0;
            foreach (BluetoothDeviceInfo currentRememberedDevice in bluetoothClient.DiscoverDevices(255, false, true, true))
            {
                // max devices   255
                // authenticated false   Existing pairings with this bt adapter
                // remembered    true    All authenticated devices are remembered.
                // unknown       true    Previously unknown devices
                if (isWiimote(currentRememberedDevice))
                {
                    App.WiimoteCount++;
                }
            }

        }

        bool doPairWithDevice(BluetoothDeviceInfo currentRememberedDevice)
        {
            try
            {
                Debug.Assert(!currentRememberedDevice.Connected);
                
                
                if (currentRememberedDevice.Authenticated) {
                    App.MainScreenData.StatusLabel = "reconnecting to Wiimote...";
                } else {
                    App.MainScreenData.StatusLabel = "connecting to Wiimote...";
                }
              
                currentRememberedDevice.SetServiceState(BluetoothService.HumanInterfaceDevice, true, false);

                Guid g = new Guid("{00001124-0000-1000-8000-00805f9b34fb}"); // HID input service
            
                if (currentRememberedDevice.GetServiceRecords(g).Length != 1)
                {
                    App.MainScreenData.StatusLabel = "did not connect to Wiimote...";
                    return false;
                }
                else
                {
                    App.MainScreenData.StatusLabel = "connecting to Wiimote...OK!";
                    return true;
                }
            }
            catch
            {
                // Device may be disconnected and out of range // powered off
                App.MainScreenData.StatusLabel = "out of range // powered off...";

                // TODO: Unpair

                return false;
            }
        }


        
        bool isWiimote(BluetoothDeviceInfo device)
        {
            if ((device.DeviceName == "Nintendo RVL-CNT-01") &&
                    (device.ClassOfDevice.MajorDevice == DeviceClass.Peripheral) &&
                    (device.ClassOfDevice.Device == DeviceClass.PeripheralJoystick))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}
