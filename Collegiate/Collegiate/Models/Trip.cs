
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collegiate.Models
{
    public class Trip
    { 
        //public Trip()
        //{
        //    this.Users = new HashSet<User>();
        //}

        public string TripId { get; set; }
        
        //[ForeignKey("UserId")]
        //public User User { get; set; }
        
        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public Address Destination { get; set; }

        public Address Departure { get; set; }
        
        public bool ToNMC { get; set; }
        
        public int SeatsAvailable { get; set; }
        
        public string DriverID { get; set; }

        public string ToMeetLocationDesc { get; set; }

        public string FromMeetLocationDesc { get; set; }

        public virtual ICollection<TripUsers> TripUsers { get; set; }

        public virtual ICollection<RiderRequest> RiderRequests { get; set; }
        
        public DriverOffer DriverOffer { get; set; }
      
        public virtual ICollection<Comment> Comments { get; set; }

        public bool OnTime { get; set; }

        public string Campus { get; set; }
    }
}