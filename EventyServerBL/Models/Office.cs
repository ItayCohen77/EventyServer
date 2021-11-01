using System;
using System.Collections.Generic;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Office
    {
        public int Id { get; set; }
        public bool HasAc { get; set; }
        public bool HasComputer { get; set; }
        public bool HasTv { get; set; }
        public bool HasWaterHeater { get; set; }
        public bool HasCoffeeMachine { get; set; }
    }
}
