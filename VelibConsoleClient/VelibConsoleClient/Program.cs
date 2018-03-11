using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelibConsoleClient.VelibSoapRef;

namespace VelibConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            VelibServiceClient client = new VelibServiceClient("service1");
            Console.WriteLine("Welcome to velib.");
            while (true)
            {
                Console.WriteLine("\nWhat do you want to do:\n1. List all stations of a city;\n" +
                                  "2. Search for a station by name;\n3. Help;\n4. Quit");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    Console.WriteLine("Please input a city:");
                    Console.WriteLine("\n" + client.GetStationsOfACity(Console.ReadLine()));
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Please input a city:");
                    string city = Console.ReadLine();
                    Console.WriteLine("Please input a station:");
                    string station = Console.ReadLine();
                    Console.WriteLine("\n" + client.GetStationInfo(city, station));
                }
                else if (choice == "3")
                {
                    Console.WriteLine("\n" + client.GetHelp());
                }
                else if (choice == "4")
                {
                    Console.WriteLine("See you later");
                    break;
                }
                else
                {
                    Console.WriteLine("Please choose from 1 to 4.");
                }
            }
        }
    }
}
