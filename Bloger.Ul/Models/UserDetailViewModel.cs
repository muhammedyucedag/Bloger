using Bloger.Entity.Concrete;

namespace Bloger.Ul.Models
{
    public class UserDetailViewModel
    {
        public List<Blog> Blogs { get; set; }
        public User User { get; set; }
    }
}
