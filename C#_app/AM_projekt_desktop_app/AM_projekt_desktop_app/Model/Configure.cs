using AM_projekt_desktop_app.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace AM_projekt_desktop_app.Model
{
    class Configure
    {
        static string PATH = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        static string FILE_NAME = @"\AM_projekt_conf.txt";
        public string CONFIG_SAVE = PATH + FILE_NAME;

        static readonly string ipAddressDefault = "192.168.0.112";
        public string IpAddress;
        static readonly int sampleTimeDefault = 1000;
        public int SampleTime;
        public readonly int MaxSampleNumber = 100;
        public double XAxisMax
        {
            get
            {
                return MaxSampleNumber * SampleTime / 1000.0;
            }
            private set { }
        }

        public Configure()
        {
            using (StreamReader r = new StreamReader(CONFIG_SAVE))
            {
                string json = r.ReadToEnd();
                List<data> config_data = JsonConvert.DeserializeObject<List<data>>(json);
                IpAddress = config_data[0].Ip_data;
                SampleTime = config_data[0].SampleTime_data;

            }
        }

        public Configure(string ip, int st)
        {
            IpAddress = ip;
            SampleTime = st;
        }
    }
}
