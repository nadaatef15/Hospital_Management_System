using FluentValidation;
using HMSContracts.Model.Diagnose;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class Diagnosevalidator : AbstractValidator<DiagnoseModel>
    {
        public readonly HMSDBContext _dbContext;
        public Diagnosevalidator(HMSDBContext context)
        {
            _dbContext = context;

            RuleFor(a => a).
                 MustAsync(IsNameUsedBefore)
                .WithMessage(NameIsUsedBefore);
        }

        public async Task<bool> IsNameUsedBefore(DiagnoseModel model, CancellationToken cancellationToken)
        {
            var diagnoses=  await _dbContext.Diagnoses.FirstOrDefaultAsync(a => a.Name.ToLower() == model.Name.ToLower());
            return diagnoses is null || diagnoses.Id == model.Id;

        }
    }
}
