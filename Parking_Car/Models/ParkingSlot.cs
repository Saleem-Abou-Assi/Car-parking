using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Car.Models
{
    public class ParkingSlot
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public SlotStatus Status { get; set; }
        public ParkingRecord ParkingRecord { get; set; }
    }
}
