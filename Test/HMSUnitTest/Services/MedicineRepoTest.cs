using FluentAssertions;
using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Medicine;
using HMSDataAccess.Repo.Patient;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HMSUnitTest.Services
{
    public class MedicineRepoTest
    {
        [Fact]
        public async Task CreateMedicine_addedMedicine()
        {
            var options = new DbContextOptionsBuilder<HMSDBContext>()
           .UseInMemoryDatabase(databaseName: "HMSDBTests")
           .Options;

            var context = new HMSDBContext(options);
            var repo = new MedicineRepo(context);

            var medicine = new MedicineEntity
            {
                Id = 1,
                Name = "Ketophan",
                Price = 100,
                Amount = 10,
                Type = "headeche"
            };

            await repo.CreateMedicine(medicine);

            var addedMedicine = await context.Medicine.FindAsync(1);

            addedMedicine.Should().NotBeNull();
            addedMedicine.Name.Should().Be("Ketophan");
        }

        [Fact]
        public async Task GetMedicineById_returnMedicine()
        {
            var options = new DbContextOptionsBuilder<HMSDBContext>()
          .UseInMemoryDatabase(databaseName: "HMSDBTests")
          .Options;

            var context = new HMSDBContext(options);
            var repo = new MedicineRepo(context);

            var medicine = context.Medicine.Add(new MedicineEntity()
            {
                Id = 1,
                Name = "Ketophan",
                Price = 100,
                Amount = 10,
                Type = "headeche",
                CreatedBy = "System",
                CreatedOn = DateTime.Now,

            });

            context.SaveChanges();
            var result = await repo.GetMedicineById(1);

            result.Should().NotBeNull();

        }


        [Fact]
        public async void GetAllMedicine_returnListOfMedicine()
        {
            var options = new DbContextOptionsBuilder<HMSDBContext>()
           .UseInMemoryDatabase(databaseName: "HMSDBTests")
           .Options;

            var context = new HMSDBContext(options);

            context.Medicine.Add(new MedicineEntity
            {

                Id = 1,
                Name = "Ketophan",
                Price = 100,
                Amount = 10,
                Type = "headeche",
                CreatedBy = "System",
                CreatedOn = DateTime.Now,

            });

            context.Medicine.Add(new MedicineEntity
            {

                Id = 2,
                Name = "panadole",
                Price = 100,
                Amount = 10,
                Type = "headeche",
                CreatedBy = "System",
                CreatedOn = DateTime.Now,

            });

            context.SaveChanges();


            List<MedicineEntity> medicines = new List<MedicineEntity>();

            var repo = new MedicineRepo(context);
            medicines = await repo.GetAllMedicine();


            medicines.Should().NotBeNull();
            medicines.Should().NotBeEmpty();
        }

        [Fact]
        public async void UpdateMedicine_throughExistsOne_Updated()
        {
            var options = new DbContextOptionsBuilder<HMSDBContext>()
           .UseInMemoryDatabase(databaseName: "HMSDBTests")
           .Options;


            var context = new HMSDBContext(options);

            context.Medicine.Add(new MedicineEntity
            {
                Id = 3,
                Name = "petaden",
                Price = 100,
                Amount = 10,
                Type = "pecteria",
                CreatedBy="system",
                CreatedOn=DateTime.Now
            });

            var medicineEntity = new MedicineEntity();

            context.SaveChanges();

            var repo = new MedicineRepo(context);
            medicineEntity = await context.Medicine.FindAsync(3);

            var updatdAmount = 30;
            medicineEntity.Amount = updatdAmount;

          
            await repo.UpdateMedicine(medicineEntity);

            medicineEntity.Should().NotBeNull();
            medicineEntity.Amount.Should().Equals(30);

        }
    }
}
