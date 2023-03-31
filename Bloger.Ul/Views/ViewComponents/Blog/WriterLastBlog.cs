using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Views.ViewComponents.Blog
{
    public class WriterLastBlog:ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());

        public IViewComponentResult Invoke()
        {
            var values = blogManager.GetBlogListByUser(1);
            return View(values);
        }
    }
}
