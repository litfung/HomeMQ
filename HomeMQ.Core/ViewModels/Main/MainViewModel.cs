using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Managers;
using HomeMQ.RabbitMQ.Consumer;
using HomeMQ.RabbitMQ.Publishers;
using MvvmCross;
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
    public class MainViewModel : MvxViewModel
    {
        private IMainControl mainControl;
        //private IMessenger messenger;
        //IStateManager stateManager;
        //ILogManager logManager;
        //IWiznetManager wiznetManager;
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

        public MainViewModel()
        {
            mainControl = new MainControl();
            //messenger.Register<MasterNavigationMessage>(this, x => MasterViewModel = x.NavigateToViewModel);
            mainControl.Messenger.Register<DetailNavigationMessage>(this, x => DetailViewModel = x.NavigateToViewModel);
        }

        public override Task Initialize()
        {

            NavigateStart();
            return base.Initialize();
        }

        private void NavigateStart()
        {
            MasterViewModel = new MenuViewModel(mainControl);
            ErrorHandlerViewModel = new ErrorHandlerViewModel(mainControl);
            mainControl.NavigatePrimaryOverview();
        }
    }
}
