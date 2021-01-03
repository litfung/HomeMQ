using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace HomeMQ.Managers
{
    public class NetworkDevice
    {
        public PhysicalAddress MAC { get; set; }
        public IPAddress IPAddress { get; set; }
        public List<string> Names { get; set; }

        public NetworkDevice()
        {
            Names = new List<string>();
        }

        public NetworkDevice(string name)
        {
            Names = new List<string> { name };
        }

        public override string ToString()
        {
            var names = "{";
            foreach (var name in Names)
            {
                names += $" {name}";
            }
            names += "}";
            return $"{IPAddress} {MAC} \n{names}";
        }
    }
}
