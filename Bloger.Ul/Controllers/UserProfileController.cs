using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using Bloger.Ul.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{

    public class UserProfileController : Controller
    {
        private BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        UserManager userManager = new UserManager(new EfUserRepository());
        private readonly IHttpContextAccessor _httpContext;

        private readonly UserManager<User> _userManager;

        public UserProfileController(IHttpContextAccessor httpContext, UserManager<User> userManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
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

        [HttpGet]
        public async Task<IActionResult> UserEditProfile()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.mail = values.Email;
            model.namesurname = values.NameSurname;
            model.imageurl = values.ImageUrl;
            model.username = values.UserName;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserEditProfile(UserUpdateViewModel model)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            values.NameSurname = model.namesurname;
            values.ImageUrl = model.imageurl;
            values.Email = model.mail;

            if (!model.changepassword)
            {
                values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.password);
            }

            var result = await _userManager.UpdateAsync(values);
            return RedirectToAction("Index", "UserProfile");
        }
    }
}
