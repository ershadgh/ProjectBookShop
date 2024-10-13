using System.ComponentModel.DataAnnotations;

namespace ProjectBookShop.Models.ViewModels
{
    public class TranslatorCreateViewModel
    {
        [Key]
        public int TranslatorID { get; set; }
        [Display(Name ="نام")]
        [Required(ErrorMessage =".وارد نمودن {0} الزامی است")]
        public string? Name { get; set; }

        [Display(Name ="نام خانوادگی")]
        [Required(ErrorMessage = ".وارد نمودن {0} الزامی است")]
        public string? Family { get; set; }

       
    }
}
