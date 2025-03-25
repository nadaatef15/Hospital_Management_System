using HMSBusinessLogic.Manager.LabTechnician;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class LabTechniciansController : BaseController
    {
        private readonly ILabTechniciansManager _labTechnicianManager;
        public LabTechniciansController(ILabTechniciansManager labTechnicianManager)=>
            _labTechnicianManager = labTechnicianManager;
        

        [HttpPost(Name = "RegisterLabTech")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> RegisterLabtech([FromForm] labTechnicianModel user)
        {
            var labTech= await _labTechnicianManager.RegisterLabTech(user);
            return CreatedAtAction(nameof(GetLabtechById), new { id = labTech.Id }, labTech);
        }

        [HttpPut("{Id}", Name = "UpdateLabtech")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateLabtech(string Id, [FromForm] labTechnicianModel user)
        {
            await _labTechnicianManager.UpdateLabTech(Id, user);
            return NoContent();
        }

        [HttpGet("{Id}", Name = "GetLabtechById")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetLabtechById(string Id)
        {
            var labTech = await _labTechnicianManager.GetLabTechById(Id);
            return Ok(labTech);
        }

        [HttpGet(Name ="GetAllLabTechnicians")]
        [ProducesResponseType(typeof(List<UserResource>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllLabtech()
        {
            var labTechs = await _labTechnicianManager.GetAllLabTechs();
            return Ok(labTechs);
        }
    }
}
