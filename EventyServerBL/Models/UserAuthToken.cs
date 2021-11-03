using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Table("UserAuthToken")]
    public partial class UserAuthToken
    {
        [Column("UserID")]
        public int UserId { get; set; }
        [Key]
        [StringLength(255)]
        public string AuthToken { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserAuthTokens")]
        public virtual User User { get; set; }
    }
}
