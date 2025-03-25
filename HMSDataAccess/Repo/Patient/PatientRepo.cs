using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Patient
{
    public interface IPatientRepo
    {
        Task<PatientEntity?> GetPatientById(string id);
        Task<List<PatientEntity>> GetAllPatients();
        void UpdatePatient(PatientEntity patient);
    }
    public class PatientRepo :IPatientRepo
    {
        private readonly HMSDBContext _dbContext;
        public PatientRepo(HMSDBContext context)=>
            _dbContext = context;
        
        public async Task<PatientEntity?> GetPatientById(string id)=>
             await _dbContext.Patients.FindAsync(id);            

        public async Task<List<PatientEntity>> GetAllPatients() =>
             await _dbContext.Patients.AsNoTracking().ToListAsync();

        public void UpdatePatient(PatientEntity patient)
        {
            _dbContext.Update(patient);
            _dbContext.SaveChanges();
            
        }
    }
}
