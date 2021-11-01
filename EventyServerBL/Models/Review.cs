using System;
using System.Collections.Generic;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class Review
    {
        public Review()
        {
            ReviewsMedia = new HashSet<ReviewsMedium>();
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public virtual Order Order { get; set; }
        public virtual ICollection<ReviewsMedium> ReviewsMedia { get; set; }
    }
}
