using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Models;
using HomeMQ.RabbitMQ.Consumer;
using HomeMQ.RabbitMQ.Publishers;
using MvvmCross.ViewModels;
using NetworkDeviceModels;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public class RabbitConsumerViewModel : BaseViewModel, IRabbitConsumerViewModel
    {
        #region Fields
        //private ITopicConsumer consumer;
        private IRabbitControlledManager _deviceManager;
        private IPiControlPublisher _commandPublisher;
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
        _ = OnUpdateView();
        }
        #endregion

        #region Override Methods
        public override async Task OnUpdateView()
        {
            var vmList = new List<IRabbitControlViewModel>();
            foreach (var item in _deviceManager.AllDevices)
            {
                vmList.Add(new RabbitControlStatusViewModel(_backgroundHandler, (IBoontonPi)item, _commandPublisher));
            }
            Devices = new ObservableCollection<IRabbitControlViewModel>(vmList);
            await base.OnUpdateView();
        }

        public void Stop()
        {

        }
        #endregion

        #region Methods

        #endregion


    }
}
