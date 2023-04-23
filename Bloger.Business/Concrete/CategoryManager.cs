using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.Business.Abstract;
using Bloger.DataAccess.Abstract;
using Bloger.DataAccess.Concrete;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;

namespace Bloger.Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private EfCategoryRepository _categoryRepository;

        public CategoryManager(EfCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Add(Category item)
        {
            _categoryRepository.Insert(item);
        }

        public void Delete(Category item)
        {
            _categoryRepository.Delete(item);
        }

        public void Update(Category item)
        {
            _categoryRepository.Update(item);
        }

        public List<Category> GetList()
        {
            return _categoryRepository.GetListAll();
        }

        public Category TGetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public List<Category> GetCategoriesForHomePage()
        {
            return _categoryRepository.GetCategoriesForHomePage();
        }
        public List<Category> GetBlogOrCategoryNumber() 
        { 
            return _categoryRepository.GetBlogOrCategoryNumber();
        }
    }
}
