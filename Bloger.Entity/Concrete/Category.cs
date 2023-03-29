using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloger.Entity.Concrete
{
    public class Category
    {
        // proprty tanımlamak için öncelikle erişim belirleyici türü - değişken türü - isim - {get set}

        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori Adını Boş geçemezsiniz")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Kategori Açıklamasını Boş geçemezsiniz")]
        public string CategoryDescription { get; set; }
        public bool CategoryStatus { get; set; }

        public virtual List<Blog> Blogs { get; set; } // kategorinin birden fazla notları varıdr.

    }
}
