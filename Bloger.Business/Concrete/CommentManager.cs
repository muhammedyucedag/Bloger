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
    public class CommentManager:ICommentService
    {
        private EfCommentRepository _commentRepository;

        public CommentManager(EfCommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void CommentAdd(Comment comment)
        {
            _commentRepository.Insert(comment);
        }

        public List<Comment> GetList(int id)
        {
           return _commentRepository.GetListAll(x => x.BlogId == id);
        }
    }
}
