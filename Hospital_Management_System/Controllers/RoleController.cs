using HMSBusinessLogic.Manager.IdentityManager;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class RoleController : BaseController
    {
        IRoleManager roleManagerIdentity;
        public RoleController(IRoleManager _roleManagerIdentity)=>
            roleManagerIdentity = _roleManagerIdentity;
        

        [HttpPost(Name = "AddRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRole(RoleNameModel roleName)
        {
            await roleManagerIdentity.CreateRole(roleName);
            return Created();
        }

        [HttpDelete("Id", Name = "DeleteRoleById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> DeleteRoleById(string roleId)
        {
            await roleManagerIdentity.DeleteRoleById(roleId);
            return NoContent();
        }

        [HttpGet(Name = "GetAllRoles")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRoles()
        {
            var data = await roleManagerIdentity.GetAllRoles();
            return Ok(data);
        }

        [HttpPut("roleId", Name = "UpdateRole")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status204NoContent)]

        public async Task<IActionResult> UpdateRoele(string roleId, RoleNameModel roleName)
        {
            await roleManagerIdentity.UpdateRole(roleId, roleName);
            return NoContent();
        }

    }
}
