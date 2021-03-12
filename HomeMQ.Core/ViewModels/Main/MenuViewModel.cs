using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.RabbitMQ.Consumer;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using RabbitMqManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WiznetControllers;

namespace HomeMQ.Core.ViewModels
{
    public class MenuViewModel : MvxViewModel, INavigationViewModel
    {
        #region Fields
        //private IMainControl mainControl;
        //private IMessenger messenger;
        //private IBackgroundHandler _backgroundHandler;
        IHomeMQNavigation _navService;
        #endregion

        #region Properties

        #endregion

        #region Commands
        public IMvxCommand ShowFirstPageCommand { get; }
        public IMvxCommand ShowSecondPageCommand { get; }
        #endregion

        #region Constructors
        //public MenuViewModel()// IMainControl mControl)
        //{
        //    //mainControl = mControl;
        //    ShowFirstPageCommand = new MvxCommand(ShowFirstPage);
        //    ShowSecondPageCommand = new MvxCommand(ShowSecondPage);
        //}

        public MenuViewModel(IHomeMQNavigation navService)// IBackgroundHandler backgroundHandler)// IMainControl mControl)
        {
            //mainControl = mControl;
            //messenger = mess;
            //_backgroundHandler = backgroundHandler;
            _navService = navService;
            ShowFirstPageCommand = new MvxCommand(ShowFirstPage);
            ShowSecondPageCommand = new MvxCommand(ShowSecondPage);
        }
        public Task UpdateUIControlAccess()
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Methods
        //public void ShowFirstPage()
        //{
        //    mainControl.NavigatePrimaryOverview();
        //}

        //public void ShowSecondPage()
        //{
        //    mainControl.NavigateUpgradeDebug();
        //}

        public void ShowFirstPage()
        {
            _navService.NavigateToPrimaryOverview();
            //_backgroundHandler.SendMessage(new DetailNavigationMessage(new PrimaryOverviewViewModel(_backgroundHandler)));
            //messenger.Send(new DetailNavigationMessage(new PrimaryOverviewViewModel(messenger)));
        }

        public void ShowSecondPage()
        {
            //mainControl.NavigateUpgradeDebug();
            //messenger.Send(new DetailNavigationMessage(new PrimaryOverviewViewModel(messenger)));
        }
        #endregion

    }
}
