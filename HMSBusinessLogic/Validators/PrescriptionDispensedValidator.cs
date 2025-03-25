using FluentValidation;
using HMSContracts.Model.Prescription;
using HMSDataAccess.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class PrescriptionDispensedValidator : AbstractValidator<PrescriptionDispensedModel>
    {
        public readonly HMSDBContext _dbContext;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public PrescriptionDispensedValidator(HMSDBContext context , IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;

            RuleFor(a => a.Id)
                .MustAsync(IsPrescriptionExist)
                .WithMessage(PrescriptionNotFound);

            RuleFor(a => a.Id)
                .MustAsync(IsPrescriptionDispensed)
                .WithMessage(PrescriptionIsDispensed);


            RuleFor(a => a)
                .MustAsync(IsPharmacistExist)
                .WithMessage(pharmDoesnotExist);
        }

        public async Task<bool> IsPrescriptionExist(int id , CancellationToken cancellationToken)=>
            await _dbContext.Prescriptions.AnyAsync(a=>a.Id == id);

        public async Task<bool> IsPrescriptionDispensed(int id, CancellationToken cancellationToken)=>
              await _dbContext.Prescriptions.AnyAsync(a=>a.Id == id && a.DispinsedById == null);
        
        public async Task<bool> IsPharmacistExist(PrescriptionDispensedModel model, CancellationToken cancellationToken)
        {
            var PharmacistId = _httpContextAccessor?.HttpContext?.User
                 .Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var pharmacist = await _dbContext.Pharmacists.FindAsync(PharmacistId);

            return pharmacist is not null; 
        }



    }

}
