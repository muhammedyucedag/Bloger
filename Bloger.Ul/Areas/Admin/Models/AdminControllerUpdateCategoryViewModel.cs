using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;

namespace Bloger.Ul.Areas.Admin.Models

{
    public class AdminControllerUpdateCategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori Adını Boş geçemezsiniz")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Kategori Açıklamasını Boş geçemezsiniz")]
        public string CategoryDescription { get; set; }
    }
}
