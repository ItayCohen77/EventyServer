using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("Place")]
    public partial class Place
    {
        public Place()
        {
            Orders = new HashSet<Order>();
            PlaceMedia = new HashSet<PlaceMedium>();
        }

        [Key]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int PlaceType { get; set; }
        public int TotalOccupancy { get; set; }
        [Required]
        [StringLength(255)]
        public string Summary { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceAddress { get; set; }
        public string Apartment { get; set; }
        [Required]
        [StringLength(255)]
        public string City { get; set; }
        public string Zip { get; set; }
        [Required]
        [StringLength(255)]
        public string Country { get; set; }
        public int Price { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceImage1 { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceImage2 { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceImage3 { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceImage4 { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceImage5 { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceImage6 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PublishedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(OwnerId))]
        [InverseProperty(nameof(User.Places))]
        public virtual User Owner { get; set; }
        [InverseProperty(nameof(Order.Place))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(PlaceMedium.Place))]
        public virtual ICollection<PlaceMedium> PlaceMedia { get; set; }
    }
}
