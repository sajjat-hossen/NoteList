using Microsoft.AspNetCore.Identity;
using NoteList.DomainLayer.Models;
using System.Security.Claims;

namespace NoteList.ServiceLayer.IServices
{
    public interface IClaimService
    {
        IEnumerable<IdentityUser> GetAllUser();
        Task<IdentityUser> FindUserByIdAsync(string id);
        Task<IEnumerable<Claim>> GetUserClaimsAsync(IdentityUser user);
        Task<UserClaimViewModel> GetUserClaimsModel(IdentityUser user);
        Task<bool> UpdateUserClaimsAsync(UserClaimViewModel model);
    }
}
