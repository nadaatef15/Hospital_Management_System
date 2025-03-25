using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.Medicine;
using HMSContracts.Model.Medicine;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Medicine;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Manager.Medicine
{ 
    public interface IMedicineManager
    {
        Task CreateMedicine(MedicineModel model);
        Task DeleteMedicine(int id);
        Task UpdateMedicine(int id, MedicineModel model);
        Task<MedicineResource?> GetMedicineById(int id);
        Task<List<MedicineResource>> GetAllMedicines();
    }
    public class MedicineManager : IMedicineManager
    {
        public readonly IMedicineRepo _medicineRepo;
        public readonly IValidator<MedicineModel> _validator;
        public readonly IMedicineService _medicineService;
        public MedicineManager(IMedicineRepo medicineRepo , IValidator<MedicineModel> validator , IMedicineService medicineService)
        {
            _medicineRepo = medicineRepo;
            _validator = validator;
            _medicineService = medicineService;
        }
        public async Task CreateMedicine(MedicineModel model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var entity = model.ToEntity();

            await _medicineRepo.CreateMedicine(entity);
        }

        public async Task UpdateMedicine(int id, MedicineModel model)
        {
            if (id != model.Id)
                throw new ConflictException("Not the same id");

            await _validator.ValidateAndThrowAsync(model);

            var entity = await _medicineRepo.GetMedicineById(id) ??
                  throw new NotFoundException(Medicinenotfound);

            _medicineService.SetValues(entity, model);

            await _medicineRepo.SaveChangesAsync();

        }

        public async Task DeleteMedicine(int id)
        {
           var entity= await _medicineRepo.GetMedicineById(id)??
                throw new NotFoundException(Medicinenotfound);

            await _medicineRepo.DeleteMedicine(entity);
        }

        public async Task<List<MedicineResource>> GetAllMedicines()=>
       
            (await _medicineRepo.GetAllMedicine()).Select(a=> a.ToResource()).ToList(); 
        

        public async Task<MedicineResource?> GetMedicineById(int id)
        {
           var entity= await _medicineRepo.GetMedicineByIdAsNoTracking(id)??
                 throw new NotFoundException(Medicinenotfound);

            return entity.ToResource();
        }


       
    }
}
