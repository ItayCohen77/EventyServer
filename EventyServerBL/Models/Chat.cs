using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("Chat")]
    public partial class Chat
    {
        public Chat()
        {
            Messages = new HashSet<Message>();
        }

        [Key]
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        [InverseProperty(nameof(User.ChatBuyers))]
        public virtual User Buyer { get; set; }
        [ForeignKey(nameof(SellerId))]
        [InverseProperty(nameof(User.ChatSellers))]
        public virtual User Seller { get; set; }
        [InverseProperty(nameof(Message.Chat))]
        public virtual ICollection<Message> Messages { get; set; }
    }
}
