﻿using Bloger.Ul.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloger.Ul.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Kullanıcı henüz giriş yapmamışsa, Login sayfasına yönlendir
                return RedirectToAction("Login", "Index");
            }
            else
            {
                // Kullanıcının oturumunun süresi dolmuşsa, SessionExpired sayfasına yönlendir
                return RedirectToAction("SessionExpired", "Home");
            }
        }

        public IActionResult SessionExpired()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}