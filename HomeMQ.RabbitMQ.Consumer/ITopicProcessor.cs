using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumer
{
    public interface ITopicProcessor
    {
        //string RoutingKey { get; set; }
        //string Queue { get; set; }

        void ProcessRabbitMessage(string bodyMessage);
    }
}
