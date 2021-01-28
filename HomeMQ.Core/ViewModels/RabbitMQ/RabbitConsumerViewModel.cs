using BaseViewModels;
using DeviceManagers;
using HomeMQ.RabbitMQ.Consumer;
using MvvmCross.ViewModels;
using NetworkDeviceModels;
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
        private MasterControlConsumer consumer;
        private RabbitControlledDeviceManager deviceManager => RabbitControlledDeviceManager.Instance;
        #endregion

        #region Properties
        private ObservableCollection<IRabbitControlled> devices;
        public ObservableCollection<IRabbitControlled> Devices
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
        public RabbitConsumerViewModel(MasterControlConsumer topicConsumer)
        {
            consumer = topicConsumer;
            Consume();
        }
        #endregion

        #region Override Methods
        public override async Task OnUpdateView()
        {
            Devices = new ObservableCollection<IRabbitControlled>(deviceManager.AllDevices);
            await base.OnUpdateView();
        }

        public void Consume()
        {
            consumer.Consume();
        }

        public void Stop()
        {

        }
        #endregion

        #region Methods

        #endregion


    }
}
