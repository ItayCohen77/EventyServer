using System;
using System.Collections.Generic;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Yard
    {
        public int Id { get; set; }
        public bool HasPool { get; set; }
        public bool HasBbq { get; set; }
        public bool HasGras { get; set; }
        public bool HasTable { get; set; }
        public bool HasChairs { get; set; }
    }
}
