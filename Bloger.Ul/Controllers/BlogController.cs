using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{
    public class BlogController : Controller
    {
        private BlogManager blogManager = new BlogManager(new EfBlogRepository());
        public IActionResult Index()
        {
            var values = blogManager.GetBlogListWithCategory();
            return View(values);
        }
    }
}
