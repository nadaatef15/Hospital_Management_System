using HMSBusinessLogic.Manager.PermissionManager;
using HMSContracts.Model.Permission;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class PermissionController : BaseController
    {
        IPermission permissionManager;
        public PermissionController(IPermission _permissionManager)
        {
            permissionManager = _permissionManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions(string roleId)
        {
               var result = await permissionManager.GetpermissionsOfRole(roleId);
                return Ok(result);
        }

        [HttpPost]
        public async Task <IActionResult> EditPermissionsForRole(string roleId, List<PermissionModel> permissionModels)
        {
            await permissionManager.EditPermissionsforRole(permissionModels, roleId);
            return Ok();
        }
    }
}
