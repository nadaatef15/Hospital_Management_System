using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Services.AccountServices
{
    public interface IUserService
    {
        Task DeleteUser(string userId);
        Task<UserEntity?> GetUserById(string userId);
        Task UpdateUser(UserEntity user);
        Task<List<UserEntity>> GetAllUsers();
        Task<List<string>> GetRolesOfUser(string userId);
    }
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        public UserService(UserManager<UserEntity> userManager) =>
            _userManager = userManager;

        public async Task UpdateUser(UserEntity user)
        {
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var messages = string.Join(", \n", result.Errors);
                throw new ConflictException(messages);
            }
        }

        public async Task DeleteUser(string userId)
        {
            var user = await GetUserById(userId);

            if (user is null)
                throw new NotFoundException(UseDoesnotExist);

            await _userManager.DeleteAsync(user);
        }

        public async Task<UserEntity?> GetUserById(string userId) =>
            await _userManager.FindByIdAsync(userId);

        public async Task<List<UserEntity>> GetAllUsers() =>
            await _userManager.Users.ToListAsync();

        public async Task<List<string>> GetRolesOfUser(string userId)
        {
            var user = await GetUserById(userId);

            if (user is null)
                throw new NotFoundException(UseDoesnotExist);

            var result = await _userManager.GetRolesAsync(user);

            return result.ToList();
        }
    }
}
