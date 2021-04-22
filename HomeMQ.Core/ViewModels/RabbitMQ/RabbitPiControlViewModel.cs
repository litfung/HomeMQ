using BaseClasses;
using BaseViewModels;
using HomeMQ.Models;
using HomeMQ.RabbitMQ.Publishers;
using MvvmCross.Commands;
using NetworkDeviceModels;
using RabbitMQ.Control.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public class BoontonPiControlViewModel : BoontonPiViewModel
    {
        #region Fields
        protected IPiControlPublisher _commandPublisher;
        private string route => Device.Hostname;
        #endregion

        #region Properties
        #endregion

        #region Commands
        public IMvxCommand StartPollingCommand { get; }
        public IMvxCommand StopPollingCommand { get; }
        public IMvxCommand BoontonStartupCommand { get; }
        public IMvxCommand BoontonCloseSensorsCommand { get; }
        public IMvxCommand BoontonResetSensorsCommand { get; }
        #endregion

        #region Constructors
        public BoontonPiControlViewModel(IBackgroundHandler backgroundHandler, IBoontonPi device, IPiControlPublisher commandPublisher) : base(backgroundHandler, device)
        {
            _commandPublisher = commandPublisher;
            foreach (var sensor in Device.Sensors)
            {
                Sensors.Add(new SensorControlViewModel(sensor, _commandPublisher));
            }

            StartPollingCommand = new MvxAsyncCommand(OnStartPi);
            StopPollingCommand = new MvxAsyncCommand(OnStopPi);
            BoontonStartupCommand = new MvxAsyncCommand(OnBoontonStartup);
            BoontonCloseSensorsCommand = new MvxAsyncCommand(OnBoontonCloseSensors);
            BoontonResetSensorsCommand = new MvxAsyncCommand(OnBoontonResetSensors);
        }
        #endregion

        #region Methods

        private Task OnStartPi()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StartPoll(), $"rasp.control.{route}"));
            return Task.CompletedTask;
        }

        private Task OnStopPi()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StopPoll(), $"rasp.control.{route}"));
            return Task.CompletedTask;
        }
        private Task OnBoontonStartup()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonStartup(), $"rasp.control.{route}"));
            return Task.CompletedTask;
        }

        private Task OnBoontonCloseSensors()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonCloseSensors(), $"rasp.control.{route}"));
            return Task.CompletedTask;
        }

        private Task OnBoontonResetSensors()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonResetSensors(), $"rasp.control.{route}"));
            return Task.CompletedTask;
        }

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
                    Sensors.Add(new SensorControlViewModel(newSensor, _commandPublisher));
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
