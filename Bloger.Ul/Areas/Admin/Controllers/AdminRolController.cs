using Bloger.Business.Concrete;
using Bloger.Entity.Concrete;
using Bloger.Ul.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Bloger.Ul.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
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

        public async Task<IActionResult> UserRoleList()
        {
            List<AdminControllerUserRoleListViewModel> model = new List<AdminControllerUserRoleListViewModel>();
            var users = userManager.Users.ToList();

            foreach (var item in users)
            {
                var roles = await userManager.GetRolesAsync(item);
                model.Add(new AdminControllerUserRoleListViewModel { User = item, Roles = (List<string>)roles });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = userManager.Users.FirstOrDefault(x => x.Id == id);
            var roles = roleManager.Roles.ToList();

            TempData["UserId"] = user.Id;

            var userRoles = await userManager.GetRolesAsync(user);

            List<AdminControllerRoleAssignViewModel> models = new List<AdminControllerRoleAssignViewModel>();
            foreach (var item in roles)
            {
                AdminControllerRoleAssignViewModel model = new AdminControllerRoleAssignViewModel();
                model.Id = item.Id;
                model.Name = item.Name;
                model.Exists = Convert.ToString(userRoles.Contains(item.Name));
                models.Add(model);
            }
            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<AdminControllerRoleAssignViewModel> model)
        {
            var userid = (int)TempData["UserId"];
            var user = userManager.Users.FirstOrDefault(x => x.Id == userid);
            foreach (var item in model)
            {
                if (bool.Parse(item.Exists))
                {
                    await userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }
            return RedirectToAction("UserRoleList");
        }
    }
}
