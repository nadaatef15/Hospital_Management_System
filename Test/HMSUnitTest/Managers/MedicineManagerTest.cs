using FluentAssertions;
using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.Medicine;
using HMSBusinessLogic.Services.Medicine;
using HMSContracts.Model.Medicine;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Medicine;
using Moq;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;


namespace HMSUnitTest.Managers
{
    public class MedicineManagerTest
    {
        private Mock<IMedicineRepo> _medicineRepoMock;
        private Mock<IValidator<MedicineModel>> _validatorMock;
        private Mock<IMedicineService> _medicineServiceMock;
        private MedicineManager _medicineManager;

        public MedicineManagerTest()
        {
            _medicineRepoMock = new Mock<IMedicineRepo>();
            _medicineServiceMock = new Mock<IMedicineService>();
            _validatorMock = new Mock<IValidator<MedicineModel>>();

            _medicineManager = new MedicineManager(
              _medicineRepoMock.Object,
              _validatorMock.Object,
              _medicineServiceMock.Object
             );
        }

        [Fact]
        public async Task GetMedicine_NotValidId_ThrowException()
        {

            Func<Task> act = () => _medicineManager.GetMedicineById(12);

            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(Medicinenotfound);
        }

        [Fact]
        public async Task GetMedicine_ValidId_returnMedicine()
        {
            var _id = 1;
            var name = "cotton";

            _medicineRepoMock
                .Setup(x => x.GetMedicineByIdAsNoTracking(It.IsAny<int>()))
                .ReturnsAsync(new MedicineEntity() { Id = _id, Name = name });

            var medicine = await _medicineManager.GetMedicineById(_id);

            medicine.Should().NotBeNull();
            medicine.Id.Should().Be(_id);
            medicine.Name.Should().Be(name);
            _medicineRepoMock.Verify(x => x.GetMedicineByIdAsNoTracking(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteMedicine_ValidId_Deleted()
        {

            int medicineId = 1;
            var medicine = new MedicineEntity { Id = medicineId, Name = "Ketophan" };

            _medicineRepoMock
                .Setup(repo => repo.GetMedicineById(medicineId))
                .ReturnsAsync(medicine);

            _medicineRepoMock.Setup(repo => repo.DeleteMedicine(medicine))
                .Returns(Task.CompletedTask);

            await _medicineManager.DeleteMedicine(medicineId);

            _medicineRepoMock.Verify(repo => repo.GetMedicineById(medicineId), Times.Once);
            _medicineRepoMock.Verify(repo => repo.DeleteMedicine(medicine), Times.Once);

        }

        [Fact]
        public void GetAllMedicines()
        {

            var Medicines = new List<MedicineEntity>()
                {
               new () {
                    Id = 3,
                    Name = "petaden", 
                },
                new (){
                    Id = 4,
                    Name = "ketophan" }
                };

            _medicineRepoMock
                .Setup(x => x.GetAllMedicine())
                .ReturnsAsync(Medicines);

            var patient = _medicineManager.GetAllMedicines();

            patient.Should().NotBeNull();
            _medicineRepoMock.Verify(x => x.GetAllMedicine(), Times.Once);
        }
    }
}
