using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Models
{
    public class TemperatureReading
    {
        #region Properties
        public string Identifier { get; set; }
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        #endregion

        #region Constructors
        public TemperatureReading() : this("") { }

        public TemperatureReading(string ident)
        {
            Time = DateTime.Now;
            Identifier = ident;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{base.ToString()} = {Temperature} F";
        }
        #endregion

    }
}
