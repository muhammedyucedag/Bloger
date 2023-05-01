using Bloger.Entity.Concrete;

namespace Bloger.Ul.Areas.Admin.Models
{
    public class AdminControllerUserRoleListViewModel
    {
        public User User { get; set; }

        public List<string> Roles { get; set; }
    }
}
