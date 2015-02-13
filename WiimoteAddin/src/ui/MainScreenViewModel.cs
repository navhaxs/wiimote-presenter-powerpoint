using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace WiimoteAddin
{
    class MainScreenViewModel : INotifyPropertyChanged
    {

        private string _statusLabel = "...";
        public string StatusLabel
        {
            get { return _statusLabel; }
            set
            {
                if (value == _statusLabel) return;
                _statusLabel = value;
                OnPropertyChanged("StatusLabel");
            }
        }

        public string labelStatus_Text
        {
            get
            {
                if (App.wiimoteManager.NoWiimotesConnected())
                {
                    return "Wiimote not connected";
                }
                else
                {
                    return "Wiimote is connected";
                }
            }
        }

        public System.Windows.Visibility Scene_WiimotesActive
        {
            get
            {
                if (App.wiimoteManager.NoWiimotesConnected() || App.wiimoteManager.isBusy())
                {
                    return System.Windows.Visibility.Hidden;
                }
                {
                    return System.Windows.Visibility.Visible;
                }
            }
        }

        Visibility _ShowWiimoteHowto = Visibility.Visible;
        public System.Windows.Visibility ShowWiimoteHowto
        {
            get
            {
                return _ShowWiimoteHowto;
            }
            set
            {
                _ShowWiimoteHowto = value;
                OnPropertyChanged("ShowWiimoteHowto");
            }

        }

        public System.Windows.Visibility ShowIfNoWiimotesActive
        {
            get
            {
                if (App.wiimoteManager.NoWiimotesConnected())
                {
                    return System.Windows.Visibility.Visible;
                }
                else
                {
                    return System.Windows.Visibility.Hidden;
                }
            }
        }

        public string StatusImage
        {
            get
            {
                if (App.wiimoteManager.NoWiimotesConnected()) {
                    return "Theme/StatusAnnotations_Critical_32xLG_color.png";                   
                }
                else
                {
                    return "Theme/StatusAnnotations_Complete_and_ok_32xLG_color.png";
                }
                
            }
        }

        public void UpdateUI()
        {
            OnPropertyChanged("Scene_WiimotesActive");
            OnPropertyChanged("ShowIfNoWiimotesActive");
            OnPropertyChanged("labelStatus_Text");
            OnPropertyChanged("StatusImage");
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        #region "MVVM PropertyChanged code"

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

 
    }
}
