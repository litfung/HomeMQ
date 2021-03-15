using BaseClasses;
using HomeMQ.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace HomeMQ.Managers
{
    public interface IStateManager
    {
        //List<RabbitMQConfigurationModel> RabbitConnections { get; }
        //SavedStateModel State { get; }
        //JObject State { get; }
        void LoadState();
    }
}