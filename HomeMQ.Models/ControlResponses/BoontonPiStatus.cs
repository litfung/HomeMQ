using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Models
{
    public class BoontonPiStatus : IBoontonPi
    {
        public string Hostname { get; set; }
        public List<IInterfaceData> Interfaces { get; set; } = new List<IInterfaceData>();
        public string Status { get; set; }
        
        public List<string> AlternateNames { get; set; } = new List<string>();
        public DateTime LastUpdateTime { get; set; }

        public List<ISensorData> Sensors { get; set; } = new List<ISensorData>();
    }
}
