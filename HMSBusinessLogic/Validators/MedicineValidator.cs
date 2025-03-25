using FluentValidation;
using HMSContracts.Model.Medicine;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Validators
{
    public class MedicineValidator : AbstractValidator<MedicineModel>
    {
        public readonly HMSDBContext _dbContext;
        public MedicineValidator(HMSDBContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(a => a).
                MustAsync(IsNameExist).
                WithMessage(Medicinenotfound);
        }

        public async Task<bool> IsNameExist(MedicineModel model , CancellationToken cancellationToken)
        {
            var medicine= await _dbContext.Medicine.FirstOrDefaultAsync(a => a.Name == model.Name);
            return medicine is null || medicine.Id == model.Id ;

        }       
    }
}
