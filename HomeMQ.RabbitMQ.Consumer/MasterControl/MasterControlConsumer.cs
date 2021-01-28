using BaseClasses;
using DeviceManagers;
using HomeMQ.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumer
{
    public class MasterControlConsumer : TopicConsumer
    {
        //private RabbitControlledDeviceManager RabbitManager = RabbitControlledDeviceManager.Instance;

        private IMasterControlProcessor commandProcessor;
        public MasterControlConsumer(IConnection conn, IMasterControlProcessor processor, string exchange, string routeKey) : base(conn, exchange, routeKey)
        {
            commandProcessor = processor;
        }

        public MasterControlConsumer(IConnectionFactory factory, IMasterControlProcessor processor, string exchange, string routeKey) : base(factory, exchange, routeKey)
        {
            commandProcessor = processor;
        }

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
            commandProcessor.Process(jo);
            //Console.WriteLine();
        }
    }
}
