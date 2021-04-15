using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumers
{
    public class ConsumerSettings
    {
        #region Fields
        const ushort PREFETCH_DEFAULT = 1;
        const bool DURABLE_DEFAULT = false;
        const bool AUTOACK_DEFAULT = false;
        const bool AUTODELETE_DEFAULT = false;
        #endregion

        #region Properties
        public ushort Prefetch { get; set; } = PREFETCH_DEFAULT;
        public bool Durable { get; set; } = DURABLE_DEFAULT;
        public bool AutoAck { get; set; } = AUTOACK_DEFAULT;
        public bool AutoDelete { get; set; } = AUTODELETE_DEFAULT;
        #endregion
    }
}
