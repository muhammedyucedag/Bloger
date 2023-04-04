using Bloger.Business.Concrete;
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

namespace Bloger.Ul.Controllers
{
    public class BlogController : Controller
    {
        private BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        private readonly IHttpContextAccessor _httpContext;


        public BlogController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
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
        [Route("blogdetails/{id}" )] // slug yapısı
        public IActionResult BlogDetails(int id)
        {
            ViewBag.BlogId = id;
            var values = blogManager.GetBlogById(id);
            return View(values);
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
        public async Task<IActionResult> BlogAdd(Blog blog,[FromForm]IFormFile[] formFile) // count 0 geliyor blogadd de form içindeki formfile id ile burası eşleşmiyor
            {
            BlogValidator validationRules = new BlogValidator();
            ValidationResult results = validationRules.Validate(blog);

            if (results.IsValid)
            {
                var userId = _httpContext.HttpContext.Session.GetInt32("UserId") ?? 0;

                blog.BlogStatus = true;
                blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.UserId = userId;

                if (formFile != null && formFile.Count() > 0)
                {
                    var file = formFile[0];
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
            var blogvalues = blogManager.TGetById(id);
            blogManager.Delete(blogvalues);

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
        public IActionResult EditBlog(Blog blog)
        {
            var userId = _httpContext.HttpContext.Session.GetInt32("UserId") ?? 0;

            blog.BlogStatus = true;
            blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            blog.UserId = userId;
            blogManager.Update(blog);
            return RedirectToAction("BlogListByWriter");
        }
    }
}
