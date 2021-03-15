using BaseClasses;
using DeviceManagers;
using HomeMQ.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Control.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumer
{
    public class MasterControlConsumer : TopicConsumer
    {

        #region Fields
        private IMasterControlProcessor responseProcessor;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public MasterControlConsumer(IConnection conn, IMasterControlProcessor processor, string exchange, string routeKey) : base(conn, exchange, routeKey)
        {
            responseProcessor = processor;
        }

        public MasterControlConsumer(IConnectionFactory factory, IMasterControlProcessor processor, string exchange, string routeKey, string readableName = null) : base(factory, exchange, routeKey, readableName)
        {
            responseProcessor = processor;
        }
        #endregion

        #region Methods
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Model.BasicAck(deliveryTag, false);
            var bodyString = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine(bodyString);

            var definition = new { Command = "" };
            var payload = new { Header = definition };
            var device = JsonConvert.DeserializeAnonymousType(bodyString, payload);

            if (device.Header.Command.Contains("response"))
            {
                HandleResponse(bodyString);
            }


        }

        private void HandleResponse(string reponseJsonString)
        {
            var jo = JsonConvert.DeserializeObject<ControlResponse>(reponseJsonString);

            //Several ways to handle this here
            //First way : Let Manager Handle data
            //This doesn;t look viable, have a circular dependency
            //RabbitManager.Process(jo);
            //Option 2: Handle with Messenger

            //Option 3 : Use DI of a command processor
            responseProcessor.Process(jo);
            //Console.WriteLine();
        }
        #endregion





    }
}
