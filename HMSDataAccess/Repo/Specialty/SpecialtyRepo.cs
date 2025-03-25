using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Specialty
{
    public interface ISpecialtyRepo
    {
        Task CreateSpecialty(SpecialtyEntity specialty);
        void UpdateSpeciality(SpecialtyEntity entity);
        Task<SpecialtyEntity?> GetSpecialtyBtId(int id);
        Task<List<SpecialtyEntity>> GetAllSpecialties();
        void DeleteSpeciality(SpecialtyEntity entity);
    }
    public class SpecialtyRepo : ISpecialtyRepo
    {
        private readonly HMSDBContext _dbContext;
        public SpecialtyRepo(HMSDBContext dbContext)
        {
            _dbContext = dbContext;  
        }

        public async Task CreateSpecialty(SpecialtyEntity specialty)
        {
            await _dbContext.AddAsync(specialty);
            _dbContext.SaveChanges();
        }

        public void UpdateSpeciality(SpecialtyEntity entity)
        {
            _dbContext.Specialties.Update(entity);
            _dbContext.SaveChanges();
        }


        public void DeleteSpeciality(SpecialtyEntity entity)
        {
            _dbContext.Specialties.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<SpecialtyEntity?> GetSpecialtyBtId(int id)=>
             await _dbContext.Specialties.FindAsync(id);

        public async Task<List<SpecialtyEntity>> GetAllSpecialties() =>
           await _dbContext.Specialties.AsNoTracking().ToListAsync();
    }
}
