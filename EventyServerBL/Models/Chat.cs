using System;
using System.Collections.Generic;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Chat
    {
        public Chat()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }

        public virtual User Buyer { get; set; }
        public virtual User Seller { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
