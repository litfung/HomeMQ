//using HomeMQ.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeMQ.MainContext
{
    public class LocationSQL
    {
        public int LocationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string LocationName { get; set; }

        [MaxLength(50)]
        public string Room { get; set; }
        public virtual ICollection<SensorSQL> SensorSQLs { get; set; }
        //public virtual ICollection<ControlSQL> ControlSQLs { get; set; }
        //public virtual ICollection<LightSQL> LightSQLs { get; set; }

        public LocationSQL()
        {

        }

        public LocationSQL(string name) : this(name, "Not Installed") { }
        public LocationSQL(string name, string room)
        {
            LocationName = name;
            Room = room;
        }


        //public Location ToModel()
        //{
        //    return new Location(LocationName, Room);
        //}
    }
}
