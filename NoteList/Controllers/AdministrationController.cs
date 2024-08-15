using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteList.ServiceLayer.IServices;
using NoteList.ServiceLayer.Models;

namespace NoteList.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdministrationController : Controller
    {
        #region Fields

        private readonly IAdministrationService _administrationService;

        #endregion

        #region Constructor

        public AdministrationController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            var users = _administrationService.GetAllUser();

            return View(users);
        }

        #endregion

        #region GetUserRoles

        [HttpGet]

        public async Task<IActionResult> GetUserRoles(int id)
        {
            var user = await _administrationService.FindUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var model = await _administrationService.GetUserRolesModel(user);

            return Json(model);
        }

        #endregion

        #region UpdateUserRoles

        [HttpPost]

        public async Task<IActionResult> UpdateUserRoles(UserRoleViewModel model)
        {
            var user = await _administrationService.FindUserByIdAsync(model.Id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _administrationService.UpdateUserRolesAsync(model);

            if (result == false)
            {
                return BadRequest(model);
            }

            return Ok();
        }

        #endregion

        #region UpdateRoleClaims

        [HttpGet]

        public async Task<IActionResult> GetRoleClaimsById(int id)
        {
            var model = await _administrationService.GetRoleClaimsAsync(id);

            return Json(model);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateRoleClaims(RoleClaimViewModel model)
        {
            var result = await _administrationService.UpdateRoleClaimsAsync(model);

            if (result == false)
            {
                return BadRequest(model);
            }

            return Ok();
        }

        #endregion
    }
}
