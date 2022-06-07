using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("PlaceType")]
    public partial class PlaceType
    {
        public PlaceType()
        {
            Places = new HashSet<Place>();
        }

        [Key]
        [Column("PlaceTypeID")]
        public int PlaceTypeId { get; set; }
        [Required]
        [StringLength(20)]
        public string TypeName { get; set; }

        [InverseProperty(nameof(Place.PlaceTypeNavigation))]
        public virtual ICollection<Place> Places { get; set; }
    }
}
