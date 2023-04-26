using Bloger.Entity.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloger.Entity.Concrete
{
    public class Blog : BaseEntity
    {
        [Key]
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public string? BlogImage { get; set; }
        public DateTime BlogCreateDate { get; set; }
        public bool BlogStatus { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public List<Comment> Comments { get; set; }


    }
}
