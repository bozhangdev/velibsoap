using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace VelibSoapIWS
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)]
    class SubStationService : ISubStationService
    {
        static Action<string, string, int, string> action = delegate { };
        private string key = "afb87edfbd60684611fff45fbb859a9e0bf023ff";
        public void GetStaion(string city, string station, int period)
        {
            MonitorStatic.AddRequestFromClient();
            string result = "";
            bool canContinue = false;
            string requestUri = "https://api.jcdecaux.com/vls/v1/stations/?contract=" + city + "&apiKey=" + key;
            WebRequest request = WebRequest.Create(requestUri);
            request.ContentType = "text/html;charset=UTF-8";
            request.Method = "GET";
            request.Proxy = null;
            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (Exception e)
            {
                result = "Not a valid city";
                action(city, station, period, result);
                return;
            }
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd(); // Display the content.
            List<Station> velibs = JsonConvert.DeserializeObject<List<Station>>(responseFromServer);

            Station a = null;
            foreach (Station velib in velibs)
            {
                if (velib.name.Contains(station.ToUpper()))
                {
                    a = velib;
                    break;
                }
            }

            if (a != null)
            {
                result = a.ToString();
                canContinue = true;
            }
            else
            {
                result = "Not a valid station, please try again.";
                action(city, station, period, result);
                return;
            }
            while (canContinue)
            {
                WebRequest request2 = WebRequest.Create(requestUri);
                request2.ContentType = "text/html;charset=UTF-8";
                request2.Method = "GET";
                request2.Proxy = null;
                WebResponse response2 = null;
                try
                {
                    response2 = request2.GetResponse();
                }
                catch (Exception e)
                {
                    result = "Not a valid city";
                }
                MonitorStatic.AddRequestToVelib();
                Stream dataStream2 = response2.GetResponseStream();
                StreamReader reader2 = new StreamReader(dataStream2);
                string responseFromServer2 = reader2.ReadToEnd(); // Display the content.
                List<Station> velibs2 = JsonConvert.DeserializeObject<List<Station>>(responseFromServer2);
                foreach (Station velib in velibs2)
                {
                    if (velib.name.Contains(station.ToUpper()))
                    {
                        a = velib;
                        break;
                    }
                }
                result = "Available bikes: " + a.available_bike_stands.ToString() + "\n";
                action(city, station, period, result);
                Thread.Sleep(period/2);
            }
            
        }

        public void SubscribeStationEvent()
        {
            ISubStationEvent subscriber = OperationContext.Current.GetCallbackChannel<ISubStationEvent>();
            action += subscriber.GetStation;
        }
    }
}
