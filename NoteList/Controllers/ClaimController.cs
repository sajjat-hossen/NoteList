using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteList.ServiceLayer.IServices;
using NoteList.ServiceLayer.Models;

namespace NoteList.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ClaimController : Controller
    {
        #region Fields

        private readonly IClaimService _claimService;

        #endregion

        #region Constructor

        public ClaimController(IClaimService administrationService)
        {
            _claimService = administrationService;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            var users = _claimService.GetAllUser();

            return View(users);
        }

        #endregion

        #region GetUserClaims

        [HttpGet]

        public async Task<IActionResult> GetUserClaims(int id)
        {
            var user = await _claimService.FindUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var model = await _claimService.GetUserClaimsModel(user);

            return Json(model);
        }

        #endregion

        #region UpdateUserClaims

        [HttpPost]

        public async Task<IActionResult> UpdateUserClaims(UserClaimViewModel model)
        {
            var user = await _claimService.FindUserByIdAsync(model.Id);

            if (user == null)
            {
                return NotFound(model);
            }

            var result = await _claimService.UpdateUserClaimsAsync(model);

            if (result == false)
            {
                return BadRequest(model);
            }

            return Ok();
        }

        #endregion
    }
}
