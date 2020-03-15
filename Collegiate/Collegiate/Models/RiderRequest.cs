
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collegiate.Models
{
   
    public class RiderRequest
    {
        public string RiderRequestId { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public DateTime ReturnTime { get; set;  }

        public Address DestinationAddress { get; set; }

        public Address ReturnAddress { get; set; }

        public bool ToNMC { get; set; }

        public string RiderComments { get; set; }

        public string Campus { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}