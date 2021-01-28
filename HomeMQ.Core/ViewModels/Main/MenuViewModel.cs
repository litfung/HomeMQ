using Acr.UserDialogs;
using BaseClasses;
using BaseViewModels;
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
        private IWiznetManager wiznetManager;
        private IUserDialogs userDialogs;
        private MQConnectionManager rabbitConnectionManager;
        private IMasterControlProcessor commandProcessor;
        #endregion

        #region Properties

        #endregion

        #region Commands
        public ICommand ShowFirstPageCommand { get; }
        public ICommand ShowSecondPageCommand { get; }
        #endregion

        #region Constructors
        public MenuViewModel(IWiznetManager wizManager, IUserDialogs dialog, MQConnectionManager rabbitManager, IMasterControlProcessor processor)
        {
            wiznetManager = wizManager;
            userDialogs = dialog;
            rabbitConnectionManager = rabbitManager;
            commandProcessor = processor;

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
            Messenger.Instance.Send(new ViewUnloadedMessage());
            Messenger.Instance.Send(new DetailNavigationMessage(new PrimaryOverviewViewModel(wiznetManager, userDialogs, rabbitConnectionManager, commandProcessor)));
        }

        public void ShowSecondPage()
        {
            Messenger.Instance.Send(new ViewUnloadedMessage());
            Messenger.Instance.Send(new DetailNavigationMessage(new PrimaryOverviewViewModel(wiznetManager, userDialogs, rabbitConnectionManager, commandProcessor)));
        }
        #endregion

    }
}
