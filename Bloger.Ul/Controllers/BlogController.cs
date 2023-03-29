using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
