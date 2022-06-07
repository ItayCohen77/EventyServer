using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("PrivateHouse")]
    public partial class PrivateHouse
    {
        [Key]
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public bool HasSpeakerAndMic { get; set; }
        public bool HasAirConditioner { get; set; }
        public bool HasTv { get; set; }
        public bool HasWaterHeater { get; set; }
        public bool HasCoffeeMachine { get; set; }
    }
}
