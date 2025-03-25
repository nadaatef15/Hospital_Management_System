using FluentValidation;
using HMSContracts.Model.Test;
using HMSDataAccess.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class MedicalRecordTestsValidators : AbstractValidator<MedicalRecordTestsModel>
    {
        public readonly HMSDBContext _dbContext;
        public readonly IHttpContextAccessor _httpContextAccessor;

        public MedicalRecordTestsValidators(HMSDBContext dbContext , IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;

            RuleFor(a => a)
                .MustAsync(IsLabTechExist)
                .WithMessage(notLabTechnician);
        }
            
        public async Task<bool> IsLabTechExist(MedicalRecordTestsModel model, CancellationToken cancellationToken)
        {
             var labTechId= _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(a=>a.Type == ClaimTypes.NameIdentifier)?.Value;
             var labTech= await _dbContext.LabTechnicians.FirstOrDefaultAsync(a=>a.Id== labTechId);

            return labTech is not null;
        }
    }
}
