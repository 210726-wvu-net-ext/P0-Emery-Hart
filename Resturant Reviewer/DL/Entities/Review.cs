using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ResturantId { get; set; }
        public int Rating { get; set; }
        public string Thoughts { get; set; }

        public virtual Resturant Resturant { get; set; }
        public virtual User User { get; set; }
    }
}
