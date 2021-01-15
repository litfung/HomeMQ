using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMQ.FirstBlazor.Models
{
    public class DisplayPerson
    {
        public int PersonId { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "First name is too long.")]
        [MinLength(5, ErrorMessage = "First name is too short.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "First name is too long.")]
        [MinLength(5, ErrorMessage = "First name is too short.")]
        public string LastName { get; set; }
        //public string EmailAddress { get; set; }
        //public Address Address { get; set; }

        public string FullInfo => $"{FirstName} {LastName}";// ({Address})";
    }
}
