using Bloger.Business.Concrete;
using Bloger.DataAccess.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Bloger.Ul.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class CommentController : Controller
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        private readonly IHttpContextAccessor _httpContext;
        private readonly SignInManager<User> _signInManager;

        public CommentController(IHttpContextAccessor httpContext, SignInManager<User> signInManager)
        {
            _httpContext = httpContext;
            _signInManager = signInManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Success(int id)
        {
            return View(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult PartialAddComment(Comment comment)
        {
            var userId = _httpContext.HttpContext.Session.GetInt32("UserId") ?? 0;

            if (userId == 0)
            {
                return Json("K");
            }
            else
            {
                comment.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                comment.CommentStatus = true;
                comment.UserId = userId;
                commentManager.CommentAdd(comment);
            }

            return Json(Url.Action("Success", "Comment", new { id = comment.BlogId }));
        }

        public PartialViewResult CommentListByBlog(int id)
        {
            var values = commentManager.GetList(id);
            return PartialView();
        }
    }
}
