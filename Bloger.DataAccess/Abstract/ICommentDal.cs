using Bloger.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloger.DataAccess.Abstract
{
    public interface ICommentDal:IGenericDal<Comment>
    {
        List<Comment> GetListWithBlog();
    }
}
