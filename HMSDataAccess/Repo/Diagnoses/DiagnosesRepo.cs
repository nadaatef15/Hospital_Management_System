using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Diagnoses
{
    public interface IDiagnosesRepo
    {
       Task CreateDiagnose(DiagnosesEntity entity);
       Task DeleteDiagnose(DiagnosesEntity entity);
        Task<DiagnosesEntity?> GetDiagnoseById(int id);
       Task UpdateDiagnose(DiagnosesEntity entity);
       Task<List<DiagnosesEntity>> GetAllDiagnoses();
       Task SaveChanges();

    }
    public class DiagnosesRepo :IDiagnosesRepo
    {
        public readonly HMSDBContext _dbContext;
        public DiagnosesRepo(HMSDBContext context)=>
            _dbContext = context;
        
        public async Task CreateDiagnose(DiagnosesEntity entity)
        {
            _dbContext.Add(entity);
           await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDiagnose(DiagnosesEntity entity)
        {
            _dbContext.Remove(entity);
           await _dbContext.SaveChangesAsync();   
        }

        public async Task<List<DiagnosesEntity>> GetAllDiagnoses()=>        
           await _dbContext.Diagnoses.AsNoTracking().ToListAsync();


        public async Task UpdateDiagnose(DiagnosesEntity entity)
        {
             _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<DiagnosesEntity?> GetDiagnoseById(int id)=>
            await _dbContext.Diagnoses.FindAsync(id);

        public async Task SaveChanges()=> 
           await _dbContext.SaveChangesAsync();
        
    }
}
