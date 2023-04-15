using Bloger.Entity.Concrete;

namespace Bloger.Ul.Models
{
    public class BlogModel
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public IFormFile BlogImage { get; set; }
        public DateTime BlogCreateDate { get; set; }
        public bool BlogStatus { get; set; }
        public List<User> Users { get; set; }
        public List<Category> Categories { get; set; }
    }
}
