using System;
using System.Collections.Generic;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Place
    {
        public Place()
        {
            Orders = new HashSet<Order>();
            PlaceMedia = new HashSet<PlaceMedium>();
        }

        public int Id { get; set; }
        public int PlaceType { get; set; }
        public int TotalOccupancy { get; set; }
        public string Summary { get; set; }
        public string PlaceAddress { get; set; }
        public int Price { get; set; }
        public DateTime PublishedAt { get; set; }
        public int OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User Owner { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PlaceMedium> PlaceMedia { get; set; }
    }
}
