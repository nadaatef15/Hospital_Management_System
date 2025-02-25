using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Appointment;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Appointment;
using HMSContracts.Model.DoctorSchadule;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;

namespace Hospital_Management_System.Controllers
{
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentManager _appointmentManager;
        public AppointmentController(IAppointmentManager appointmentManager) =>
            _appointmentManager = appointmentManager;


        [HttpPost(Name = "CreateAppointment")]
        //[PermissionRequirement($"{Permission}.{Appointment}.{Create}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAppointment([FromBody]AppointmentModel model)
        {
            await _appointmentManager.CreateAppointment(model);
            return Created();
        }


        [HttpDelete("Id", Name = "DeleteAppointment")]
        //[PermissionRequirement($"{Permission}.{Appointment}.{Delete}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentManager.DeleteAppointment(id);
            return NoContent();
        }

        [HttpGet("appointmentId", Name = "GetAppointmentById")]
       // [PermissionRequirement($"{Permission}.{Appointment}.{View}")]
        [ProducesResponseType(typeof(AppointmentResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            var appointment = await _appointmentManager.GetAppointmentById(appointmentId);
            return Ok(appointment);
        }

        [HttpGet("doctorId", Name = "GetAllAppointments")]
        //[PermissionRequirement($"{Permission}.{Appointment}.{View}")]
        [ProducesResponseType(typeof(List<AppointmentResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAppointmentsForDoctor(string docId)
        {
            var appointment = await _appointmentManager.GetAllAppointmentsForDoctor(docId);
            return Ok(appointment);
        }


        [HttpGet("patientId", Name = "GetAllAppointmentForPatient")]
        //[PermissionRequirement($"{Permission}.{Appointment}.{View}")]
        [ProducesResponseType(typeof(List<AppointmentResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAppointmentForPatient(string patientId)
        {
            var appointment = await _appointmentManager.GetAllAppointmentsForPatient(patientId);
            return Ok(appointment);
        }

        [HttpPut("Id", Name = "UpdateAppointment")]
        //  [PermissionRequirement($"{Permission}.{Appointment}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAppointment(int Id, AppointmentModel model)
        {
            await _appointmentManager.UpdateAppointment(Id, model);
            return NoContent();
        }

    }
}
