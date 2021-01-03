using BaseClasses.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Models
{
    public class ControlMessage
    {
        #region Properties
        public string Command { get; set; }
        public object Payload { get; set; }
        #endregion


        #region Constructors

        #endregion
        public ControlMessage(object o)
        {
            Command = o.GetType().Name.ToSnakeCase();
            Payload = o;
        }
    }
}
