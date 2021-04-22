using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Models;
using HomeMQ.RabbitMQ.Publishers;
using MvvmCross.Commands;
using RabbitMQ.Control.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public class RabbitControlOverviewViewModel : BaseViewModel
    {
        #region Fields
        private IRabbitControlledManager _deviceManager;
        private IPiControlPublisher _commandPublisher;
        private CancellationTokenSource statusToken = new CancellationTokenSource();
        #endregion

        #region Properties

        public ObservableCollection<IBoontonPiControlViewModel> Devices { get; set; } = new ObservableCollection<IBoontonPiControlViewModel>();
        #endregion

        #region Commands
        //public IMvxCommand StartPi1Command { get; }
        public IMvxCommand StartAllPisCommand { get; }
        //public IMvxCommand StopPi1Command { get; }
        public IMvxCommand StopAllPisCommand { get; }
        public IMvxCommand BoontonStartupCommand { get; }
        public IMvxCommand BoontonCloseSensorsCommand { get; }
        public IMvxCommand BoontonResetSensorsCommand { get; }
        #endregion

        #region Constructors
        public RabbitControlOverviewViewModel(IBackgroundHandler backgroundHandler, IRabbitControlledManager deviceManager, IPiControlPublisher commandPublisher) : base(backgroundHandler)
        {
            _deviceManager = deviceManager;
            _commandPublisher = commandPublisher;

            //StartPi1Command = new MvxAsyncCommand(OnStartPi1);
            StartAllPisCommand = new MvxAsyncCommand(OnStartAllPis);
            //StopPi1Command = new MvxAsyncCommand(OnStopPi1);
            StopAllPisCommand = new MvxAsyncCommand(OnStopAllPis);
            BoontonStartupCommand = new MvxAsyncCommand(OnBoontonStartup);
            BoontonCloseSensorsCommand = new MvxAsyncCommand(OnBoontonCloseSensors);
            BoontonResetSensorsCommand = new MvxAsyncCommand(OnBoontonResetSensors);

            _ = PollUpdates();
        }

        public override Task OnUnloaded()
        {
            statusToken.Cancel();
            return base.OnUnloaded();
        }
        #endregion

        #region Methods

        private async Task PollUpdates()
        {
            while (!statusToken.IsCancellationRequested)
            {
                ReconcileCollections();
                foreach (var device in Devices)
                {
                    await device.PollUpdates();
                }
                await Task.Delay(2000);
            }

        }

        void ReconcileCollections()
        {
            //var idk = _deviceManager.DevicesByName.Keys;
            var ocHostnames = from device in Devices select device.Device.Hostname;
            var dmHostnames = _deviceManager.DevicesByName.Keys;
            var ocMissingNames = dmHostnames.Where(x => !ocHostnames.Any(x.Contains));
            var dmMissingNames = ocHostnames.Where(x => !dmHostnames.Any(x.Contains));

            try
            {
                //Add new viewmodels previously not found in device manager
                foreach (var item in ocMissingNames)
                {
                    var newDevice = _deviceManager.DevicesByName[item];
                    Devices.Add(new BoontonPiControlViewModel(_backgroundHandler, (IBoontonPi)newDevice, _commandPublisher));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PollUpdates Error {ex.Message}");
            }

            //Remove old viewmodels no longer found in device manager
            var tmp = new List<IBoontonPiControlViewModel>();//  Devices.Where(x => dmMissingNames.Any(x.Device.Hostname.Contains));

            foreach (var item in Devices)
            {
                if (!dmHostnames.Contains(item.Device.Hostname))
                {
                    tmp.Add(item);
                }
            }
            try
            {
                foreach (var item in tmp)
                {
                    var index = Devices.IndexOf(item);
                    var vm = Devices[index];
                    vm = null;
                    Devices.RemoveAt(index);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PollUpdates Error {ex.Message}");
            }
        }
        private Task OnStartPi1()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StartPoll(), "rasp.control.pi1"));
            return Task.CompletedTask;
        }

        private Task OnStartAllPis()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StartPoll(), "rasp.control.all"));
            return Task.CompletedTask;
        }

        private Task OnStopPi1()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StopPoll(), "rasp.control.pi1"));
            return Task.CompletedTask;
        }

        private Task OnStopAllPis()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StopPoll(), "rasp.control.all"));
            return Task.CompletedTask;
        }

        private Task OnBoontonStartup()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonStartup(), "rasp.control.all"));
            return Task.CompletedTask;
        }

        private Task OnBoontonCloseSensors()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonCloseSensors(), "rasp.control.all"));
            return Task.CompletedTask;
        }

        private Task OnBoontonResetSensors()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonResetSensors(), "rasp.control.all"));
            return Task.CompletedTask;
        }
        #endregion

    }
}
