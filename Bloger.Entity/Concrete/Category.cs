using Bloger.Entity.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloger.Entity.Concrete
{
    public class Category : BaseEntity
    {
        // proprty tanımlamak için öncelikle erişim belirleyici türü - değişken türü - isim - {get set}

        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int Order { get; set; }

        public virtual List<Blog> Blogs { get; set; } // kategorinin birden fazla notları varıdr.

    }
}
