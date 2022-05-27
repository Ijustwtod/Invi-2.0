using Invi_2._0.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Invi_2._0.Models
{
    public class PostQuerry
    {
        public void On(string device_id)
        {
            WebRequest request = WebRequest.Create("https://api.iot.yandex.net/v1.0/devices/actions");
            request.Method = "POST"; // для отправки используется метод Post
            request.Headers.Add("Authorization", "Bearer AQAAAAAogRrRAAdqAqYo6Wl0XUQDlLifxNMcw3U");
            request.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string postData = "{\"devices\":[{\"id\":\"" + device_id + "\",\"actions\":[{\"type\":\"devices.capabilities.on_off\",\"state\":{\"instance\":\"on\",\"value\":true}}]}]}";
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
            }
        }

        public void Off(string device_id)
        {
            WebRequest request = WebRequest.Create("https://api.iot.yandex.net/v1.0/devices/actions");
            request.Method = "POST"; // для отправки используется метод Post
            request.Headers.Add("Authorization", $"Bearer {User.qauthtoken}");
            request.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string postData = "{\"devices\":[{\"id\":\"" + device_id + "\",\"actions\":[{\"type\":\"devices.capabilities.on_off\",\"state\":{\"instance\":\"on\",\"value\":false}}]}]}";
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public static void ColorSwap(int H, int S, int V, string device_id)
        {
            WebRequest request = WebRequest.Create("https://api.iot.yandex.net/v1.0/devices/actions");
            request.Method = "POST"; // для отправки используется метод Post
            request.Headers.Add("Authorization", $"Bearer {User.qauthtoken}");
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string color = $"\"h\": {H} , \"s\": {S} , \"v\": {V}";
                string postData = "{\"devices\":[{\"id\":\"" + device_id + "\",\"actions\":[{\"type\":\"devices.capabilities.color_setting\",\"state\":{\"instance\":\"hsv\",\"value\":{" + color + " } }}]}]}";
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
        }

        public static void TemperatureSwap(int temperature, string device_id) 
        {
            WebRequest request = WebRequest.Create("https://api.iot.yandex.net/v1.0/devices/actions");
            request.Method = "POST"; // для отправки используется метод Post
            request.Headers.Add("Authorization", $"Bearer {User.qauthtoken}");
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
               
                string postData = "{\"devices\":[{\"id\":\"" + device_id + "\",\"actions\":[{\"type\":\"devices.capabilities.color_setting\",\"state\":{\"instance\":\"temperature_k\",\"value\":" + temperature + "  }}]}]}";
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }

        }

    }

}

