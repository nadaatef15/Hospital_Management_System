using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Test;
using HMSDataAccess.Repo.Test;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.Test
{
    public interface IMedicalRecordTestsManager
    {
        Task<List<MedicalRecordTestsResource>> GetMedicalRecordTests(int MedicalRecordId);
        Task<List<MedicalRecordTestsResource>> GetMedicalRecordTestForPatient(string patientId);
        Task ExecuteMedicalRecordTest(MedicalRecordTestsModel model);
    }
    public class MedicalRecordTestsManager : IMedicalRecordTestsManager
    {
        public readonly IMedicalRecordTestsRepo _medicalRecordTestsRepo;
        public readonly IValidator<MedicalRecordTestsModel> _validator;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public MedicalRecordTestsManager(IMedicalRecordTestsRepo medicalRecordTestsRepo 
            , IValidator<MedicalRecordTestsModel> validator, IHttpContextAccessor httpContextAccessor)
        {
            _medicalRecordTestsRepo = medicalRecordTestsRepo;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
        }
        public async Task ExecuteMedicalRecordTest(MedicalRecordTestsModel model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var entity = await _medicalRecordTestsRepo.GetMedicalRecordTestByIds(model.MedicalRecordId, model.TestId) ??
                throw new NotFoundException(MedicalRecordTestNotFound);

            var labTechId = _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            entity.LabTechnicianId= labTechId;
            entity.ExecutedOn = DateTime.Now;
            entity.TestResult = model.TestResult;

            await _medicalRecordTestsRepo.SaveChangesAsync();    
        }

        public async Task<List<MedicalRecordTestsResource>> GetMedicalRecordTestForPatient(string patientId)=>
            (await _medicalRecordTestsRepo.GetMedicalRecordTestForPatient(patientId)).Select(a=>a.ToResource()).ToList();
       
        public async Task<List<MedicalRecordTestsResource>> GetMedicalRecordTests(int MedicalRecordId)=> 
            (await _medicalRecordTestsRepo.GetMedicalRecordTests(MedicalRecordId)).Select(a=>a.ToResource()).ToList();
        
    }
}
