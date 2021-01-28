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

        private RabbitControlledDeviceManager Manager => RabbitControlledDeviceManager.Instance;
        public void Process(ControlResponse data)
        {
            var device = data.ToPiDeviceStatus();

            Manager.AddDevice(device);
            Messenger.Instance.Send(new UpdateViewMessage());
        }
    }
}
