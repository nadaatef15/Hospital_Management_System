using HMSBusinessLogic.Manager.Identity;
using HMSBusinessLogic.Resource;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class UserController : BaseController
    {
        IUserManager userManager;
        public UserController(IUserManager _userManager)=>
            userManager = _userManager;
        

        [HttpPost("Id", Name = "AssignRolesToUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IActionResult> AssignRolesToUser(string id, List<string> roleName)
        {
            await userManager.AssignRolesToUser(id, roleName);
            return Created();
        }

        [HttpGet("Id", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await userManager.GetUserById(id);
            return Ok(user);
        }

        [HttpDelete("Id", Name = "DeleteUserById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> DeleteUserById(string id)
        {
            await userManager.DeleteUser(id);
            return NoContent();
        }

        [HttpGet(Name ="GetAllUser")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllUser()
        {
            var users = await userManager.GetAllUsers();
            return Ok(users);
        }
    }
}
