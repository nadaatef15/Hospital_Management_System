using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.Prescription;
using HMSContracts.Model.Prescription;
using HMSDataAccess.Repo.Medicine;
using HMSDataAccess.Repo.Pharmacist;
using HMSDataAccess.Repo.Prescription;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;
namespace HMSBusinessLogic.Manager.Prescription
{
    public interface IPrescriptionManager
    {
        Task CreatePrescription(PrescriptionModel model);
        Task UpdatePrescription(int id, PrescriptionModel model);
        Task DeletePrescription(int id);
        Task<PrescriptionResource> GetPrescriptionById(int id);
        Task<List<PrescriptionResource>> GetAllPrescriptions();
        Task HandlePrescriptionDispenseUpdate(PrescriptionDispensedModel model);
    }
    public class PrescriptionManager : IPrescriptionManager
    {
        public readonly IPrescriptionRepo _prescriptionRepo;
        public readonly IValidator<PrescriptionModel> _validator;
        public readonly IValidator<PrescriptionDispensedModel> _validatorDispensedModel;
        public readonly IPrescriptionService _prescriptionService;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IPharmacistRepo _pharmacistRepo;
        public readonly IMedicineRepo _medicineRepo;
        public PrescriptionManager(
            IPrescriptionRepo prescriptionRepo,
             IValidator<PrescriptionModel> validator,
             IPrescriptionService prescriptionService,
             IValidator<PrescriptionDispensedModel> validatorDispensedModel,
             IHttpContextAccessor httpContextAccessor,
             IPharmacistRepo pharmacistRepo , 
             IMedicineRepo medicineRepo)
        {
            _prescriptionRepo = prescriptionRepo;
            _validator = validator;
            _validatorDispensedModel = validatorDispensedModel;
            _prescriptionService = prescriptionService;
            _httpContextAccessor = httpContextAccessor;
            _pharmacistRepo = pharmacistRepo;
            _medicineRepo = medicineRepo;
        }

        public async Task CreatePrescription(PrescriptionModel model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var entity = model.ToEntity();

            await _prescriptionRepo.CreatePrescription(entity);
        }

        public async Task DeletePrescription(int id)
        {
            var entity = await _prescriptionRepo.GetPrescriptionById(id) ??
                   throw new NotFoundException(PrescriptionNotFound);

            if (entity.DispinsedById is not null)
                throw new ConflictException(NotDeletePrescriptionDispensed);

            await _prescriptionRepo.DeletePrescription(entity);
        }

        public async Task<List<PrescriptionResource>> GetAllPrescriptions() =>
            (await _prescriptionRepo.GetAllPrescriptions()).Select(a => a.ToResource()).ToList();


        public async Task<PrescriptionResource> GetPrescriptionById(int id)
        {
            var entity = await _prescriptionRepo.GetPrescriptionByIdAsNoTracking(id) ??
                 throw new NotFoundException(PrescriptionNotFound);

            return entity.ToResource();
        }

        public async Task UpdatePrescription(int id, PrescriptionModel model)
        {
            if (id != model.Id)
                throw new ConflictException(NotTheSameId);

            await _validator.ValidateAndThrowAsync(model);

            var entity = await _prescriptionRepo.GetPrescriptionById(id) ??
                 throw new NotFoundException(PrescriptionNotFound);

            if (entity.DispinsedById is not null)
                throw new ConflictException(PrescriptionDispensedUpdated);

            _prescriptionService.SetValue(entity, model);

            await _prescriptionRepo.SaveChanges();
        }

        public async Task HandlePrescriptionDispenseUpdate(PrescriptionDispensedModel model)
        {
            await _validatorDispensedModel.ValidateAndThrowAsync(model);

            var PharmacistId = _httpContextAccessor?.HttpContext?.User
                 .Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var entity = await _prescriptionRepo.GetPrescriptionById(model.Id);

            entity!.DispinsedById = PharmacistId;
            entity.DispinsedOn = DateTime.Now;

            var medicine = await _medicineRepo.GetMedicineById(entity.MedicineId);
            medicine!.Amount -= entity.Quentity; 

            await _prescriptionRepo.SaveChanges();
        }
    }
}
