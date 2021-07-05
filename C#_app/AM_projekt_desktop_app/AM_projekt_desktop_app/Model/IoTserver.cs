using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AM_projekt_desktop_app.Model
{
    class IoTserver
    {
        public string ip;

        public IoTserver(string _ip)
        {
            ip = _ip;
        }

        public T _download_serialized_json_data<T>() where T : new()
        {
            using (var w = new WebClient())
            {
                string url = $"http://{ip}/AM_projekt_testmock.php";
                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }

    }
}
