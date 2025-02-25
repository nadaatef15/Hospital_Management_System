using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Specialty;
using HMSDataAccess.Repo.Specialty;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Manager.Specialty
{
    public interface ISpecialtiesManager
    {
        Task CreateSpecialty(SpecialtyModel model);
        Task UpdateSpecialty( int id ,SpecialtyModel model);
        Task DeleteSpecialty(int Id);
        Task<SpecialtyResource> GetSpecialityById(int Id);
        Task<List<SpecialtyResource>> GetAllSpecialities();

    }
    public class SpecialtiesManager : ISpecialtiesManager
    {
        private readonly ISpecialtyRepo _specialtyRepo;
        private readonly IValidator<SpecialtyModel> _specialtyValidator;
        public SpecialtiesManager(ISpecialtyRepo specialtyRepo , IValidator<SpecialtyModel> specialtyValidator)
        {
            _specialtyRepo = specialtyRepo;
            _specialtyValidator = specialtyValidator;
        }
        public async Task CreateSpecialty(SpecialtyModel model)
        {
            await _specialtyValidator.ValidateAndThrowAsync(model);
            
            var speciaity = model.ToEntity();

          await _specialtyRepo.CreateSpecialty(speciaity);

        }

        public async Task DeleteSpecialty(int id)
        {
            var specialty = await _specialtyRepo.GetSpecialtyBtId(id) ??
                throw new NotFoundException(SpecialityIsNotExist);

            _specialtyRepo.DeleteSpeciality(specialty);
        }

        public async Task<List<SpecialtyResource>> GetAllSpecialities() =>
            (await _specialtyRepo.GetAllSpecialties()).Select(a=>a.ToResource()).ToList();
        

        public async Task<SpecialtyResource> GetSpecialityById(int id)
        {
            var specialty = await _specialtyRepo.GetSpecialtyBtId(id) ??
                 throw new NotFoundException(SpecialityIsNotExist);

            return specialty.ToResource();
        }

        public async Task UpdateSpecialty(int id ,SpecialtyModel model)
        {
            if (id != model.Id)
                throw new ConflictException(NotTheSameId);

            await _specialtyValidator.ValidateAndThrowAsync(model);

            var specialty = await _specialtyRepo.GetSpecialtyBtId(id) ??
                 throw new NotFoundException(SpecialityIsNotExist);

            specialty.Name= model.Name;

            _specialtyRepo.UpdateSpeciality(specialty);
        }
    }
}
