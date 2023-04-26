using Bloger.Business.Abstract;
using Bloger.Business.Concrete;
using Bloger.Business.ValidationRules;
using Bloger.DataAccess.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;
using Bloger.Ul.Areas.Admin.Models;
using FluentValidation;
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

                category.IsActive = true;
                category.IsDeleted = false;

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
            var category = categoryManager.TGetById(id);

            AdminControllerUpdateCategoryViewModel model = new AdminControllerUpdateCategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditCategory(AdminControllerUpdateCategoryViewModel model)
        {
            CategoryValidator validationRules = new CategoryValidator();
            var category = categoryManager.TGetById(model.CategoryId);
            category.CategoryId = model.CategoryId;
            category.CategoryName = model.CategoryName;
            category.CategoryDescription = model.CategoryDescription;
            ValidationResult results = validationRules.Validate(category);


            if (results.IsValid)
            {
                categoryManager.Update(category);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError("",item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Passive(int id)
        {
            var category = categoryManager.TGetById(id);
            category.IsDeleted = true;
            categoryManager.Update(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Active(int id)
        {
            var category = categoryManager.TGetById(id);
            category.IsDeleted = false;
            categoryManager.Update(category);
            return RedirectToAction("Index");
        }

    }
}
