using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class ReviewsMedium
    {
        [Key]
        public int Id { get; set; }
        public int ReviewId { get; set; }
        [Required]
        [StringLength(255)]
        public string FilePath { get; set; }
        [StringLength(255)]
        public string MimeType { get; set; }

        [ForeignKey(nameof(ReviewId))]
        [InverseProperty("ReviewsMedia")]
        public virtual Review Review { get; set; }
    }
}
