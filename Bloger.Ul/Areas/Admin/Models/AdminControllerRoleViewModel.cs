using System.ComponentModel.DataAnnotations;

namespace Bloger.Ul.Areas.Admin.Models
{
    public class AdminControllerRoleViewModel
    {
        [Required(ErrorMessage = "Lütfen rol adını giriniz")]
        public string name { get; set; }
    }
}
