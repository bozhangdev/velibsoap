using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace VelibSoapIWS
{
    
    interface ISubStationEvent
    {
        [OperationContract(IsOneWay = true)]
        void GetStation(string city, string station, int period, string result);
    }
}
