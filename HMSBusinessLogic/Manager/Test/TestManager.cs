using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Test;
using HMSDataAccess.Repo.Test;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.Test
{
    public interface ITestManager
    {
        Task CreateTest(TestModel model);
        Task DeleteTest(int id);
        Task UpdateTest(int id, TestModel model);
        Task<TestResource?> GetTestById(int id);
        Task<List<TestResource>> GetAllTest();
    }
    public class TestManager : ITestManager
    {
        public readonly ITestRepo _testRepo;
        public readonly IValidator<TestModel> _validator;
        public TestManager(ITestRepo testRepo, IValidator<TestModel> validator)
        {
            _testRepo = testRepo;
            _validator = validator;
        }
        public async Task CreateTest(TestModel model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var entity = model.ToEntity();

            await _testRepo.CreateTest(entity);
        }

        public async Task DeleteTest(int id)
        {
            var entity = await _testRepo.GetTestById(id) ??
               throw new NotFoundException(TestNotFound);

            if (await _testRepo.IsTestUsedInMedicalRecord(entity.Id))
                throw new ConflictException(TestIsInMedicalRecord);

            await _testRepo.DeleteTest(entity);
        }

        public async Task<List<TestResource>> GetAllTest() =>
            (await _testRepo.GetAllTest()).Select(a => a.ToResource()).ToList();

        public async Task<TestResource?> GetTestById(int id)
        {
            var entity = await _testRepo.GetTestById(id) ??
                throw new NotFoundException(TestNotFound);

            return entity.ToResource();
        }

        public async Task UpdateTest(int id, TestModel model)
        {
            if (id != model.Id)
                throw new ConflictException(NotTheSameId);

            await _validator.ValidateAndThrowAsync(model);

            var entity= await _testRepo.GetTestById(id)??
                throw new NotFoundException(TestNotFound);

            entity.Name = model.Name;
            entity.Price = model.Price;

            await _testRepo.SaveChanges();
        }
    }
}
