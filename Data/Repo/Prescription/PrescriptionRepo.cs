using HMSDataAccess.DBContext;
using HMSDataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Prescription
{
    public interface IPrescriptionRepo
    {
        Task CreatePrescription(PrescriptionEntity entity);
        Task UpdatePrescription(PrescriptionEntity entity);
        Task DeletePrescription(PrescriptionEntity entity);
        Task<PrescriptionEntity?> GetPrescriptionById(int id);
        Task<PrescriptionEntity?> GetPrescriptionByIdAsNoTracking(int id);
        Task<List<PrescriptionEntity>> GetAllPrescriptions();
        Task SaveChanges();
    }
    public class PrescriptionRepo : IPrescriptionRepo
    {
        public readonly HMSDBContext _dbContext;
        public PrescriptionRepo(HMSDBContext context) =>
            _dbContext = context;

        public async Task CreatePrescription(PrescriptionEntity entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePrescription(PrescriptionEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<PrescriptionEntity>> GetAllPrescriptions()
        {
         var result=  await _dbContext.Prescriptions
                .Include(a => a.MedicalRecord)
                .Include(a => a.Medicine)
                .Include(a => a.Pharmasist)
                .AsNoTracking()
                .ToListAsync();

            return result;

        }
        public async Task<PrescriptionEntity?> GetPrescriptionById(int id) =>
           await _dbContext.Prescriptions.FindAsync(id);

        public async Task<PrescriptionEntity?> GetPrescriptionByIdAsNoTracking(int id) =>
              await _dbContext.Prescriptions
                .Include(a => a.MedicalRecord)
                .Include(a => a.Medicine)
                .Include(a => a.Pharmasist)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);


        public async Task UpdatePrescription( PrescriptionEntity entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges()=>
            await _dbContext.SaveChangesAsync();
        
    }
}
