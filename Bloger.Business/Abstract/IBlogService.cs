using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bloger.Entity.Concrete;

namespace Bloger.Business.Abstract
{
    public interface IBlogService:IGenericService<Blog>
    {
        // kategori ile blog listeleme
        List<Blog> GetBlogListWithCategory();
        List<Blog> GetAdminBlogListWithCategory(Expression<Func<Blog, bool>> expression);

        // kullanıcıya göre blog listeleme
        List<Blog> GetBlogListByUser(int id);

        int DeleteBlog(int id);

    }
}
