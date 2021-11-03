using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Review
    {
        public Review()
        {
            ReviewsMedia = new HashSet<ReviewsMedium>();
        }

        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        [Required]
        [StringLength(255)]
        public string Comment { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("Reviews")]
        public virtual Order Order { get; set; }
        [InverseProperty(nameof(ReviewsMedium.Review))]
        public virtual ICollection<ReviewsMedium> ReviewsMedia { get; set; }
    }
}
