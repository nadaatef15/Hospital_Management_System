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
        Task SaveChangesAsync();
        Task AddDoctor(DoctorEntity entity);
    }
    public class DoctorRepo : IDoctorRepo
    {
        private readonly HMSDBContext _dbContext;
        public DoctorRepo(HMSDBContext context)
        {
            _dbContext = context;
        }

        public async Task AddDoctor(DoctorEntity entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync() =>
            await _dbContext.SaveChangesAsync();

        public async Task<DoctorEntity?> GetDoctorById(string id) =>
             await _dbContext.Doctors.FindAsync(id);

        public async Task<DoctorEntity?> GetDoctorByIdAsNoTracking(string id) =>
             await _dbContext.Doctors
                    .Include(a => a.DoctorSpecialties)
                    .ThenInclude(a => a.Specialty)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<List<DoctorEntity>> GetAllDoctors() =>
                     await _dbContext.Doctors
                    .Include(a => a.DoctorSpecialties)
                    .ThenInclude(a => a.Specialty)
                    .AsNoTracking()
                    .ToListAsync();
    }
}
