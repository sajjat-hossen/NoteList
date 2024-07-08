using Microsoft.AspNetCore.Identity;
using NoteList.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.ServiceLayer.IServices
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateAccountAsync(RegisterViewModel model);
        Task SignInAccountAsync(RegisterViewModel model);
        Task<SignInResult> PasswordSignInAccountAsync(LoginViewModel model);
        Task SignOutAccountAsync();
    }
}
