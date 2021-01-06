using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace HomeMQ.Models
{
    public class Sensor
    {
        public string Identifier { get; set; }
        public IPAddress IPAddress { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }
    }
}
