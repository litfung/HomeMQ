using BaseClasses;
using DeviceManagers;
using HomeMQ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumer
{
    public class MasterControlProcessor : IMasterControlProcessor
    {

        #region Fields
        //private RabbitControlledDeviceManager Manager => RabbitControlledDeviceManager.Instance;
        private IRabbitControlledManager manager;
        private IMessenger messenger;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public MasterControlProcessor(IRabbitControlledManager iManager, IMessenger iMessenger)
        {
            manager = iManager;
            messenger = iMessenger;
        }
        #endregion

        #region Methods

        #endregion

        public void Process(ControlResponse data)
        {
            var device = data.ToPiDeviceStatus();
            var found = false;

            foreach (var item in manager.AllDevices)
            {
                if (item.Hostname.Equals(device.Hostname))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                manager.AddDevice(device);
                messenger.Send(new UpdateViewMessage());
            }

            
        }
    }
}
