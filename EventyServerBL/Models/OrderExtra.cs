using System;
using System.Collections.Generic;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class OrderExtra
    {
        public OrderExtra()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string ExtraType { get; set; }
        public int Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
