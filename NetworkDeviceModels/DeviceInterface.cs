using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace NetworkDeviceModels
{
    public class DeviceInterface : IDeviceInterface
    {
        public string InterfaceName { get; set; }
        public PhysicalAddress MAC { get; set; }
        public IPAddress IPAddress { get; set; }
    }
}
