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


        //public ObservableCollection<WiznetControlsSCPI> Wiznet { get; set; }

        #endregion

        #region Commands
        public IMvxCommand StartPi1Command { get; }
        #endregion
        #region Constructors

        public PrimaryOverviewViewModel(IBackgroundHandler backgroundHandler, IWiznetManager wiznetManager, IRabbitControlledManager deviceManager, IPiControlPublisher commandPublisher) : base(backgroundHandler)
        {
            _backgroundHandler = backgroundHandler;
            _wiznetManager = wiznetManager;
            _deviceManager = deviceManager;
            _commandPublisher = commandPublisher;

            

            StartPi1Command = new MvxAsyncCommand(OnStartPi1);
            foreach (var item in _wiznetManager.AllWiznets)
            {
                WiznetStatusControls.Add(new WiznetStatusViewModel(_backgroundHandler, (IWiznetPiControl)item));
            }

            RabbitConsumer = new RabbitConsumerViewModel(_backgroundHandler, _deviceManager);
        }


        //public PrimaryOverviewViewModel(IMessenger iMessenger, IWiznetManager wizManager, IMQConnectionManager mqConnections, 
        //    IMasterControlProcessor processor, IRabbitControlledManager dManager) : base(iMessenger)
        //{
        //    WiznetManager = wizManager;
        //    rabbitConnectionManager = mqConnections;
        //    rabbitCommandProcessor = processor;
        //    deviceManager = dManager;

        //    foreach (var item in WiznetManager.AllWiznets)
        //    {
        //        WiznetStatusControls.Add(new WiznetStatusViewModel(iMessenger, (IWiznetPiControl)item));
        //    }


        //    var exchangeName = "rtsh_topics";
        //    var routeKey = "master.control.*";
        //    var factory = mqConnections.FactoriesByName["home"];
        //    var consumer = new MasterControlConsumer(factory, rabbitCommandProcessor, exchangeName, routeKey, "master control");

        //    RabbitConsumer = new RabbitConsumerViewModel(iMessenger, consumer, deviceManager);
        //}


        #endregion

        #region Methods
        //private Task OnUpdateView()
        //{
        //    throw new NotImplementedException();
        //}

        private Task OnStartPi1()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StartPoll(), "rasp.control.pi1"));
            return Task.CompletedTask;
        }
        #endregion

    }
}
