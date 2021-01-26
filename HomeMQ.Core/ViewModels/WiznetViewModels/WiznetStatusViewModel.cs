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

        //public IWiznetControl Wiznet => throw new NotImplementedException();

        public ClientStatus ClientStatus => Wiznet.ClientStatus;

        /// <summary>
        /// Connect this to Textbox or similar on UI
        /// </summary>
        //private string ipAddress;
        //public string IPAddress
        //{
        //    get { return ipAddress; }
        //    set
        //    {
        //        if (ipAddress != value)
        //        {
        //            ipAddress = value;
        //            RaisePropertyChanged();
        //        }
        //    }
        //}

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
        public WiznetStatusViewModel(IWiznetPiControl wizController)
        {
            ConnectCommand = new MvxAsyncCommand(OnConnect, CanConnect);
            DisconnectCommand = new MvxAsyncCommand(OnDisconnect, CanDisconnect);
            CancelConnectCommand = new MvxAsyncCommand(OnCancelConnect, CanCancelConnect);

            //this might be needed to handle initial bindings
            //Wiznet = new WiznetControlSCPI();
            Wiznet = (IWiznetPiControl)wizController;

            PiPowerControls = new ObservableCollection<WiznetPiControlViewModel>
            {
                new WiznetPiControlViewModel(Wiznet, 1),
                new WiznetPiControlViewModel(Wiznet, 2),
            };

            Messenger.Instance.Register<UpdateViewMessage>(this, async x => await OnUpdateView());
            Messenger.Instance.Register<ViewUnloadedMessage>(this, async x => await OnUnloaded());
        }
        #endregion

        #region Override Methods
        //public override async Task Initialize()
        //{
        //    await base.Initialize();
        //    try
        //    {
        //        var connection = await Mvx.IoCProvider.Resolve<IWiznetManager>().GetWiznetAsync(IPAddress);
        //        Wiznet = connection;
        //    }
        //    finally
        //    {

        //    }

        //}

        public override async Task UpdateUIControlAccess()
        {
            await base.UpdateUIControlAccess();
            await RaisePropertyChanged(nameof(ClientStatus));
            IsConnecting = Wiznet.ClientStatus == ClientStatus.Connecting;
        }

        public override async Task OnUnloaded()
        {
            isDisplayed = false;
            Messenger.Instance.Unregister(this);
        }
        #endregion

        #region Methods
        public virtual async Task OnUpdateView()
        {
            await UpdateUIControlAccess();
        }

        private async Task OnConnect()
        {
            IsConnecting = true;
            await Wiznet.Connect();
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
            Messenger.Instance.Send(new UpdateViewMessage());
        }

        private bool CanCancelConnect()
        {
            return Wiznet.ClientStatus == ClientStatus.Connecting;
        }

        private async Task OnDisconnect()
        {
            await Wiznet.CloseAsync();
            Messenger.Instance.Send(new UpdateViewMessage());
            //await UpdateUIControlAccess();
        }

        private bool CanDisconnect()
        {
            return Wiznet.IsConnected;
            //Wiznet.ClientStatus == ClientStatus.Connected;
        }
        #endregion

    }
}
