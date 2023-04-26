using Bloger.Business.Concrete;
using Bloger.Business.ValidationRules;
using Bloger.DataAccess.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;
using X.PagedList;

namespace Bloger.Ul.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin,Moderator")]
    public class AdminCategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        Context context = new Context();
        public IActionResult Index(int page = 1)
        {
            var values = categoryManager.GetList().ToPagedList(page, 5); // sayfalama işlemi
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            CategoryValidator validationRules = new CategoryValidator();
            ValidationResult results = validationRules.Validate(category);
            if (results.IsValid)
            {

                category.CategoryStatus = true;

                categoryManager.Add(category);

                return RedirectToAction("Index", "Category");

            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var blogvalue = categoryManager.TGetById(id);
            return View(blogvalue);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            CategoryValidator validationRules = new CategoryValidator();
            ValidationResult results = validationRules.Validate(category);

            if (results.IsValid)
            {
                var x = categoryManager.TGetById(category.CategoryId);
                category.CategoryId = x.CategoryId;
                category.CategoryStatus = true;

                categoryManager.Update(category);
                return RedirectToAction("Index");

            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult status(int id, bool status)
        {
            var category = categoryManager.TGetById(id);
            category.CategoryStatus = status;
            category.CategoryId = id;
            categoryManager.Update(category);

            var result = category.CategoryName + " " + "Pasif oldu";

            if (status)
            {
                result = category.CategoryName + " " + "Aktif oldu";
            }

            return Json(result);
        }

    }
}
