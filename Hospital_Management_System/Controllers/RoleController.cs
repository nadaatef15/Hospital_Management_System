using HMSBusinessLogic.Manager.IdentityManager;
using HMSContracts.Model.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class RoleController : BaseController
    {
        IRoleManager roleManagerIdentity;
        public RoleController(IRoleManager _roleManagerIdentity)
        {
            roleManagerIdentity = _roleManagerIdentity;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> createRole(RoleNameModel roleName)
        {
                await roleManagerIdentity.CreateRole(roleName);
                return Created();
        }

        [HttpDelete("DeleteRoleById")]
        public async Task <IActionResult> deleteRole(string roleId)
        {
                await roleManagerIdentity.DeleteRoleById(roleId);
                return Ok();
        }

        [HttpGet("GetAllRoles")]
        public async Task <IActionResult> GetAll()
        {
          var data=await roleManagerIdentity.GetAllRoles(); 
            return Ok(data);
        }

        [HttpPut("UpdateRole")]
        public async Task <IActionResult> UpdateRoele(string roleId,RoleNameModel roleName)
        {
            await roleManagerIdentity.UpdateRole(roleId , roleName);
            return Ok();
        }

    }
}
