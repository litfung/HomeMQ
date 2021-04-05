using HomeMQ.RabbitMQ.Consumer;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumers
{
    public class DataCollectionConsumer : TopicConsumer
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public DataCollectionConsumer(IConnectionFactory factory, string exchange, string routeKey, string consumerName = null) : base(factory, exchange, routeKey, consumerName)
        {
        }
        #endregion

        #region Methods
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Model.BasicAck(deliveryTag, false);
            var bodyString = Encoding.UTF8.GetString(body.ToArray());


        }
        #endregion


    }
}
