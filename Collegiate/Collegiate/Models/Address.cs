
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collegiate.Models
{
    public class Address
    { 
        [Required]
        public string AddressId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        
        [Required]
        public LocationType Type { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string LocationDescription { get; set; }

        public string Address2 { get; set; }
        
        
    }
}