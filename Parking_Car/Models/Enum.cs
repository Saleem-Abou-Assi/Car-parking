using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Car.Models
{
    
    public enum VehicleType
    {
        سيارة,
        متور,
        متوركهربا
        // Add other types as needed
    }

    public enum SlotStatus
    {
        محجوز,
        متاح,
        // Add other statuses as needed
    }
    public class DriverName
    {
        public string FullName { get; set; }
    }
    public class ParkingRecordData
    {
        public string VehicleId { get; set; }
        public string SlotId { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string Parcode { get; set; }
    }
}
