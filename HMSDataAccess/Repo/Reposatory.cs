using HMSDataAccess.DBContext;

namespace HMSDataAccess.Repo
{
    public static class Reposatory
    {
        public static async Task SaveAsync(HMSDBContext _dbcontext)
        {
            await _dbcontext.SaveChangesAsync();
        }
        
    }
}
