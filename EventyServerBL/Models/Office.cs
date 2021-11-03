using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("Office")]
    public partial class Office
    {
        [Key]
        public int Id { get; set; }
        public bool HasAc { get; set; }
        public bool HasComputer { get; set; }
        public bool HasTv { get; set; }
        public bool HasWaterHeater { get; set; }
        public bool HasCoffeeMachine { get; set; }
    }
}
