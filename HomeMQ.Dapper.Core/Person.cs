using System;

namespace HomeMQ.DapperCore
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string EmailAddress { get; set; }
        public Address Address { get; set; }

        public string FullInfo => $"{FirstName} {LastName}";// ({Address})";

    }
}
