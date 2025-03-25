using FluentAssertions;
using HMSBusinessLogic.Manager.Patient;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Users;
using Hospital_Management_System.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HMSUnitTest.Controllers
{
    public class PatientControllerTest
    {
        private readonly Mock<IPatientsManager> _patientsManager;
        private readonly PatientsController _controller;

        public PatientControllerTest()
        {
            _patientsManager = new Mock<IPatientsManager>();
            _controller = new PatientsController(_patientsManager.Object);
        }

        [Fact]
        public async Task GetAllPatients_ReturnOkResponse()
        {
            var patientList = new List<PatientResource>()
            {
                new (){Id="123"},
                new (){Id="456"}
            };

            _patientsManager.Setup(a => a.GetAllPatients()).ReturnsAsync(patientList);

            var patients = await _controller.GetAllPatients();

            _patientsManager.Verify(a => a.GetAllPatients(), Times.Once());

            patients.Should().NotBeNull();
            patients.Should().BeOfType<OkObjectResult>();

            ((OkObjectResult)patients).Value.Should().BeEquivalentTo(patientList);
        }

        [Fact]
        public async Task GetPatient_ById_returnOkResponse()
        {
            var _id = "123";
            var patient = new PatientResource() { Id = _id };

            _patientsManager
                .Setup(a => a.GetPatientById(_id))
                .ReturnsAsync(patient);

            var result= await _controller.GetPatientById(_id);

            _patientsManager.Verify(a=>a.GetPatientById(_id), Times.Once());
            patient.Should().NotBeNull();
            patient.Id.Should().Be("123");

            ((OkObjectResult)result).Value.Should().BeEquivalentTo(patient);

        }
        [Fact]
        public async Task DeletePatient_ById_ReturnNoContentResponse()
        {
            var _id = "123";

            var result= await _controller.DeletePatientById(_id);

            _patientsManager.Verify(a=>a.DeletePatient(_id), Times.Once());
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdatePatient__ReturnNoContentResponse()
        {
            var _id = "123";
            var patient = new PatientModel { Id = _id };

            _patientsManager.Setup(a => a.UpdatePatient(_id, patient));

            var result= await _controller.UpdatePatient(_id, patient);


            result.Should().BeOfType<NoContentResult>();
            _patientsManager.Verify(a => a.UpdatePatient(_id, patient), Times.Once());
        }

        [Fact]
        public async Task RegisterPatient_returnCreatedAtAction()
        {
            var patient = new PatientModel { Id="123" };

            _patientsManager
                .Setup(a => a.RegisterPatient(patient))
                .ReturnsAsync(new PatientResource());

            var result= await _controller.RegisterPatient(patient);

            result.Should().BeOfType<CreatedAtActionResult>();
            _patientsManager.Verify(a=>a.RegisterPatient(patient), Times.Once());
        }

    }
}
