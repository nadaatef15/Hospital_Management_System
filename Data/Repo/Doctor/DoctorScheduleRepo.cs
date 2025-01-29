using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Doctor
{
    public interface IDoctorScheduleRepo
    {
        Task CreateSchedule(DoctorScheduleEntity entity);
        List<DoctorScheduleEntity> GetSchedulesForDoctor(string docId, DateOnly? dateFrom, DateOnly? dateTo);
        Task UpdateDoctorSchedule(DoctorScheduleEntity doctorSpecialties);
        Task<DoctorScheduleEntity?> GetDoctorScheduleByScheduleId(int id);
        Task DeleteSchedule(DoctorScheduleEntity docSchedule);
        Task<DoctorEntity?> GetDoctorById(string docId);
    }
    public class DoctorScheduleRepo : IDoctorScheduleRepo
    {
        private readonly HMSDBContext _dbContext;
        public DoctorScheduleRepo(HMSDBContext context) =>
            _dbContext = context;

        public async Task CreateSchedule(DoctorScheduleEntity entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<DoctorEntity?> GetDoctorById(string docId) =>
            await _dbContext.Doctors.FindAsync(docId);

        public List<DoctorScheduleEntity> GetSchedulesForDoctor(string docId, DateOnly? dateFrom, DateOnly? dateTo)=>
                _dbContext.DoctorSchedule
                .Where(schedule => schedule.DoctorId == docId && 
                (dateFrom == null || dateFrom <= schedule.Date ) && ( dateTo == null || dateTo >= schedule.Date))
                .Include(a=>a.Doctor)
                .ToList();

        public async Task<DoctorScheduleEntity?> GetDoctorScheduleByScheduleId(int id) =>
            await _dbContext.DoctorSchedule.FindAsync(id);


        public async Task UpdateDoctorSchedule(DoctorScheduleEntity doctorSpecialty)
        {
            _dbContext.Update(doctorSpecialty);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSchedule(DoctorScheduleEntity docSchedule)
        {
            _dbContext.Remove(docSchedule);
           await _dbContext.SaveChangesAsync();
        }


    }
}
