using Invi_2._0.Data;
using Invi_2._0.Models.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Invi_2._0.Models
{
  
    public class GetQuerry
    {
        [Obsolete]
        public static Root UpdateDeviceList()
        {
            UserModel user = new UserModel();
            user.SearchUser();

            HttpClient client = new HttpClient();
            string url = "https://api.iot.yandex.net/v1.0/user/info";
            Uri uri = new Uri(url);
            string answer = "";
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(url);
            wrGETURL.Headers.Add("Authorization", $"Bearer {User.qauthtoken}");
            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;
            wrGETURL.Proxy = myProxy;
            wrGETURL.Proxy = WebProxy.GetDefaultProxy();
            Stream objStream;

            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;
            string json;
            string json2 = "";

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    answer = sLine.ToString();

                json = JsonConvert.SerializeObject(answer);
                json2 = JsonConvert.DeserializeObject(json).ToString();
            }

            Root root = JsonConvert.DeserializeObject<Root>(json2);
            Root values = JsonConvert.DeserializeObject<Root>(json2);


            for (int j = 0; j < values.devices.Count; j++)
            {
                //FIX ON
                if(values.devices[j].capabilities.Count > 1)
                {
                    if (values.devices[j].capabilities[0].type.Contains("on_off"))
                    {
                        root.devices[j].capabilities[0] = values.devices[j].capabilities[0];
                    }
                    else if (values.devices[j].capabilities[1].type.Contains("on_off"))
                    {
                        root.devices[j].capabilities[0] = values.devices[j].capabilities[1];
                    }
                    else if (values.devices[j].capabilities[2].type.Contains("on_off"))
                    {
                        root.devices[j].capabilities[0] = values.devices[j].capabilities[2];
                    }

                    //FIX HSV
                    if (values.devices[j].capabilities[0].type.Contains("color_setting"))
                    {
                        root.devices[j].capabilities[1] = values.devices[j].capabilities[0];
                    }
                    else if (values.devices[j].capabilities[1].type.Contains("color_setting"))
                    {
                        root.devices[j].capabilities[1] = values.devices[j].capabilities[1];
                    }
                    else if (values.devices[j].capabilities[2].type.Contains("color_setting"))
                    {
                        root.devices[j].capabilities[1] = values.devices[j].capabilities[2];
                    }


                    //FIX BRINTLESS
                    if (values.devices[j].capabilities[0].type.Contains("range"))
                    {
                        root.devices[j].capabilities[2] = values.devices[j].capabilities[0];
                    }
                    else if (values.devices[j].capabilities[1].type.Contains("range"))
                    {
                        root.devices[j].capabilities[2] = values.devices[j].capabilities[1];
                    }
                    else if (values.devices[j].capabilities[2].type.Contains("range"))
                    {
                        root.devices[j].capabilities[2] = values.devices[j].capabilities[2];
                    }
                }
            }

            for (int q = 0; q < root.rooms.Count; q++)
            {
                root.rooms[q].richdevices = new List<Device>();
                for (int d = 0; d < root.devices.Count; d++)
                {
                    if (root.rooms[q].id == root.devices[d].room)
                    {
                        root.rooms[q].richdevices.Add(root.devices[d]);

                        if ((bool)root.devices[d].capabilities[0].state.value)
                        {
                            root.rooms[q].state = true;
                        }
                    }
                }
            }
            return root;
        }
      
    }
}

