using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Parking_Car.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int? TotalParking { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Subscription> Subscription { get; set; }
    }
}
