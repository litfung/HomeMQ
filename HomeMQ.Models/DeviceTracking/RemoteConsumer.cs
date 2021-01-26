//using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceManagers
{
    public class RemoteConsumer
    {
        #region Properties
        //public List<IDeviceInterface> Interfaces { get; set; }
        public List<string> ConsumeRoutingKeys { get; set; }
        public List<string> ResponseRoutingKeys { get; set; }
        #endregion
    }
}
