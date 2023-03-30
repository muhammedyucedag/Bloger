using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.Business.Abstract;
using Bloger.DataAccess.Abstract;
using Bloger.Entity.Concrete;

namespace Bloger.Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category item)
        {
            _categoryDal.Insert(item);
        }

        public void Delete(Category item)
        {
            _categoryDal.Delete(item);
        }

        public void Update(Category item)
        {
            _categoryDal.Update(item);
        }

        public List<Category> GetList()
        {
            return _categoryDal.GetListAll();
        }

        public Category TGetById(int id)
        {
            return _categoryDal.GetById(id);
        }
    }
}
