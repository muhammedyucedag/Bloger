using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{
    public class CommentController : Controller
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult PartialAddComment()
        {
            return PartialView();
        }

        public PartialViewResult CommentListByBlog(int id)
        {
            var values = commentManager.GetList(id);
            return PartialView();
        }
    }
}
