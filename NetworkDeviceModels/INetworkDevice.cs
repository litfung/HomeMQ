using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace NetworkDeviceModels
{
    public interface INetworkDevice
    {
        IPAddress IPAddress { get; set; }
        PhysicalAddress MAC { get; set; }
        List<string> Names { get; set; }

        string ToString();
    }
}