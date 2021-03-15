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
        private IRabbitControlledManager _rabbitTracker;
        private IBackgroundHandler _backgroundHandler;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public MasterControlProcessor(IRabbitControlledManager rabbitTracker, IBackgroundHandler backgroundHandler)
        {
            _rabbitTracker = rabbitTracker;
            _backgroundHandler = backgroundHandler;
        }
        #endregion

        #region Methods

        #endregion

        public void Process(ControlResponse data)
        {
            var device = data.ToPiDeviceStatus();
            var found = false;

            foreach (var item in _rabbitTracker.AllDevices)
            {
                if (item.Hostname.Equals(device.Hostname))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                _rabbitTracker.AddDevice(device);
                _backgroundHandler.SendMessage(new UpdateViewMessage());
            }

            
        }
    }
}
