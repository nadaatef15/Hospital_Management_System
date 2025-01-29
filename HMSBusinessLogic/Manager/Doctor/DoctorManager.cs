using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.Identity;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Constants;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Doctor;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Manager.Doctor
{
    public interface IDoctorManager
    {
        Task<DoctorResource> RegisterDoctor(DoctorModel user);
        Task UpdateDoctor(string dctorId, DoctorModel doctorModel);
        Task<DoctorResource> GetDoctorById(string id);
        Task<List<DoctorResource>> GetAllDoctors();
    }
    public class DoctorManager : IDoctorManager
    {
        private readonly UserManager<UserEntity> _userManagerIdentity;
        private readonly IValidator<UserModel> _validator;
        private readonly IFileService _fileService;
        private readonly IUserManager _userManager;
        private readonly IDoctorRepo _doctorRepo;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DoctorManager(
            UserManager<UserEntity> userManagerIdentity,
            RoleManager<IdentityRole> roleManager,
            IValidator<UserModel> validator, IFileService fileService,
            IUserManager userManager,
            IDoctorRepo doctorRepo
            )
        {
            _userManagerIdentity = userManagerIdentity;
            _roleManager = roleManager;
            _validator = validator;
            _fileService = fileService;
            _userManager = userManager;
            _doctorRepo = doctorRepo;
        }

        public async Task<DoctorResource> RegisterDoctor(DoctorModel user)
        {
            await _validator.ValidateAndThrowAsync(user);

            if (!await _roleManager.RoleExistsAsync(SysConstants.Doctor))
                throw new NotFoundException(RoleDoctorDoesNotExist);

            var doctorEntity = user.ToEntity();

            if (user.Image is not null)
                doctorEntity.ImagePath = await _fileService.UploadImage(user.Image);

            var result = await _userManagerIdentity.CreateAsync(doctorEntity, user.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new ValidationException(errors);
            }

             await _userManagerIdentity.AddToRoleAsync(doctorEntity, SysConstants.Doctor);

            return doctorEntity.ToResource();
        }

        public async Task UpdateDoctor(string dctorId, DoctorModel doctorModel)
        {
            if (doctorModel.Id != dctorId)
                throw new ConflictException(NotTheSameId);

            await _validator.ValidateAndThrowAsync(doctorModel);

            var doctor = await _doctorRepo.GetDoctorById(dctorId) ??
                throw new NotFoundException(UseDoesnotExist);

            doctor.Salary = doctorModel.Salary;
            await _userManager.UpdateUser(doctor, doctorModel);
        }

        public async Task<DoctorResource> GetDoctorById(string id)
        {
            var doctor = await _doctorRepo.GetDoctorByIdAsNoTracking(id)??
                throw new NotFoundException(UseDoesnotExist);

            return doctor.ToResource();
        }

        public async Task<List<DoctorResource>> GetAllDoctors() =>
             (await _doctorRepo.GetAllDoctors()).Select(a => a.ToResource()).ToList();


    }
}
