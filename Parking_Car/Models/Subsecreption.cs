using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Car.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
