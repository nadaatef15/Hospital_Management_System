using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Doctor
{
    public interface IDoctorRepo
    {
        Task<DoctorEntity?> GetDoctorById(string id);
        Task<List<DoctorEntity>> GetAllDoctors();
        Task<DoctorEntity?> GetDoctorByIdAsNoTracking(string id);
    }
    public class DoctorRepo : IDoctorRepo
    {
        private readonly HMSDBContext _dbContext;
        public DoctorRepo(HMSDBContext context)
        {
            _dbContext = context;
        }

        public async Task<DoctorEntity?> GetDoctorById(string id) =>
             await _dbContext.Doctors.FindAsync(id);

        public async Task<DoctorEntity?> GetDoctorByIdAsNoTracking(string id) =>
             await _dbContext.Doctors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        public async Task<List<DoctorEntity>> GetAllDoctors()=>
             await _dbContext.Doctors.AsNoTracking().ToListAsync();
          

    }
}
