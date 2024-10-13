using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectBookShop.Models.ViewModels
{
    public class RolesViewModel
    {
        public string? RoleID { get; set; }
        [DisplayName("عنوان نقش  ")]
        [Required(ErrorMessage ="افزودن {0} الزامی است")]
        public string? RoleName { get; set; }
        [DisplayName("توضیحات ")]
        [Required(ErrorMessage = "افزودن {0} الزامی است")]
        public string? Description { get; set; }

        [Display(Name ="کاربران")]
        public int UserCount { get; set; }

        public string? RecentRoleName { get; set; }
    }
}
