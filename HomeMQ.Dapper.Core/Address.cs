using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Dapper.Core
{
    public class Address
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
