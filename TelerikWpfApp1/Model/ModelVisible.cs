 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace TelerikWpfApp1.Model
{
     public class ModelVisible : INotifyPropertyChanged
    {
        private bool alertVisible = false;

        public bool AlertVisible
        {
            get
            {
                return alertVisible;
            }

            set
            {
                alertVisible = value;
                NotifyPropertyChanged("AlertVisible");
            }
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}