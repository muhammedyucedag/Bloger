using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.Entity.Concrete;

namespace Bloger.Business.Abstract
{
    public interface IBlogService:IGenericService<Blog>
    {
        // kategori ile blog listeleme
        List<Blog> GetBlogListWithCategory();

        // kullanıcıya göre blog listeleme
        List<Blog> GetBlogListByUser(int id);

        int DeleteBlog(int id);

    }
}
