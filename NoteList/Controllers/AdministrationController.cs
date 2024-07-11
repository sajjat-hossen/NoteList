using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteList.DomainLayer.Models;
using NoteList.ServiceLayer.IServices;
using NoteList.ServiceLayer.Services;

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

        #region ManageRoles

        [HttpGet]

        public async Task<IActionResult> ManageRoles(string id)
        {
            var user = await _administrationService.FindUserByIdAsync(id);

            if (user == null)
            {
                return View("NotFound");
            }

            var model = await _administrationService.GetUserRolesModel(user);

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> ManageRoles(UserRoleViewModel model)
        {
            var user = await _administrationService.FindUserByIdAsync(model.Id);

            if (user == null)
            {
                return View("NotFound");
            }

            var result = await _administrationService.UpdateUserRolesAsync(model);

            if (result == false)
            {
                return View(model);
            }

            return RedirectToAction("index");
        }

        #endregion

        #region UpdateRoleClaims

        [HttpGet]

        public async Task<IActionResult> UpdateRoleClaims()
        {
            var models = await _administrationService.GetRoleClaimsAsync();
            var x = 10;

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoleClaims(List<RoleClaimViewModel> models)
        {
            //if (models[0].RoleClaims[0].ClaimType == "Create Role")
            //{
            //    var x = 5;
            //}
            var result = await _administrationService.UpdateRoleClaimsAsync(models);

            if (result == false)
            {
                return View(models);
            }

            return RedirectToAction("index");
        }

        #endregion
    }
}
