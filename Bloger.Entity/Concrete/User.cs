using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bloger.Entity.Concrete
{
    public class User : IdentityUser<int>
    {
        public string NameSurname { get; set; }
        public string CoverLetter { get; set; }
        public string? ImageUrl { get; set; }

        public virtual List<Blog> Blogs { get; set; } // Kullanıcının birden fazla notları varıdr.
        public virtual List<Comment> Comments { get; set; } // Kullanıcının birden fazla yorumu varıdr.
    }
}
