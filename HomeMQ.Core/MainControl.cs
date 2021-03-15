//using BaseClasses;
//using BaseViewModels;
//using DeviceManagers;
//using HomeMQ.Core.ViewModels;
//using HomeMQ.Managers;
//using HomeMQ.RabbitMQ.Consumer;
//using HomeMQ.RabbitMQ.Publishers;
//using RabbitMQ.Client;
//using RabbitMqManagers;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using WiznetControllers;

//namespace HomeMQ.Core
//{
//    public class MainControl : IMainControl
//    {

//        //public IStateManager StateManager { get; private set; }
//        //public ILogManager LogManager { get; private set; }
//        //public IMessenger Messenger { get; private set; }
//        //public IWiznetManager WiznetManager { get; private set; }
//        //public IMQConnectionManager RabbitConnectionManager { get; private set; }
//        //public IRabbitControlledManager DeviceManager { get; private set; }
//        //public IMasterControlProcessor CommandProcessor { get; private set; }
//        //public IPiControlPublisher PiController { get; private set; }

//        //public MainControl()
//        //{
//        //    StateManager = new StateManager();  //Low level, no dependencies
//        //    Messenger = new Messenger();  //Low level, no dependencies
//        //    LogManager = new LogManager();  //Low level, no dependencies
//        //    WiznetManager = new WiznetManager();// LogManager);//.Instance;
//        //    RabbitConnectionManager = new MQConnectionManager();//.Instance;
//        //    DeviceManager = new RabbitControlledDeviceManager();//.Instance;
//        //    CommandProcessor = new MasterControlProcessor(DeviceManager, Messenger);

//        //    foreach (var item in StateManager.RabbitConnections)
//        //    {
//        //        var factory = new ConnectionFactory()
//        //        {
//        //            HostName = item.Hostname,
//        //            UserName = item.UserName,
//        //            Password = item.Password
//        //        };
//        //        RabbitConnectionManager.AddFactory(item.ConnectionName, factory);
//        //        //{ HostName = "192.168.68.109", UserName = "devin", Password = "Ikorgil19" };
//        //    }

//        //    var firstWiz = new WiznetControlSCPI("169.254.208.100");
//        //    WiznetManager.AddWiznet(firstWiz);
//        //    var homeFactory = RabbitConnectionManager.FactoriesByName["home"];
//        //    PiController = new PiControlPublisher(homeFactory, RabbitConnectionManager, "rtsh_topics", "Pi Controller");
//        //}

//        IStateManager stateManager;
//        ILogManager logManager;
//        IMessenger messenger;
//        IWiznetManager wiznetManager;
//        IMQFactoryManager rabbitConnectionManager;
//        IRabbitControlledManager deviceManager;
//        IMasterControlProcessor commandProcessor;
//        IPiControlPublisher piController;

//        public MainControl(IStateManager sm, IMessenger m, ILogManager lm,
//            IWiznetManager wm, IMQFactoryManager mq, IRabbitControlledManager dm,
//            IMasterControlProcessor mc)//, IPiControlPublisher pi)
//        {
//            //stateManager = StateManager.Instance;
//            //messenger = messenger;
//            //logManager = new LogManager(messenger);
//            //wiznetManager = new WiznetManager(logManager);//.Instance;
//            //rabbitConnectionManager = MQConnectionManager.Instance;
//            //deviceManager = RabbitControlledDeviceManager.Instance;
//            //commandProcessor = new MasterControlProcessor();

//            //stateManager = sm;
//            //logManager = lm;
//            //messenger = m;
//            //wiznetManager = wm;
//            //rabbitConnectionManager = mq;
//            //deviceManager = dm;
//            //commandProcessor = mc;
//            ////piController = pi;

//            //foreach (var item in stateManager.RabbitConnections)
//            //{
//            //    var factory = new ConnectionFactory()
//            //    {
//            //        HostName = item.Hostname,
//            //        UserName = item.UserName,
//            //        Password = item.Password
//            //    };
//            //    rabbitConnectionManager.AddFactory(item.ConnectionName, factory);
//            //    //{ HostName = "192.168.68.109", UserName = "devin", Password = "Ikorgil19" };
//            //}
//            //var firstWiz = new WiznetControlSCPI("169.254.208.100", messenger);
//            //wiznetManager.AddWiznet(firstWiz);
//            //var homeFactory = rabbitConnectionManager.FactoriesByName["home"];
//            //PiController = new PiControlPublisher(homeFactory, rabbitConnectionManager, "rtsh_topics", "Pi Controller");
//        }

//        #region Methods
//        public void NavigatePrimaryOverview()
//        {
//            //messenger.Send(new ViewUnloadedMessage());
//            //messenger.Send(new DetailNavigationMessage(
//            //    new PrimaryOverviewViewModel(messenger, wiznetManager, rabbitConnectionManager, commandProcessor, deviceManager))
//            //);
//        }

//        public void NavigateUpgradeDebug()
//        {
//            //messenger.Send(new ViewUnloadedMessage());
//            //messenger.Send(new DetailNavigationMessage(new UpgradeDebugViewModel(messenger)));
//        }

//        //public void NavigatePrimaryOverview()
//        //{
//        //    Messenger.Send(new ViewUnloadedMessage());
//        //    Messenger.Send(new DetailNavigationMessage(
//        //        new PrimaryOverviewViewModel(Messenger, WiznetManager, RabbitConnectionManager, CommandProcessor, DeviceManager))
//        //    );
//        //}

//        //public void NavigateUpgradeDebug()
//        //{
//        //    Messenger.Send(new ViewUnloadedMessage());
//        //    Messenger.Send(new DetailNavigationMessage(new UpgradeDebugViewModel(Messenger)));
//        //}



//        #endregion

//        //Mvx.IoCProvider.RegisterSingleton<IStateManager>(() => StateManager.Instance);
//        //    Mvx.IoCProvider.RegisterSingleton(() => WPFUserDialogs.Instance);
//        //    Mvx.IoCProvider.RegisterSingleton(() => WiznetManager.Instance);
//        //    Mvx.IoCProvider.RegisterSingleton(() => MQConnectionManager.Instance);
//        //    Mvx.IoCProvider.RegisterSingleton<IRabbitControlledManager>(() => RabbitControlledDeviceManager.Instance);
//        //    Mvx.IoCProvider.RegisterSingleton<IMasterControlProcessor>(new MasterControlProcessor());
//        //    var tmpFactory = MQConnectionManager.Instance.FactoriesByName["home"];
//        //var connection = tmpFactory.CreateConnection();
//        //Mvx.IoCProvider.RegisterSingleton<IPiControlPublisher>(new PiControlPublisher(connection, "rtsh_topics"));
//    }
//}
