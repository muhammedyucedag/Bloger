﻿using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Views.ViewComponents.Blog
{
    public class BlogLast3Post : ViewComponent
    {
        private BlogManager blogManager = new BlogManager(new EfBlogRepository());

        public IViewComponentResult Invoke()
        {
            var values = blogManager.GetLast3Blog();
            return View(values);
        }
    }
}
