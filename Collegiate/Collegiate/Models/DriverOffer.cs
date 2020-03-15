
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collegiate.Models
{

    
    public class DriverOffer
    {
        public string DriverOfferId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }


        public virtual Address Destination { get; set; }

        [Required]
        public bool ToNMC { get; set; }

        public string Campus { get; set; }

        public string ToMeetLocationDesc { get; set; }

        public string FromMeetLocationDesc { get; set; }

        public int SeatsAvailable { get; set; }

        public ICollection<Comment> DriverComments { get; set; }
    }
}