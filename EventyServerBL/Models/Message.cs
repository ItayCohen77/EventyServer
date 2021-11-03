using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("Message")]
    public partial class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string MessageText { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WhenSent { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }

        [ForeignKey(nameof(ChatId))]
        [InverseProperty("Messages")]
        public virtual Chat Chat { get; set; }
    }
}
