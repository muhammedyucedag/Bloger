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
    public class EfCommentRepository : GenericRepository<Comment>,ICommentDal
    {
        public List<Comment> GetListWithBlog()
        {
            using (var context = new Context())
            {
                return context.Comment.Include(x=>x.Blog).ToList();
            }
        }
    }
}
