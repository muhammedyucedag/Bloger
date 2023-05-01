using Bloger.Business.Concrete;
using Bloger.Entity.Concrete;
using Bloger.Ul.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRolController : Controller
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;

        public AdminRolController(RoleManager<Role> roleManager, UserManager<User> userManager = null)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var values = roleManager.Roles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdminControllerRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Role role = new Role
                {
                    Name = model.name,
                    IsActive = true,
                    IsDeleted = false
                };

                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var values = roleManager.Roles.FirstOrDefault(x => x.Id == id);
            AdminControllerRoleUpdateViewModel model = new AdminControllerRoleUpdateViewModel
            {
                Id = values.Id,
                name = values.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(AdminControllerRoleUpdateViewModel model)
        {
            var values = roleManager.Roles.Where(x => x.Id == model.Id).FirstOrDefault();

            values.Name = model.name;
            values.IsDeleted = false;
            values.IsActive = true;

            var result = await roleManager.UpdateAsync(values);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult UserRoleList()
        {
            var values = userManager.Users.ToList();
            return View(values);
        }
    }
}
