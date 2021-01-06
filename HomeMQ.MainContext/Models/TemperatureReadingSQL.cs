//using HomeMQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeMQ.MainContext
{
    public class TemperatureReadingSQL
    {
        public int ReadingId { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public double Temperature { get; set; }

        public int SensorId { get; set; }
        public virtual SensorSQL Sensor { get; set; }


        public TemperatureReadingSQL()
        {
            Time = DateTime.Now;
        }

        #region Methods
        #endregion


        //public TemperatureReading ToModel()
        //{
        //    var reading = new TemperatureReading
        //    {
        //        Time = Time,
        //        Temperature = Temperature,
        //        Identifier = Sensor.Identifier
        //    };
        //    return reading;
        //}



    }
}
