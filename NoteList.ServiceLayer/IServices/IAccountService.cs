using Microsoft.AspNetCore.Identity;
using NoteList.ServiceLayer.Models;

namespace NoteList.ServiceLayer.IServices
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateAccountAsync(RegisterViewModel model);
        Task SignInAccountAsync(RegisterViewModel model);
        Task<SignInResult> PasswordSignInAccountAsync(LoginViewModel model);
        Task SignOutAccountAsync();
        Task AddRoleToUser(string email);
    }
}
