using SubscriptionConsoleClient.ServiceReferenceSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SubStationServiceCallBackSink objSink = new SubStationServiceCallBackSink();
            InstanceContext context = new InstanceContext(objSink);
            SubStationServiceClient client = new SubStationServiceClient(context);
            client.SubscribeStationEvent();
            Console.WriteLine("Welcome to Velib Subscription\n");
            while (true) {
                Console.WriteLine("Please input the city:");
                string city = Console.ReadLine();
                Console.WriteLine("Please input the station:");
                string station = Console.ReadLine();
                Console.WriteLine("Please input the frequency of update (ms):");
                string period = Console.ReadLine();
                try
                {
                    int number = Convert.ToInt32(period);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Not a number, please try again!\n");
                    continue;
                }
                client.GetStaion(city, station, Convert.ToInt32(period));
                Console.WriteLine("\n");
            }
        }
    }
}
