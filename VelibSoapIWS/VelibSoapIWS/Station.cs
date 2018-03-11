using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelibSoapIWS
{
    class Station
    {
        public int number { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
        public bool banking { get; set; }
        public bool bonus { get; set; }
        public Status status { get; set; }
        public string contract_name { get; set; }
        public int bike_stands { get; set; }
        public int available_bike_stands { get; set; }
        public int available_bikes { get; set; }
        public long last_update { get; set; }

        public override string ToString()
        {
            string result = "Number: " + number + "\nName: " + name + "\nAddress: " + address + "\nPosition: " + position.lat
                            + ", " + position.lng + "\nBanking: " + banking + "\nBonus: " + bonus
                            + "\nContract name: " + contract_name + "\nBike stands: " + bike_stands +
                            "\nAvailable bike stands: "
                            + available_bike_stands + "\nAvailable bikes: " + available_bikes + "\nLast update: " +
                            last_update;
            return result;
        }

        internal class Position
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        internal enum Status
        {
            Open
        }
    }
}
