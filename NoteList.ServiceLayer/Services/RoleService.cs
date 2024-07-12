using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteList.DomainLayer.Models;
using NoteList.ServiceLayer.IServices;

namespace NoteList.ServiceLayer.Services
{
    public class RoleService : IRoleService
    {
        #region Fields

        private readonly RoleManager<IdentityRole> _roleManager;

        #endregion

        #region Constructor

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        #endregion

        #region GetAllRoles

        public async Task<IEnumerable<IdentityRole>> GetAllRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        #endregion

        #region CreateRoleAsync

        public async Task<bool> CreateRoleAsync(CreateRole model)
        {
            if (string.IsNullOrEmpty(model.RoleName))
            {
                return false;
            }

            var roleExist = await _roleManager.RoleExistsAsync(model.RoleName);

            if (roleExist)
            {
                return false;
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(model.RoleName));

            if (!result.Succeeded)
            {
                return false;
            }

            return true;

        }

        #endregion

        #region DeleteRoleAsync

        public async Task DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
        }

        #endregion
    }
}
