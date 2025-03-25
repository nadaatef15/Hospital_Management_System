using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Test
{
    public interface IMedicalRecordTestsRepo
    {
        Task CreateMedicalRecordTests(MedicalRecordTests entity);
        Task UpdateMedicalRecordTests(MedicalRecordTests entity);
        Task SaveChangesAsync();
        Task<List<MedicalRecordTests>> GetMedicalRecordTests(int medicalRecordId);
        Task<MedicalRecordTests?> GetMedicalRecordTestByIds(int medicalRecordId, int testId);
        Task<List<MedicalRecordTests>> GetMedicalRecordTestForPatient(string patientId);

    }
    public class MedicalRecordTestsRepo : IMedicalRecordTestsRepo
    {
        public readonly HMSDBContext _dbContext;
        public MedicalRecordTestsRepo(HMSDBContext context) =>
            _dbContext = context;

        public async Task CreateMedicalRecordTests(MedicalRecordTests entity)
        {
            _dbContext.MedicalRecordTests.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<MedicalRecordTests>> GetMedicalRecordTestForPatient(string patientId) =>
             await _dbContext.MedicalRecord
                            .Include(x => x.MedicalRecordTests)
                            .Where(a => a.PatientId == patientId)
                            .SelectMany(a => a.MedicalRecordTests)
                            .ToListAsync();

        public async Task<List<MedicalRecordTests>> GetMedicalRecordTests(int medicalRecordId) =>
            await _dbContext.MedicalRecordTests.Where(a => a.MedicalRecordId == medicalRecordId).ToListAsync();

        public async Task<MedicalRecordTests?> GetMedicalRecordTestByIds(int medicalRecordId, int testId) =>
           await _dbContext.MedicalRecordTests.FirstOrDefaultAsync(a => a.MedicalRecordId == medicalRecordId && a.TestId == testId);

        public async Task SaveChangesAsync() =>
            await _dbContext.SaveChangesAsync();

        public async Task UpdateMedicalRecordTests(MedicalRecordTests entity)
        {
            _dbContext.MedicalRecordTests.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
