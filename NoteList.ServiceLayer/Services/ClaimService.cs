using Microsoft.AspNetCore.Http;
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
    public class ClaimService : IClaimService
    {
        #region Fileds

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        public ClaimService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
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

        #region GetUserClaimsAsync

        public async Task<IEnumerable<Claim>> GetUserClaimsAsync(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            return claims;
        }

        #endregion

        #region GetUserClaimsModel

        public async Task<UserClaimViewModel> GetUserClaimsModel(IdentityUser user)
        {
            var model = new UserClaimViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Cliams = new List<UserClaim>()
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            var existingUserRolesClaims = new List<Claim>();

            foreach (var role in userRoles)
            {
                var identityRole = await _roleManager.FindByNameAsync(role);
                var claims = await _roleManager.GetClaimsAsync(identityRole);

                foreach (var claim in claims)
                {
                    existingUserRolesClaims.Add(claim);
                }
            }

            var existingUserClaims = await GetUserClaimsAsync(user);

            foreach (Claim claim in ClaimsStore.GetAllClaims())
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }

                if (existingUserRolesClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.isRoleClaimed = true;
                    userClaim.IsSelected = true;
                }

                model.Cliams.Add(userClaim);
            }

            return model;
        }

        #endregion

        #region UpdateUserClaimsAsync

        public async Task<bool> UpdateUserClaimsAsync(UserClaimViewModel model)
        {
            var user = await FindUserByIdAsync(model.Id);
            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                return false;
            }

            var allSelectedClaims = model.Cliams.Where(c => c.IsSelected)
                .Select(c => new Claim(c.ClaimType, c.ClaimType))
                .ToList();

            if (allSelectedClaims.Any())
            {
                result = await _userManager.AddClaimsAsync(user, allSelectedClaims);

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
