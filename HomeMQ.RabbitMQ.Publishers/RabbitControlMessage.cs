using HomeMQ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.RabbitMQ.Publishers
{
    public class RabbitControlMessage
    {
        public ControlMessage ControlMessage { get; set; }
        public string RoutingKey { get; set; }

        public RabbitControlMessage(ControlMessage cm, string rk)
        {
            ControlMessage = cm;
            RoutingKey = rk;
        }
    }
}
