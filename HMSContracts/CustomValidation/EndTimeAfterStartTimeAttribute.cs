
using HMSContracts.Model.DoctorSchadule;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.CustomValidation
{
    public class EndTimeAfterStartTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is ITimeRange timeRange)
            {
                if (timeRange.EndTime <= timeRange.StartTime)
                {
                    return new ValidationResult("EndTime must be after StartTime.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
