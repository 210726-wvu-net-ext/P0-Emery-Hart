using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Resturant
    {
        public Resturant()
        {
            Reviews = new HashSet<Review>();
        }

        public int ResturantId { get; set; }
        public string Name { get; set; }
        public int Style { get; set; }
        public string Description { get; set; }
        public int Zip { get; set; }

        public virtual Style StyleNavigation { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
