using BaseClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Managers
{

    public enum ConfigType
    {
        LocalPath,
        OutputPath
    }
    public class HomeStateManager : IHomeStateManager
    {
        #region Fields
        private const string stateFilename = "home_control.json";
        #endregion

        #region Properties
        //public SavedStateModel State { get; private set; }
        public List<RabbitMQFactoryModel> RabbitFactories { get; private set; } = new List<RabbitMQFactoryModel>();
        public List<RabbitMQConnectionModel> RabbitConnections { get; set; } = new List<RabbitMQConnectionModel>();
        public List<WiznetConnectionModel> WiznetConnections { get; set; } = new List<WiznetConnectionModel>();
        //public JObject State { get; set; }

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
            //SimpleLoad();
            //return LoadFromJson();
        }

        public static HomeStateManager Create(ConfigType type, string fileName)
        {
            //SimpleLoad();
            switch (type)
            {
                case ConfigType.LocalPath:
                    //Not quite what I was looking for but it works /shrug
                    var tmp = "config.json";
                    return LoadFromJson(tmp);
                    //Debug.WriteLine(tmp);
                case ConfigType.OutputPath:
                    var filePath = new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}{fileName}").FullName;
                    return LoadFromJson(filePath);
                default:
                    throw new ArgumentException("Need proper config type");
            }
            
        }

        private static HomeStateManager LoadFromJson(string fullPath)
        {
            using (var file = File.OpenText(fullPath))
            {
                var serializer = new JsonSerializer();
                var tmp = (HomeStateManager)serializer.Deserialize(file, typeof(HomeStateManager));
                return tmp;
            }
        }

        private void SimpleLoad()
        {
            var testConfig = new RabbitMQFactoryModel
            {
                ConnectionName = "home",
                UserName = "devin",
                Password = "Ikorgil19",
                Hostname = "192.168.68.109"
            };
            RabbitFactories.Add(testConfig);

            var masterConsumer = new RabbitMQConnectionModel
            {
                FactoryName = "home",
                ConnectionName = "Master consumer",
                ExchangeName = "rtsh_topics",
                RabbitMQDirection = RabbitMQDirection.Consumer,
                //ConsumerType = ConsumerType.MasterControl
            };

            var piControl = new RabbitMQConnectionModel
            {
                FactoryName = "home",
                ConnectionName = "pi controller 1",
                ExchangeName = "rtsh_topics",
                RabbitMQDirection = RabbitMQDirection.Publisher
            };
            RabbitConnections.Add(masterConsumer);
            RabbitConnections.Add(piControl);

            var firstWiz = new WiznetConnectionModel
            {
                IPAddress = "169.254.208.100"
            };
            WiznetConnections.Add(firstWiz);

            SaveState("renew_config.json");
        }

        private void SaveState(string fileName)
        {
            var filePath = new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}{fileName}");

            using (var file = File.CreateText(filePath.FullName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, this);
            }
        }
        #endregion
    }
}
