using System.Net;
using System.Net.NetworkInformation;

namespace NetworkDeviceModels
{
    public interface IDeviceInterface
    {
        string InterfaceName { get; set; }
        IPAddress IPAddress { get; set; }
        PhysicalAddress MAC { get; set; }
    }
}