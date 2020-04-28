using System;
using System.Collections.Generic;

namespace RateAd.Models
{
    public partial class Category
    {
        public Category()
        {
            Images = new HashSet<Image>();
        }

        public long Id { get; set; }
        public string Category1 { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
