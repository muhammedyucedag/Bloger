using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.Entity.Concrete;

namespace Bloger.DataAccess.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
        User GetUserByUsername(string username);
    }
}
