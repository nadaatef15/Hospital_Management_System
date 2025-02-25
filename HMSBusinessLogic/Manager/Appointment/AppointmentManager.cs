using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.Appointment;
using HMSContracts.Model.Appointment;
using HMSDataAccess.Repo.Appointment;
using HMSDataAccess.Repo.Doctor;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.Appointment
{
    public interface IAppointmentManager
    {
        Task CreateAppointment(AppointmentModel model);
        Task DeleteAppointment(int id);
        Task UpdateAppointment(int id, AppointmentModel model);
        Task<AppointmentResource> GetAppointmentById(int id);
        Task<List<AppointmentResource>> GetAllAppointmentsForDoctor(string docId);
        Task<List<AppointmentResource>> GetAllAppointmentsForPatient(string patientId);
    }
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly IDoctorScheduleRepo _doctorScheduleRepo;
        private readonly IAppointmentService _appointmentUpdateService;
        private readonly IValidator<AppointmentModel> _validator;

        public AppointmentManager(IAppointmentRepo appointmentRepo,
            IAppointmentService appointmentUpdateService,
            IDoctorScheduleRepo doctorScheduleRepo,
            IValidator<AppointmentModel> validator
            )
        {
            _appointmentRepo = appointmentRepo;
            _appointmentUpdateService = appointmentUpdateService;
            _doctorScheduleRepo = doctorScheduleRepo;
            _validator = validator;
        }

        public async Task CreateAppointment(AppointmentModel model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var appointment = model.ToEntity();

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

            _appointmentRepo.UpdateAppointment(appointment);
        }

        public async Task<AppointmentResource> GetAppointmentById(int id)
        {
            var appointment = await _appointmentRepo.GetAppointmentByIdAsNoTracking(id) ??
                   throw new NotFoundException(appointmentDoesnotExist);

            return appointment.ToResource();
        }

        public async Task<List<AppointmentResource>> GetAllAppointmentsForDoctor(string docId) =>
          (await _appointmentRepo.GetAllAppointments(docId)).Select(a => a.ToResource()).ToList();


        public async Task<List<AppointmentResource>> GetAllAppointmentsForPatient(string patientId) =>
          (await _appointmentRepo.GetAllAppointments(patientId: patientId)).Select(a => a.ToResource()).ToList();

    }
}
