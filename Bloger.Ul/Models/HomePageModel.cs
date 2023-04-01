using Bloger.Entity.Concrete;

namespace Bloger.Ul.Models
{
    public class HomePageModel
    {
        public List<Blog> Blogs { get; set; }
        public List<Category> Categories { get; set; }
    }
}
