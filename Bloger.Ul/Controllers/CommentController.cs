using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult PartialAddComment()
        {
            return PartialView();
        }

        public PartialViewResult CommentListByBlog()
        {
            return PartialView();
        }
    }
}
