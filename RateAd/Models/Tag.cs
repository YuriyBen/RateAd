using System;
using System.Collections.Generic;

namespace RateAd.Models
{
    public partial class Tag
    {
        public long Id { get; set; }
        public string Tag1 { get; set; }
        public long ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}
