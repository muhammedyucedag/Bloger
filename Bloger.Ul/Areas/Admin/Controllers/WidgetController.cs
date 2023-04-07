using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Areas.Admin.Controllers
{
    public class WidgetController : Controller
    {
        [Area("Admin")]
        //[Authorize(Roles = "Admin,Moderator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
