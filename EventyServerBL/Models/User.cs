using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EventyServerBL.Models
{
    [Index(nameof(Email), Name = "UQ__Users__A9D1053408A58327", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            ChatBuyers = new HashSet<Chat>();
            ChatSellers = new HashSet<Chat>();
            Orders = new HashSet<Order>();
            Places = new HashSet<Place>();
            Receipts = new HashSet<Receipt>();
            UserAuthTokens = new HashSet<UserAuthToken>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Pass { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(255)]
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        [Required]
        [StringLength(255)]
        public string ProfileImage { get; set; }

        [InverseProperty(nameof(Chat.Buyer))]
        public virtual ICollection<Chat> ChatBuyers { get; set; }
        [InverseProperty(nameof(Chat.Seller))]
        public virtual ICollection<Chat> ChatSellers { get; set; }
        [InverseProperty(nameof(Order.User))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(Place.Owner))]
        public virtual ICollection<Place> Places { get; set; }
        [InverseProperty(nameof(Receipt.Customer))]
        public virtual ICollection<Receipt> Receipts { get; set; }
        [InverseProperty(nameof(UserAuthToken.User))]
        public virtual ICollection<UserAuthToken> UserAuthTokens { get; set; }
    }
}
