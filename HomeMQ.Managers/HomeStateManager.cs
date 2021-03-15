using BaseClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Managers
{
    public class HomeStateManager : IHomeStateManager
    {
        //#region Singleton
        //private static readonly Lazy<StateManager> instance = new Lazy<StateManager>();
        //public static StateManager Instance => instance.Value;
        //public StateManager()
        //{
            
        //}
        //#endregion

        #region Fields
        private const string stateFilename = "home_control.json";
        #endregion

        #region Properties
        //public SavedStateModel State { get; private set; }
        public List<RabbitMQConfigurationModel> RabbitConnections { get; private set; } = new List<RabbitMQConfigurationModel>();
        public JObject State { get; set; }
        #endregion

        #region Constructors
        public HomeStateManager()
        {
            LoadState();
        }
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

            //var tmp = JObject.FromObject(testConfig);//    JsonConvert.SerializeObject(testConfig);
            //State = tmp;
            RabbitConnections.Add(testConfig);

            //var wiznetConfig = new WiznetSCPIConfigurationModel
            //var 

        }


        #endregion
    }
}
