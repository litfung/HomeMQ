using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace HomeMQ.Managers
{
    public class NetworkDeviceManager
    {
        #region Singleton
        private static readonly Lazy<NetworkDeviceManager> instance = new Lazy<NetworkDeviceManager>();
        public static NetworkDeviceManager Instance => instance.Value;

        public NetworkDeviceManager()
        {
            var leaf = new NetworkDevice
            {
                Names = new List<string> { "leaf", "nano", "nanoleaf", "scene" },
                MAC = PhysicalAddress.Parse("00-55-DA-57-44-31")
            };
            AddDevice(leaf);
        }
        #endregion

        #region Properties
        public Dictionary<string, NetworkDevice> DevicesByMAC { get; set; } = new Dictionary<string, NetworkDevice>();
        public List<NetworkDevice> AllDevices { get; set; } = new List<NetworkDevice>();
        #endregion

        #region Methods
        public void AddDevice(NetworkDevice device)
        {
            if (!AllDevices.Contains(device))
            {
                DevicesByMAC[device.MAC.ToString()] = device;
                AllDevices.Add(device);
            }
        }
        #endregion
    }
}
