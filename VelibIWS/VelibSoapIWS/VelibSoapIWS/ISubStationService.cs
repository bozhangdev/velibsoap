using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace VelibSoapIWS
{
    [ServiceContract(CallbackContract = typeof(ISubStationEvent))]
    interface ISubStationService
    {
        [OperationContract]
        void GetStaion(string city, string station, int period);

        [OperationContract]
        void SubscribeStationEvent();
    }
}
