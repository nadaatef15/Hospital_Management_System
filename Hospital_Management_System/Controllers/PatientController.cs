using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Patient;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;


namespace Hospital_Management_System.Controllers
{
    public class PatientController : BaseController
    {
        private readonly IPatientsManager _patientsManager;
        public PatientController(IPatientsManager patientsManager)=>
            _patientsManager = patientsManager;
        

        [HttpPost("RegisterPatient")]
        [PermissionRequirement($"{Permission}.{Patient}.{Create}")]
        public async Task<IActionResult> RegisterPatient([FromForm] PatientModel user)
        {
          var patient= await _patientsManager.RegisterPatient(user);
            return CreatedAtAction(nameof(GetPatientById),new {id= patient.Id} ,patient);
        }


        [HttpPut("Id" , Name = "UpdatePatient")]
        [PermissionRequirement($"{Permission}.{Patient}.{Edit}")]
        public async Task<IActionResult> UpdatePatient(string id, [FromForm] PatientModel user)
        {
            await _patientsManager.UpdatePatient(id, user);
            return NoContent();
        }


        [HttpDelete("Id", Name = "DeletePatientById")]
        [PermissionRequirement($"{Permission}.{Patient}.{Delete}")]
        public async Task<IActionResult> DeletePatientById(string id)
        {
            await _patientsManager.DeletePatient(id);
            return NoContent();
        }


        [HttpGet("Id", Name = "GetPatientById")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        [PermissionRequirement($"{Permission}.{Patient}.{View}")]
        public async Task<IActionResult> GetPatientById(string id)
        {
            var result = await _patientsManager.GetPatientById(id);
            return Ok(result);
        }

        [HttpGet("GetAllPatients")]
        [ProducesResponseType(typeof(List<UserResource>), StatusCodes.Status200OK)]
        [PermissionRequirement($"{Permission}.{Patient}.{View}")]
        public async Task<IActionResult> GetAllPatients()
        {
            var result = await _patientsManager.GetAllPatients();
            return Ok(result);
        }
    }
}
