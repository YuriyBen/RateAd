using System;
using System.Collections.Generic;

namespace RateAd.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Galleries = new HashSet<Gallery>();
            Images = new HashSet<Image>();
            Roles = new HashSet<Role>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordRecoveryToken { get; set; }
        public byte UnseccessfulAttemptsCount { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public string RegistrationToken { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
