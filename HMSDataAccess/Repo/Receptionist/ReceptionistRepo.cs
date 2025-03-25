using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Receptionist
{
    public interface IReceptionistRepo
    {
        Task<List<ReceptionistEntity>> GetAllReceptionist();
        Task<ReceptionistEntity?> GetReceptionistById(string id);
        Task<ReceptionistEntity?> GetReceptionistByIdAsNoTracking(string id);
    }
    public class ReceptionistRepo : IReceptionistRepo
    {
        private readonly HMSDBContext _dbContext;
        public ReceptionistRepo(HMSDBContext context)
        {
           _dbContext = context;
        }
        public async Task<ReceptionistEntity?> GetReceptionistById(string id) =>
             await _dbContext.Receptionists.FindAsync(id);

        public async Task<ReceptionistEntity?> GetReceptionistByIdAsNoTracking(string id) =>
           await _dbContext.Receptionists.AsNoTracking().FirstOrDefaultAsync(a=>a.Id==id);

        public async Task<List<ReceptionistEntity>> GetAllReceptionist() =>
             await _dbContext.Receptionists.Where(a => a.IsDeleted == false).ToListAsync();

    }
}
