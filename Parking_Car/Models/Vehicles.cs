using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Car.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public VehicleType Type { get; set; }
        public int DriverId { get; set; }
        public string Description { get; set; }
        public Driver Driver { get; set; }
        public ParkingRecord ParkingRecord { get; set; }
        public Subscription Subscription { get; set; }
    }
}
