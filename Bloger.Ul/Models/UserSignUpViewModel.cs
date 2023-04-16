
using System.ComponentModel.DataAnnotations;

namespace Bloger.Ul.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Lütfen Ad Soyad Girişi Sağlayınız")]
        [StringLength(60, ErrorMessage = "{0} alanı maksimum {1} karakter olabilir!")]
        public string NameSurname { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen Şifre Girişi Sağlayınız")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "{0} alanı maksimum {1} karakter olabilir ve en az 1 karakter girişi sağlamanız gerekir!")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor!")] // eşeleşme
        [StringLength(16, MinimumLength = 6, ErrorMessage = "{0} alanı maksimum {1} karakter olabilir en az 1 karakter girişi sağlamanız gerekir!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage = "Lütfen Mail Girişi Sağlayınız")]
        [EmailAddress(ErrorMessage = "{0} alanı geçerli bir e-posta adresi giriniz!")]
        [StringLength(60, ErrorMessage = "{0} alanı maksimum {1} karakter olabilir!")]
        public string Mail { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen Kullanıcı Adı Girişi Sağlayınız")]
        [StringLength(30, ErrorMessage = "{0} alanı maksimum {1} karakter olabilir!")]
        public string UserName { get; set; }

        [Display(Name = "Ön Yazı")]
        [Required(ErrorMessage = "Lütfen Ön Yazı Girişi Sağlayınız")]
        [StringLength(130, MinimumLength = 6, ErrorMessage = "{0} alanı maksimum {1} karakter olabilir!")]
        public string CoverLetter { get; set; }

        [Display(Name = "Sözleşme")]
        [Required(ErrorMessage = "Sayfamıza kayıt olabilmek için gizlilik sözleşmesini kabul etmeniz gerekmektedir.")]
        public bool IsAcceptTheContract { get; set; }
    }
}
