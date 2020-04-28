using System;
using System.Collections.Generic;

namespace RateAd.Models
{
    public partial class Gallery
    {
        public Gallery()
        {
            Images = new HashSet<Image>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
