using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Car.Models
{
    public class ParkingRecord
    {
        [Key]
        public int Id { get; set; }
        public int SlotId { get; set; }//fk
        public int VehicleId { get; set; } //fk
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string Parcode { get; set; }
        public Vehicle Vehicle { get; set; }
        public ParkingSlot ParkingSlot { get; set; }
    }
}
