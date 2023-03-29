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
    public class BlogManager : IBlogService
    {
        IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public void Add(Blog item)
        {
            _blogDal.Insert(item);
        }

        public void Delete(Blog item)
        {
            _blogDal.Delete(item);
        }

        public void Update(Blog item)
        {
            _blogDal.Update(item);
        }

        public List<Blog> GetList()
        {
            return _blogDal.GetListAll();
        }

        public Blog TGetById(int id)
        {
            return _blogDal.GetById(id);
        }

        public List<Blog> GetBlogListWithCategory()
        {
            return _blogDal.GetListWithCategory();
        }

        public List<Blog> GetBlogListByUser(int id)
        {
            return _blogDal.GetListAll(x => x.User!.Id == id).ToList();
        }
    }
}
