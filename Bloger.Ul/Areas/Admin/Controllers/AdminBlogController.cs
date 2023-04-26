using Bloger.Business.Concrete;
using Bloger.Business.ValidationRules;
using Bloger.DataAccess.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using Bloger.Ul.Areas.Admin.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloger.Ul.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminBlogController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        private readonly IHttpContextAccessor _httpContext;
        private readonly Context _context;

        public AdminBlogController(IHttpContextAccessor httpContext, Context context)
        {
            _httpContext = httpContext;
            _context = context;
        }


        public IActionResult Index()
        {
            var values = blogManager.GetAdminBlogListWithCategory(x=>x.IsActive);
            return View(values);
        }

        public IActionResult Detail()
        {
            var values = blogManager.GetAdminBlogListWithCategory(x => x.IsActive);

            AdminControllerDetailBlogViewModel model = new AdminControllerDetailBlogViewModel
            {
                Blogs = values
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
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
        public async Task<IActionResult> Add(AdminCreateBlogViewModel model, IFormFile formFile)
        {
            BlogValidator validationRules = new BlogValidator();
            //ValidationResult results = validationRules.Validate(model);

            if (ModelState.IsValid)
            {
                var userId = _httpContext.HttpContext.Session.GetInt32("UserId") ?? 0;


                Blog blog = new Blog
                {
                    BlogContent = model.BlogContent,
                    BlogImage = model.BlogImage,
                    BlogTitle = model.BlogTitle,
                    CategoryId = model.CategoryId,
                    UserId = userId,
                    IsDeleted = false,
                    IsActive = true,
                    BlogCreateDate = DateTime.Now
                };

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

                return RedirectToAction("Index");

            }

            List<SelectListItem> categoryvalues = (from x in
                   categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.CategoryValues = categoryvalues;

            //foreach (var item in results.Errors)
            //{
            //    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //}
            return View();
        }


        [HttpPost]
        public IActionResult Passive(int id)
        {
            var project = blogManager.TGetById(id);
            project.IsDeleted = true;
            blogManager.Update(project);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Active(int id)
        {
            var project = blogManager.TGetById(id);
            project.IsDeleted = false;
            blogManager.Update(project);
            return RedirectToAction("Index");
        }
    }
}
