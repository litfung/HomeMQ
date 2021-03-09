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
        private IMessenger messenger;
        private ILogManager logger;
        #endregion

        #region Properties
        private LogMessage logMessage;
        public LogMessage LogMessage
        {
            get { return logMessage; }
            set
            {
                if (logMessage != value)
                {
                    logMessage = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(CanShowMessage));
                }
            }
        }

        public bool CanShowMessage => logMessage != null;

        #endregion

        #region Commands
        public IMvxCommand DismissMessageCommand { get; private set; }
        public IMvxCommand PreviousMessageCommand { get; private set; }
        public IMvxCommand NextMessageCommand { get;  set; }
        #endregion

        #region Constructors
        public ErrorHandlerViewModel(IMainControl mc)//   IMessenger mess, ILogManager logs)
        {
            messenger = mess;
            logger = logs;
            mess.Register<UpdateViewMessage>(this, async x => await OnUpdateView());
            DismissMessageCommand = new MvxCommand(OnDismiss);
        }

        public override async Task OnUpdateView()
        {
            logger.Err
            await base.OnUpdateView();
        }


        private void OnDismiss()
        {

        }
        #endregion

        #region Methods

        #endregion

    }
}
