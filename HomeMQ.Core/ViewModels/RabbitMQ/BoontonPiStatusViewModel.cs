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
    public class BoontonPiStatusViewModel : BoontonPiViewModel
    {
        #region Fields
        
        #endregion

        #region Properties
        
        

        

        #endregion

        #region Constructors
        public BoontonPiStatusViewModel(IBackgroundHandler backgroundHandler, IBoontonPi device) : base(backgroundHandler, device)
        {
            foreach (var sensor in Device.Sensors)
            {
                Sensors.Add(new SensorInfoViewModel(sensor));
            }
        }
        #endregion

        #region Methods

        #endregion

    }
}
