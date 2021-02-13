using Acr.UserDialogs;
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
        //private IWiznetManager wiznetManager;
        //private IUserDialogs userDialogs;
        //private MQConnectionManager rabbitConnectionManager;
        //private IMasterControlProcessor commandProcessor;
        //private IRabbitControlledManager deviceManager;
        private IMainControl mainControl;
        #endregion

        #region Properties

        #endregion

        #region Commands
        public IMvxCommand ShowFirstPageCommand { get; }
        public IMvxCommand ShowSecondPageCommand { get; }
        #endregion

        #region Constructors
        public MenuViewModel(IMainControl mControl)
            //IWiznetManager wizManager, IUserDialogs dialog, MQConnectionManager rabbitManager, 
            //IMasterControlProcessor processor, IRabbitControlledManager dManager)
        {
            mainControl = mControl;
            //wiznetManager = wizManager;
            //userDialogs = dialog;
            //rabbitConnectionManager = rabbitManager;
            //commandProcessor = processor;
            //deviceManager = dManager;

            ShowFirstPageCommand = new MvxCommand(ShowFirstPage);
            ShowSecondPageCommand = new MvxCommand(ShowSecondPage);

            

        }
        public Task UpdateUIControlAccess()
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Methods
        public void ShowFirstPage()
        {
            mainControl.NavigatePrimaryOverview();
        }

        public void ShowSecondPage()
        {
            mainControl.NavigateUpgradeDebug();
        }
        #endregion

    }
}
