using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;
namespace HMSDataAccess.Repo.MedicalRecord
{
    public interface IMedicalRecordREpo
    {
        Task CreateMedicalRecord(MedicalRecordEntity entity);
        Task DeleteMedicalRecord(MedicalRecordEntity medicalRecord);
        Task<MedicalRecordEntity?> GetMedicalRecordById(int id);
        Task<MedicalRecordEntity?> GetMedicalRecordByIdNoTracking(int id);
        Task<List<MedicalRecordEntity>> GetAllMedicalRecordsForPatient(string patientId);
        Task SaveChanges();
        Task<List<MedicalRecordEntity>> GetAllMedicalRecords();
    }
    public class MedicalRecordRepo : IMedicalRecordREpo
    {
        private readonly HMSDBContext _dbContext;

        public MedicalRecordRepo(HMSDBContext context) =>
            _dbContext = context;

        public async Task CreateMedicalRecord(MedicalRecordEntity entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMedicalRecord(MedicalRecordEntity medicalRecord)
        {
            _dbContext.Remove(medicalRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<MedicalRecordEntity?> GetMedicalRecordById(int id) =>
             await _dbContext.MedicalRecord.Include(a => a.MedicalRecordDiagnoses).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<MedicalRecordEntity?> GetMedicalRecordByIdNoTracking(int id) =>
        await _dbContext.MedicalRecord
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.Appointment)
            .Include(a => a.MedicalRecordTests)
               .ThenInclude(ts => ts.Test)
            .Include(a => a.MedicalRecordDiagnoses)
                .ThenInclude(mrd => mrd.Diagnoses)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<List<MedicalRecordEntity>> GetAllMedicalRecords() =>
            await _dbContext.MedicalRecord
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.Appointment)
            .Include(a => a.Doctor)
            .Include(a => a.MedicalRecordTests)
               .ThenInclude(ts => ts.Test)
            .Include(a => a.MedicalRecordDiagnoses)
                .ThenInclude(mrd => mrd.Diagnoses)
            .AsNoTracking().ToListAsync();

        public async Task<List<MedicalRecordEntity>> GetAllMedicalRecordsForPatient(string patientId) =>
           await _dbContext.MedicalRecord.Where(a => a.PatientId == patientId)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.MedicalRecordTests)
               .ThenInclude(ts => ts.Test)
            .Include(a => a.Appointment)
            .AsNoTracking()
            .ToListAsync();
        public async Task SaveChanges() =>
            await _dbContext.SaveChangesAsync();
    }
}
