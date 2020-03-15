

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collegiate.Models
{
    public class Driver
    {
        public string DriverId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public double DriverRating { get; set; }

        public  ICollection<Vehicle> Vehicle { get; set; }

    }
}