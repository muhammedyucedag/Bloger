using Bloger.Business.Concrete;
using Bloger.DataAccess.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Bloger.Ul.Controllers
{
    public class CommentController : Controller
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        private readonly IHttpContextAccessor _httpContext;

        public CommentController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string PartialAddComment(Comment comment)
        {
            string message = "";
            var userId = _httpContext.HttpContext.Session.GetInt32("UserId") ?? 0;

            if (userId < 1)
            {
                message = "Kullanıcı hatası";
            }
            else
            {
                comment.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                comment.CommentStatus = true;
                comment.UserId = userId;
                commentManager.CommentAdd(comment);
            }

            return message;
        }

        public PartialViewResult CommentListByBlog(int id)
        {
            var values = commentManager.GetList(id);
            return PartialView();
        }
    }
}
