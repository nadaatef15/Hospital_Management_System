using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;
namespace HMSBusinessLogic.Services.AccountServices
{
    public interface IAccountService
    {
        Task<UserEntity> Login(string username, string password);
    }
    public class AccountService : IAccountService
    {
        UserManager<UserEntity> userManager;

        public AccountService(UserManager<UserEntity> _userManager)
        {
            userManager = _userManager;
        }
        public async Task<UserEntity> Login(string Email, string password)
        {
            var user = await userManager.FindByEmailAsync(Email);

            if (user is null)
                throw new ConflictException(EmailNotFound);

            var result = await userManager.CheckPasswordAsync(user, password);
            if (!result)
                throw new ConflictException(WrongPassword);

            return user;
        }

    }
}
