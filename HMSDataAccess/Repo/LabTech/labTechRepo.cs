using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.LabTech
{
    public interface ILabTechRepo
    {
        Task<LabTechnicianEntity?> GetLabTechById(string id);
        Task<List<LabTechnicianEntity>> GetAllLabTechnicians();
        Task<LabTechnicianEntity?> GetLabTechByIdAsNoTracking(string id);
    }
    public class LabTechRepo : ILabTechRepo
    {
        private readonly HMSDBContext _dbContext;
        public LabTechRepo(HMSDBContext context) =>
            _dbContext = context;
        

        public async Task<LabTechnicianEntity?> GetLabTechById(string id) =>
             await _dbContext.LabTechnicians.FindAsync(id);

        public async Task<LabTechnicianEntity?> GetLabTechByIdAsNoTracking(string id) =>
        await _dbContext.LabTechnicians.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        public async Task<List<LabTechnicianEntity>> GetAllLabTechnicians() =>
             await _dbContext.LabTechnicians.AsNoTracking().ToListAsync();

    }
}
