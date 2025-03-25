using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Doctor;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;

namespace Hospital_Management_System.Controllers
{
    public class DoctorsController : BaseController
    {
        private readonly IDoctorManager _doctorManager;
        public DoctorsController(IDoctorManager doctorManager) =>
            _doctorManager = doctorManager;

        [HttpPost(Name = "RegisterDoctor")]
        [PermissionRequirement($"{Permission}.{Doctor}.{Create}")]
        [ProducesResponseType(typeof(DoctorResource), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterDoctor([FromForm] DoctorModel user)
        {
            var doctor = await _doctorManager.RegisterDoctor(user);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id.ToString() }, doctor);
        }

        [HttpPut("{Id}", Name = "UpdateDoctor")]
        [PermissionRequirement($"{Permission}.{Doctor}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateDoctor(string Id, [FromForm] DoctorModel user)
        {
            await _doctorManager.UpdateDoctor(Id, user);
            return NoContent();
        }

        [HttpGet("{Id}", Name = "GetDoctorById")]
        [PermissionRequirement($"{Permission}.{Doctor}.{View}")]
        [ProducesResponseType(typeof(DoctorResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctorById(string Id)
        {
            var doctor = await _doctorManager.GetDoctorById(Id);
            return Ok(doctor);
        }

        [HttpGet(Name ="GetAllDoctors")]
        [PermissionRequirement($"{Permission}.{Doctor}.{View}")]
        [ProducesResponseType(typeof(List<DoctorResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctor = await _doctorManager.GetAllDoctors();
            return Ok(doctor);
        }

        [HttpDelete("{Id}",Name ="DeleteDoctor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDoctor(string Id)
        {
            await _doctorManager.DeleteDoctor(Id);
            return NoContent();
        }
    }
}
