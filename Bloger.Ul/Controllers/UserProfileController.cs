using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{
    
    public class UserProfileController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
