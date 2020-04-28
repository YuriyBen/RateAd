using RateAd.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateAd.DTO
{
    [PasswordValidation]
    [UserNameValidation]
    public class UserForCreationDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
