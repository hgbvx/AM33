using AM_projekt_desktop_app.Model;
using System;
using System.ComponentModel;

namespace AM_projekt_desktop_app.ViewModel
{
    class BaseViewModel : INotifyPropertyChanged
    {

        public IoTserver Server;
        public Configure config = new Configure();

        public int sampleTime;
        public string SampleTime
        {
            get
            {
                return sampleTime.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out int st))
                {
                    if (sampleTime != st)
                    {
                        sampleTime = st;
                        OnPropertyChanged("SampleTime");
                    }
                }
            }
        }

        public string ipAddress;
        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                if (ipAddress != value)
                {
                    ipAddress = value;
                    OnPropertyChanged("IpAddress");
                }
            }
        }

        public string portNumber;
        public string PortNumber
        {
            get
            {
                return portNumber;
            }
            set
            {
                if (portNumber != value)
                {
                    portNumber = value;
                    OnPropertyChanged("PortNumber");
                }
            }
        }

        public string apiNumber;
        public string ApiNumber
        {
            get
            {
                return apiNumber;
            }
            set
            {
                if (apiNumber != value)
                {
                    apiNumber = value;
                    OnPropertyChanged("ApiNumber");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
