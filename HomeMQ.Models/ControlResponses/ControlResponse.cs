using NetworkDeviceModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace HomeMQ.Models
{
    public class ControlResponse
    {
        public Header Header { get; set; }
        public ResponsePayload Payload { get; set; }

        public PiDeviceStatus ToPiDeviceStatus()
        {
            var interfaces = new List<IInterfaceData>();

            foreach (var item in Payload.Interfaces)
            {
                interfaces.Add(item);
            }

            var device = new PiDeviceStatus
            {
                Hostname = Header.Hostname,
                Status = Payload.Status,
                Interfaces = interfaces
            };
            return device;
        }

        public PiDeviceStatus UpdatePollingStatus()
        {
            var interfaces = new List<IInterfaceData>();

            foreach (var item in Payload.Interfaces)
            {
                interfaces.Add(item);
            }

            var device = new PiDeviceStatus
            {
                Hostname = Header.Hostname,
                Status = Payload.Status,
                Interfaces = interfaces
            };
            return device;
        }
    }

    public class Header
    {
        public string Command { get; set; }
        public string Hostname { get; set; }
    }

    public class ResponsePayload
    {
        public InterfaceInfo[] Interfaces { get; set; }
        public string Status { get; set; }
    }

    public class InterfaceInfo : IInterfaceData
    {
        public string Name { get; set; }
        public string MAC { get; set; }
        public string IPAddress { get; set; }
    }
}
