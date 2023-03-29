using Bloger.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<User> _userManager;

        public RegisterController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
