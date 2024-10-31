using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMSBusinessLogic.Services.AccountServices
{
    public interface IRoleService
    {
        Task AddRole(string roleName);
        Task DeleteRole(IdentityRole roleId);
        Task UpdateRole(IdentityRole role);
        Task<List<IdentityRole>> GetRoles();
    }
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(RoleManager<IdentityRole> roleManager) =>
            _roleManager = roleManager;

        public async Task AddRole(string roleName)
        {
            var role = new IdentityRole()
            {
                Name = roleName,
            };

            await _roleManager.CreateAsync(role);
        }

        public async Task DeleteRole(IdentityRole role) =>
            await _roleManager.DeleteAsync(role);

        public async Task UpdateRole(IdentityRole role) =>
           await _roleManager.UpdateAsync(role);

        public async Task<List<IdentityRole>> GetRoles() =>
            await _roleManager.Roles.ToListAsync();
    }
}
