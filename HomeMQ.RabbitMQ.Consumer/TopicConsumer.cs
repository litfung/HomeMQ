using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumer
{
    public class TopicConsumer : DefaultBasicConsumer, ITopicConsumer
    {
        #region Fields

        #endregion

        #region Properties
        public ConsumerSettings Settings { get; set; } = new ConsumerSettings();
        public string ExchangeName { get; set; }
        public List<string> RoutingKeys { get; set; }
        private IConnection Connection { get; set; }
        public Dictionary<string, string> RoutesToQueues { get; set; } = new Dictionary<string, string>();
        #endregion

        #region Constructors
        public TopicConsumer(IConnection conn, string exchange, string routeKey)
        {
            Connection = conn;
            ExchangeName = exchange;
            RoutingKeys = new List<string> { routeKey };
            Model = Connection.CreateModel();
        }

        public TopicConsumer(IConnectionFactory factory, string exchange, string routeKey, string consumerName = null)
            : this(factory, exchange, new List<string> { routeKey }, consumerName) { }

        public TopicConsumer(IConnectionFactory factory, string exchange, List<string> routeKeys, string consumerName = null)
        {
            if (consumerName != null)
            {
                Connection = factory.CreateConnection(clientProvidedName:consumerName);
            }
            else
            {
                Connection = factory.CreateConnection();
            }
            
            ExchangeName = exchange;
            RoutingKeys = routeKeys;
            Model = Connection.CreateModel();
        }

        ~TopicConsumer()
        {
            Connection.Dispose();
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
            Model.ExchangeDeclare(exchange: ExchangeName, type: "topic", durable: Settings.Durable, autoDelete: Settings.AutoDelete, arguments: null);
            foreach (var key in RoutingKeys)
            {
                var queueName = Model.QueueDeclare().QueueName;
                Model.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: key);
                Model.BasicQos(0, Settings.Prefetch, false);
                Model.BasicConsume(queue: queueName, Settings.AutoAck, consumer: this);
                RoutesToQueues.Add(key, queueName);
            }
        }

        public void CancelByKey(string key)
        {
            Model.BasicCancel(RoutesToQueues[key]);
            Connection.Dispose();
        }

        public void CancelByQueue(string queueName)
        {
            Model.BasicCancel(queueName);
            Connection.Dispose();
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
        public TopicConsumer WithPrefetchCount(int prefetch)
        {
            Settings.Prefetch = (ushort)prefetch;
            return this;
        }

        public TopicConsumer WithDurable(bool durable)
        {
            Settings.Durable = durable;
            return this;
        }

        public TopicConsumer WithAutoAck(bool autoack)
        {
            Settings.AutoAck = autoack;
            return this;
        }

        public TopicConsumer WithAutoDelete(bool delete)
        {
            Settings.AutoDelete = delete;
            return this;
        }
        #endregion

    }
}
