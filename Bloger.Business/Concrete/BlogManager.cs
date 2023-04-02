using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.Business.Abstract;
using Bloger.DataAccess.Abstract;
using Bloger.DataAccess.EntityFramework;
using Bloger.Entity.Concrete;

namespace Bloger.Business.Concrete
{
    public class BlogManager : IBlogService
    {
        private EfBlogRepository _blogRepository = new EfBlogRepository();

        public BlogManager(EfBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public void Add(Blog item)
        {
            _blogRepository.Insert(item);
        }

        public void Delete(Blog item)
        {
            _blogRepository.Delete(item);
        }

        public void Update(Blog item)
        {
            _blogRepository.Update(item);
        }

        public List<Blog> GetList()
        {
            return _blogRepository.GetListAll();
        }

        public Blog TGetById(int id)
        {
            return _blogRepository.GetById(id);
        }

        public List<Blog> GetLast3Blog()
        {
            return _blogRepository.GetListAll().TakeLast(3).ToList();
        }

        public List<Blog> GetBlogListWithCategory()
        {
            return _blogRepository.GetListWithCategory();
        }

        public List<Blog> GetListWithCategoryByUserBm(int id)
        {
            return _blogRepository.GetListWithCategoryByUser(id);
        }

        public List<Blog> GetBlogListByUser(int id)
        {
            return _blogRepository.GetListAll(x => x.User.Id == id).ToList();
        }

        public Blog GetBlogById(int id)
        {
            return _blogRepository.GetBlogById( id);
        }
    }
}
