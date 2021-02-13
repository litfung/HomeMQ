using BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Managers
{
    public class StateManager : IStateManager
    {
        #region Singleton
        private static readonly Lazy<StateManager> instance = new Lazy<StateManager>();
        public static StateManager Instance => instance.Value;
        public StateManager()
        {
            LoadState();
        }
        #endregion

        #region Fields
        private const string stateFilename = "home_control.json";
        #endregion

        #region Properties
        public SavedStateModel State { get; private set; }
        public List<RabbitMQConfigurationModel> RabbitConnections { get; private set; } = new List<RabbitMQConfigurationModel>();

        #endregion

        #region Methods
        public void LoadState()
        {
            var testConfig = new RabbitMQConfigurationModel
            {
                ConnectionName = "home",
                UserName = "devin",
                Password = "Ikorgil19",
                Hostname = "192.168.68.109"
            };

            RabbitConnections.Add(testConfig);

        }


        #endregion
    }
}
