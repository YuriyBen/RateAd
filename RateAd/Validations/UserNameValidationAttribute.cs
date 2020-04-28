using Microsoft.EntityFrameworkCore;
using RateAd.DTO;
using RateAd.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateAd.Validations
{
    public class UserNameValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var user = (UserForCreationDTO)validationContext.ObjectInstance;

            const int MIN_LENGTH = 3;
            const int MAX_LENGTH = 20;

            if (user.UserName == null) throw new ArgumentNullException();

            if (!Enumerable.Range(MIN_LENGTH, MAX_LENGTH).Contains(user.UserName.Length))
            {
                return new ValidationResult(
                      $"Username length should be in range between {MIN_LENGTH} and { MAX_LENGTH} ..",
                      new[] { nameof(UserForCreationDTO) });
            }

            if (user.UserName.Contains('&') || user.UserName.Contains('=') || user.UserName.Contains('<')
             || user.UserName.Contains('>') || user.UserName.Contains('+') || user.UserName.Contains(','))
            {
                return new ValidationResult(
                      "Username can't contain an ampersand (&), equal sign (=), brackets (<,>),  " +
                      "plus sign (+), comma (,) ..",
                      new[] { nameof(UserForCreationDTO) });
            }

            return ValidationResult.Success;
        }
    }
}
