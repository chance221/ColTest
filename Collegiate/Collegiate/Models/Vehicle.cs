using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collegiate.Models
{
    public class Vehicle
    {
        public string VehicleId { get; set; }

        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int MaxSeating { get; set; }
    }
}