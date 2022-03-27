using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("Apartment")]
    public partial class Apartment
    {
        [Key]
        public int Id { get; set; }
        public bool HasSpeakerAndMic { get; set; }
        public bool HasAirConditioner { get; set; }
        public bool HasTv { get; set; }
        public bool HasWaterHeater { get; set; }
        public bool HasCoffeeMachine { get; set; }
    }
}
