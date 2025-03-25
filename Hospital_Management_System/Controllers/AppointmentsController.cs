using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Appointment;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Appointment;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;


namespace Hospital_Management_System.Controllers
{
    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentManager _appointmentManager;
        public AppointmentsController(IAppointmentManager appointmentManager) =>
            _appointmentManager = appointmentManager;

        [HttpPost(Name = "CreateAppointment")]
        [PermissionRequirement($"{Permission}.{Appointment}.{Create}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAppointment([FromBody]AppointmentModel model)
        {
            await _appointmentManager.CreateAppointment(model);
            return Created();
        }


        [HttpDelete("{Id}", Name = "DeleteAppointment")]
        [PermissionRequirement($"{Permission}.{Appointment}.{Delete}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAppointment(int Id)
        {
            await _appointmentManager.DeleteAppointment(Id);
            return NoContent();
        }

        [HttpGet("{Id}", Name = "GetAppointmentById")]
        [PermissionRequirement($"{Permission}.{Appointment}.{View}")]
        [ProducesResponseType(typeof(AppointmentResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAppointmentById(int Id)
        {
            var appointment = await _appointmentManager.GetAppointmentById(Id);
            return Ok(appointment);
        }

        [HttpGet("GetDoctorAppointments", Name = "GetAllAppointments")]
        [PermissionRequirement($"{Permission}.{Appointment}.{View}")]
        [ProducesResponseType(typeof(List<AppointmentResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAppointmentsForDoctor(string doctorId)
        {
            var appointment = await _appointmentManager.GetAllAppointmentsForDoctor(doctorId);
            return Ok(appointment);
        }


        [HttpGet("GetPatientAppointments", Name = "GetAllAppointmentForPatient")]
        [PermissionRequirement($"{Permission}.{Appointment}.{View}")]
        [ProducesResponseType(typeof(List<AppointmentResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAppointmentForPatient(string patientId)
        {
            var appointment = await _appointmentManager.GetAllAppointmentsForPatient(patientId);
            return Ok(appointment);
        }

        [HttpPut("{Id}", Name = "UpdateAppointment")]
        [PermissionRequirement($"{Permission}.{Appointment}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAppointment(int Id, AppointmentModel model)
        {
            await _appointmentManager.UpdateAppointment(Id, model);
            return NoContent();
        }

    }
}
