using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumers
{
    public class RabbitMQMessage
    {
        public Header Header { get; set; }
        public object Payload { get; set; }
    }

    public class Header
    {
        public string Command { get; set; }
        public string Hostname { get; set; }
    }

   
}
