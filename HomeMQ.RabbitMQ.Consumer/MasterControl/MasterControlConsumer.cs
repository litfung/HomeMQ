using BaseClasses;
using DeviceManagers;
using HomeMQ.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Control.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumer
{
    public class MasterControlConsumer : TopicConsumer
    {

        #region Fields
        private JsonSerializerSettings jsonSettings;
        delegate void ParamsFunc(string jsonString);
        private IMasterControlProcessor responseProcessor;
        //private Dictionary<string, Type> ResponseDictionary = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        //{
        //    {"status_check_response", typeof(StatusCheck)},
        //    {"start_poll_response", typeof(StartPoll) }
        //};

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public MasterControlConsumer(IConnection conn, IMasterControlProcessor processor, string exchange, string routeKey) : base(conn, exchange, routeKey)
        {
            responseProcessor = processor;
            InitJsonSettings();
        }

        public MasterControlConsumer(IConnectionFactory factory, IMasterControlProcessor processor, string exchange, string routeKey, string readableName = null) : base(factory, exchange, routeKey, readableName)
        {
            responseProcessor = processor;
            InitJsonSettings();
        }
        #endregion

        #region Methods

        void InitJsonSettings()
        {
            jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
        }
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Model.BasicAck(deliveryTag, false);
            var bodyString = Encoding.UTF8.GetString(body.ToArray());
            Debug.WriteLine(bodyString);

            var resp = JsonConvert.DeserializeObject<ControlResponse>(bodyString, jsonSettings);
            responseProcessor.Process(resp);
            //Debug.WriteLine(device.Header.Command);
            //var type = Type.GetType(device.Header.Command);
            //Debug.WriteLine($"type = {type}");
            //HandleResponse(device.Header.Command, bodyString);


        }

        //private void HandleResponse(string command, string responseString)
        //{
            
        //    try
        //    {
        //        ResponseDictionary["command"](responseString);
        //        //var type = ResponseDictionary[command];
        //        //var jo = JsonConvert.DeserializeObject(responseString, type, settings);
        //        //responseProcessor.Process(jo);
        //        //Console.WriteLine();
        //    }
        //    catch(Exception ex)
        //    {
        //        Debug.WriteLine($"handle response exception --> {ex.Message}");
        //    }
        //}

        //private void HandleResponse(string reponseJsonString)
        //{
        //    var settings = new JsonSerializerSettings
        //    {
        //        ContractResolver = new DefaultContractResolver
        //        {
        //            NamingStrategy = new SnakeCaseNamingStrategy()
        //        }
        //    };
        //    var jo = JsonConvert.DeserializeObject<ControlResponse>(reponseJsonString, settings);

        //    //Several ways to handle this here
        //    //First way : Let Manager Handle data
        //    //This doesn;t look viable, have a circular dependency
        //    //RabbitManager.Process(jo);
        //    //Option 2: Handle with Messenger

        //    //Option 3 : Use DI of a command processor
        //    responseProcessor.Process(jo);
        //    //Console.WriteLine();
        //}

        //private void ProcessStatusCheck(string jsonString)
        //{
        //    var jo = JsonConvert.DeserializeObject<ControlResponse>(jsonString, jsonSettings);
        //}

        //private void ProcessStartPoll(string jsonString)
        //{
        //    var jo = JsonConvert.DeserializeObject<ControlResponse>(jsonString, jsonSettings);
        //}
        #endregion





    }
}
