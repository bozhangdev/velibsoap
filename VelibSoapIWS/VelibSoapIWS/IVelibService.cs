using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace VelibSoapIWS
{
    [ServiceContract]
    interface IVelibService
    {
        [OperationContract]
        string GetStationsOfACity(string city);

        [OperationContract]
        string GetStationInfo(string city, string station);

        [OperationContract]
        string GetHelp();
    }
}
