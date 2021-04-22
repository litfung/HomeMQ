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
using System.Linq;
using System.Diagnostics;

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
        protected override void ReconcileSensors()
        {
            var deviceSerialNumbers = from sensor in Device.Sensors select sensor.SerialNumber;
            var ocSerialNumbers = from sensor in Sensors select sensor.SerialNumber;

            var deviceMissing = ocSerialNumbers.Where(x => !deviceSerialNumbers.Any(x.Contains));
            var ocMissing = deviceSerialNumbers.Where(x => !ocSerialNumbers.Any(x.Contains));

            try
            {
                //Add new viewmodels previously not found in device info
                foreach (var serialNumber in ocMissing)
                {
                    var newSensor = Device.Sensors.Where(x => string.Equals(x.SerialNumber, serialNumber, StringComparison.OrdinalIgnoreCase)).Single();
                    Sensors.Add(new SensorInfoViewModel(newSensor));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Boonton Pi Status Add New Sensors Error {ex.Message}");
            }

            //Remove old viewmodels no longer found in device manager
            var tmp = new List<ISensorInfoViewModel>();

            foreach (var vm in Sensors)
            {
                if (!deviceSerialNumbers.Contains(vm.SerialNumber))
                {
                    tmp.Add(vm);
                }
            }
            try
            {
                foreach (var item in tmp)
                {
                    var index = Sensors.IndexOf(item);
                    var vm = Sensors[index];
                    Sensors.RemoveAt(index);
                    vm = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Boonton Pi Status Remove Old Sensors Error {ex.Message}");
            }

        }

        #endregion

    }
}
