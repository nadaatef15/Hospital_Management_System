using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.DoctorSchadule;
using HMSDataAccess.Repo.Appointment;
using HMSDataAccess.Repo.Doctor;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.Doctor
{
    public interface IDoctorScheduleManager
    {
        Task CreateDoctorSchedule(DoctorScheduleModel model);
        Task<List<DoctorScheduleResource>> GetDoctorSchedulesForDoctor(string docId , DateOnly? dateFrom , DateOnly? dateTo);
        Task UpdateDoctorSchedule(int id, DoctorScheduleModel model);
        Task DeleteDoctorSchedule(int id);
    }
    public class DoctorScheduleManager : IDoctorScheduleManager
    {
        private readonly IDoctorScheduleRepo _doctorScheduleRepo;
        private readonly IValidator<DoctorScheduleModel> _validator;
        private readonly IAppointmentRepo _appointmentRepo;

        public DoctorScheduleManager(IDoctorScheduleRepo doctorScheduleRepo , 
            IAppointmentRepo appointmentRepo,
            IValidator<DoctorScheduleModel> validator
            )
        {
            _doctorScheduleRepo = doctorScheduleRepo;
            _validator = validator;
            _appointmentRepo = appointmentRepo;
        }

        public async Task CreateDoctorSchedule(DoctorScheduleModel model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var docSchedule= model.ToEntity();

            await _doctorScheduleRepo.CreateSchedule(docSchedule);
        }

        public async Task<List<DoctorScheduleResource>> GetDoctorSchedulesForDoctor
            (string docId  , DateOnly? dateFrom, DateOnly? dateTo) 
        {
            var doctor = await _doctorScheduleRepo.GetDoctorById(docId) ??
                throw new NotFoundException(UseDoesnotExist);

            var doctorScheduleEntities= await _doctorScheduleRepo.GetDoctorSchedules(docId , dateFrom , dateTo);

            return  doctorScheduleEntities.Select(a=>a.ToResource()).ToList();
        }

        public async Task UpdateDoctorSchedule(int id, DoctorScheduleModel model)
        {
            if (id != model.Id)
                throw new ConflictException(NotTheSameId); 

            await _validator.ValidateAndThrowAsync(model);

            var doctorSchedule= await _doctorScheduleRepo.GetDoctorScheduleByScheduleId(id)??
                     throw new NotFoundException(ScheduleIsNotExist);

            var appointments = _appointmentRepo.GetDoctorScheduleAppointments(doctorSchedule);

            if (appointments.Any(a => (a.StartTime < model.StartTime || model.EndTime < a.StartTime) ||
                             (a.EndTime < model.StartTime || model.EndTime < a.EndTime)))
                    throw new ConflictException("The schedule time conflicts with an existing appointment");

            var docSchedule = model.ToEntity();

           await _doctorScheduleRepo.UpdateDoctorSchedule(docSchedule);
        }

        public async Task DeleteDoctorSchedule(int id) 
        {
            var docSchedule =await _doctorScheduleRepo.GetDoctorScheduleByScheduleId(id) ?? 
                throw new NotFoundException(ScheduleIsNotExist);

            var result = _appointmentRepo.GetDoctorScheduleAppointments(docSchedule) ??
                 throw new ConflictException("There are Appointments you can not Delete the schedule");

           await _doctorScheduleRepo.DeleteSchedule(docSchedule);
        }

    }
}
