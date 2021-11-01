using System;
using System.Collections.Generic;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Hall
    {
        public int Id { get; set; }
        public bool HasChairs { get; set; }
        public bool HasTable { get; set; }
        public bool HasProjector { get; set; }
        public bool HasScreen { get; set; }
    }
}
