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
        IMessenger messenger;
        IWiznetManager wiznetManager;
        MQConnectionManager rabbitConnectionManager;
        IRabbitControlledManager deviceManager;
        IMasterControlProcessor commandProcessor;
        IPiControlPublisher piController;

        //public IStateManager StateManager { get; private set; }
        //public ILogManager LogManager { get; private set; }
        //public IMessenger Messenger { get; private set; }
        //public IWiznetManager WiznetManager { get; private set; }
        //public MQConnectionManager RabbitConnectionManager { get; private set; }
        //public IRabbitControlledManager DeviceManager { get; private set; }
        //public IMasterControlProcessor CommandProcessor { get; private set; }
        //public IPiControlPublisher PiController { get; private set; }

        public MainControl()
        {
            stateManager = StateManager.Instance;
            messenger = Messenger.Instance;
            logManager = new LogManager(messenger);
            wiznetManager = new WiznetManager(logManager);//.Instance;
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
        //public MainControl(IStateManager sm, IMessenger m,  ILogManager lm, 
        //    IWiznetManager wm, MQConnectionManager mq, IRabbitControlledManager dm, 
        //    IMasterControlProcessor mc, IPiControlPublisher pi)
        //{
        //    //stateManager = StateManager.Instance;
        //    //messenger = Messenger.Instance;
        //    //logManager = new LogManager(messenger);
        //    //wiznetManager = new WiznetManager(logManager);//.Instance;
        //    //rabbitConnectionManager = MQConnectionManager.Instance;
        //    //deviceManager = RabbitControlledDeviceManager.Instance;
        //    //commandProcessor = new MasterControlProcessor();

        //    StateManager = sm;
        //    LogManager = lm;
        //    Messenger = m;
        //    WiznetManager = wm;
        //    RabbitConnectionManager = mq;
        //    DeviceManager = dm;
        //    CommandProcessor = mc;
        //    PiController = pi;

        //    foreach (var item in StateManager.RabbitConnections)
        //    {
        //        var factory = new ConnectionFactory()
        //        {
        //            HostName = item.Hostname,
        //            UserName = item.UserName,
        //            Password = item.Password
        //        };
        //        RabbitConnectionManager.AddFactory(item.ConnectionName, factory);
        //        //{ HostName = "192.168.68.109", UserName = "devin", Password = "Ikorgil19" };
        //    }
        //    var homeFactory = RabbitConnectionManager.FactoriesByName["home"];
        //    PiController = new PiControlPublisher(homeFactory, RabbitConnectionManager, "rtsh_topics", "Pi Controller");
        //}

        #region Methods
        public void NavigatePrimaryOverview()
        {
            messenger.Send(new ViewUnloadedMessage());
            messenger.Send(new DetailNavigationMessage(
                new PrimaryOverviewViewModel(wiznetManager, rabbitConnectionManager, commandProcessor, deviceManager))
            );
        }

        public void NavigateUpgradeDebug()
        {
            messenger.Send(new ViewUnloadedMessage());
            messenger.Send(new DetailNavigationMessage(new UpgradeDebugViewModel()));
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
