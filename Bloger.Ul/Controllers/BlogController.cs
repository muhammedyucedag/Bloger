﻿using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Configuration;
using System.Reflection.Metadata;
using Bloger.Business.ValidationRules;
using Bloger.Ul.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Bloger.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Bloger.Ul.Controllers
{
    public class BlogController : Controller
    {
        private BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        private readonly IHttpContextAccessor _httpContext;
        private readonly Context _context;

        public BlogController(IHttpContextAccessor httpContext, Context context)
        {
            _httpContext = httpContext;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            HomePageModel model = new HomePageModel();
            var blogs = blogManager.GetBlogListWithCategory();
            var categories = categoryManager.GetCategoriesForHomePage();
            model.Blogs = blogs;
            model.Categories = categories;
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }
            
        [AllowAnonymous]
        [Route("blogdetails/{id}")] // slug yapısı
        public async Task<IActionResult> BlogDetails(int id)
        {
            BlogDetailsViewModel model = new BlogDetailsViewModel();
            ViewBag.BlogId = id;
            var blog = blogManager.GetBlogById(id);
            var category = categoryManager.GetBlogOrCategoryNumber();
            model.Categories = category;
            model.Blog = blog;

            if (model == null)
            {
                return RedirectToAction("Error404", "ErrorPage");
            }
            return View(model);
        }

        public async Task<IActionResult> BlogListByWriter()
        {
            // userin id sini tutan bir sesion değer set işlemini login controller de aldık
            // eğer session verisi boş gelire sıfır ataması yap

            var userId = _httpContext.HttpContext.Session.GetInt32("UserId") ?? 0;
            var userblogs = blogManager.GetListWithCategoryByUserBm(userId);

            return View(userblogs);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryvalues = (from x in
                    categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.CategoryValues = categoryvalues;
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> BlogAdd(Blog blog, IFormFile formFile)
        {
            BlogValidator validationRules = new BlogValidator();
            ValidationResult results = validationRules.Validate(blog);

            if (results.IsValid)
            {
                var userId = _httpContext.HttpContext.Session.GetInt32("UserId") ?? 0;

                blog.IsDeleted = false;
                blog.IsActive = true;
                blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.UserId = userId;

                try
                {
                    if (formFile != null && formFile.Length > 0)
                    {
                        var file = formFile;
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CoverImage", fileName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            blog.BlogImage = "/CoverImage/" + fileName;
                        }
                    }
                }
                catch (Exception error)
                {

                    return Content(error.ToString());
                }


                blogManager.Add(blog);

                return RedirectToAction("BlogListByWriter", "Blog");

            }

            List<SelectListItem> categoryvalues = (from x in
                   categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.CategoryValues = categoryvalues;

            foreach (var item in results.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            blogManager.DeleteBlog(id);
            return RedirectToAction("BlogListByWriter", "Blog");
        }

        public IActionResult EditBlog(int id)
        {
            var blogvalue = blogManager.TGetById(id);
            List<SelectListItem> categoryValues = (from x in categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();

            ViewBag.CategoryValues = categoryValues;
            return View(blogvalue);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(Blog blog, IFormFile formFile)
        {
            BlogValidator validationRules = new BlogValidator();
            ValidationResult results = validationRules.Validate(blog);
            if (results.IsValid)
            {
                var userId = _httpContext.HttpContext.Session.GetInt32("UserId") ?? 0;

                blog.IsDeleted = false;
                blog.IsActive = true;
                blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.UserId = userId;
                if (formFile != null && formFile.Length > 0)
                {
                    var file = formFile;
                    if (file.Length > 0)
                    {
                        var fileName = file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CoverImage", fileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        blog.BlogImage = "/CoverImage/" + fileName;
                    }
                }
                blogManager.Update(blog);
                return RedirectToAction("BlogListByWriter");
            }
            return View();
        }
    }
}
