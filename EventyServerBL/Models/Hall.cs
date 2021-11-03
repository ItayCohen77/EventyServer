using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("Hall")]
    public partial class Hall
    {
        [Key]
        public int Id { get; set; }
        public bool HasChairs { get; set; }
        public bool HasTable { get; set; }
        public bool HasProjector { get; set; }
        public bool HasScreen { get; set; }
    }
}
