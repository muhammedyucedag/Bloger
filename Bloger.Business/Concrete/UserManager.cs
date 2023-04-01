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
    public class UserManager : IUserService
    {
        private EfUserRepository _userRepository;

        public UserManager(EfUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User item)
        {
            _userRepository.Insert(item);
        }

        public void Delete(User item)
        {
            _userRepository.Delete(item);
        }

        public void Update(User item)
        {
            _userRepository.Update(item);
        }

        public List<User> GetList()
        {
            return _userRepository.GetListAll();
        }

        public User TGetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }
    }
}
