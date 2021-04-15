using HomeMQ.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Control.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.RabbitMQ.Publishers
{
    public class TopicPublisher : IRabbitMQConnection, ITopicPublisher
    {
        #region Fields
        private DefaultContractResolver contractResolver;
        private Queue<RabbitControlMessage> MessageQueue = new Queue<RabbitControlMessage>();
        private IBasicProperties publishProperties;
        private JsonSerializerSettings jsonSettings;
        #endregion

        #region Properties
        public string ExchangeName { get; set; }
        public List<string> RoutingKeys { get; set; }
        public IConnection Connection { get; set; }
        public Dictionary<string, string> RoutesToQueues { get; set; } = new Dictionary<string, string>();
        public IModel Channel { get; set; }


        #endregion

        #region Constructors
        //public TopicPublisher(IConnection conn, string exchange)
        //{
        //    Connection = conn;
        //    ExchangeName = exchange;
        //    Channel = Connection.CreateModel();
        //    Channel.ExchangeDeclare(ExchangeName, type: "topic");

        //    contractResolver = new DefaultContractResolver
        //    {
        //        NamingStrategy = new SnakeCaseNamingStrategy()
        //    };
        //}

        public TopicPublisher(IConnectionFactory factory, string exchange, string connectionName = null)
        {
            if (connectionName != null)
            {
                Connection = factory.CreateConnection(clientProvidedName: connectionName);
            }
            else
            {
                Connection = factory.CreateConnection();
            }
            ExchangeName = exchange;
            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(ExchangeName, type: "topic");

            contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            publishProperties = Channel.CreateBasicProperties();
            publishProperties.ContentType = "application/json";

            jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };


            _ = PublishMessages();// ScheduleNextMessage();
        }
        #endregion

        #region Methods
        public Task Publish(object o, string routingKey)
        {
            var message = JsonConvert.SerializeObject(o, jsonSettings);
            var body = Encoding.UTF8.GetBytes(message);
            //Debug.WriteLine($"publish string -->{message}");
            
            Channel.BasicPublish(exchange: ExchangeName, routingKey: routingKey, basicProperties: publishProperties, body: body);
            return Task.CompletedTask;
        }

        public async Task PublishMessages()
        {
            while (true)
            {
                try
                {
                    if (MessageQueue.Count > 1)
                    {
                        var next = MessageQueue.Dequeue();
                        await Publish(next, next.RoutingKey);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                await Task.Delay(1);
            }
        }

        //public async Task ScheduleNextMessage()
        //{
        //    try
        //    {
        //        if (MessageQueue.Count > 1)
        //        {
        //            var next = MessageQueue.Dequeue();
        //            await Publish(next.ControlMessage, next.RoutingKey);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
            
                
            
        //    await Task.Delay(1);
        //    await ScheduleNextMessage();
        //}

        public void AddMessage(RabbitControlMessage rcm)
        {
            MessageQueue.Enqueue(rcm);
        }
        #endregion

    }
}
