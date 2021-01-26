using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumer
{
    public class MasterControlConsumer : TopicConsumer
    {
        public MasterControlConsumer(IConnection conn, string exchange, string routeKey) : base(conn, exchange, routeKey)
        {
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Model.BasicAck(deliveryTag, false);
            var bodyString = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine(bodyString);
        }
    }
}
