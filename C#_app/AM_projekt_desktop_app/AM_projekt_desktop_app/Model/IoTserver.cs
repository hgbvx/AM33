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

        public void Display_Set(string pass_url)
        {
            //WebRequest request = WebRequest.Create($"http://192.168.0.11/post_display.php/?arg1=t&arg2=p&arg3=l&arg4=i");
            string final_url = $"http://{ip}/{pass_url}";
            WebRequest request = WebRequest.Create(final_url);
            request.Proxy = null;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            response.Close();
        }




    }
}
