using FluentAssertions;
using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Patient;
using Microsoft.EntityFrameworkCore;
namespace HMSUnitTest.Services
{
    public class PatientRepoTest
    {
        [Fact]
        public async Task GetPatientById_ByValidId_ReturnPatient()
        {
            var options = new DbContextOptionsBuilder<HMSDBContext>()
            .UseInMemoryDatabase(databaseName: "HMSDBTests")
            .Options;

            var context = new HMSDBContext(options);


            context.Patients.Add(new PatientEntity
            {
                Id = "123abcccc",
                UserName = "Patient1",
                Address = "Kafr",
                Age = 20,
                CreatedBy = "Project",
                Allergies = ["Cats"],
                BloodGroup = "O+",
                Email = "Patient.123@gmail.com",
                Gender = 0,
                MedicalHistory = ["cold"],
                PhoneNumber = "01019821370"
            });

            context.SaveChanges();


            PatientEntity patientEntity = new PatientEntity();

            var repo = new PatientRepo(context);
            patientEntity = await repo.GetPatientById("123abcccc");


            patientEntity.Should().NotBeNull();
            patientEntity.UserName.Should().Be("Patient1");
        }

        [Fact]
        public async Task GetPatientById_NotValidId_ReturnNull()
        {
            var options = new DbContextOptionsBuilder<HMSDBContext>()
            .UseInMemoryDatabase(databaseName: "HMSDBTests")
            .Options;

            var context = new HMSDBContext(options);

            PatientEntity patientEntity = new PatientEntity();

            var repo = new PatientRepo(context);
            patientEntity = await repo.GetPatientById("1");

            patientEntity.Should().BeNull();
       
        }

        [Fact]
        public async void GetAllPatients_returnListOfPatient()
        {
            var options = new DbContextOptionsBuilder<HMSDBContext>()
           .UseInMemoryDatabase(databaseName: "HMSDBTests")
           .Options;

            var context = new HMSDBContext(options);
            
                context.Patients.Add(new PatientEntity
                {
                    Id = "Nada123abc",
                    UserName = "Patient1",
                    Address = "Kafr",
                    Age = 20,
                    CreatedBy = "Project",
                    Allergies = ["Cats"],
                    BloodGroup = "O+",
                    Email = "Patient1.123@gmail.com",
                    Gender = 0,
                    MedicalHistory = ["cold"],
                    PhoneNumber = "01019821370"
                });

                context.Patients.Add(new PatientEntity
                {
                    Id = "123abccc",
                    UserName = "Patient2",
                    Address = "Kafr",
                    Age = 20,
                    CreatedBy = "Project",
                    Allergies = ["Cats"],
                    BloodGroup = "O+",
                    Email = "Patient2.123@gmail.com",
                    Gender = 0,
                    MedicalHistory = ["cold"],
                    PhoneNumber = "01019821370"
                });

                context.SaveChanges();
            

            List<PatientEntity> patients = new List<PatientEntity>();
            
                var repo = new PatientRepo(context);
                patients = await repo.GetAllPatients();
            

            patients.Should().NotBeNull();
            patients.Should().NotBeEmpty();
        }

        [Fact]
        public async void UpdatePatient_throughExistsOne()
        {
            var options = new DbContextOptionsBuilder<HMSDBContext>()
           .UseInMemoryDatabase(databaseName: "HMSDBTests")
           .Options;

            PatientEntity patientEntity = new PatientEntity();

            var context = new HMSDBContext(options);
            
            context.Patients.Add(new PatientEntity
            {
                Id = "123abc",
                UserName = "Patient1",
                Address = "Kafr",
                Age = 20,
                CreatedBy = "Project",
                Allergies = ["Cats"],
                BloodGroup = "O+",
                Email = "Patient.123@gmail.com",
                Gender = 0,
                MedicalHistory = ["cold"],
                PhoneNumber = "01019821370"
            });

            context.SaveChanges();
           
            var repo = new PatientRepo(context);
            patientEntity = await context.Patients.FindAsync("123abc");

            var updatedEmail = "Patient123.123@gmail.com";
            patientEntity.Email = updatedEmail;

            repo.UpdatePatient(patientEntity);

            patientEntity.Should().NotBeNull();
            patientEntity.UserName.Should().BeSameAs("Patient1");
            patientEntity.Email.Should().BeSameAs(updatedEmail);

        }
    }
}
