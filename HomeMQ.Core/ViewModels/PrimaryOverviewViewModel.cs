using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Models;
using HomeMQ.RabbitMQ.Consumers;
using HomeMQ.RabbitMQ.Publishers;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using RabbitMQ.Client;
using RabbitMQ.Control.Core;
using RabbitMqManagers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WiznetControllers;

namespace HomeMQ.Core.ViewModels
{
    public class PrimaryOverviewViewModel : BaseViewModel
    {
        #region Fields
        bool isDisplayed = true;
        private IWiznetManager _wiznetManager;
        private IRabbitControlledManager _deviceManager;
        private IPiControlPublisher _commandPublisher;
        private CancellationTokenSource statusToken = new CancellationTokenSource();
        #endregion

        #region Properties
        private ObservableCollection<IWiznetStatusViewModel> wiznetStatusControls = new ObservableCollection<IWiznetStatusViewModel>();
        public ObservableCollection<IWiznetStatusViewModel> WiznetStatusControls
        {
            get { return wiznetStatusControls; }
            set
            {
                if (wiznetStatusControls != value)
                {
                    wiznetStatusControls = value;
                    RaisePropertyChanged();
                }
            }
        }
        private IRabbitConsumerViewModel rabbitConsumer;
        public IRabbitConsumerViewModel RabbitConsumer
        {
            get { return rabbitConsumer; }
            set
            {
                if (rabbitConsumer != value)
                {
                    rabbitConsumer = value;
                    RaisePropertyChanged();
                }
            }
        }


        #endregion

        

        #region Constructors

        public PrimaryOverviewViewModel(IBackgroundHandler backgroundHandler, IWiznetManager wiznetManager, IRabbitControlledManager deviceManager, IPiControlPublisher commandPublisher) : base(backgroundHandler)
        {
            //_backgroundHandler = backgroundHandler;
            _wiznetManager = wiznetManager;
            _deviceManager = deviceManager;
            _commandPublisher = commandPublisher;

            foreach (var item in _wiznetManager.AllWiznets)
            {
                WiznetStatusControls.Add(new WiznetStatusViewModel(_backgroundHandler, (IWiznetPiControl)item));
            }

            RabbitConsumer = new RabbitConsumerViewModel(_backgroundHandler, _deviceManager, _commandPublisher);

            //_ = PollRabbitDevices();
        }


        #endregion

        #region Override Methods
        public override async Task OnUnloaded()
        {
            statusToken.Cancel();
            await base.OnUnloaded();
        }
        #endregion

        #region Methods


        private async Task PollRabbitDevices()
        {
            while (!statusToken.IsCancellationRequested)
            {
                foreach (var device in _deviceManager.AllDevices)
                {
                    if ((DateTime.Now - device.LastUpdateTime).TotalSeconds > 10.0)
                    {
                        device.Status = "lost";
                    }
                    else if ((DateTime.Now - device.LastUpdateTime).TotalSeconds > 4.0 )
                    {
                        device.Status = "stale";
                    }
                }

                _backgroundHandler.SendMessage(new UpdateViewMessage());
                await Task.Delay(2000);
            }
        }
        #endregion

    }
}
