using BaseClasses;
using BaseClasses.StateManagers;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Managers;
using HomeMQ.RabbitMQ.Consumers;
using HomeMQ.RabbitMQ.Publishers;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RabbitMQ.Client;
using RabbitMqManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiznetControllers;

namespace HomeMQ.Core.ViewModels
{
    public class MainViewModel : MvxViewModel, IHomeMQNavigation
    {
        //private IMainControl mainControl;
        IBackgroundHandler _backgroundHandler;
        INotificationHandler _notificationService;
        IMessenger _messenger;
        IHomeStateManager _stateManager;
        ILogManager _logManager;
        IWiznetManager _wiznetManager;
        IMQFactoryManager _mqFactoryManager;
        IMQConnectionManager _mqConnectionManager;
        IRabbitControlledManager _rabbitDeviceTracker;


        //MQConnectionManager rabbitConnectionManager;
        //IRabbitControlledManager deviceManager;
        //IMasterControlProcessor commandProcessor;
        //IPiControlPublisher piController;

        private IErrorViewModel errorHandlerViewModel;
        public IErrorViewModel ErrorHandlerViewModel
        {
            get { return errorHandlerViewModel; }
            set
            {
                if (errorHandlerViewModel != value)
                {
                    errorHandlerViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }


        private INavigationViewModel detailViewModel;
        public INavigationViewModel DetailViewModel
        {
            get { return detailViewModel; }
            set
            {
                if (detailViewModel != value)
                {
                    detailViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        private INavigationViewModel masterViewModel;
        public INavigationViewModel MasterViewModel
        {
            get { return masterViewModel; }
            set
            {
                if (masterViewModel != value)
                {
                    masterViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }



        public MainViewModel()// IBackgroundHandler backgroundHandler)
        {
            _stateManager = StateManager.Create<HomeStateManager>(ConfigType.OutputPath, "config.json");
            //_stateManager = HomeStateManager.Create(ConfigType.OutputPath, "config.json");
            InitializeBackgroundHandler();
            InitializeWiznetStuff();
            InitializeRabbitStuff();


        }

        void InitializeBackgroundHandler()
        {
            _messenger = new Messenger();
            _logManager = new LogManager();
            _notificationService = new SimpleNotificationService();

            _backgroundHandler = new SimpleBackgroundHandler(_messenger, _logManager, _notificationService);
            _backgroundHandler.RegisterMessage<MasterNavigationMessage>(this, x => MasterViewModel = x.NavigateToViewModel);
            _backgroundHandler.RegisterMessage<DetailNavigationMessage>(this, x => DetailViewModel = x.NavigateToViewModel);
            //messenger = iMessenger;
            //messenger.Register<MasterNavigationMessage>(this, x => MasterViewModel = x.NavigateToViewModel);
            //messenger.Register<DetailNavigationMessage>(this, x => DetailViewModel = x.NavigateToViewModel);
        }

        void InitializeWiznetStuff()
        {
            _wiznetManager = new WiznetManager(_stateManager, _backgroundHandler);


        }

        void InitializeRabbitStuff()
        {
            _mqFactoryManager = new MQFactoryManager(_stateManager);
            _rabbitDeviceTracker = new RabbitControlledDeviceManager();
            _mqConnectionManager = new MQConnectionManager(_stateManager, _mqFactoryManager, _backgroundHandler, _rabbitDeviceTracker);
        }

        public override Task Initialize()
        {
            _ = NavigateStart();
            return base.Initialize();
        }

        private async Task NavigateStart()
        {
            MasterViewModel = new MenuViewModel(this);
            ErrorHandlerViewModel = new ErrorHandlerViewModel(_backgroundHandler);
            NavigateToPrimaryOverview();
        }

        public void NavigateToPrimaryOverview()
        {
            IPiControlPublisher publisher;
            //try
            //{
            //    publisher = (IPiControlPublisher)_mqConnectionManager.ConnectionsByName["pi controller 1"];
            //}
            //catch (Exception)
            //{
                publisher = new NonePiControlPublisher(); 
            //}
            //new ViewUnloadedMessage()
            _backgroundHandler.SendMessage(new ViewUnloadedMessage());
            _backgroundHandler.SendMessage(new DetailNavigationMessage(new PrimaryOverviewViewModel(_backgroundHandler, _wiznetManager, _rabbitDeviceTracker, publisher)));
        }

        public void NavigateToSensorControl()
        {
            var publisher = (IPiControlPublisher)_mqConnectionManager.ConnectionsByName["pi controller 1"];
            _backgroundHandler.SendMessage(new ViewUnloadedMessage());
            _backgroundHandler.SendMessage(new DetailNavigationMessage(new RabbitControlOverviewViewModel(_backgroundHandler, _rabbitDeviceTracker, publisher)));
        }
    }
}
