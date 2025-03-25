using FluentValidation;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Validators
{
    public class MedicalRecordValidator : AbstractValidator<MedicalRecordModel>
    {
        private readonly HMSDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MedicalRecordValidator(HMSDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;

          
            RuleFor(x => x.AppointmentId)
               .MustAsync(AppointmentExist)
               .WithMessage(appointmentDoesnotExist);

            RuleFor(x => x.Diagnoses)
               .MustAsync(IsDiagnosesExist)
               .WithMessage(DiagnosesNotFound);


            RuleFor(x => x.Tests)
               .MustAsync(IsTestsExist)
               .WithMessage(TestNotFound);

        }

        public async Task<bool> AppointmentExist(int appointmentId, CancellationToken cancellation)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.Claims
                 .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return await _dbContext.Appointments.AnyAsync(a => a.Id == appointmentId && a.DoctorId == userId); 
        }

        public async Task<bool> IsDiagnosesExist(List<int> dignosesIds, CancellationToken cancellation)
        {
            var exist= await _dbContext.Diagnoses
                   .Where(a=> dignosesIds.Contains(a.Id))
                   .Select(a=>a.Id)
                   .CountAsync();

            return dignosesIds.Count == exist;
        }
          
        public async Task<bool> IsTestsExist(List<int> testsIds, CancellationToken cancellationToken)
        {
            var exist= await _dbContext.Tests
                .Where(a=> testsIds.Contains(a.Id))
                .Select(a=>a.Id)
                .CountAsync();

            return testsIds.Count == exist;
        }
    }
}
