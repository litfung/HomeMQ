using RabbitMQ.Control.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HomeMQ.RabbitMQ.Publishers
{
    public class NonePiControlPublisher : IPiControlPublisher
    {
        public void AddMessage(RabbitControlMessage rcm)
        {
            Debug.WriteLine("None publisher add message");
        }
    }
}
