using System.ComponentModel.DataAnnotations;

namespace VILA.Web.Models.Customer
{
    public class RegisterModel
    {
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "موبایل اجباری است")]
        [MaxLength(11, ErrorMessage = "موبایل باید 11 رقم باشد .")]
        [MinLength(11, ErrorMessage = "موبایل باید 11 رقم باشد .")]
        public string Mobile { get; set; }


        [Display(Name = "رمز عبور ")]
        [Required(ErrorMessage = "رمز عبور اجباری است")]
        [MaxLength(90)]
        public string Pass { get; set; }
    }
}
