using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.IdentityManager
{
    public interface IUserManager
    {
        Task AssignRolesToUser(string userId, List<string> rolesId);
        Task<UserResource> GetUserById(string userId);
        Task UpdateUser(string userId, ModifyUser userModel);
        Task DeleteUser(string userId);
        Task<List<UserResource>> GetAllUsers();

    }
    public class UserManager : IUserManager
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IFileService _fileService;
        public UserManager(UserManager<UserEntity> userManagerIdentity, IFileService fileService)
        {
            _userManager = userManagerIdentity;
            _fileService = fileService;
        }

        public async Task AssignRolesToUser(string userId, List<string> roleNames)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException(UseDoesnotExist);

            var currentUserRoles = await _userManager.GetRolesAsync(user);

            var rolesToAdd = roleNames.Except(currentUserRoles).ToList();
            var rolesToRemove = currentUserRoles.Except(roleNames).ToList();

            foreach (var item in rolesToAdd)
                await _userManager.AddToRoleAsync(user, item);

            foreach (var role in rolesToRemove)
                await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<UserResource> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException(UseDoesnotExist);

            return user.ToResource();
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException(UseDoesnotExist);

            await _userManager.DeleteAsync(user);
        }

        public async Task UpdateUser(string userId, ModifyUser userModified)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException(UseDoesnotExist);

            var name = await _userManager.FindByNameAsync(userModified.UserName);
            if (name is null)
            {
                user.PhoneNumber = userModified.Phone;
                user.Address = userModified.Address;
                user.Age = userModified.Age;
                user.UserName = userModified.UserName;

                if (userModified.Image is not null)
                    user.ImagePath = await _fileService.UploadImage(userModified.Image);
            }
            await _userManager.UpdateAsync(user);

        }

        public async Task<List<UserResource>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            return users.Select(x => x.ToResource()).ToList();
        }

    }
}