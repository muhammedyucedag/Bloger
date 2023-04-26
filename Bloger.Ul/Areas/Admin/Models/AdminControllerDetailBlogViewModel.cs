using Bloger.Entity.Concrete;
using System.ComponentModel.DataAnnotations;


namespace Bloger.Ul.Areas.Admin.Models
{
    public class AdminControllerDetailBlogViewModel
    {
        public List<Blog> Blogs { get; set; }
    }
}
