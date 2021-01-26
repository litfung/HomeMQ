using Acr.UserDialogs;
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
        private IUserDialogs dialogs;
        private MQConnectionManager rabbitConnectionManager;
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
        public PrimaryOverviewViewModel(IWiznetManager wizManager, IUserDialogs dialog, MQConnectionManager mqConnections)
        {
            WiznetManager = wizManager;
            dialogs = dialog;
            rabbitConnectionManager = mqConnections;

            foreach (var item in WiznetManager.AllWiznets)
            {
                WiznetStatusControls.Add(new WiznetStatusViewModel((IWiznetPiControl)item));
            }

            var username = "devin";
            var password = "Ikorgil19";
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.68.109",
                UserName = username,
                Password = password
            };

            rabbitConnectionManager.AddFactory(factory);
            var exchangeName = "rtsh_topics";
            var routeKey = "master.control.*";

            RabbitConsumer = new RabbitConsumerViewModel(new MasterControlConsumer(factory, exchangeName, routeKey));
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
