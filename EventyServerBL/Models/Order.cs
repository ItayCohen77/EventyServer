using System;
using System.Collections.Generic;

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

        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public DateTime StartDateAndTime { get; set; }
        public DateTime EndDateAndTime { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ExtraId { get; set; }

        public virtual OrderExtra Extra { get; set; }
        public virtual Place Place { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
