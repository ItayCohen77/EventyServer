using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Order
    {
        public Order()
        {
            Receipts = new HashSet<Receipt>();
            Reviews = new HashSet<Review>();
        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDateAndTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDateAndTime { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }
        public int ExtraId { get; set; }

        [ForeignKey(nameof(ExtraId))]
        [InverseProperty(nameof(OrderExtra.Orders))]
        public virtual OrderExtra Extra { get; set; }
        [ForeignKey(nameof(PlaceId))]
        [InverseProperty("Orders")]
        public virtual Place Place { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Orders")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(Receipt.Order))]
        public virtual ICollection<Receipt> Receipts { get; set; }
        [InverseProperty(nameof(Review.Order))]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
