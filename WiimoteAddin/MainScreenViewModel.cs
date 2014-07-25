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

        public string ActionButtonText
        {
            get
            {
                if (App.NoWiimotesActive())
                {
                    return "Connect Wiimote";
                }
                else
                {
                    return "Disconnect Wiimote";
                }
            }
        }

        public System.Windows.Visibility ShowIfWiimotesActive
        {
            get
            {
                if (App.NoWiimotesActive())
                {
                    return System.Windows.Visibility.Hidden;
                }
                else
                {
                    return System.Windows.Visibility.Visible;
                }
            }
        }

        public System.Windows.Visibility ShowIfNoWiimotesActive
        {
            get
            {
                if (App.NoWiimotesActive())
                {
                    return System.Windows.Visibility.Visible;
                }
                else
                {
                    return System.Windows.Visibility.Hidden;
                }
            }
        }

        public void UpdateUI()
        {
            OnPropertyChanged("ShowIfWiimotesActive");
            OnPropertyChanged("ShowIfNoWiimotesActive");
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
