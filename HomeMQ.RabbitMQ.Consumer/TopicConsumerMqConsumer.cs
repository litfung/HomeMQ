using NetworkDeviceModels;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumer
{
    public class TopicConsumerMqConsumer : DefaultBasicConsumer
    {
        #region Properties
        public ConsumerSettings Settings { get; set; } = new ConsumerSettings();
        public string ExchangeName { get; set; }
        public List<string> RoutingKeys { get; set; }
        public Dictionary<string, string> RoutesToQueues { get; set; } = new Dictionary<string, string>();
        //public Dictionary<string, ITopicProcessor> ProcessorsByRoutes { get; set; } = new Dictionary<string, ITopicProcessor>();
        #endregion

        #region Constructors
        public TopicConsumerMqConsumer(IConnection conn, string exchange, string routeKey)
        {
            ExchangeName = exchange;
            RoutingKeys = new List<string> { routeKey };
            Model = conn.CreateModel();
        }
        #endregion

        #region Methods
        public void AddRouteKey(string key)
        {
            RoutingKeys.Add(key);
        }

        //public void AddTopicProcessor(ITopicProcessor processor)
        //{
        //    TopicProcessors.Add(processor);
        //}

        //public void RemoveTopicProcessor(ITopicProcessor processor)
        //{
        //    processor.Cancel();
        //    TopicProcessors.Remove(processor);
        //}

        public void Consume()
        {
            Model.ExchangeDeclare(exchange: ExchangeName, type: "topic", Settings.Durable, Settings.AutoDelete, null);
            foreach (var key in RoutingKeys)
            {
                var queueName = Model.QueueDeclare().QueueName;
                Model.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: key);
                Model.BasicQos(0, Settings.Prefetch, false);
                Model.BasicConsume(queue: queueName, Settings.AutoAck, consumer: this);
                RoutesToQueues.TryAdd(key, queueName);
            }
        }

        public void CancelByKey(string key)
        {
            Model.BasicCancel(RoutesToQueues[key]);
        }

        public void CancelByQueue(string queueName)
        {
            Model.BasicCancel(queueName);
        }

        //public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        //{
        //    Model.BasicAck(deliveryTag, false);
        //    var bodyString = Encoding.UTF8.GetString(body.ToArray());
        //    if

        //    ProcessRabbitMessage(routingKey, bodyString);
        //}

        #endregion

        #region Fluent Methods
        public TopicConsumerMqConsumer WithPrefetchCount(int prefetch)
        {
            Settings.Prefetch = (ushort)prefetch;
            return this;
        }

        public TopicConsumerMqConsumer WithDurable(bool durable)
        {
            Settings.Durable = durable;
            return this;
        }

        public TopicConsumerMqConsumer WithAutoAck(bool autoack)
        {
            Settings.AutoAck = autoack;
            return this;
        }

        public TopicConsumerMqConsumer WithAutoDelete(bool delete)
        {
            Settings.AutoDelete = delete;
            return this;
        }
        #endregion
    }
}
