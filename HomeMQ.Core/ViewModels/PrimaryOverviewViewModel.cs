using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.RabbitMQ.Consumer;
using MvvmCross;
using RabbitMQ.Client;
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
        private IWiznetManager WiznetManager;
        private IMQConnectionManager rabbitConnectionManager;
        private IMasterControlProcessor rabbitCommandProcessor;
        private IRabbitControlledManager deviceManager;
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

        #region Constructors
        public PrimaryOverviewViewModel(IMessenger iMessenger, IWiznetManager wizManager, IMQConnectionManager mqConnections, 
            IMasterControlProcessor processor, IRabbitControlledManager dManager) : base(iMessenger)
        {
            WiznetManager = wizManager;
            rabbitConnectionManager = mqConnections;
            rabbitCommandProcessor = processor;
            deviceManager = dManager;

            foreach (var item in WiznetManager.AllWiznets)
            {
                WiznetStatusControls.Add(new WiznetStatusViewModel(iMessenger, (IWiznetPiControl)item));
            }

            
            var exchangeName = "rtsh_topics";
            var routeKey = "master.control.*";
            var factory = mqConnections.FactoriesByName["home"];
            var consumer = new MasterControlConsumer(factory, rabbitCommandProcessor, exchangeName, routeKey, "master control");

            RabbitConsumer = new RabbitConsumerViewModel(iMessenger, consumer, deviceManager);
        }


        #endregion

        #region Methods
        //private Task OnUpdateView()
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

    }
}
