using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
