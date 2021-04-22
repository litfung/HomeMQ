using BaseClasses;
using BaseViewModels;
using HomeMQ.Models;
using HomeMQ.RabbitMQ.Publishers;
using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public class BoontonPiControlViewModel : BoontonPiViewModel
    {
        #region Fields
        protected IPiControlPublisher _commandPublisher;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public BoontonPiControlViewModel(IBackgroundHandler backgroundHandler, IBoontonPi device, IPiControlPublisher commandPublisher) : base(backgroundHandler, device)
        {
            _commandPublisher = commandPublisher;
            foreach (var sensor in Device.Sensors)
            {
                Sensors.Add(new SensorControlViewModel(sensor, _commandPublisher));
            }
        }
        #endregion

        #region Methods

        #endregion

    }
}
