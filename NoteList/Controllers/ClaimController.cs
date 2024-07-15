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

        #region ManagePermissions

        [HttpGet]

        public async Task<IActionResult> ManageClaims(int id)
        {
            var user = await _claimService.FindUserByIdAsync(id);

            if (user == null)
            {
                return View("NotFound");
            }

            var model = await _claimService.GetUserClaimsModel(user);

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> ManageClaims(UserClaimViewModel model)
        {
            var user = await _claimService.FindUserByIdAsync(model.Id);

            if (user == null)
            {
                return View("NotFound");
            }

            var result = await _claimService.UpdateUserClaimsAsync(model);

            if (result == false)
            {
                return View(model);
            }

            return RedirectToAction("index");
        }

        #endregion
    }
}
