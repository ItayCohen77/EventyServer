using System;
using System.Collections.Generic;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public DateTime WhenSent { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }

        public virtual Chat Chat { get; set; }
    }
}
