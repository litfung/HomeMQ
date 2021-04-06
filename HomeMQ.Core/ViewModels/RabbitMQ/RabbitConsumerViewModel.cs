using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.RabbitMQ.Consumer;
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
        //public RabbitConsumerViewModel(IMessenger iMessenger, MasterControlConsumer topicConsumer, IRabbitControlledManager dManager) : base(iMessenger)
        //{
        //    messenger = iMessenger;
        //    consumer = topicConsumer;
        //    deviceManager = dManager;
        //    Consume();
        //}

        public RabbitConsumerViewModel(IBackgroundHandler backgroundHandler, IRabbitControlledManager deviceManager) : base(backgroundHandler)
        {
            _deviceManager = deviceManager;
            _ = OnUpdateView();
        }
        #endregion

        #region Override Methods
        public override async Task OnUpdateView()
        {
            var vmList = new List<IRabbitControlViewModel>();
            foreach (var item in _deviceManager.AllDevices)
            {
                vmList.Add(new RabbitControlStatusViewModel(_backgroundHandler, item));
            }
            Devices = new ObservableCollection<IRabbitControlViewModel>(vmList);
            //Devices = new ObservableCollection<IRabbitControlled>((IEnumerable<IRabbitControlled>)deviceManager.AllDevices);
            await base.OnUpdateView();
        }

        //public void Consume()
        //{
        //    consumer.Consume();
        //}

        public void Stop()
        {

        }
        #endregion

        #region Methods

        #endregion


    }
}
