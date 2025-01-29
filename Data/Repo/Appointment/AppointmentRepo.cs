using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using static HMSContracts.Constants.SysEnums.Status;


namespace HMSDataAccess.Repo.Appointment
{
    public interface IAppointmentRepo
    {
        Task CreateAppointment(AppointmentEntity appointment);
        Task DeleteAppointment(AppointmentEntity Appointment);
        Task<AppointmentEntity?> GetAppointmentByIdAsNoTracking(int id);
        Task<AppointmentEntity?> GetAppointmentById(int id);
        Task<List<AppointmentEntity>> GetAllAppointmentsForDoctor(string docId);
        Task<List<AppointmentEntity>> GetAllAppointmentsForPatient(string patientId);
        void UpdateAppointment(AppointmentEntity appointment);
        List<AppointmentEntity> AllAppointmentsForDoctorInSpecificDate(AppointmentEntity appointment);
        Task<DoctorScheduleEntity?> docScheduleWithSameDate(AppointmentEntity appointment);

        List<AppointmentEntity> GetDoctorAppointmentsWithInaDate(string doctorId, DateOnly date);
    }
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly HMSDBContext _dbContext;
        public AppointmentRepo(HMSDBContext context) =>
            _dbContext = context;


        public async Task CreateAppointment(AppointmentEntity appointment)
        {
            await _dbContext.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public List<AppointmentEntity> AllAppointmentsForDoctorInSpecificDate(AppointmentEntity appointment) =>
             _dbContext.Appointments
                    .Where(a => a.DoctorId == appointment.DoctorId
                         && a.Date == appointment.Date && a.Status == complete).ToList();

        public async Task<DoctorScheduleEntity?> docScheduleWithSameDate(AppointmentEntity appointment) =>
             await _dbContext.DoctorSchedule.Where(a => a.Date == appointment.Date)
                      .FirstOrDefaultAsync(a => a.DoctorId == appointment.DoctorId);


        public async Task DeleteAppointment(AppointmentEntity appointment)
        {
            _dbContext.Remove(appointment);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<AppointmentEntity?> GetAppointmentByIdAsNoTracking(int id) =>
            await _dbContext.Appointments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        public async Task<AppointmentEntity?> GetAppointmentById(int id) =>
           await _dbContext.Appointments.FindAsync(id);

        public async Task<List<AppointmentEntity>> GetAllAppointmentsForDoctor(string docId) =>
             await _dbContext.Appointments.Where(a => a.DoctorId == docId).AsNoTracking().ToListAsync();

        public async Task<List<AppointmentEntity>> GetAllAppointmentsForPatient(string patientId) =>
             await _dbContext.Appointments.Where(a => a.PatientId == patientId).AsNoTracking().ToListAsync();

        public void UpdateAppointment(AppointmentEntity appointment)
        {
            _dbContext.Update(appointment);
            _dbContext.SaveChanges();
        }

        public List<AppointmentEntity> GetDoctorAppointmentsWithInaDate(string doctorId, DateOnly date) =>
            _dbContext.Appointments.Where(a => a.DoctorId == doctorId &&
                  a.Status == complete && a.Date == date).ToList();

    }
}
