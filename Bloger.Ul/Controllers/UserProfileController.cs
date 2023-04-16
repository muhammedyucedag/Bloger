using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using Bloger.Ul.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{

    public class UserProfileController : Controller
    {
        private BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        UserManager userManager = new UserManager(new EfUserRepository());
        private readonly IHttpContextAccessor _httpContext;

        public UserProfileController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public IActionResult Index()
        {
            UserDetailViewModel model = new UserDetailViewModel();
            var userId = _httpContext.HttpContext.Session.GetInt32("UserId") ?? 0;
            var userblogs = blogManager.GetListWithCategoryByUserBm(userId);
            var user = userManager.TGetById(userId);
            model.Blogs = userblogs;
            model.User = user;

            return View(model);
        }

        public IActionResult AuthorPosts(int id)
        {
            UserDetailViewModel model = new UserDetailViewModel();
            var userblogs = blogManager.GetListWithCategoryByUserBm(id);
            var user = userManager.TGetById(id);
            model.Blogs = userblogs;
            model.User = user;

            return View(model);
        }
    }
}
