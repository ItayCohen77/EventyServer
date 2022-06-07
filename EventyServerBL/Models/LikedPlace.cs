using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class LikedPlace
    {
        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [Key]
        [Column("PlaceID")]
        public int PlaceId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("LikedPlaces")]
        public virtual User User { get; set; }
    }
}
