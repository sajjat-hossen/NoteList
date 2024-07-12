using Microsoft.AspNetCore.Identity;
using NoteList.DomainLayer.Models;
using System.Security.Claims;

namespace NoteList.ServiceLayer.IServices
{
    public interface IClaimService
    {
        IEnumerable<IdentityUser<int>> GetAllUser();
        Task<IdentityUser<int>> FindUserByIdAsync(int id);
        Task<IEnumerable<Claim>> GetUserClaimsAsync(IdentityUser<int> user);
        Task<UserClaimViewModel> GetUserClaimsModel(IdentityUser<int> user);
        Task<bool> UpdateUserClaimsAsync(UserClaimViewModel model);
    }
}
