using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Managers;
using HomeMQ.RabbitMQ.Consumer;
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
        //private IMvxNavigationService navService;
        IMessenger _messenger;
        IStateManager _stateManager;
        ILogManager _logManager;
        IWiznetManager _wiznetManager;
        //MQConnectionManager rabbitConnectionManager;
        //IRabbitControlledManager deviceManager;
        //IMasterControlProcessor commandProcessor;
        //IPiControlPublisher piController;

        //private IErrorViewModel errorHandlerViewModel;
        //public IErrorViewModel ErrorHandlerViewModel
        //{
        //    get { return errorHandlerViewModel; }
        //    set
        //    {
        //        if (errorHandlerViewModel != value)
        //        {
        //            errorHandlerViewModel = value;
        //            RaisePropertyChanged();
        //        }
        //    }
        //}


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

            InitializeBackgroundHandler();
            InitializeWiznetStuff();
            
        }

        void InitializeBackgroundHandler()
        {
            _messenger = new Messenger();
            _logManager = new LogManager();
            _stateManager = new StateManager();
            _backgroundHandler = new SimpleBackgroundHandler(_messenger, _logManager);
            _backgroundHandler.RegisterMessage<MasterNavigationMessage>(this, x => MasterViewModel = x.NavigateToViewModel);
            _backgroundHandler.RegisterMessage<DetailNavigationMessage>(this, x => DetailViewModel = x.NavigateToViewModel);
            //messenger = iMessenger;
            //messenger.Register<MasterNavigationMessage>(this, x => MasterViewModel = x.NavigateToViewModel);
            //messenger.Register<DetailNavigationMessage>(this, x => DetailViewModel = x.NavigateToViewModel);
        }

        void InitializeWiznetStuff()
        {
            _wiznetManager = new WiznetManager();
            var firstWiz = new WiznetControlSCPI("169.254.208.100", _backgroundHandler);
            _wiznetManager.AddWiznet(firstWiz);

        }

        //public MainViewModel()
        //{
        //    stateManager = StateManager.Instance;
        //    messenger = messenger;
        //    logManager = new LogManager(messenger);
        //    wiznetManager = new WiznetManager(logManager);//.Instance;
        //    rabbitConnectionManager = MQConnectionManager.Instance;
        //    deviceManager = RabbitControlledDeviceManager.Instance;
        //    commandProcessor = new MasterControlProcessor();
        //    mainControl = new MainControl(stateManager,
        //        messenger, logManager, wiznetManager, rabbitConnectionManager, deviceManager, commandProcessor, piController);
        //    messenger.Register<MasterNavigationMessage>(this, x => MasterViewModel = x.NavigateToViewModel);
        //    messenger.Register<DetailNavigationMessage>(this, x => DetailViewModel = x.NavigateToViewModel);
        //}

        //public MainViewModel(IMessenger iMessenger)
        //{
        //    //messenger = new Messenger();
        //    //stateManager = new StateManager();
        //    //logManager = new LogManager();
        //    //wiznetManager = new WiznetManager();
        //    //rabbitConnectionManager = new MQConnectionManager();
        //    //deviceManager = new RabbitControlledDeviceManager();
        //    //commandProcessor = new MasterControlProcessor(deviceManager, messenger);
        //    ////IPiControlPublisher piController = new PiControlPublisher();
        //    //mainControl = new MainControl(stateManager, messenger, logManager, wiznetManager, rabbitConnectionManager, deviceManager, commandProcessor);
        //    //messenger.Register<MasterNavigationMessage>(this, x => MasterViewModel = x.NavigateToViewModel);
        //    //mainControl.Messenger.Register<DetailNavigationMessage>(this, x => DetailViewModel = x.NavigateToViewModel);
        //    messenger.Register<DetailNavigationMessage>(this, x => DetailViewModel = x.NavigateToViewModel);

        //}

        public override Task Initialize()
        {

            _ = NavigateStart();
            return base.Initialize();
        }

        private async Task NavigateStart()
        {
            MasterViewModel = new MenuViewModel(this);// mainControl);
            //ErrorHandlerViewModel = new ErrorHandlerViewModel(mainControl);
            //mainControl.NavigatePrimaryOverview();
        }

        public void NavigateToPrimaryOverview()
        {

            _backgroundHandler.SendMessage(new DetailNavigationMessage(new PrimaryOverviewViewModel(_backgroundHandler, _wiznetManager)));
        }
    }
}
