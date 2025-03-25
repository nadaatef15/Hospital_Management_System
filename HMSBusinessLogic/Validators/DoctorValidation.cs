using FluentValidation;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class DoctorValidation : AbstractValidator<DoctorModel>
    {
        private readonly HMSDBContext _dbContext;
        IValidator<UserModel> _userValidator;
        public DoctorValidation(HMSDBContext context , IValidator<UserModel> userValidator)
        {
            _dbContext = context;
            _userValidator = userValidator;

            RuleFor(a => a).SetValidator(_userValidator);

            RuleForEach(a => a.DoctorSpecialtiesIds)
                .NotEmpty().WithMessage("Specialty can not be empty")
                .MustAsync(IsSpecialtyIdExist)
                .WithMessage(SpecialityIsNotExist);

        }

        public async Task<bool> IsSpecialtyIdExist(int doctorSpecialtiesId , CancellationToken cancellationToken)=>
              await _dbContext.Specialties.AnyAsync(a=>a.Id==doctorSpecialtiesId);
        
    }
}
