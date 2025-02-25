using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.Identity;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.GeneralServices;
using HMSBusinessLogic.Services.PatientService;
using HMSContracts.Constants;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Patient;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;
using static HMSContracts.Constants.SysConstants;


namespace HMSBusinessLogic.Manager.Patient
{
    public interface IPatientsManager
    {
        Task<PatientResource> RegisterPatient(PatientModel patient);
        Task UpdatePatient(string id, PatientModel patientModel);
        Task<List<PatientResource>> GetAllPatients();
        Task<PatientResource> GetPatientById(string id);
        Task DeletePatient(string id);
    }
    public class PatientsManager : IPatientsManager
    {
        private readonly UserManager<UserEntity> _userManagerIdentity;
        private readonly IValidator<UserModel> _validator;
        private readonly IFileService _fileService;
        private readonly IUserManager _userManager;
        private readonly IPatientRepo _patientRepo;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPatientService _patientEntityService;
        public PatientsManager(UserManager<UserEntity> userManagerIdentity,
            IValidator<UserModel> validator, IFileService fileService,
            IUserManager userManager, IPatientRepo patientRepo,
            RoleManager<IdentityRole> roleManager ,
            IPatientService patientEntityService)
        {
            _userManagerIdentity = userManagerIdentity;
            _validator = validator;
            _fileService = fileService;
            _userManager = userManager;
            _patientRepo = patientRepo;
            _roleManager = roleManager;
            _patientEntityService = patientEntityService;
        }

        public async Task<PatientResource> RegisterPatient(PatientModel patient)
        {
            await _validator.ValidateAndThrowAsync(patient);

            if (!await _roleManager.RoleExistsAsync(SysConstants.Patient))
                throw new NotFoundException(RolePatientDoesNotExist);

            var PatientEntity = patient.ToEntity();

            if (patient.Image is not null)
                PatientEntity.ImagePath = await _fileService.UploadImage(patient.Image);
            
            var result = await _userManagerIdentity.CreateAsync(PatientEntity, patient.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new ValidationException(errors);
            }

            await _userManagerIdentity.AddToRoleAsync(PatientEntity, SysConstants.Patient);

            return PatientEntity.ToPatientResource();
        }

        public async Task UpdatePatient(string id, PatientModel patientModel)
        {
            if (patientModel.Id != id)
                throw new ConflictException(NotTheSameId);

            await _validator.ValidateAndThrowAsync(patientModel);

            var patient = await _patientRepo.GetPatientById(id) ??
                 throw new NotFoundException(UseDoesnotExist);

            _patientEntityService.SetValues(patient, patientModel);

            if (patientModel.Image is not null)
                   patient.ImagePath = await _fileService.UploadImage(patientModel.Image);

              _patientRepo.UpdatePatient(patient);

        }

        public async Task DeletePatient(string id)
        {
            var patient = await _patientRepo.GetPatientById(id) ??
                  throw new NotFoundException(UseDoesnotExist);

            var result = await _userManagerIdentity.IsInRoleAsync(patient, SysConstants.Patient);
            
            if (!result)
                throw new NotFoundException(UseDoesnotExist);

            await _userManager.DeleteUser(id);
        }

        
        public async Task<PatientResource> GetPatientById(string id)
        {
            var patient = await _patientRepo.GetPatientById(id) ??
                throw new NotFoundException(UseDoesnotExist);

            return patient.ToPatientResource();
        }

        public async Task<List<PatientResource>> GetAllPatients() =>
              (await _patientRepo.GetAllPatients()).Select(a => a.ToPatientResource()).ToList();
    }
}
