using Microsoft.AspNetCore.Identity;
using NoteList.ServiceLayer.Models;

namespace NoteList.ServiceLayer.IServices
{
    public interface IAdministrationService
    {
        IEnumerable<IdentityUser<int>> GetAllUser();
        Task<IdentityUser<int>> FindUserByIdAsync(int id);
        Task<IEnumerable<string>> GetUserRolesAsync(IdentityUser<int> user);
        Task<UserRoleViewModel> GetUserRolesModel(IdentityUser<int> user);
        Task<bool> UpdateUserRolesAsync(UserRoleViewModel model);
        Task<RoleClaimViewModel> GetRoleClaimsAsync(int id);
        Task<bool> UpdateRoleClaimsAsync(RoleClaimViewModel model);
    }
}
