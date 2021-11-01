using System;
using System.Collections.Generic;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Receipt
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Amount { get; set; }
        public int HoursBooked { get; set; }

        public virtual User Customer { get; set; }
        public virtual Order Order { get; set; }
    }
}
