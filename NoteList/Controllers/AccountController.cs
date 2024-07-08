using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteList.DomainLayer.Models;
using NoteList.ServiceLayer.IServices;

namespace NoteList.Controllers
{
    public class AccountController : Controller
    {
        #region Fields

        private readonly IAccountService _accountService;

        #endregion

        #region Constructor

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateAccountAsync(model);

                if (result.Succeeded)
                {
                    await _accountService.AddRoleToUser(model.Email);
                    await _accountService.SignInAccountAsync(model);

                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.PasswordSignInAccountAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                
                // Handle failure
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            return View(model);
        }

        #endregion

        #region Logout

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAccountAsync();
            return RedirectToAction("index", "home");
        }

        #endregion

    }
}
