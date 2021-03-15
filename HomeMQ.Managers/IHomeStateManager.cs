using BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Managers
{
    public interface IHomeStateManager : IStateManager
    {

        List<RabbitMQConfigurationModel> RabbitConnections { get; }
    }
}
