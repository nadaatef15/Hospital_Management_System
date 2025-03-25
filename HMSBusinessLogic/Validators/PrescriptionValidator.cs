using FluentValidation;
using HMSContracts.Model.Prescription;
using HMSDataAccess.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Validators
{
    public class PrescriptionValidator : AbstractValidator<PrescriptionModel>
    {
        public readonly HMSDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PrescriptionValidator(HMSDBContext context , IHttpContextAccessor httpContextAccessor) 
        {
              _dbContext = context;
            _httpContextAccessor = httpContextAccessor;

            RuleFor(a => a.MedicalRecordId)
                .MustAsync(IsDoctorHasTheMedicalRecord)
                .WithMessage(DoctorDorsNotHaveMedicalRecord);

            RuleFor(a => a.MedicineId)
                .MustAsync(IsMedicineExist)
                .WithMessage(Medicinenotfound);
        }

        public async Task<bool> IsDoctorHasTheMedicalRecord(int medicalRecordId , CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor?.HttpContext?.User
               .Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var medicalRecord = await _dbContext.MedicalRecord
                 .FirstOrDefaultAsync(a=>a.Id== medicalRecordId && a.DoctorId == userId);

            return medicalRecord is not null;
        }

        public async Task<bool> IsMedicineExist(int MedicineId , CancellationToken cancellationToken)=>
            await _dbContext.Medicine.AnyAsync(a=>a.Id == MedicineId);

        
        
    }
}
