using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Receptionist;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;

namespace Hospital_Management_System.Controllers
{
    public class ReceptionistsController : BaseController
    {
        private readonly IReceptionistManager _receptionistManager;
        public ReceptionistsController(IReceptionistManager receptionistManager)=>
        
            _receptionistManager = receptionistManager;
        

        [HttpPost(Name ="RegisterReceptionist")]
        [PermissionRequirement($"{Permission}.{Receptionist}.{Create}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task< IActionResult> RegisterReceptionist([FromForm] ReceptionistModel user)
        {
           var receptionist= await _receptionistManager.RegisterReceptionist(user);
            return CreatedAtAction(nameof(GetReceptionistById) , new {id= receptionist.Id} , receptionist);
        }

        [HttpPut("{Id}", Name = "UpdateReceptionist")]
        [PermissionRequirement($"{Permission}.{Receptionist}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateReceptionist(string Id ,  [FromForm] ReceptionistModel user)
        {
            await _receptionistManager.UpdateReceptionist(Id,user);
            return NoContent();
        }


        [HttpGet("{Id}" , Name = "GetReceptionistById")]
        [PermissionRequirement($"{Permission}.{Receptionist}.{View}")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReceptionistById(string Id)
        {
            var result = await _receptionistManager.GetReceptionistById(Id);
            return Ok(result);
        }

        [HttpGet(Name ="GetAllReceptionist")]
        [PermissionRequirement($"{Permission}.{Receptionist}.{View}")]
        [ProducesResponseType(typeof(List<UserResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPharmacist()
        {
            var result = await _receptionistManager.GetAllReceptionists();
            return Ok(result);
        }
    }
}
