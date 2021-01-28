using HomeMQ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumer
{
    public interface IMasterControlProcessor
    {
        void Process(ControlResponse data);
    }
}
