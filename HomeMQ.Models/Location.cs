using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Models
{
    public class Location
    {

        #region Properties
        public string Name { get; set; }
        public string Room { get; set; }
        #endregion
        public Location(string loc, string room)
        {
            Name = loc;
            Room = room;
        }


    }
}
