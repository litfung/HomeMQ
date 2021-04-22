using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Models;
using HomeMQ.RabbitMQ.Consumers;
using HomeMQ.RabbitMQ.Publishers;
using MvvmCross.ViewModels;
using NetworkDeviceModels;
using RabbitMQ.Client;
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
    public class RabbitConsumerViewModel : BaseViewModel, IRabbitConsumerViewModel
    {
        #region Fields
        //private ITopicConsumer consumer;
        private IRabbitControlledManager _deviceManager;
        private IPiControlPublisher _commandPublisher;
        //private SynchronizationContext _uiContext;
        #endregion

        #region Properties
        private ObservableCollection<IBoontonPiControlViewModel> devices = new ObservableCollection<IBoontonPiControlViewModel>();
        public ObservableCollection<IBoontonPiControlViewModel> Devices
        {
            get { return devices; }
            set
            {
                if (devices != value)
                {
                    devices = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors
        public RabbitConsumerViewModel(IBackgroundHandler backgroundHandler, IRabbitControlledManager deviceManager, IPiControlPublisher commandPublisher) : base(backgroundHandler)
        {
            _deviceManager = deviceManager;
            _commandPublisher = commandPublisher;
            //_uiContext = SynchronizationContext.Current;
            _ = OnUpdateView();
            //_ = InitView();
        }
        #endregion

        #region Override Methods
        public Task PollUpdates()
        {
            ReconcileCollection();
            return Task.CompletedTask;
        }

        void ReconcileCollection()
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
                    Devices.Add(new BoontonPiStatusViewModel(_backgroundHandler, (IBoontonPi)newDevice));
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

        //public override async Task OnUnloaded()
        //{
        //    while(Devices.Count > 0)
        //    {
        //        var vm = Devices[0];
        //        Devices.RemoveAt(0);
        //        vm = null;
        //    }
        //    await base.OnUnloaded();
        //}

        //public override async Task OnUpdateView()
        //{
        //    ReconcileCollection();
        //    await base.OnUpdateView();
        //}

        //public override async Task OnUpdateView()
        //{
        //    _uiContext?.Send((x) =>
        //    {
        //        var vmList = new List<IRabbitControlViewModel>();
        //        Devices.Clear();
        //        foreach (var item in _deviceManager.AllDevices)
        //        {
        //            Devices.Add(new RabbitControlStatusViewModel(_backgroundHandler, (IBoontonPi)item, _commandPublisher));
        //        }
        //    },
        //    null);
        //    //App
        //    //var vmList = new List<IRabbitControlViewModel>();
        //    //foreach (var item in _deviceManager.AllDevices)
        //    //{
        //    //    Devices.Add(new RabbitControlStatusViewModel(_backgroundHandler, (IBoontonPi)item, _commandPublisher));
        //    //}
        //    //Devices

        //    //Devices.Clear();
        //    //Devices = new ObservableCollection<IRabbitControlViewModel>(vmList);
        //    //await base.OnUpdateView();
        //}

        #endregion

        #region Methods
        //private async Task InitView()
        //{
        //    var vmList = new List<IRabbitControlViewModel>();
        //    foreach (var item in _deviceManager.AllDevices)
        //    {
        //        vmList.Add(new RabbitControlStatusViewModel(_backgroundHandler, (IBoontonPi)item, _commandPublisher));
        //    }
        //    foreach (var device in Devices)
        //    {
        //        device = null;
        //    }
        //    Devices = new ObservableCollection<IRabbitControlViewModel>(vmList);

        //}
        #endregion


    }
}
