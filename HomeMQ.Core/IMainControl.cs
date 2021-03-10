using BaseClasses;
using DeviceManagers;
using HomeMQ.Managers;
using HomeMQ.RabbitMQ.Consumer;
using HomeMQ.RabbitMQ.Publishers;
using RabbitMqManagers;
using WiznetControllers;

namespace HomeMQ.Core
{
    public interface IMainControl
    {
        IMasterControlProcessor CommandProcessor { get; }
        IRabbitControlledManager DeviceManager { get; }
        ILogManager LogManager { get; }
        IMessenger Messenger { get; }
        IPiControlPublisher PiController { get; }
        IMQConnectionManager RabbitConnectionManager { get; }
        IStateManager StateManager { get; }
        IWiznetManager WiznetManager { get; }

        void NavigatePrimaryOverview();
        void NavigateUpgradeDebug();
    }
}