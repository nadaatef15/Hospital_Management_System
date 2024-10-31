using FluentValidation;
using HMSDataAccess.Entity;
using static HMSContracts.Language.Resource;
namespace HMSBusinessLogic.Validators
{

   public class ImageUploadValidator : AbstractValidator<UserEntity>
    {
        public ImageUploadValidator()
        {

            RuleFor(x => x.ImagePath)
                .NotEmpty()
                .When(x => x.ImagePath == null)
                .WithMessage(MustUpload);

            RuleFor(x => x.ImagePath)
                .Must(IsValidUrl)
                .When(x => !string.IsNullOrEmpty(x.ImagePath))
                .WithMessage(ValidUrl);
        }

        private bool IsValidUrl(string imagePath)
        {
            return Uri.IsWellFormedUriString(imagePath, UriKind.Absolute);
        }
    }

}

