using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Style
    {
        public Style()
        {
            Resturants = new HashSet<Resturant>();
        }

        public int StyleId { get; set; }
        public string Style1 { get; set; }

        public virtual ICollection<Resturant> Resturants { get; set; }
    }
}
