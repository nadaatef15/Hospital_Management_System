using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;
namespace HMSDataAccess.Repo.MedicalRecord
{
    public interface IMedicalRecordREpo
    {
        Task CreateMedicalRecord(MedicalRecordEntity model);
        Task DeleteMedicalRecord(MedicalRecordEntity medicalRecord);
        Task UpdateMedicalRecord(MedicalRecordEntity medicalRecord);
        Task<MedicalRecordEntity?> GetMedicalRecordByIdTracking(int id);
        Task<MedicalRecordEntity?> GetMedicalRecordByIdNoTracking(int id);
        Task SaveChanges();
        Task<List<MedicalRecordEntity>> GetAllMedicalRecords();
    }
    public class MedicalRecordRepo : IMedicalRecordREpo
    {
        private readonly HMSDBContext _dbContext;

        public MedicalRecordRepo(HMSDBContext context)=>
            _dbContext = context;
        

        public async Task CreateMedicalRecord(MedicalRecordEntity model)
        {
            await _dbContext.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMedicalRecord(MedicalRecordEntity medicalRecord)
        {
            _dbContext.Remove(medicalRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMedicalRecord(MedicalRecordEntity medicalRecord)
        {
            _dbContext.Update(medicalRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<MedicalRecordEntity?> GetMedicalRecordByIdTracking(int id)=>
             await _dbContext.MedicalRecord.FindAsync(id);

        public async Task<MedicalRecordEntity?> GetMedicalRecordByIdNoTracking(int id) =>
            await _dbContext.MedicalRecord.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        public async Task< List<MedicalRecordEntity>> GetAllMedicalRecords() =>
            await  _dbContext.MedicalRecord.AsNoTracking().ToListAsync();

        public async Task SaveChanges()=>
            await _dbContext.SaveChangesAsync();
    }
}
