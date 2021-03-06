using AM_projekt_desktop_app.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace AM_projekt_desktop_app.Model
{
    class IoTserver
    {
        public string ip;

        public IoTserver(string _ip)
        {
            ip = _ip;
        }

        public List<Measurements> Download()
        {
            using (WebClient wc = new WebClient())
            {
                string url = $"http://{ip}/get_all_sensors.php";
                var json = wc.DownloadString(url);
                List<Measurements> json_mlist = JsonConvert.DeserializeObject<List<Measurements>>(json);
                return json_mlist;
            }
            
        }

        public void Display_Set(string pass_url)
        {
            //WebRequest request = WebRequest.Create($"http://192.168.0.11/post_display.php/?arg1=t&arg2=p&arg3=l&arg4=i");
            string final_url = $"http://{ip}/{pass_url}";
            WebRequest request = WebRequest.Create(final_url);
            request.Proxy = null;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            response.Close();
        }




    }
}
