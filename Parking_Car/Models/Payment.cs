using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Car.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string PaidBy { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
