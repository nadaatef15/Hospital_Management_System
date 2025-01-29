using HMSBusinessLogic.Manager.Receptionist;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class ReceptionistController : BaseController
    {
        private readonly IReceptionistManager _receptionistManager;
        public ReceptionistController(IReceptionistManager receptionistManager)=>
        
            _receptionistManager = receptionistManager;
        

        [HttpPost(Name ="RegisterReceptionist")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task< IActionResult> RegisterReceptionist([FromForm] ReceptionistModel user)
        {
           var receptionist= await _receptionistManager.RegisterReceptionist(user);
            return CreatedAtAction(nameof(GetReceptionistById) , new {id= receptionist.Id} , receptionist);
        }

        [HttpPut("Id", Name = "UpdateReceptionist")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateReceptionist(string id ,  [FromForm] ReceptionistModel user)
        {
            await _receptionistManager.UpdateReceptionist(id,user);
            return NoContent();
        }


        [HttpGet("Id" , Name = "GetReceptionistById")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReceptionistById(string id)
        {
            var result = await _receptionistManager.GetReceptionistById(id);
            return Ok(result);
        }

        [HttpGet( Name ="GetAllReceptionist")]
        [ProducesResponseType(typeof(List<UserResource>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllPharmacist()
        {
            var result = await _receptionistManager.GetAllReceptionists();
            return Ok(result);
        }
    }
}
