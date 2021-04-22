using HomeMQ.Models;
using HomeMQ.RabbitMQ.Consumers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumers
{
    public class DataCollectionConsumer : TopicConsumer
    {
        #region Fields
        private IDataCollectionProcessor _processor;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public DataCollectionConsumer(IConnectionFactory factory, IDataCollectionProcessor processor, string exchange, string routeKey, string consumerName = null) : base(factory, exchange, routeKey, consumerName)
        {
            _processor = processor;
        }

        public DataCollectionConsumer(IConnectionFactory factory, IDataCollectionProcessor processor, string exchange, List<string> routeKeys, string consumerName = null) : base(factory, exchange, routeKeys, consumerName)
        {
            _processor = processor;
        }
        #endregion

        #region Methods
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Model.BasicAck(deliveryTag, false);
            var bodyString = Encoding.UTF8.GetString(body.ToArray());
            //Debug.WriteLine(bodyString);

            var resp = JsonConvert.DeserializeObject<DataSaveMessage>(bodyString, jsonSettings);
            _processor.Process(resp);


        }
        #endregion


    }
}
