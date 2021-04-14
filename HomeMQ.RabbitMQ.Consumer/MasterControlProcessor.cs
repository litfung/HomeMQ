using BaseClasses;
using DeviceManagers;
using HomeMQ.Models;
using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumer
{
    public class MasterControlProcessor : IMasterControlProcessor
    {
        #region Fields
        private IRabbitControlledManager _rabbitTracker;
        private IBackgroundHandler _backgroundHandler;

        //private Dictionary<string, Type> ResponseDictionary = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        //{
        //    {"status_check_response", typeof(StatusCheck)},
        //    {"start_poll_response", typeof(StartPoll) }
        //};
        
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
            switch (data.Header.Command)
            {
                case "status_check_response":
                    UpdateDevices(data);
                    break;
                case "boonton_startup_response":
                case "start_poll_response":
                case "stop_poll_response":
                    UpdateDeviceStatus(data);
                    break;
                default:
                    break;
            }
        }

        private void UpdateDevices(ControlResponse data)
        {
            var found = false;

            foreach (var item in _rabbitTracker.AllDevices)
            {
                if (item.Hostname.Equals(data.Header.Hostname))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                var device = data.ToPiDeviceStatus();
                _rabbitTracker.AddDevice(device);
                _backgroundHandler.SendMessage(new UpdateViewMessage());
            }
            else
            {
                UpdateDeviceStatus(data);
            }
        }

        private void UpdateDeviceStatus(ControlResponse data)
        {
            IRabbitControlled tmp;
            if (_rabbitTracker.DevicesByName.TryGetValue(data.Header.Hostname, out tmp))
            {
                tmp.Status = data.Payload.Status;
                tmp.LastUpdateTime = DateTime.Now;
                _backgroundHandler.SendMessage(new UpdateViewMessage());
            }
        }
    }
}
