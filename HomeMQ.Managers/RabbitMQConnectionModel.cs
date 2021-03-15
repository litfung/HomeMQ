using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Managers
{
    public class RabbitMQConnectionModel
    {
        public string FactoryName { get; set; }
        public string ExchangeName { get; set; }
        public RabbitMQDirection RabbitMQDirection { get; set; }
        public string ConnectionName { get; set; }
    }

    public enum RabbitMQDirection
    {
        Publisher,
        Consumer
    }

    public enum ConsumerType
    {
        MasterControl
    }
}
