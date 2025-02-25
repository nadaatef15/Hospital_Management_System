using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Specialty;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Doctor;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;
namespace HMSBusinessLogic.Manager.Doctor
{
    public interface IDoctorSpecialtiesManager
    {
        Task AddDoctorSpeciality(DoctorSpecialtyModel doctorSpecialty);
        Task DeleteDoctorSpecialty(DoctorSpecialtyModel doctorSpecialty);
        Task<List<DoctorSpecialtyResource>> GetDoctorSpecialties(string docId);

    }
    public class DoctorSpecialtiesManager : IDoctorSpecialtiesManager
    {
        private readonly IDoctorSpecialtiesRepo _doctorSpecialtiesRepo;
        private readonly IValidator<DoctorSpecialtyModel> _validator;
        public DoctorSpecialtiesManager(IDoctorSpecialtiesRepo repo, IValidator<DoctorSpecialtyModel> validator)
        {
            _doctorSpecialtiesRepo = repo;
            _validator = validator;
        }

        public async Task AddDoctorSpeciality(DoctorSpecialtyModel doctorSpecialty)
        {
            await _validator.ValidateAndThrowAsync(doctorSpecialty);

            var docSpecialtyEntity= doctorSpecialty.ToEntity();

            var docSpecialty = await _doctorSpecialtiesRepo.DoctorHasSpecialty(docSpecialtyEntity);

            if (docSpecialty is not null)
                throw new ConflictException(DocHasSpecialty);
       
            _doctorSpecialtiesRepo.AddSpecialtyToDoctor(docSpecialtyEntity);
        }

        public async Task DeleteDoctorSpecialty(DoctorSpecialtyModel doctorSpecialty)
        {
            await _validator.ValidateAndThrowAsync(doctorSpecialty);
           var docSpecialtyEntity=  doctorSpecialty.ToEntity();

            _doctorSpecialtiesRepo.DeleteSpecialtyOfDoctor(docSpecialtyEntity);
        }

        public async Task<List<DoctorSpecialtyResource>> GetDoctorSpecialties(string docId)=>

                  (await _doctorSpecialtiesRepo.GetDoctorSpecialties(docId))
                  .Select(a=>a.ToResource()).ToList() ??
                     throw new NotFoundException(NoSpecialtyForDoctor);
        
    }
}
