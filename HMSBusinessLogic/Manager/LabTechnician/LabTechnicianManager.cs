using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.Identity;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Constants;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.LabTech;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Manager.LabTechnician
{
    public interface ILabTechniciansManager
    {
        Task<UserResource> RegisterLabTech(labTechnicianModel user);
        Task UpdateLabTech(string id, labTechnicianModel labTechModel);
        Task<UserResource> GetLabTechById(string id);
        Task<List<UserResource>> GetAllLabTechs();

    }
    public class LabTechnicianManager : ILabTechniciansManager
    {
        private readonly UserManager<UserEntity> _userManagerIdentity;
        private readonly IValidator<UserModel> _validator;
        private readonly IFileService _fileService;
        private readonly IUserManager _userManager;
        private readonly ILabTechRepo _labTechRepo;
        private readonly RoleManager<IdentityRole> _roleManager;
        public LabTechnicianManager(UserManager<UserEntity> userManagerIdentity,
            IValidator<UserModel> validator, IFileService fileService,
            IUserManager userManager,
            ILabTechRepo labTechRepo,
            RoleManager<IdentityRole> roleManager)
        {
            _userManagerIdentity = userManagerIdentity;
            _validator = validator;
            _fileService = fileService;
            _userManager = userManager;
            _labTechRepo = labTechRepo;
            _roleManager = roleManager;

        }

        public async Task<UserResource> RegisterLabTech(labTechnicianModel user)
        {
            await _validator.ValidateAndThrowAsync(user);

            if (!await _roleManager.RoleExistsAsync(SysConstants.LabTechnician))
                throw new NotFoundException(RoleLabTechDoesNotExist);

            var labTechnicialEntity = user.ToEntity();

            if (user.Image is not null)
                labTechnicialEntity.ImagePath = await _fileService.UploadImage(user.Image);

            var result = await _userManagerIdentity.CreateAsync(labTechnicialEntity, user.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new ValidationException(errors);
            }

            await _userManagerIdentity.AddToRoleAsync(labTechnicialEntity, SysConstants.LabTechnician);

            return labTechnicialEntity.ToResource();

        }

        public async Task UpdateLabTech(string id, labTechnicianModel labTechModel)
        {
            await _validator.ValidateAndThrowAsync(labTechModel);

            if (labTechModel.Id != id)
                throw new ConflictException(NotTheSameId);

            var user = await _userManagerIdentity.FindByIdAsync(id) ??
                 throw new NotFoundException(UseDoesnotExist);

            await _userManager.UpdateUser(user, labTechModel);
        }

        public async Task<UserResource> GetLabTechById(string id)
        {
            var labTech = await _labTechRepo.GetLabTechByIdAsNoTracking(id) ??
                 throw new NotFoundException(UseDoesnotExist);

            return labTech.ToResource();
        }

        public async Task<List<UserResource>> GetAllLabTechs() =>
            (await _labTechRepo.GetAllLabTechnicians()).Select(a => a.ToResource()).ToList();



    }
}
