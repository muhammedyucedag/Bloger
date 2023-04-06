using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using Bloger.Ul.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{
    [AllowAnonymous] // tanımlamış olduğum tüm kurallardan muaf ol
    public class LoginController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContext;

        public LoginController(SignInManager<User> signInManager, IHttpContextAccessor httpContext)
        {
            _signInManager = signInManager;
            _httpContext = httpContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignInViewModel signIn)
        {
	        UserManager userManager = new UserManager(new EfUserRepository());
	        if (ModelState.IsValid)
	        {
		        var result = await _signInManager.PasswordSignInAsync(signIn.username, signIn.password, false, true);
		        var user = userManager.GetByUsername(signIn.username);
		        if (result.Succeeded)
		        {
                    _httpContext.HttpContext.Session.SetInt32("UserId",user.Id); // id sessionda tutuyoruz
					return RedirectToAction("Index", "Blog");
				}
		        else
		        {
			        return RedirectToAction("Index", "Login");
		        }
	        }
	        return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            _httpContext.HttpContext.Session.Remove("UserId");        
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Blog");
        }
    }
}
