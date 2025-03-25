using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Pharmacist;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;
namespace Hospital_Management_System.Controllers
{
    public class PharmacistsController : BaseController
    {
        private readonly IPharmacistManager _PharmacistManager;
        public PharmacistsController(IPharmacistManager PharmacistManager)=>
            _PharmacistManager= PharmacistManager;  
        

        [HttpPost(Name = "RegisterPharmacist")]
        [PermissionRequirement($"{Permission}.{Pharmacist}.{Create}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterPharmacist([FromForm] pharmacistModel user)
        {
           var pharmacist= await _PharmacistManager.RegisterPharmacist(user);
            return CreatedAtAction(nameof(GetPharmacistById) , new {id= pharmacist.Id} , pharmacist);
        }

        [HttpPut("{Id}", Name = "UpdatePharmacist")]
        [PermissionRequirement($"{Permission}.{Pharmacist}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> UpdatePharmacist(string Id, [FromForm] pharmacistModel user)
        {
            await _PharmacistManager.UpdatePharmacist(Id, user);
            return NoContent();
        }

        [HttpGet("{Id}", Name = "GetPharmacistById")]
        [PermissionRequirement($"{Permission}.{Pharmacist}.{View}")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPharmacistById(string Id)
        {
            var result = await  _PharmacistManager.GetPharmacistById(Id);
            return Ok(result);
        }

        [HttpGet(Name = "GetAllPharmacist")]
        [PermissionRequirement($"{Permission}.{Pharmacist}.{View}")]
        [ProducesResponseType(typeof(List<UserResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPharmacist()
        {
            var result = await _PharmacistManager.GetAllPharmacist();
            return Ok(result);
        }
    }
}
