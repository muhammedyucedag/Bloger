using Bloger.Business.Concrete;
using Bloger.DataAccess.Concrete;
using Bloger.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloger.Ul.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminUserController : Controller
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        Context context = new Context();

        public IActionResult Index()
        {
            var values = userManager.GetList();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Lock(int id)
        {
            var applicatonUser = await context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (applicatonUser == null)
            {
                return NotFound();
            }
            applicatonUser.LockoutEnd = DateTime.Now.AddYears(1);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> UnLock(int id)
        {
            var applicatonUser = await context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (applicatonUser == null)
            {
                return NotFound();
            }
            applicatonUser.LockoutEnd = null;
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
