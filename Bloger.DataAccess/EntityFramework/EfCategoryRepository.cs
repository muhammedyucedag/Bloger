using Bloger.DataAccess.Abstract;
using Bloger.DataAccess.Repositories;
using Bloger.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloger.DataAccess.EntityFramework
{
    public class EfCategoryRepository : GenericRepository<Category>, ICategoryDal
    {
    }
}
