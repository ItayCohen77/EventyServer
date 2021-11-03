using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class PlaceMedium
    {
        [Key]
        public int Id { get; set; }
        public int PlaceId { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceType { get; set; }
        [Required]
        [StringLength(255)]
        public string FilePath { get; set; }
        [StringLength(255)]
        public string MimeType { get; set; }

        [ForeignKey(nameof(PlaceId))]
        [InverseProperty("PlaceMedia")]
        public virtual Place Place { get; set; }
    }
}
