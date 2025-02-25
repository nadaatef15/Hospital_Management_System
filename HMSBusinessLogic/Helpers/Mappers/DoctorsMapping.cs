using HMSBusinessLogic.Resource;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class DoctorsMapping
    {
        public static DoctorEntity ToEntity(this DoctorModel user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.Phone,
            Age = user.Age,
            Address = user.Address,
            Gender = user.Gender == 'M' ? Gender.M : Gender.F,
            Salary = user.Salary
        };

        public static DoctorResource ToResource(this DoctorEntity user) => new()
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Phone = user.PhoneNumber!,
            Age = user.Age,
            Address = user.Address,
            Salary = user.Salary,
            Gender = user.Gender,
            Image = user.ImagePath,
            DoctorSpecialties = user.DoctorSpecialties.Select(a => a.Specialty.ToResource()).ToList(),
        };


    }
}
