using Microsoft.AspNetCore.Identity;
using NoteList.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.ServiceLayer.IServices
{
    public interface IAdministrationService
    {
        IEnumerable<IdentityUser> GetAllUser();
        Task<IdentityUser> FindUserByIdAsync(string id);

        Task<IEnumerable<Claim>> GetUserClaimsAsync(IdentityUser user);

        Task<UserClaimViewModel> GetUserClaimsModel(IdentityUser user);

        Task<bool> UpdateUserClaimsAsync(UserClaimViewModel model);
    }
}
