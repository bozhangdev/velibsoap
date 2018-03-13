using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace VelibSoapIWS
{
    class VelibService : IVelibService
    {
        private string key = "afb87edfbd60684611fff45fbb859a9e0bf023ff";
        ObjectCache cacheCity;
        ObjectCache cacheStation;
        CacheItemPolicy policy;

        public VelibService()
        {
            cacheCity = MemoryCache.Default;
            cacheStation = MemoryCache.Default;
            policy = new CacheItemPolicy();
            policy.SlidingExpiration = new TimeSpan(0, 30, 0);
        }

        public string GetHelp()
        {
            return "Choose 1. List all stations of a city, the server will show you all the stations available in the city you input.\n" +
                "Attention: You should input a valid city.\n\n" +
                "Choose 2. Search for a station by name, first you need to input a city, then you input the name of the station you want to search, you can get the information of this station.\n" +
                "Attention: Both the name of the city and the station should be valid, there is no limitation of upper or lower case for the names.\n";
        }

        public async Task<string> GetStationInfo(string city, string station)
        {
            if (cacheStation[station] != null)
            {
                return (string)cacheStation[station];
            }
            string requestUri = "https://api.jcdecaux.com/vls/v1/stations/?contract=" + city + "&apiKey=" + key;
            WebRequest request = WebRequest.Create(requestUri);
            request.ContentType = "text/html;charset=UTF-8";
            request.Method = "GET";
            request.Proxy = null;
            WebResponse response;
            try
            {
                response = await request.GetResponseAsync(); 
            }
            catch (Exception e)
            {
                return "Not a valid city";
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
                    cacheStation.Set(station, a.ToString(), policy);
                    break;
                }
            }

            if (a != null)
            {
                return a.ToString();
            }

            return "Not a valid station, please try again.";
        }

        public async Task<string> GetStationsOfACity(string city)
        {
            if (cacheCity[city] != null)
            {
                return (string)cacheCity[city];
            }
            string requestUri = "https://api.jcdecaux.com/vls/v1/stations/?contract=" + city + "&apiKey=" + key;
            WebRequest request = WebRequest.Create(requestUri);
            request.ContentType = "text/html;charset=UTF-8";
            request.Method = "GET";
            request.Proxy = null;
            WebResponse response;
            try
            {
                response = await request.GetResponseAsync();
            }
            catch (Exception e)
            {
                return "Not a valid city.Please try again.";
            }

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd(); // Display the content.
            List<Station> velibs = JsonConvert.DeserializeObject<List<Station>>(responseFromServer);
            string result = "";
            foreach (var station in velibs)
            {
                result += station.name;
                result += "\n";
            }
            cacheCity.Set(city, result, policy);
            return result;
        }
    }
}
