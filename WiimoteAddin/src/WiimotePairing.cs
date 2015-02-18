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

        public int WiimoteCount = 0;

        // helper function
        public bool NoWiimotesConnected()
        {
            if (WiimoteCount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        internal bool isBusy() {
            return mutex;
        }

        internal WiimoteLib.WiimoteCollection wiimoteCollection = new WiimoteLib.WiimoteCollection();

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

            if (e.Result != null)
            {
                try {
                    if ((string) e.Result == "pairerror")
                    {
                        MessageBox.Show("There is a Wiimote paired to this computer, but it seems to be disconnected. Please try the \"Disconnect all Wiimotes\" button first.");
                    }
                } catch {

                }
            } else {

                if (App.wiimoteManager.WiimoteCount > 0)
                {

                    App.restartwiimoteListner();
                }
                else
                {
                    // pause to show last message
                    System.Threading.Thread.Sleep(5);
                    // restart background scanning no more wiimotes connected
                    startPairWorker(true);

                }
            }
        }

        private void UnpairWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            mutex = false;
            App.MainScreenData.UpdateUI();

            // pause to show how many were disconnected
            System.Threading.Thread.Sleep(5);

            // restart background scanning no more wiimotes connected
            if (App.wiimoteManager.WiimoteCount == 0)
            {
                startPairWorker(true);
            }
        }


        bool mutex = false;

        public void startPairWorker(bool isContinousScanning)
        {
            if (!pairWorker.IsBusy && !mutex && checkBluetoothOK())
            {
                mutex = true;
                pairWorker.WorkerSupportsCancellation = true;
                pairWorker.RunWorkerAsync(isContinousScanning);
            }
        }


        public bool checkBluetoothOK()
        {
            bool result = (BluetoothRadio.PrimaryRadio != null);
            if (!result)
            {
                App.MainScreenData.StatusLabel = "Bluetooth not available";
                App.MainScreenData.ShowWiimoteHowto = System.Windows.Visibility.Collapsed;
                App.MainScreenData.Scene_NoBluetooth = System.Windows.Visibility.Visible;
            }
            else
            {
                App.MainScreenData.Scene_NoBluetooth = System.Windows.Visibility.Hidden;
            }

            return result;
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

            if (!unpairWorker.IsBusy && !mutex && checkBluetoothOK())
            {
                mutex = true;
                unpairWorker.RunWorkerAsync();
            }
        }

        void pairWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool loopUntilFound = (bool)e.Argument;

            App.MainScreenData.StatusLabel = "Scanning for Wiimotes...";
            App.MainScreenData.ShowWiimoteHowto = System.Windows.Visibility.Visible;

            // do while
            //if (loopUntilFound)
            //{
            //    while (App.wiimoteManager.WiimoteCount == 0 && !pairWorker.CancellationPending)
            //    {
            //        _pair_(e);
            //    }

            //}
            //else
            //{
            // TODO: Huh isn't this global?
            App.wiimoteManager.wiimoteCollection.Clear();

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
                if (matchesWiimoteClass(currentRememberedDevice))
                {
                    if (currentRememberedDevice.Connected == false)
                    {
                        if (doPairWithDevice(currentRememberedDevice) == false)
                        {
                            e.Result = "pairerror";
                        }
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
                if (matchesWiimoteClass(currentFoundDevice))
                {
                    doPairWithDevice(currentFoundDevice);
                }
            }

            // Temporarily connect to all available Wiimotes
            try
            {
                App.wiimoteManager.wiimoteCollection.FindAllWiimotes();
                foreach (WiimoteLib.Wiimote wm in App.wiimoteManager.wiimoteCollection)
                {
                    wm.Connect();
                    wm.SetLEDs(true, true, true, true);
                    System.Threading.Thread.Sleep(200);
                    wm.SetRumble(true);
                    System.Threading.Thread.Sleep(200);
                    wm.SetRumble(false);
                    wm.Disconnect(); // release hook for now
                    WiimoteCount_Existing = WiimoteCount_Existing + 1;
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

            App.wiimoteManager.WiimoteCount = WiimoteCount_New + WiimoteCount_Existing;
            //}

            App.MainScreenData.ShowWiimoteHowto = System.Windows.Visibility.Hidden;

            // Output results
            if (App.wiimoteManager.WiimoteCount == 0)
            {
                App.MainScreenData.StatusLabel = "No Wiimotes found, try again.";
            } else if (App.wiimoteManager.WiimoteCount == 1)
            {
                App.MainScreenData.StatusLabel = "Ready to present! Please save your work first, then start the slideshow";
            } else if (App.wiimoteManager.WiimoteCount > 0)
            {
                App.MainScreenData.StatusLabel = "Connected " + App.wiimoteManager.WiimoteCount + " Wiimotes.";
            }
    
        }
 
        void unpairWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            App.MainScreenData.StatusLabel = "Disconnecting Wiimotes...";
            App.MainScreenData.UpdateUI();

            App.stopwiimoteListner();

            App.wiimoteManager.wiimoteCollection.Clear();

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
                App.wiimoteManager.wiimoteCollection.FindAllWiimotes();
                foreach (WiimoteLib.Wiimote wm in App.wiimoteManager.wiimoteCollection)
                {
                    wm.SetLEDs(true, false, false, true);
                    wm.Disconnect();
                }
            }
            catch (WiimoteLib.WiimoteNotFoundException exception)
            {
                Debug.Print("No Wiimotes available. " + exception.Message);
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
                if (matchesWiimoteClass(currentRememberedDevice))
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

            App.MainScreenData.ShowMessage(WiimoteCount_Existing + " Wiimotes removed.");

            // Update counter
            App.wiimoteManager.WiimoteCount = 0;
            foreach (BluetoothDeviceInfo currentRememberedDevice in bluetoothClient.DiscoverDevices(255, false, true, true))
            {
                // max devices   255
                // authenticated false   Existing pairings with this bt adapter
                // remembered    true    All authenticated devices are remembered.
                // unknown       true    Previously unknown devices
                if (matchesWiimoteClass(currentRememberedDevice))
                {
                    App.wiimoteManager.WiimoteCount++;
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


        
        bool matchesWiimoteClass(BluetoothDeviceInfo device)
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
