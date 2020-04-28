using System;
using System.Collections.Generic;

namespace RateAd.Models
{
    public partial class Role
    {
        public long Id { get; set; }
        public string Role1 { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}
