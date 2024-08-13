using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteList.DomainLayer.Models;
using NoteList.ServiceLayer.IServices;
using NoteList.ServiceLayer.ValidatorModels;

namespace NoteList.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    public class RoleController : Controller
    {
        #region Fields

        private readonly IRoleService _roleService;

        #endregion

        #region Constructor

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRoles();
            return View(roles);
        }

        #endregion

        #region Create

        [HttpPost]
        public async Task<IActionResult> Create(CreateRole model)
        {
            CreateRoleValidator validator = new CreateRoleValidator();
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid)
            {
                var result = await _roleService.CreateRoleAsync(model);

                if (result == true)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            await _roleService.DeleteRoleAsync(id);

            return Ok();
        }

        #endregion
    }
}
