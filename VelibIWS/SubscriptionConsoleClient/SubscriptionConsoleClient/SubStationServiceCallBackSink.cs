using SubscriptionConsoleClient.ServiceReferenceSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionConsoleClient
{
    class SubStationServiceCallBackSink : ISubStationServiceCallback
    {
        public void GetStation(string city, string station, int period, string result)
        {
            Console.WriteLine(result);

        }
    }
}
