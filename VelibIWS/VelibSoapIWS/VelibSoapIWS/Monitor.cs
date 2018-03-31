using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelibSoapIWS
{
    class Monitor : IMonitor
    {
        public int GetCacheNumber()
        {
            return MonitorStatic.GetCacheInfo();
        }

        public string GetDelay()
        {
            return MonitorStatic.GetDelay();
        }

        public string GetRequestFromClient(string startTime, string endTime)
        {
            double start;
            double end;
            try
            {
                start = Convert.ToDouble(startTime);
                end = Convert.ToDouble(endTime);
            }catch(Exception e)
            {
                return "Please input a valid number";
            }
            return "Number of requests from client: " + MonitorStatic.GetRequestFromClient(start, end).ToString();
        }

        public string GetRequestToVelib(string startTime, string endTime)
        {
            double start;
            double end;
            try
            {
                start = Convert.ToDouble(startTime);
                end = Convert.ToDouble(endTime);
            }catch(Exception e)
            {
                return "Please input a valid number";
            }
            return "Number of requests to Velib: " + MonitorStatic.GetRequestNumberToVelib(start, end).ToString();
        }
    }
}
