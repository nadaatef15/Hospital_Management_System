using FluentValidation;
using HMSContracts.Model.Appointment;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class AppointmentValidator : AbstractValidator<AppointmentModel>
    {
        private readonly HMSDBContext _dbContext;
        public AppointmentValidator(HMSDBContext dbContext)
        {
            _dbContext= dbContext;

            RuleFor(x => x)
              .MustAsync(IsDoctorExist)
              .WithMessage(UseDoesnotExist);

            RuleFor(x => x)
                .MustAsync(IsPatientExist)
                .WithMessage(UseDoesnotExist);

            RuleFor(x => x)
                .MustAsync(IsAppointmentTimeAvailable)
                .WithMessage(AnotherAppointmentInTime);


            RuleFor(x => x)
                .MustAsync(IsAppointmentTimeWithInSchedule)
                .WithMessage(AppointmentTimeNotInSchedue);
        }

        public async Task<bool> IsDoctorExist(AppointmentModel model, CancellationToken cancellation)=>
             await _dbContext.Doctors.AnyAsync(a => a.Id == model.DoctorId);   

        public async Task<bool> IsPatientExist(AppointmentModel model, CancellationToken cancellation)=>
             await _dbContext.Patients.AnyAsync(a => a.Id == model.PatientId);

        public async Task<bool> IsAppointmentTimeAvailable(AppointmentModel model, CancellationToken cancellation)
        {
            return !await _dbContext.Appointments
                .AnyAsync(a => a.Date == model.Date &&
                               a.DoctorId == model.DoctorId &&
                               a.Id != model.Id && 
                               (
                                   (a.StartTime <= model.StartTime && model.StartTime < a.EndTime) ||
                                   (a.StartTime < model.EndTime && model.EndTime <= a.EndTime) 
                               ));
        }

        public async Task<bool> IsAppointmentTimeWithInSchedule(AppointmentModel model, CancellationToken cancellationToken)
        {
            return await _dbContext.DoctorSchedule
                .AnyAsync(a => a.DoctorId == model.DoctorId &&
                               a.Date == model.Date &&
                                   (a.StartTime <= model.StartTime && model.StartTime < a.EndTime) &&
                                   (a.StartTime < model.EndTime && model.EndTime <= a.EndTime));
        }
    }
}
