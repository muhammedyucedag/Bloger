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
            HomePageModel model = new HomePageModel();
            var userId = _httpContext.HttpContext.Session.GetInt32("UserId") ?? 0;
            var userblogs = blogManager.GetListWithCategoryByUserBm(userId);
            var users = userManager.GetByNameSurname(userId);

            model.Blogs = userblogs;
            ViewBag.NameSurname = users;

            return View(model);
        }

        public IActionResult AuthorPosts(int id)
        {
            HomePageModel model = new HomePageModel();
            var userId = userManager.GetUserById(id);
            var userblogs = blogManager.GetListWithCategoryByUserBm(id) ;
            var users = userManager.GetByNameSurname(id);

            model.Blogs = userblogs;
            model.Users = userId;
            ViewBag.NameSurname = users;

            return View(model);
        }

    }
}
