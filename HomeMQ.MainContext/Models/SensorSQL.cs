//using HomeMQ.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeMQ.MainContext
{
    public class SensorSQL
    {
        public int SensorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Identifier { get; set; }
        //public ICollection<string> Type { get; set; }
        //public ICollection<string> IPAddress { get; set; }
        //public ICollection<string> MACAddress { get; set; }

        public int LocationId { get; set; }
        public virtual LocationSQL Location { get; set; }

        public virtual ICollection<TemperatureReadingSQL> TemperatureReadingSQLs { get; set; }

        //public Sensor ToModel()
        //{
        //    var sensor = new Sensor
        //    {

        //    };
        //    return sensor;
        //}
    }
}
