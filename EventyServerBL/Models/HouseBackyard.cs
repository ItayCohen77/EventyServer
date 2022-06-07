using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("HouseBackyard")]
    public partial class HouseBackyard
    {
        [Key]
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public bool HasPool { get; set; }
        public bool HasBbq { get; set; }
        public bool HasHotub { get; set; }
        public bool HasTables { get; set; }
        public bool HasChairs { get; set; }
    }
}
