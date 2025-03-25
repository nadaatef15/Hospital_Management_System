using FluentValidation;
using HMSContracts.Model.Test;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Validators
{
    public class TestValidator :AbstractValidator<TestModel>
    {
        public readonly HMSDBContext _dbContext;
        public TestValidator(HMSDBContext context)
        {
            _dbContext = context;

            RuleFor(a => a)
                .MustAsync(IsNameRepeated)
                .WithMessage(NameIsUsedBefore);
        }

        public async Task<bool> IsNameRepeated(TestModel model, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Tests.FirstOrDefaultAsync(a => a.Name.ToLower() == model.Name.ToLower());
            return entity is null || entity.Id == model.Id;

        }
         
    }
}
