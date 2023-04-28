using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using Bloger.Ul.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bloger.Ul.Controllers
{
    [AllowAnonymous] // tanımlamış olduğum tüm kurallardan muaf ol
    public class LoginController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<LoginController> _logger;

        public LoginController(SignInManager<User> signInManager, IHttpContextAccessor httpContext, ILogger<LoginController> logger)
        {
            _signInManager = signInManager;
            _httpContext = httpContext;
            _logger = logger;
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
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("\"Kullanıcı hesabı kilitlendi.\"");
                    ModelState.AddModelError(string.Empty, "Hesabınız Kilitlendi.");
                }
		        else
		        {
                    ViewData["Error"] = "Kullanıcı adı yada şifre yanlış";
                    return View(); // 
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
