using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class OrderExtra
    {
        public OrderExtra()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string ExtraType { get; set; }
        public int Price { get; set; }

        [InverseProperty(nameof(Order.Extra))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
