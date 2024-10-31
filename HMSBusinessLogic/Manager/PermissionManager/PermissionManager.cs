using HMSBusinessLogic.ManagePermissions;
using HMSContracts.Model.Permission;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Constants.SysConstants;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.PermissionManager
{
    public interface IPermission
    {
        Task<List<PermissionModel>> GetpermissionsOfRole(string roleId);
        Task EditPermissionsforRole(List<PermissionModel> permissionModels, string roleId);
    }
    public class PermissionManager : IPermission
    {
        RoleManager<IdentityRole> _roleManager;
        public PermissionManager(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<PermissionModel>> GetpermissionsOfRole(string roleId)
        {
            var permissionModels = new List<PermissionModel>();
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is not null)
            {
                var allClaims = _roleManager.GetClaimsAsync(role).Result
                    .Where(a => a.Type == Permission)
                    .Select(a => a.Value)
                    .ToHashSet();


                Permissions.GetAllPermissions().ForEach(per =>
                {
                    if (allClaims.Contains(per))
                    {
                        permissionModels.Add(new PermissionModel()
                        {
                            isSelected = true,
                            Model = per.Split('.')[1],
                            PermissionType = per.Split('.')[2]
                        });
                    }
                    else
                    {
                        permissionModels.Add(new PermissionModel()
                        {
                            isSelected = false,
                            Model = per.Split('.')[1],
                            PermissionType = per.Split('.')[2]
                        });
                    }
                });
            }
            else
                throw new NotFoundException(RoleDoesnotExist);


            return permissionModels;

        }

        public async Task EditPermissionsforRole(List<PermissionModel> permissionModels, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is not null)
            {
                (await _roleManager.GetClaimsAsync(role))
                    .Where(a => a.Type == Permission)
                    .ToList()
                    .ForEach(async claim =>
                    {
                        await _roleManager.RemoveClaimAsync(role, claim);
                    });

                permissionModels.ForEach(async model =>
                {

                    if (model.isSelected == true)
                    {
                        await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(Permission, $"{Permission}.{model.Model}.{model.PermissionType}"));
                    }
                });
            }
            throw new NotFoundException(RoleDoesnotExist);

        }
    }
}
