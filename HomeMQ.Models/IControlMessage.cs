using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Models
{
    public interface IControlMessage
    {
        string Command { get; set; }
        string Payload { get; set; }
    }
}
