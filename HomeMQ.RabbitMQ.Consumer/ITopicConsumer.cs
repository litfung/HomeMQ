using System.Collections.Generic;

namespace HomeMQ.RabbitMQ.Consumer
{
    public interface ITopicConsumer
    {
        string ExchangeName { get; set; }
        Dictionary<string, string> RoutesToQueues { get; set; }
        List<string> RoutingKeys { get; set; }
        ConsumerSettings Settings { get; set; }

        void AddRouteKey(string key);
        void CancelByKey(string key);
        void CancelByQueue(string queueName);
        void Consume();
        TopicConsumer WithAutoAck(bool autoack);
        TopicConsumer WithAutoDelete(bool delete);
        TopicConsumer WithDurable(bool durable);
        TopicConsumer WithPrefetchCount(int prefetch);
    }
}