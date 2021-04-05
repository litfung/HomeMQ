using RabbitMQ.Control.Core;

namespace HomeMQ.RabbitMQ.Publishers
{
    public  interface ITopicPublisher
    {
        void AddMessage(RabbitControlMessage rcm);
    }
}