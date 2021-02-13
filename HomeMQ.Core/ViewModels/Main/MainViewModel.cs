using Acr.UserDialogs;
using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Managers;
using HomeMQ.RabbitMQ.Consumer;
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
        //private IStateManager stateManager;
        //private IWiznetManager wiznetManager;
        //private IUserDialogs alerts;
        //private MQConnectionManager rabbitConnectionManager;
        //private IMasterControlProcessor commandProcessor;
        //private IRabbitControlledManager deviceManager;
        private IMainControl mainControl = new MainControl();

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

        public MainViewModel()
        {
            Messenger.Instance.Register<MasterNavigationMessage>(this, x => MasterViewModel = x.NavigateToViewModel);
            Messenger.Instance.Register<DetailNavigationMessage>(this, 
                x => 
                DetailViewModel = x.NavigateToViewModel);
        }

        public override Task Initialize()
        {
            //stateManager = Mvx.IoCProvider.Resolve<IStateManager>();
            //wiznetManager = Mvx.IoCProvider.Resolve<IWiznetManager>();
            //alerts = Mvx.IoCProvider.Resolve<IUserDialogs>();
            //rabbitConnectionManager = Mvx.IoCProvider.Resolve<MQConnectionManager>();
            //commandProcessor = Mvx.IoCProvider.Resolve<IMasterControlProcessor>();
            //deviceManager = Mvx.IoCProvider.Resolve<IRabbitControlledManager>();

            //foreach (var item in stateManager.RabbitConnections)
            //{
            //    var factory = new ConnectionFactory()
            //    {
            //        HostName = item.Hostname,
            //        UserName = item.UserName,
            //        Password = item.Password
            //    };

            //    rabbitConnectionManager.AddFactory(item.ConnectionName, factory);
            //}
            NavigateStart();
            return base.Initialize();
        }

        private void NavigateStart()
        {
            MasterViewModel = new MenuViewModel(mainControl);//    wiznetManager, alerts, rabbitConnectionManager, commandProcessor, deviceManager);
            mainControl.NavigatePrimaryOverview();
            //DetailViewModel = new PrimaryOverviewViewModel(wiznetManager, alerts, rabbitConnectionManager, commandProcessor, deviceManager);
        }
    }
}
