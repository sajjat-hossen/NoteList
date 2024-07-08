using Microsoft.AspNetCore.Identity;
using NoteList.DomainLayer.Models;
using NoteList.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.ServiceLayer.Services
{
    public class AccountService : IAccountService
    {
        #region Fields

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        #endregion

        #region Constructor

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        #region CreateAccountAsync

        public async Task<IdentityResult> CreateAccountAsync(RegisterViewModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;

        }

        #endregion

        #region SignInAccountAsync

        public async Task SignInAccountAsync(RegisterViewModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        #endregion

        #region PasswordSignInAccountAsync

        public async Task<SignInResult> PasswordSignInAccountAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model
                .RememberMe, lockoutOnFailure: false);

            return result;
        }

        #endregion

        #region SignOutAccountAsync

        public async Task SignOutAccountAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #endregion

        public async Task AddRoleToUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _userManager.AddToRoleAsync(user, "User");

        }

    }
}
