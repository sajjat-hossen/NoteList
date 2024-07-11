using Microsoft.AspNetCore.Identity;
using NoteList.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.ServiceLayer.IServices
{
    public interface IAdministrationService
    {
        IEnumerable<IdentityUser> GetAllUser();
        Task<IdentityUser> FindUserByIdAsync(string id);
        Task<IEnumerable<string>> GetUserRolesAsync(IdentityUser user);
        Task<UserRoleViewModel> GetUserRolesModel(IdentityUser user);
        Task<bool> UpdateUserRolesAsync(UserRoleViewModel model);
        Task<List<RoleClaimViewModel>> GetRoleClaimsAsync();
        Task<bool> UpdateRoleClaimsAsync(List<RoleClaimViewModel> models);
    }
}
