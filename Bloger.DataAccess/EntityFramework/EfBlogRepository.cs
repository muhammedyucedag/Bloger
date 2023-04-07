using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.DataAccess.Abstract;
using Bloger.DataAccess.Concrete;
using Bloger.DataAccess.Repositories;
using Bloger.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Bloger.DataAccess.EntityFramework
{
    public class EfBlogRepository : GenericRepository<Blog>
    {
        public List<Blog> GetListWithCategory()
        {
            using (var context = new Context())
            {
                return context.Blog.Include(x => x.User).Include(x => x.Category).Where(x => x.BlogStatus).ToList();
            }
        }

        public List<Blog> GetListWithCategoryByUser(int id)
        {
            // user id göre blogları çekiyoruz ama çekerken categori ve userin bilgilerini inculde ediyoruz
            using (var context = new Context())
            {
                return context.Blog.Include(x => x.Category).Include(x => x.User).Where(x => x.User.Id == id && x.BlogStatus).ToList();
            }
        }

        public Blog GetBlogById(int id)
        {
            //çektiğim blog blogid göre gelecek bunu çekerken içerisindeki user ve category dolu gelecek.
            using (var context = new Context())
            {
                return context.Blog.Include(x => x.Comments).Include(x => x.Category).Include(x => x.User).FirstOrDefault(x => x.BlogId == id && x.BlogStatus);

            }
        }

        public int DeleteBlog(int id)
        {
            using (var context = new Context())
            {
                var blog = context.Blog.FirstOrDefault(x => x.BlogId == id);
                blog.BlogStatus = false;
                context.Update(blog);
                return context.SaveChanges();
            }
        }
    }
}
