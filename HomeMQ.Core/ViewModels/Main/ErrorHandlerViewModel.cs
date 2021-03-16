using BaseClasses;
using BaseViewModels;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public class ErrorHandlerViewModel : BaseViewModel, IErrorViewModel
    {
        #region Fields
        #endregion

        #region Properties
        public string Message => _backgroundHandler.CurrentNotification;
        public int MessageNumber
        {
            get
            {
                return _backgroundHandler.NotificationIndex + 1;
            }
        }
        public int MessageCount => _backgroundHandler.NotificationCount;
        public bool HasMessages => _backgroundHandler.NotificationCount != 0;

        #endregion

        #region Commands
        public IMvxCommand DismissMessageCommand { get; private set; }
        public IMvxCommand PreviousMessageCommand { get; private set; }
        public IMvxCommand NextMessageCommand { get; set; }
        #endregion

        #region Constructors
        public ErrorHandlerViewModel(IBackgroundHandler backgroundHandler) : base(backgroundHandler)
        {
            _backgroundHandler = backgroundHandler;
            DismissMessageCommand = new MvxAsyncCommand(OnDismiss);
        }

        private async Task OnDismiss()
        {
            _backgroundHandler.DismissCurrentMessage();
        }
        #endregion

        #region Methods
        public override async Task UpdateUIControlAccess()
        {
            await base.UpdateUIControlAccess();
            await RaisePropertyChanged(nameof(Message));
            await RaisePropertyChanged(nameof(MessageNumber));
            await RaisePropertyChanged(nameof(HasMessages));
            await RaisePropertyChanged(nameof(MessageCount));
        }
        #endregion

    }
}
