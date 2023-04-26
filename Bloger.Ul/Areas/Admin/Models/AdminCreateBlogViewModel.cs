using System.ComponentModel.DataAnnotations;

namespace Bloger.Ul.Areas.Admin.Models
{
    public class AdminCreateBlogViewModel
    {
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public string? BlogImage { get; set; }
        public int CategoryId { get; set; }


    }
}
