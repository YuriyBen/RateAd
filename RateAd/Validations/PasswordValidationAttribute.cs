﻿using RateAd.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateAd.Validations
{
    public class PasswordValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var user = (UserForCreationDTO)validationContext.ObjectInstance;

            const int MIN_LENGTH = 5;
            const int MAX_LENGTH = 35;

            if (user.Password == null) throw new ArgumentNullException();

            if (!Enumerable.Range(MIN_LENGTH, MAX_LENGTH).Contains(user.Password.Length))
            {
                return new ValidationResult(
                      $"Password length should be in range between {MIN_LENGTH} and { MAX_LENGTH} ..",
                      new[] { nameof(UserForCreationDTO) });
            }
            if (!user.Password.Any(x => char.IsUpper(x)))
            {
                return new ValidationResult(
                      "Password should include at least 1 upper character ..",
                      new[] { nameof(UserForCreationDTO) });
            }


            if (!user.Password.Any(x => char.IsDigit(x)))
            {
                return new ValidationResult(
                      "Password should include at least 1 number character ..",
                      new[] { nameof(UserForCreationDTO) });
            }
            return ValidationResult.Success;
        }
    }
}
