using BaseClasses;
using BaseClasses.StateManagers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Managers
{
    public interface IHomeStateManager : ITcpStateManager
    {

        List<RabbitMQFactoryModel> RabbitFactories { get; }
        List<RabbitMQConnectionModel> RabbitConnections { get; }
        List<WiznetConnectionModel> WiznetConnections { get; }
    }
}
