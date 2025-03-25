using FluentValidation;
using HMSContracts.Model.DoctorSchadule;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class DoctorScheduleValidator : AbstractValidator<DoctorScheduleModel>
    {
        private readonly HMSDBContext _dbContext;
        public DoctorScheduleValidator(HMSDBContext context)
        {
            _dbContext = context;
            RuleFor(a => a)
              .MustAsync(IsADoctor)
              .WithMessage(IsNotADoctor);


            RuleFor(a => a)
              .Must(DoesTheDoctorHasScheduleInTheSameDate)
              .WithMessage(DochasScheduleInThisDate);
        }

        public async Task<bool> IsADoctor(DoctorScheduleModel model, CancellationToken cancellationToken) =>
            await _dbContext.Doctors.AnyAsync(a => a.Id == model.DoctorId);


        public bool DoesTheDoctorHasScheduleInTheSameDate(DoctorScheduleModel model) =>
               !_dbContext.DoctorSchedule
                    .Any(a => a.DoctorId == model.DoctorId &&
                                a.Date == model.Date &&
                                (
                                    (a.StartTime <= model.StartTime && model.StartTime < a.EndTime && (!model.Id.HasValue || a.Id != model.Id)) ||
                                    (a.StartTime < model.EndTime && model.EndTime < a.EndTime && (!model.Id.HasValue || a.Id != model.Id)) ||
                                    (model.StartTime < a.StartTime && a.StartTime < model.EndTime && (!model.Id.HasValue || a.Id != model.Id))
                                )
                        );

    }
}

