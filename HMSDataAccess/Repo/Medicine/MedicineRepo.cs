using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Medicine
{
    public interface IMedicineRepo
    {
        Task CreateMedicine(MedicineEntity entity);
        Task DeleteMedicine(MedicineEntity entity);
        Task<MedicineEntity?> GetMedicineById(int id);
        Task<MedicineEntity?> GetMedicineByIdAsNoTracking(int id);
        Task<List<MedicineEntity>> GetAllMedicine();
        Task SaveChangesAsync();
    }
    public class MedicineRepo : IMedicineRepo
    {
        public readonly HMSDBContext _dbContext;

        public MedicineRepo(HMSDBContext dbContext)=>
            _dbContext = dbContext;

        public async Task CreateMedicine(MedicineEntity entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMedicine(MedicineEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<MedicineEntity>> GetAllMedicine()=>
            await _dbContext.Medicine.AsNoTracking().ToListAsync();
        

        public async Task<MedicineEntity?> GetMedicineById(int id)=>
            await _dbContext.Medicine.FindAsync(id);


        public async Task<MedicineEntity?> GetMedicineByIdAsNoTracking(int id) =>
            await _dbContext.Medicine.AsNoTracking().FirstOrDefaultAsync(a=>a.Id==id);

       public async Task SaveChangesAsync()=>
          await  _dbContext.SaveChangesAsync();
        
    }
}
