using System;
using System.Collections.Generic;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class User
    {
        public User()
        {
            ChatBuyers = new HashSet<Chat>();
            ChatSellers = new HashSet<Chat>();
            Orders = new HashSet<Order>();
            Places = new HashSet<Place>();
            Receipts = new HashSet<Receipt>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public string ProfileImage { get; set; }

        public virtual ICollection<Chat> ChatBuyers { get; set; }
        public virtual ICollection<Chat> ChatSellers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Place> Places { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
