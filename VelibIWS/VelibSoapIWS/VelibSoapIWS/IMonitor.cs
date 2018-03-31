using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace VelibSoapIWS
{
    [ServiceContract]
    interface IMonitor
    { 
        [OperationContract]
        string GetRequestToVelib(string startTime, string endTime);

        [OperationContract]
        string GetRequestFromClient(string startTime, string endTime);

        [OperationContract]
        int GetCacheNumber();

        [OperationContract]
        string GetDelay();

    }
}
