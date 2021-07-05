using AM_projekt_desktop_app.Commands;
using AM_projekt_desktop_app.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace AM_projekt_desktop_app.ViewModel
{
    public class data
    {
        public string Port_data;
        public string Ip_data;
        public string API_data;
        public int SampleTime_data;
    }

    class ConfigureViewModel : BaseViewModel
    {
        public ButtonCommand UPCBtn { get; set; }

        public void SaveConfig()
        {
            Server = new IoTserver(IpAddress);
            config = new Configure(ipAddress, sampleTime);
            string ip_param = $"{ipAddress}";
            int st_param = sampleTime;
            List<data> _data = new List<data>();
            _data.Add(new data()
            {
                Port_data = "25565",
                Ip_data = ip_param,
                API_data = "8.10",
                SampleTime_data = st_param
            });

            string json = JsonConvert.SerializeObject(_data.ToArray());
            File.WriteAllText(@"C:\Users\Marcin\Desktop\polibuda\Aktualne\semestr 6\AM\lab\IoT_System_Apps\C#_app\AM_projekt_desktop_app\AM_projekt_conf.txt", json); ;
        }

        public ConfigureViewModel()
        {
            ipAddress = config.IpAddress;
            sampleTime = config.SampleTime;
            UPCBtn = new ButtonCommand(SaveConfig);

        }

    }
}
