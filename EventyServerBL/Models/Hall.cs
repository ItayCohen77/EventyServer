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
        public int PlaceId { get; set; }
        public bool HasTables { get; set; }
        public bool HasChairs { get; set; }
        public bool HasSpeakerAndMic { get; set; }
        public bool HasProjector { get; set; }
        public bool HasBar { get; set; }
    }
}
