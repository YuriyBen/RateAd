using RateAd.Models;

namespace RateAd.Controllers
{
    public class UserWithToken:User
    {
        //public string AccessToken { get; set; }
        //public string RefreshToken { get; set; }
        public string Token { get; set; }

        public UserWithToken(User user)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.PasswordHash = user.PasswordHash;
            this.PasswordSalt = user.PasswordSalt;
            this.PasswordRecoveryToken = user.PasswordRecoveryToken;
            this.UnseccessfulAttemptsCount = user.UnseccessfulAttemptsCount;
            this.IsBlocked = user.IsBlocked;
            this.IsDeleted = user.IsDeleted;
            this.RegistrationToken = user.RegistrationToken;
        }
    }
}