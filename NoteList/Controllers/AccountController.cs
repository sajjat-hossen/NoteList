﻿using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteList.ServiceLayer.IServices;
using NoteList.ServiceLayer.Models;
using NoteList.ServiceLayer.ValidatorModels;

namespace NoteList.Controllers
{
    public class AccountController : Controller
    {
        #region Fields

        private readonly IAccountService _accountService;
        private static readonly ILog _log = LogManager.GetLogger(typeof(AccountController));

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
            _log.Info("This is register action inside account controller");
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            RegisterValidator validator = new RegisterValidator();
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid)
            {
                var result = await _accountService.CreateAccountAsync(model);

                if (result.Succeeded)
                {
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
            _log.Info("This is login action inside account controller");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            LoginValidator validator = new LoginValidator();
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid)
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

        #region ChangePassword

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            ChangePasswordValidator validator = new ChangePasswordValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return View(model);
            }

            var result = await _accountService.ChangePassword(model);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            return RedirectToAction("index", "home");

        }
        #endregion
    }
}
