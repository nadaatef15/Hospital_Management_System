using HMSContracts.Model.Appointment;
using HMSContracts.Model.Users;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.CustomValidation
{
    public class NotRepeatedAttribute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

             var model = (DoctorModel)validationContext.ObjectInstance;

            if (value is List<int> specialtyId && specialtyId.Distinct().Count() < specialtyId.Count()) 
            {
                return new ValidationResult("Invalid Repeated Id");
            }
            return ValidationResult.Success;


        }

    }
}
