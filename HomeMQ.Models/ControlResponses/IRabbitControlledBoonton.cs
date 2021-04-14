using NetworkDeviceModels;
using System.Collections.Generic;

namespace HomeMQ.Models
{
    public interface IBoontonPi : IRabbitControlled
    {
        List<ISensorData> Sensors { get; set; }
    }
    public interface ISensorData
    {
        string SerialNumber { get; set; }
    }
}