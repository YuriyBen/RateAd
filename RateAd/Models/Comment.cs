using System;
using System.Collections.Generic;

namespace RateAd.Models
{
    public partial class Comment
    {
        public long Id { get; set; }
        public string Comment1 { get; set; }
        public long ImageId { get; set; }
        public long UserId { get; set; }

        public virtual Image Image { get; set; }
        public virtual User User { get; set; }
    }
}
