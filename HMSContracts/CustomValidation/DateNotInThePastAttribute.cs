using System;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.CustomValidation
{
    public class DateNotInThePastAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateOnly date)
            {
                if (date < DateOnly.FromDateTime(DateTime.Now))
                    return new ValidationResult("The date must be today or a future date.");  
            }
            else
                return new ValidationResult("Invalid date format.");
           
            return ValidationResult.Success;
        }
    }
}
