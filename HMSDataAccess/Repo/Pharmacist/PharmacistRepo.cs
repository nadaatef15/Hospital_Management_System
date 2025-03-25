using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Pharmacist
{
    public interface IPharmacistRepo
    {
        Task<PharmacistEntity?> GetPharmacistById(string id);
        Task<List<PharmacistEntity>> GetAllPharmacist();
        Task<PharmacistEntity?> GetPharmacistByIdAsNoTracking(string id);

    }
    public class PharmacistRepo : IPharmacistRepo
    {
        private readonly HMSDBContext _dbContext;
        public PharmacistRepo(HMSDBContext context)=>
            _dbContext = context;


        public async Task<PharmacistEntity?> GetPharmacistById(string id) =>
            await _dbContext.Pharmacists.FindAsync(id);

        public async Task<PharmacistEntity?> GetPharmacistByIdAsNoTracking(string id) =>
            await _dbContext.Pharmacists.AsNoTracking().FirstOrDefaultAsync(a=>a.Id ==id);

        public async Task<List<PharmacistEntity>> GetAllPharmacist() =>
             await _dbContext.Pharmacists.AsNoTracking().ToListAsync();
    }
}
