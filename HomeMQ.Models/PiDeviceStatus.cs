using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace HomeMQ.Models
{
    public class PiDeviceStatus : IRabbitControlled
    {
        public string Hostname { get; set; }
        public List<IInterfaceData> Interfaces { get; set; } = new List<IInterfaceData>();
        public string Status { get; set; }
        public List<string> AlternateNames { get; set; } = new List<string>();
    }

    //public class PiInterfaceData : IInterfaceData
    //{
    //    public string Name { get; set; }
    //    public string MAC { get; set; }
    //    public string IPAddress { get; set; }
    //}



}
