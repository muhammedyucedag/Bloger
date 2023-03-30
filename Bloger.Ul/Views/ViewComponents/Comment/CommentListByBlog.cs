using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Views.ViewComponents.Comment
{
    public class CommentListByBlog:ViewComponent
    {
        CommentManager comment = new CommentManager(new EfCommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var values = comment.GetList(id);
            return View(values);
        }
    }
}
