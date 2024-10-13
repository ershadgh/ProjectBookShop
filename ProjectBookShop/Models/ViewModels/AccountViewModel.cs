using System.ComponentModel.DataAnnotations;

namespace ProjectBookShop.Models.ViewModels
{
    public class RegisterViewModel

    {
        [Required(ErrorMessage ="وارد نمودن {0} الزامی است")]
        [EmailAddress(ErrorMessage ="ایمیل شما نامعتبر است")]
        [Display(Name ="ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage ="وارد نمودن {0} الزامی است")]
        [StringLength(100,ErrorMessage =" {0}باید دارای حداقل {0} کاراکتر و حداکثر دارای {2} کاراکتر باشد",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="کلمه عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="تکرار کلمه عبور")]
        [Compare("Password",ErrorMessage="کلمه عبور وارد شده با تکرار کلمه عبور مطاقبت ندارد.")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="نام کاریری")]
        [Required(ErrorMessage =" وارد نمودن {0} الزامی است")]
        public string? UserName { get; set; }

        [Display(Name ="شماره موبایل")]
        [Required(ErrorMessage ="وارد نمودن {0} الزامی است")]
        public string PhoneNumber { get; set; }
    }
}
