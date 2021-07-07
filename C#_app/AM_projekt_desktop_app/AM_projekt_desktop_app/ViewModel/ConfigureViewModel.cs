using AM_projekt_desktop_app.Commands;
using AM_projekt_desktop_app.Model;
using Newtonsoft.Json;
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
            config = new Configure(ipAddress, sampleTime, portNumber, apiNumber);
            string ip_param = $"{ipAddress}";
            int st_param = sampleTime;
            string pt_param = portNumber;
            string api_param = apiNumber;
            List<data> _data = new List<data>();
            _data.Add(new data()
            {
                Port_data = pt_param,
                Ip_data = ip_param,
                API_data = api_param,
                SampleTime_data = st_param
            });

            string json = JsonConvert.SerializeObject(_data.ToArray());
            File.WriteAllText(config.CONFIG_SAVE, json); ;
        }

        public ConfigureViewModel()
        {
            ipAddress = config.IpAddress;
            sampleTime = config.SampleTime;
            portNumber = config.PortNumber;
            apiNumber = config.ApiNumber;
            UPCBtn = new ButtonCommand(SaveConfig);

        }

    }
}
