using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Managers
{
    public class RabbitMQConfigurationModel
    {
        public string ConnectionName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Hostname { get; set; }


    }
}
