using FluentValidation;
using HMSContracts.Model.Appointment;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Validators
{
    public class AppointmentValidator : AbstractValidator<AppointmentModel>
    {
        private readonly HMSDBContext _dbcontext;
        public AppointmentValidator(HMSDBContext dbcontext)
        {
            _dbcontext= dbcontext;

            RuleFor(x => x)
              .MustAsync(IsDoctorExist)
              .WithMessage(UseDoesnotExist);

            RuleFor(x => x)
                .MustAsync(IsPatientExist)
                .WithMessage(UseDoesnotExist);

            RuleFor(x => x)
                .MustAsync(IsDoctorHasScheduleInTheDateOfAppointment)
                .WithMessage("Doctor Does Not have Schedule this Day");
        }


        public async Task<bool> IsDoctorExist(AppointmentModel model, CancellationToken cancellation)
        {
            var doctor = await _dbcontext.Doctors.FirstOrDefaultAsync(a => a.Id == model.DoctorId);
           if( doctor is null) 
                return false;
            return true;
        }

        public async Task<bool> IsPatientExist(AppointmentModel model, CancellationToken cancellation)
        {
            var patient = await _dbcontext.Patients.FirstOrDefaultAsync(a => a.Id == model.PatientId);
            if( patient is null)
                return false;
            return true;
        }

        public async Task<bool> IsDoctorHasScheduleInTheDateOfAppointment(AppointmentModel model, CancellationToken cancellation)
        {
            var docScheduleWithSameDate = await _dbcontext.DoctorSchedule.Where(a => a.Date == model.Date)
                      .FirstOrDefaultAsync(a => a.DoctorId == model.DoctorId);

            if (docScheduleWithSameDate is null)
                return false;

            return true;
        }


    }
}
