using HomeMQ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumers
{
    public interface IMasterControlProcessor : IRabbitProcessor
    {
        void Process(ControlResponse data);
    }
}
