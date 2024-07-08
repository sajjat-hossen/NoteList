using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteList.DomainLayer.Models;
using NoteList.Models;
using NoteList.ServiceLayer.IServices;
using System.Security.Claims;

namespace NoteList.Controllers
{
    [Authorize(Roles = "Admin")]
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

        #region ManagePermissions

        [HttpGet]
        public async Task<IActionResult> ManagePermissions(string id)
        {
            var user = await _administrationService.FindUserByIdAsync(id);

            if (user == null)
            {
                return View("NotFound");
            }

            var model = await _administrationService.GetUserClaimsModel(user);

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> ManagePermissions(UserClaimViewModel model)
        {
            var user = await _administrationService.FindUserByIdAsync(model.Id);

            if (user == null)
            {
                return View("NotFound");
            }

            var result = await _administrationService.UpdateUserClaimsAsync(model);

            if (result == false)
            {
                return View(model);
            }

            return RedirectToAction("index");
        }

        #endregion
    }
}
