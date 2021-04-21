using BaseClasses;
using BaseViewModels;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WiznetControllers;

namespace HomeMQ.Core.ViewModels
{
    public class WiznetStatusViewModel : BaseViewModel, IWiznetStatusViewModel
    {
        #region Fields
        bool isDisplayed = true;
        #endregion

        #region Properties
        public IWiznetPiControl Wiznet { get; private set; }

        IWiznetControl IWiznetStatusViewModel.Wiznet => Wiznet;

        public ClientStatus ClientStatus => Wiznet.ClientStatus;

       public string IPAddress => Wiznet.IPAddress.ToString();

        private bool isConnecting;
        public bool IsConnecting
        {
            get { return isConnecting; }
            set
            {
                if (isConnecting != value)
                {
                    isConnecting = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<WiznetPiControlViewModel> piPowerControls;
        public ObservableCollection<WiznetPiControlViewModel> PiPowerControls
        {
            get { return piPowerControls; }
            set
            {
                if (piPowerControls != value)
                {
                    piPowerControls = value;
                    RaisePropertyChanged();
                }
            }
        }



        #endregion

        #region Commands
        public IMvxCommand ConnectCommand { get; set; }
        public IMvxCommand CancelConnectCommand { get; set; }
        public IMvxCommand DisconnectCommand { get; set; }


        #endregion

        #region Constructors
        public WiznetStatusViewModel(IBackgroundHandler backgroundHandler, IWiznetPiControl wizController) : base(backgroundHandler)
        {
            ConnectCommand = new MvxAsyncCommand(OnConnect, CanConnect);
            DisconnectCommand = new MvxAsyncCommand(OnDisconnect, CanDisconnect);
            CancelConnectCommand = new MvxAsyncCommand(OnCancelConnect, CanCancelConnect);

            //this might be needed to handle initial bindings
            //Wiznet = new WiznetControlSCPI();
            Wiznet = wizController;

            //_ = WiznetStatus();

            PiPowerControls = new ObservableCollection<WiznetPiControlViewModel>
            {
                new WiznetPiControlViewModel(backgroundHandler, Wiznet, 1),
                new WiznetPiControlViewModel(backgroundHandler, Wiznet, 2),
            };

            _backgroundHandler.RegisterMessage<UpdateViewMessage>(this, async x => await OnUpdateView());
            _backgroundHandler.RegisterMessage<ViewUnloadedMessage>(this, async x => await OnUnloaded());
        }
        #endregion

        #region Override Methods

        public override async Task UpdateUIControlAccess()
        {
            await base.UpdateUIControlAccess();
            await RaisePropertyChanged(nameof(ClientStatus));
            IsConnecting = Wiznet.ClientStatus == ClientStatus.Connecting;
        }
        #endregion

        #region Methods
        public virtual async Task OnUpdateView()
        {
            await UpdateUIControlAccess();
        }

        public override async Task OnUnloaded()
        {
            while (PiPowerControls.Count > 0)
            {
                var vm = PiPowerControls[0];
                PiPowerControls.RemoveAt(0);
                vm = null;
            }
            await base.OnUnloaded();
        }

        public async Task OnConnect()
        {
            IsConnecting = true;
            await Wiznet.ConnectAsync();
            await RaisePropertyChanged(nameof(ClientStatus));
        }

        private bool CanConnect()
        {
            return Wiznet.ClientStatus != ClientStatus.Connected;
        }

        private async Task OnCancelConnect()
        {
            await Wiznet.CancelConnect();
            await RaisePropertyChanged(nameof(ClientStatus));
            _backgroundHandler.SendMessage(new UpdateViewMessage());
        }

        private bool CanCancelConnect()
        {
            return Wiznet.ClientStatus == ClientStatus.Connecting;
        }

        public async Task OnDisconnect()
        {
            await Wiznet.CloseAsync();
            _backgroundHandler.SendMessage(new UpdateViewMessage());
            //await UpdateUIControlAccess();
        }

        private bool CanDisconnect()
        {
            return Wiznet.IsConnected;
            //Wiznet.ClientStatus == ClientStatus.Connected;
        }

        //private async Task WiznetStatus()
        //{
        //    //await RaiseAllPropertiesChanged();
        //    //messenger.Send(new UpdateViewMessage());
        //    await Task.Delay(500);
        //    await WiznetStatus();
        //}
        #endregion

    }
}
