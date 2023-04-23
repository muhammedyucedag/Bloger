using Bloger.DataAccess.Abstract;
using Bloger.DataAccess.Concrete;
using Bloger.DataAccess.Repositories;
using Bloger.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bloger.DataAccess.EntityFramework
{
    public class EfCategoryRepository : GenericRepository<Category>
    {
        public List<Category> GetCategoriesForHomePage()
        {
            using (var context = new Context())
            {
                return context.Category.Where(x=>x.CategoryStatus && x.Order > 0).Take(5).OrderBy(x=>x.Order).ToList();
            }
        }

        public List<Category> GetBlogOrCategoryNumber()
        {
            using (var context = new Context())
            {
                return context.Category.Include(x => x.Blogs).ToList();

            }
        }
    }
}
