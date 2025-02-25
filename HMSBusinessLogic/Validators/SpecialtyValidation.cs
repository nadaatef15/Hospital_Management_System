using FluentValidation;
using HMSContracts.Model.Specialty;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Validators
{
    public class SpecialtyValidation : AbstractValidator<SpecialtyModel>
    {
        private readonly HMSDBContext _dbContext;
        public SpecialtyValidation(HMSDBContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name)
                .MustAsync(NameOfSpecialtyIsTaken)
                .WithMessage(SpecialityIsExist);

            RuleFor(x => x.Name)
                .Must(ValidNameOfSpecialty)
                .WithMessage(NameIsNotCorrect);
        }

        public bool ValidNameOfSpecialty(string specialtyName) => 
            !(string.IsNullOrWhiteSpace(specialtyName) || specialtyName.Any(ch => !char.IsLetterOrDigit(ch)));

        public async Task<bool> NameOfSpecialtyIsTaken(string specialtyName, CancellationToken cancellationToken) =>
             await _dbContext.Specialties.AnyAsync(a => a.Name == specialtyName);

    }
}
