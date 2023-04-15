using Bloger.DataAccess.Concrete;
using Bloger.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.DataAccess.Abstract;
using Bloger.DataAccess.Repositories;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace Bloger.DataAccess.EntityFramework
{
    public class EfUserRepository : GenericRepository<User>
    {
        public User GetUserByUsername(string username)
        {
            using (var context = new Context())
            {
                return context.Users.FirstOrDefault(x => x.UserName == username)!;
            }
        }

        public string GetUserByNameSurname(int id)
        {
            using (var context = new Context())
            {
                //return context.Users.FirstOrDefault(x => x.Id == id);
                return context.Users.Where(x => x.Id == id).Select(w => w.NameSurname).FirstOrDefault()!;
            }
        }

        public List<User> GetUserById(int id)
        {
            
            using (var context = new Context())
            {
                return context.Users.Include(x => x.Comments).Where(x => x.Id == id).ToList();

            }
        }
    }
}
