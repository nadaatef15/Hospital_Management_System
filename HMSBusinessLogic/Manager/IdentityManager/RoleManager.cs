using HMSBusinessLogic.Services.AccountServices;
using HMSContracts.Model.Identity;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.IdentityManager
{
    public interface IRoleManager
    {
        Task CreateRole(RoleNameModel roleNameDto);
        Task DeleteRoleById(string roleId);
        Task UpdateRole(string roleId, RoleNameModel role);
        Task<List<string?>> GetAllRoles();
    }

    public class RoleManager : IRoleManager
    {
        private readonly IRoleService _roleService;
        private readonly RoleManager<IdentityRole> _roleManagerIdentity;
        public RoleManager(IRoleService roleService, RoleManager<IdentityRole> roleManagerIdentity)
        {
            _roleService = roleService;
            _roleManagerIdentity = roleManagerIdentity;
        }

        public async Task CreateRole(RoleNameModel roleNameModel)
        {
            var checkRole = await _roleManagerIdentity.FindByNameAsync(roleNameModel.Name);

            if (checkRole is not null)
                throw new ConflictException(RoleIsExist);

            await _roleService.AddRole(roleNameModel.Name);
        }

        public async Task DeleteRoleById(string roleId)
        {
            var role = await _roleManagerIdentity.FindByIdAsync(roleId);

            if (role is null)
                throw new NotFoundException(RoleDoesnotExist);

            await _roleService.DeleteRole(role);
        }

        public async Task UpdateRole(string roleId, RoleNameModel role)
        {
            var isRole = await _roleManagerIdentity.FindByIdAsync(roleId);

            if (isRole is null)
                throw new NotFoundException(RoleDoesnotExist);

            isRole.Name = role.Name;
            await _roleService.UpdateRole(isRole);
        }

        public async Task<List<string?>> GetAllRoles() =>
             (await _roleService.GetRoles()).Select(a => a.Name).ToList();

    }
}
