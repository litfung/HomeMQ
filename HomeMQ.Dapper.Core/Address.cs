using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.DapperCore
{
    public class Address
    {
        #region Properties
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Override Methods
        public override string ToString()
        {
            return $"{StreetAddress}\n{City},{State} {ZipCode}";
        }
        #endregion

    }
}
