using FluentValidation;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class UserValidator : AbstractValidator<UserModel> 
    {
        UserManager<UserEntity> _userManager;
        public UserValidator(UserManager<UserEntity> userManager)
        {
             _userManager = userManager;

            RuleFor(x => x)
                .MustAsync(EmailNotTakenBefore)
                .WithMessage(EmailUsedBefore);

            RuleFor(x => x)
                .MustAsync(UserNameNotTakenBefore)
                .WithMessage(UserNameUsedBefore);
        }

        public async Task<bool> EmailNotTakenBefore(UserModel userModel, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            return user is null || user.Id == userModel.Id;
        }

        public async Task<bool> UserNameNotTakenBefore(UserModel userModel, CancellationToken cancellation)
        {
            var user = await _userManager.FindByNameAsync(userModel.UserName);
            return user is null || user.Id== userModel.Id;
        }

    }
}
