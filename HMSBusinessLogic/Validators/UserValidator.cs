using FluentValidation;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        UserManager<UserEntity> userManager;
        public UserValidator(UserManager<UserEntity> _userManager)
        {
            userManager = _userManager;

            RuleFor(x => x.Email)
                .MustAsync(EmailNotTakenBefore)
                .WithMessage(EmailUsedBefore);

            RuleFor(x => x.UserName)
                .MustAsync(UserNameNotTakenBefore)
                .WithMessage(UserNameUsedBefore);
        }

        public async Task<bool> EmailNotTakenBefore(string email, CancellationToken cancellation)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user is null;
        }

        public async Task<bool> UserNameNotTakenBefore(string UserName, CancellationToken cancellation)
        {
            var user = await userManager.FindByNameAsync(UserName);
            return user is null;
        }
    }
}
