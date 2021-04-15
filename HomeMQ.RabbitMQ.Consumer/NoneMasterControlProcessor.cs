using HomeMQ.Models;
using HomeMQ.RabbitMQ.Consumers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumers
{
    public class NoneMasterControlProcessor : IMasterControlProcessor
    {
        public void Process(ControlResponse data)
        {
            Debug.WriteLine("None Master Control Processor, process");
        }
    }
}
