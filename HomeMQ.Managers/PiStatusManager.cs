using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Managers
{
    public class PiStatusManager
    {
        #region Singleton
        private static readonly Lazy<PiStatusManager> instance = new Lazy<PiStatusManager>();
        public static PiStatusManager Instance => instance.Value;

        public PiStatusManager()
        {

        }
        #endregion

        #region Properties
        public Dictionary<string, IDeviceInterface> DevicesByName { get; set; } = new Dictionary<string, IDeviceInterface>();
        public List<IDeviceInterface> AllRaspberryPis { get; set; }
        #endregion

        #region CRUD Methods
        //public void AddDevice(IDeviceInterface device)
        //{
        //    if (!AllRaspberryPis.Contains(device))
        //    {
        //        DevicesByName[device.InterfaceName]
        //    }
        //}
        #endregion

    }
}
