using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Doctor
{
    public interface IDoctorSpecialtiesRepo
    {
        public void AddSpecialtyToDoctor(DoctorSpecialties doctorSpecialties);
        void DeleteSpecialtyOfDoctor(DoctorSpecialties doctorSpecialty);
        Task<DoctorSpecialties?> DoctorHasSpecialty(DoctorSpecialties doctorSpecialty);
        Task<List<DoctorSpecialties>> GetDoctorSpecialties(string doctorId);

    }
    public class DoctorSpecialtiesRepo : IDoctorSpecialtiesRepo
    {
        private readonly HMSDBContext _dbContext;
        public DoctorSpecialtiesRepo(HMSDBContext context)=>
            _dbContext = context;

        public void AddSpecialtyToDoctor(DoctorSpecialties doctorSpecialties)
        {
            _dbContext.Add(doctorSpecialties);
            _dbContext.SaveChanges();
        }

        public void DeleteSpecialtyOfDoctor(DoctorSpecialties doctorSpecialty)
        {
            _dbContext.DoctorSpecialties.Remove(doctorSpecialty);
            _dbContext.SaveChanges();
        }
        public async Task<DoctorSpecialties?> DoctorHasSpecialty(DoctorSpecialties doctorSpecialty) =>
                 await _dbContext.DoctorSpecialties
                      .FirstOrDefaultAsync(ds => ds.DoctorId == doctorSpecialty.DoctorId
                                && ds.SpecialtyId == doctorSpecialty.SpecialtyId);

        public async Task<List<DoctorSpecialties>> GetDoctorSpecialties(string  doctorId)=>
             await _dbContext.DoctorSpecialties
             .Where(a=>a.DoctorId == doctorId)
             .ToListAsync();


    }
}
