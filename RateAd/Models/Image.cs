using System;
using System.Collections.Generic;

namespace RateAd.Models
{
    public partial class Image
    {
        public Image()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public long CategoryId { get; set; }
        public long? GalleryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Gallery Gallery { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
