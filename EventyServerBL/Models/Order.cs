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
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EventDate { get; set; }
        public int AmountOfPeople { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndTime { get; set; }
        public int TotalHours { get; set; }

        [ForeignKey(nameof(PlaceId))]
        [InverseProperty("Orders")]
        public virtual Place Place { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Orders")]
        public virtual User User { get; set; }
    }
}
