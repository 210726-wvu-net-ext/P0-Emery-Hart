using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class User
    {
        public User()
        {
            Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }
        public string Uname { get; set; }
        public string Upass { get; set; }
        public int AccessLvl { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
