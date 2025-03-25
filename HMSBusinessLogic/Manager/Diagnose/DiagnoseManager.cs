using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Diagnose;
using HMSDataAccess.Repo.Diagnoses;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;
namespace HMSBusinessLogic.Manager.Diagnose
{
    public interface IDiagnoseManager
    {
        Task CreateDiagnose(DiagnoseModel model);
        Task DeleteDiagnose(int id);
        Task UpdateDiagnose(int id,DiagnoseModel model);
        Task<List<DiagnoseResource>> GetAllDiagnoses();
        Task<DiagnoseResource?> GetDiagnoseById(int id);
    }
    public class DiagnoseManager : IDiagnoseManager
    {
        public readonly IDiagnosesRepo _diagnoseRepo;
        public readonly IValidator<DiagnoseModel> _validator;

        public DiagnoseManager(IValidator<DiagnoseModel> validator , IDiagnosesRepo diagnoseRepo)
        {
            _diagnoseRepo = diagnoseRepo;
            _validator = validator;
        }
        public async Task CreateDiagnose(DiagnoseModel model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var entity = model.ToEntity();

            await _diagnoseRepo.CreateDiagnose(entity);
        }

        public async Task DeleteDiagnose(int id)
        {
            var entity = await _diagnoseRepo.GetDiagnoseById(id) ??
                throw new NotFoundException("This is Diagnoses not found");
        
            await _validator.ValidateAndThrowAsync(entity.ToModel());

            await _diagnoseRepo.DeleteDiagnose(entity);   
        }

        public async Task<List<DiagnoseResource>> GetAllDiagnoses()=>
           (await _diagnoseRepo.GetAllDiagnoses()).Select(a=>a.ToResource()).ToList() ;

        public async Task UpdateDiagnose(int id, DiagnoseModel model)
        {
            if(id != model.Id)
                 throw new ConflictException(NotTheSameId);

            await _validator.ValidateAndThrowAsync(model);

            var entity = await _diagnoseRepo.GetDiagnoseById(id)??
                throw new NotFoundException("This is Diagnoses not found");

            entity.Name= model.Name;

            await _diagnoseRepo.SaveChanges();
        }

        public async Task<DiagnoseResource?> GetDiagnoseById(int id)
        {
            var diagnoses = await _diagnoseRepo.GetDiagnoseById(id) ??
                 throw new NotFoundException("Diagnose is not found");

            return diagnoses.ToResource();

        }
    }
}
