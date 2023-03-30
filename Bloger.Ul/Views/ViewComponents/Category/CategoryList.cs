using Bloger.Business.Concrete;
using Bloger.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Bloger.Ul.Views.ViewComponents.Category
{
    public class CategoryList:ViewComponent
    {
        private CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());

        public IViewComponentResult Invoke() // invoke Bir Class İçerisindeki methodları dinamik olarak çağırmaya yarar
        {
            var values = categoryManager.GetList();
            return View(values);
        }
    }
}
