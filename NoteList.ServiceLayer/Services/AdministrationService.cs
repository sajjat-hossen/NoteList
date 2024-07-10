﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NoteList.DomainLayer.Models;
using NoteList.Models;
using NoteList.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.ServiceLayer.Services
{
    public class AdministrationService : IAdministrationService
    {
        #region Fileds

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        public AdministrationService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }

        #endregion

        #region GetAllUser

        public IEnumerable<IdentityUser> GetAllUser()
        {
            var users = _userManager.Users;

            return users;
        }

        #endregion

        #region FindUserByIdAsync

        public async Task<IdentityUser> FindUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        #endregion

        #region GetUserRolesAsync

        public async Task<IEnumerable<string>> GetUserRolesAsync(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        #endregion

        #region GetUserRolesModel

        public async Task<UserRoleViewModel> GetUserRolesModel(IdentityUser user)
        {
            var model = new UserRoleViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = new List<UserRole>()
            };

            var existingUserRoles = await GetUserRolesAsync(user);

            foreach (string role in RolesStore.GetAllRoles())
            {
                UserRole userRole = new UserRole
                {
                    RoleName = role
                };

                if (existingUserRoles.Any(c => c == role))
                {
                    userRole.IsSelected = true;
                }

                model.Roles.Add(userRole);
            }

            return model;
        }

        #endregion

        #region UpdateUserRolesAsync

        public async Task<bool> UpdateUserRolesAsync(UserRoleViewModel model)
        {
            var user = await FindUserByIdAsync(model.Id);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                return false;
            }

            var allSelectedRoles = model.Roles.Where(c => c.IsSelected)
                .Select(c => c.RoleName)
                .ToList();

            if (allSelectedRoles.Any())
            {
                result = await _userManager.AddToRolesAsync(user, allSelectedRoles);

                if (!result.Succeeded)
                {
                    return false;
                }

                var logedUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                var logedUser = await FindUserByIdAsync(logedUserId);
                await _signInManager.RefreshSignInAsync(logedUser);
            }

            return true;

        }

        #endregion
    }
}
