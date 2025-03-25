using FluentValidation;
using HMSContracts.Model.Specialty;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class DoctorSpecialtyValidator : AbstractValidator<DoctorSpecialtyModel>
    {
        private readonly HMSDBContext _dbContext;
        public DoctorSpecialtyValidator( HMSDBContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(a => a.DoctorId)
                .MustAsync(IsDoctorIdValid)
                .WithMessage(UseDoesnotExist);

            RuleFor(a => a.SpecialtyId)
               .MustAsync(IsSpecialtyIdValid)
               .WithMessage(SpecialityIsNotExist);
        }

        public async Task< bool> IsDoctorIdValid(string doctorId , CancellationToken cancellationToken)=>
            await _dbContext.Doctors.AnyAsync(a=>a.Id == doctorId);
            
        public async Task<bool> IsSpecialtyIdValid(int specialtyId, CancellationToken cancellationToken)=>
            await _dbContext.Specialties.AnyAsync(a=>a.Id == specialtyId);
    }
}
