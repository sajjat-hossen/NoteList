using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NoteList.DomainLayer.Models;
using NoteList.Models;
using NoteList.ServiceLayer.IServices;
using NoteList.ServiceLayer.Models;
using System.Security.Claims;

namespace NoteList.ServiceLayer.Services
{
    public class ClaimService : IClaimService
    {
        #region Fileds

        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        public ClaimService(UserManager<IdentityUser<int>> userManager, SignInManager<IdentityUser<int>> signInManager, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
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

        #region GetUserClaimsAsync

        public async Task<IEnumerable<Claim>> GetUserClaimsAsync(IdentityUser<int> user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            return claims;
        }

        #endregion

        #region GetUserClaimsModel

        public async Task<UserClaimViewModel> GetUserClaimsModel(IdentityUser<int> user)
        {
            var model = new UserClaimViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Cliams = new List<UserClaim>()
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            var existingUserRolesClaims = (await Task.WhenAll(userRoles.Select(async role =>
            {
                var identityRole = await _roleManager.FindByNameAsync(role);
                return await _roleManager.GetClaimsAsync(identityRole);
            }))).SelectMany(claims => claims).ToList();


            var existingUserClaims = await GetUserClaimsAsync(user);

            var userClaims = ClaimsStore.GetAllClaims().Select(claim =>
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

                return userClaim;
            });

            model.Cliams.AddRange(userClaims);

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
                var logedUser = await FindUserByIdAsync(Convert.ToInt32(logedUserId));
                await _signInManager.RefreshSignInAsync(logedUser);
            }

            return true;
        }

        #endregion
    }
}
