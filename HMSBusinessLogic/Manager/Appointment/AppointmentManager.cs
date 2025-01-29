using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.Appointment;
using HMSContracts.Model.Appointment;
using HMSDataAccess.Repo.Appointment;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.Appointment
{
    public interface IAppointmentManager
    {
        Task CreateAppointment(AppointmentModel model);
        Task DeleteAppointment(int id);
        Task<AppointmentResource> GetAppointmentById(int id);
        Task<List<AppointmentResource>> GetAllAppointmentsForDoctor(string docId);
        Task<List<AppointmentResource>> GetAllAppointmentsForPatient(string patientId);
    }
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly IAppointmentService _appointmentUpdateService;
        private readonly IValidator<AppointmentModel> _validator;

        public AppointmentManager(IAppointmentRepo appointmentRepo,
            IAppointmentService appointmentUpdateService,
            IValidator<AppointmentModel> validator
            )
        {
            _appointmentRepo = appointmentRepo;
            _appointmentUpdateService = appointmentUpdateService;
            _validator = validator;
        }

        public async Task CreateAppointment(AppointmentModel model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var appointment = model.ToEntity();

            var appointmentsInSameDate = _appointmentRepo.AllAppointmentsForDoctorInSpecificDate(appointment);

            var docScheduleInSameDate = await _appointmentRepo.docScheduleWithSameDate(appointment);

         
                if (appointment.StartTime < docScheduleInSameDate.StartTime ||
                    appointment.EndTime > docScheduleInSameDate.EndTime)
                {
                    throw new ConflictException("The appointment time is outside the Doctor schedule");
                }

            foreach (var appoint in appointmentsInSameDate)
            {
                if (appointment.StartTime < appoint.EndTime && appointment.EndTime > appoint.StartTime)
                {
                    throw new ConflictException("The appointment time conflicts with an existing appointment");
                }
            }

            await _appointmentRepo.CreateAppointment(appointment);
        }

        public async Task DeleteAppointment(int id)
        {
            var appointment = await _appointmentRepo.GetAppointmentByIdAsNoTracking(id) ??
                  throw new NotFoundException(appointmentDoesnotExist);

            await _appointmentRepo.DeleteAppointment(appointment);
        }

        public async Task UpdateAppointment(int id, AppointmentModel model)
        {
            if (id != model.Id)
                throw new ConflictException(NotTheSameId);

            await _validator.ValidateAndThrowAsync(model);

            var appointment = await _appointmentRepo.GetAppointmentById(id) ??
                   throw new NotFoundException(appointmentDoesnotExist); 

            _appointmentUpdateService.SetValues(appointment, model);

            var appointmentsInSameDate = _appointmentRepo.AllAppointmentsForDoctorInSpecificDate(appointment);

            var docScheduleInSameDate = await _appointmentRepo.docScheduleWithSameDate(appointment);


            if (appointment.StartTime < docScheduleInSameDate.StartTime ||
                appointment.EndTime > docScheduleInSameDate.EndTime)
            {
                throw new ConflictException("The appointment time is outside the Doctor schedule");
            }

            foreach (var appoint in appointmentsInSameDate)
            {
                if (appointment.StartTime < appoint.EndTime && appointment.EndTime > appoint.StartTime)
                {
                    throw new ConflictException("The appointment time conflicts with an existing appointment");
                }
            }
            _appointmentRepo.UpdateAppointment(appointment);
        }

        public async Task<AppointmentResource> GetAppointmentById(int id)
        {
            var appointment = await _appointmentRepo.GetAppointmentByIdAsNoTracking(id) ??
                   throw new NotFoundException(appointmentDoesnotExist);

            return appointment.ToResource();
        }

        public async Task<List<AppointmentResource>> GetAllAppointmentsForDoctor(string docId) =>
          (await _appointmentRepo.GetAllAppointmentsForDoctor(docId)).Select(a => a.ToResource()).ToList();


        public async Task<List<AppointmentResource>> GetAllAppointmentsForPatient(string patientId) =>
          (await _appointmentRepo.GetAllAppointmentsForPatient(patientId)).Select(a => a.ToResource()).ToList();

    }
}
