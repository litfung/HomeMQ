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
        private SynchronizationContext _uiContext;
        #endregion

        #region Properties
        private ObservableCollection<IRabbitControlViewModel> devices = new ObservableCollection<IRabbitControlViewModel>();
        public ObservableCollection<IRabbitControlViewModel> Devices
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
            _uiContext = SynchronizationContext.Current;
            _ = OnUpdateView();
            //_ = InitView();
        }
        #endregion

        #region Override Methods
        public override async Task OnUpdateView()
        {
            _uiContext?.Send((x) =>
            {
                var vmList = new List<IRabbitControlViewModel>();
                Devices.Clear();
                foreach (var item in _deviceManager.AllDevices)
                {
                    Devices.Add(new RabbitControlStatusViewModel(_backgroundHandler, (IBoontonPi)item, _commandPublisher));
                }
            },
            null);
            //App
            //var vmList = new List<IRabbitControlViewModel>();
            //foreach (var item in _deviceManager.AllDevices)
            //{
            //    Devices.Add(new RabbitControlStatusViewModel(_backgroundHandler, (IBoontonPi)item, _commandPublisher));
            //}
            //Devices

            //Devices.Clear();
            //Devices = new ObservableCollection<IRabbitControlViewModel>(vmList);
            //await base.OnUpdateView();
        }

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
