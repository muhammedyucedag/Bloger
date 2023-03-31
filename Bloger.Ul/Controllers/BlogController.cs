﻿using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Controllers
{
    public class BlogController : Controller
    {
        private BlogManager blogManager = new BlogManager(new EfBlogRepository());

        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = blogManager.GetBlogListWithCategory();
            return View(values);
        }

        [AllowAnonymous]
        public IActionResult BlogDetails(int id)
        {
            ViewBag.BlogId = id;
            var values = blogManager.GetBlogById(id);
            return View(values);
        }
    }
}
