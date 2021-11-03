using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("Yard")]
    public partial class Yard
    {
        [Key]
        public int Id { get; set; }
        public bool HasPool { get; set; }
        public bool HasBbq { get; set; }
        public bool HasGras { get; set; }
        public bool HasTable { get; set; }
        public bool HasChairs { get; set; }
    }
}
