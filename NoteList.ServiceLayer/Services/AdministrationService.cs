using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteList.DomainLayer.Models;
using NoteList.Models;
using NoteList.ServiceLayer.IServices;
using System.Data;
using System.Security.Claims;

namespace NoteList.ServiceLayer.Services
{
    public class AdministrationService : IAdministrationService
    {
        #region Fileds

        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        public AdministrationService(UserManager<IdentityUser<int>> userManager, IHttpContextAccessor httpContextAccessor, SignInManager<IdentityUser<int>> signInManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #endregion

        #region GetAllUser

        public IEnumerable<IdentityUser<int>> GetAllUser()
        {
            var users = _userManager.Users;

            return users;
        }

        #endregion

        #region FindUserByIdAsync

        public async Task<IdentityUser<int>> FindUserByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            return user;
        }

        #endregion

        #region GetUserRolesAsync

        public async Task<IEnumerable<string>> GetUserRolesAsync(IdentityUser<int> user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        #endregion

        #region GetUserRolesModel

        public async Task<UserRoleViewModel> GetUserRolesModel(IdentityUser<int> user)
        {
            var model = new UserRoleViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = new List<UserRole>()
            };

            var existingUserRoles = await GetUserRolesAsync(user);

            var roles = await _roleManager.Roles.ToListAsync();

            foreach (IdentityRole<int> role in roles)
            {
                UserRole userRole = new UserRole
                {
                    RoleName = role.Name
                };

                if (existingUserRoles.Any(c => c == role.Name))
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
                var logedUser = await FindUserByIdAsync(Convert.ToInt32(logedUserId));
                await _signInManager.RefreshSignInAsync(logedUser);
            }

            return true;

        }

        #endregion

        #region GetRoleClaimsAsync

        public async Task<List<RoleClaimViewModel>> GetRoleClaimsAsync()
        {
            var models = new List<RoleClaimViewModel>();
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (IdentityRole<int> identityRole in roles)
            {
                var roleClaimModel = new RoleClaimViewModel
                {
                    RoleName = identityRole.Name,
                    RoleClaims = new List<RoleClaim>()
                };


                var existingRoleClaims = await _roleManager.GetClaimsAsync(identityRole);

                foreach(Claim claim in ClaimsStore.GetAllClaims())
                {
                    RoleClaim roleClaim = new RoleClaim
                    {
                        ClaimType = claim.Type
                    };

                    if (existingRoleClaims.Any(c => c.Type == claim.Type))
                    {
                        roleClaim.IsSelected = true;
                    }

                    roleClaimModel.RoleClaims.Add(roleClaim);
                }

                models.Add(roleClaimModel);
            }

            return models;
        }

        #endregion

        #region UpdateRoleClaimsAsync

        public async Task<bool> UpdateRoleClaimsAsync(List<RoleClaimViewModel> models)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (IdentityRole<int> identityRole in roles)
            {
                var existingRoleClaims = await _roleManager.GetClaimsAsync(identityRole);

                foreach(Claim claim in existingRoleClaims)
                {
                    await _roleManager.RemoveClaimAsync(identityRole, claim);
                }
            }

            foreach(var model in models)
            {
                var identityRole = await _roleManager.FindByNameAsync(model.RoleName);
                var allSelectedClaims = model.RoleClaims.Where(c => c.IsSelected)
                .Select(c => new Claim(c.ClaimType, c.ClaimType))
                .ToList();

                foreach(var claim in allSelectedClaims)
                {
                    await _roleManager.AddClaimAsync(identityRole, claim);
                }
            }

            return true;
        }

        #endregion
    }
}
