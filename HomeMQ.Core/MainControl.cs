using Acr.UserDialogs;
using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Core.ViewModels;
using HomeMQ.Managers;
using HomeMQ.RabbitMQ.Consumer;
using HomeMQ.RabbitMQ.Publishers;
using RabbitMQ.Client;
using RabbitMqManagers;
using System;
using System.Collections.Generic;
using System.Text;
using WiznetControllers;
using WPFMessageBox;

namespace HomeMQ.Core
{
    public class MainControl : IMainControl
    {
        IStateManager stateManager;
        ILogManager logManager;
        IUserDialogs userDialogs;
        IWiznetManager wiznetManager;
        MQConnectionManager rabbitConnectionManager;
        IRabbitControlledManager deviceManager;
        IMasterControlProcessor commandProcessor;
        IPiControlPublisher piController;
        

        public MainControl()
        {
            stateManager = StateManager.Instance;
            logManager = new LogManager();
            userDialogs = WPFUserDialogs.Instance;
            wiznetManager = new WiznetManager();//.Instance;
            rabbitConnectionManager = MQConnectionManager.Instance;
            deviceManager = RabbitControlledDeviceManager.Instance;
            commandProcessor = new MasterControlProcessor();

            foreach (var item in stateManager.RabbitConnections)
            {
                var factory = new ConnectionFactory()
                {
                    HostName = item.Hostname,
                    UserName = item.UserName,
                    Password = item.Password
                };
                rabbitConnectionManager.AddFactory(item.ConnectionName, factory);
                //{ HostName = "192.168.68.109", UserName = "devin", Password = "Ikorgil19" };
            }
            var homeFactory = rabbitConnectionManager.FactoriesByName["home"];
            piController = new PiControlPublisher(homeFactory, rabbitConnectionManager, "rtsh_topics", "Pi Controller");
        }

        #region Methods
        public void NavigatePrimaryOverview()
        {
            Messenger.Instance.Send(new ViewUnloadedMessage());
            Messenger.Instance.Send(new DetailNavigationMessage(
                new PrimaryOverviewViewModel(wiznetManager, userDialogs, rabbitConnectionManager, commandProcessor, deviceManager))
            );
        }

        public void NavigateUpgradeDebug()
        {
            Messenger.Instance.Send(new ViewUnloadedMessage());
            Messenger.Instance.Send(new DetailNavigationMessage(new UpgradeDebugViewModel()));
        }
            


        #endregion

        //Mvx.IoCProvider.RegisterSingleton<IStateManager>(() => StateManager.Instance);
        //    Mvx.IoCProvider.RegisterSingleton(() => WPFUserDialogs.Instance);
        //    Mvx.IoCProvider.RegisterSingleton(() => WiznetManager.Instance);
        //    Mvx.IoCProvider.RegisterSingleton(() => MQConnectionManager.Instance);
        //    Mvx.IoCProvider.RegisterSingleton<IRabbitControlledManager>(() => RabbitControlledDeviceManager.Instance);
        //    Mvx.IoCProvider.RegisterSingleton<IMasterControlProcessor>(new MasterControlProcessor());
        //    var tmpFactory = MQConnectionManager.Instance.FactoriesByName["home"];
        //var connection = tmpFactory.CreateConnection();
        //Mvx.IoCProvider.RegisterSingleton<IPiControlPublisher>(new PiControlPublisher(connection, "rtsh_topics"));
    }
}
