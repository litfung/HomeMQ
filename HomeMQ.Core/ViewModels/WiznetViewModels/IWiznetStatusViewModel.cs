using MvvmCross.Commands;
using NetworkDeviceModels;
using System.Threading.Tasks;
using WiznetControllers;

namespace HomeMQ.Core.ViewModels
{
    public interface IWiznetStatusViewModel
    {
        IMvxCommand CancelConnectCommand { get; set; }
        ClientStatus ClientStatus { get; }
        IMvxCommand ConnectCommand { get; set; }
        IMvxCommand DisconnectCommand { get; set; }
        string IPAddress { get; }
        bool IsConnecting { get; set; }
        IWiznetControl Wiznet { get; }

        Task Initialize();
        Task OnUnloaded();
        Task OnUpdateView();
    }
}