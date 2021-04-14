using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Models;
using HomeMQ.RabbitMQ.Consumer;
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
        private CancellationToken statusToken;
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

        #region Commands
        public IMvxCommand StartPi1Command { get; }
        public IMvxCommand StartAllPisCommand { get; }
        public IMvxCommand StopPi1Command { get; }
        public IMvxCommand StopAllPisCommand { get; }
        public IMvxCommand BoontonStartupCommand { get; }
        #endregion
       
        #region Constructors

        public PrimaryOverviewViewModel(IBackgroundHandler backgroundHandler, IWiznetManager wiznetManager, IRabbitControlledManager deviceManager, IPiControlPublisher commandPublisher) : base(backgroundHandler)
        {
            _backgroundHandler = backgroundHandler;
            _wiznetManager = wiznetManager;
            _deviceManager = deviceManager;
            _commandPublisher = commandPublisher;
            
            StartPi1Command = new MvxAsyncCommand(OnStartPi1);
            StartAllPisCommand = new MvxAsyncCommand(OnStartAllPis);
            StopPi1Command = new MvxAsyncCommand(OnStopPi1);
            StopAllPisCommand = new MvxAsyncCommand(OnStopAllPis);
            BoontonStartupCommand = new MvxAsyncCommand(OnBoontonStartup);
            foreach (var item in _wiznetManager.AllWiznets)
            {
                WiznetStatusControls.Add(new WiznetStatusViewModel(_backgroundHandler, (IWiznetPiControl)item));
            }

            RabbitConsumer = new RabbitConsumerViewModel(_backgroundHandler, _deviceManager);

            _ = PollRabbitDevices();
        }


        #endregion

        #region Methods
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

        private Task OnBoontonStartup()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonStartup(), "rasp.control.all"));
            return Task.CompletedTask;
        }
        #endregion

    }
}
