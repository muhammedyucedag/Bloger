using Bloger.DataAccess.Concrete;
using Bloger.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.DataAccess.Abstract;
using Bloger.DataAccess.Repositories;

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
	}
}
