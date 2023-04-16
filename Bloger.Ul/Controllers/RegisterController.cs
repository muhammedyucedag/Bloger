using Bloger.Entity.Concrete;
using Bloger.Ul.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<User> _userManager;

        public RegisterController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel userSignUpViewModel)
        {

            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Email = userSignUpViewModel.Mail,
                    UserName = userSignUpViewModel.UserName,
                    NameSurname = userSignUpViewModel.NameSurname,
                    CoverLetter = userSignUpViewModel.CoverLetter

                };

                var result = await _userManager.CreateAsync(user, userSignUpViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(userSignUpViewModel);
        }
    }
}
