using Acr.UserDialogs;
using BaseClasses;
using BaseViewModels;
using HomeMQ.RabbitMQ.Consumer;
using MvvmCross;
using MvvmCross.ViewModels;
using RabbitMqManagers;
using System;
using System.Collections.Generic;
using System.Text;
using WiznetControllers;

namespace HomeMQ.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IWiznetManager wiznetManager;
        private INavigationViewModel detailViewModel;
        private IUserDialogs alerts;
        private MQConnectionManager rabbitConnectionManager;
        private IMasterControlProcessor commandProcessor;
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
            Messenger.Instance.Register<DetailNavigationMessage>(this, x => DetailViewModel = x.NavigateToViewModel);

            wiznetManager = Mvx.IoCProvider.Resolve<IWiznetManager>();
            alerts = Mvx.IoCProvider.Resolve<IUserDialogs>();
            rabbitConnectionManager = Mvx.IoCProvider.Resolve<MQConnectionManager>();
            commandProcessor = Mvx.IoCProvider.Resolve<IMasterControlProcessor>();

            MasterViewModel = new MenuViewModel(wiznetManager, alerts, rabbitConnectionManager, commandProcessor);
            DetailViewModel = new PrimaryOverviewViewModel(wiznetManager, alerts, rabbitConnectionManager, commandProcessor);
        }
    }
}
