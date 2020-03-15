
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Collegiate.Models
{
    public class User : IdentityUser
    {
        //public User()
        //{
        //    this.Trips = new HashSet<Trip>();
        //}
        
       //Commented Out as the ID will be provided by IdentityModel
       //[Required]
        //public string Id { get; set; }

           

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public override string UserName { get; set; }

        //This is provided by identity Model
        //[Required]
        //public string SchoolEmail { get; set; }

        //Provided by identity model
        //[Required]
        //public string Password { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime UpdatedOn { get; set; } = DateTime.Now;
       
        public DateTime CreatedOn { get; set; }

        public string ContactEmail { get; set; }

        public virtual Driver Driver { get; set; }
        
        public virtual Preference Preferences { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        
        public virtual ICollection<RiderRequest> RiderRequests { get; set; } 
        
        public virtual ICollection<DriverOffer> DriverOffer { get; set; }
        
        public ICollection<TripUsers> TripUsers { get; set; }
        
    }
}