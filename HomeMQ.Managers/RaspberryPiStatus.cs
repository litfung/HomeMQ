using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Managers
{
    public class RaspberryPiStatus
    {
        #region Properties
        public List<IDeviceInterface> Interfaces { get; set; } = new List<IDeviceInterface>();
        public string Name { get; set; }
        public List<string> RoutingKeys { get; set; } = new List<string>();
        public string Status { get; set; }
        #endregion
    }
}
