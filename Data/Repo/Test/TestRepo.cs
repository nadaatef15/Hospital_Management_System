using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Test
{
    public interface ITestRepo
    {
        Task CreateTest(TestEntity entity);
        Task DeleteTest(TestEntity entity);
        Task UpdateTest(TestEntity entity);
        Task<TestEntity?> GetTestById(int id);
        Task<List<TestEntity>> GetAllTest();
        Task SaveChanges();
        Task<bool> IsTestUsedInMedicalRecord(int id);
    }
    public class TestRepo : ITestRepo
    {
        public readonly HMSDBContext _dbContext;
        public TestRepo(HMSDBContext dbContext)=>
            _dbContext = dbContext;
       
        public async Task CreateTest(TestEntity entity)
        {
             _dbContext.Tests.Add(entity);
             await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTest(TestEntity entity)
        {           
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsTestUsedInMedicalRecord(int id)=>
            await _dbContext.MedicalRecordTests.AnyAsync(a => a.TestId == id);

        
        public async Task<List<TestEntity>> GetAllTest() =>
          await _dbContext.Tests.ToListAsync();
             
        public async Task<TestEntity?> GetTestById(int id)=>
            await _dbContext.Tests.FindAsync(id);
        

        public async Task UpdateTest(TestEntity entity)
        {
             _dbContext.Tests.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges() =>
            await _dbContext.SaveChangesAsync();
    }
}
