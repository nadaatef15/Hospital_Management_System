using FluentValidation;
using HMSContracts.Model.Specialty;
using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class DoctorSpecialtyValidation : AbstractValidator<DoctorSpecialtyModel>
    {
        private readonly HMSDBContext _dbContext;
        public DoctorSpecialtyValidation( HMSDBContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(a => a)
                .MustAsync(IsDoctorIdvalid)
                .WithMessage(UseDoesnotExist);

            RuleFor(a => a)
               .MustAsync(IsSpecialtyIdvalid)
               .WithMessage(SpecialityIsNotExist);


        }

        public async Task< bool> IsDoctorIdvalid(DoctorSpecialtyModel doctorSpecialty , CancellationToken cancellationToken)
        {
            var doctor =await _dbContext.Doctors.FirstOrDefaultAsync(a=>a.Id == doctorSpecialty.DoctorId);
            return doctor is not null;
        }

        public async Task<bool> IsSpecialtyIdvalid(DoctorSpecialtyModel doctorSpecialty, CancellationToken cancellationToken)
        {
            var specialty = await _dbContext.Specialties.FirstOrDefaultAsync(a=>a.Id == doctorSpecialty.SpecialtyId);
            return specialty is not null;
        }

    }
}
