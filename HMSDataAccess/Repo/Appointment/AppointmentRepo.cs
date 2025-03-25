using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using static HMSContracts.Constants.SysEnums;
using static HMSContracts.Constants.SysEnums.Status;


namespace HMSDataAccess.Repo.Appointment
{
    public interface IAppointmentRepo
    {
        Task CreateAppointment(AppointmentEntity appointment);
        Task DeleteAppointment(AppointmentEntity Appointment);
        Task<AppointmentEntity?> GetAppointmentByIdAsNoTracking(int id);
        Task<AppointmentEntity?> GetAppointmentById(int id);
        Task<List<AppointmentEntity>> GetAllAppointments(string? docId = null, string? patientId = null);
        void UpdateAppointment(AppointmentEntity appointment);
        List<AppointmentEntity> GetDoctorAppointmentsByDate(DateOnly date, string doctorId, Status status);
        List<AppointmentEntity> GetDoctorScheduleAppointments(DoctorScheduleEntity doctorSchedules);
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

        public List<AppointmentEntity> GetDoctorAppointmentsByDate(DateOnly date, string doctorId, Status status) =>
             _dbContext.Appointments
                    .Where(a => a.DoctorId == doctorId
                         && a.Date == date && a.Status == status).ToList();

        public async Task DeleteAppointment(AppointmentEntity appointment)
        {
            _dbContext.Remove(appointment);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<AppointmentEntity?> GetAppointmentByIdAsNoTracking(int id) =>
            await _dbContext.Appointments.AsNoTracking().Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<AppointmentEntity?> GetAppointmentById(int id) =>
           await _dbContext.Appointments.FindAsync(id);

        public async Task<List<AppointmentEntity>> GetAllAppointments(string? docId=null, string? patientId=null)=>
             await _dbContext.Appointments
                    .Where(a=> (docId==null || a.DoctorId==docId) && (patientId==null || a.PatientId==patientId))
                    .Include(a => a.Doctor)
                    .Include(a => a.Patient)
                    .AsNoTracking()
                    .ToListAsync();
        

        public void UpdateAppointment(AppointmentEntity appointment)
        {
            _dbContext.Update(appointment);
            _dbContext.SaveChanges();
        }

        public List<AppointmentEntity> GetDoctorScheduleAppointments(DoctorScheduleEntity doctorSchedule) =>
            _dbContext.Appointments.Where(a => a.DoctorId == doctorSchedule.DoctorId &&
                  a.Status == complete && a.Date == doctorSchedule.Date &&
                  doctorSchedule.StartTime <= a.StartTime && a.StartTime < doctorSchedule.EndTime &&
                  doctorSchedule.StartTime < a.EndTime && a.EndTime <= doctorSchedule.EndTime).ToList();

    }
}
