using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Receipt
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        [Column(TypeName = "date")]
        public DateTime PaymentDate { get; set; }
        public int Amount { get; set; }
        public int HoursBooked { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(User.Receipts))]
        public virtual User Customer { get; set; }
        [ForeignKey(nameof(OrderId))]
        [InverseProperty("Receipts")]
        public virtual Order Order { get; set; }
    }
}
