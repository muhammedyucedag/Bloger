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
    internal class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User item)
        {
            _userDal.Insert(item);
        }

        public void Delete(User item)
        {
            _userDal.Delete(item);
        }

        public void Update(User item)
        {
            _userDal.Update(item);
        }

        public List<User> GetList()
        {
            return _userDal.GetListAll();
        }

        public User TGetById(int id)
        {
            return _userDal.GetById(id);
        }

        public User GetByUsername(string username)
        {
            return _userDal.GetUserByUsername(username);
        }
    }
}
