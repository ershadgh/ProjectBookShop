using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectBookShop.Models.ViewModels
{
    public class BooksCreateEditViewModel
    {

      

        public BooksCreateEditViewModel()
        {

        }

        public BooksCreateEditViewModel(IEnumerable<TreeViewCategory>? categories)
        {
            Categories = categories;
        }

        public int BookId { get; set; }
       // public int[] CatagoryArray { get; set; }
        public IEnumerable<TreeViewCategory>? Categories { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن{0}الزامی است")]
        public string Title { get; set; }
        [Display(Name = "خلاصه")]
        public string? Summary { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "وارد کردن {0}الزامی است")]

        public decimal Price { get; set; } = 50000;
        [Display(Name = "موجودی")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است")]
        public int Stock { get; set; }

        public string? ImageBase64 { get; set; }
        public byte[]? Image { get; set; }
        [Display(Name = "تعداد صحفات")]
        public int NumOfPages { get; set; }
        [Display(Name = "وزن")]
        public short Weight { get; set; }
        [Display(Name = "شابک")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی و است. وتکراری نباشد")]
        public string? ISBN { get; set; }
        [Display(Name = "این کتاب روی این سایت منتشر شد")]
        public bool IsPublish { get; set; }
        [Display(Name = "سال انتشار")]
        public int PublisheYear { get; set; }
        [Display(Name = "زبان")]
        [Required(ErrorMessage = "وارد نمودن {0}الزامی است ")]
        public int LanguageID { get; set; }
        [Display(Name = "ناشر")]
        [Required(ErrorMessage = "وارد نمودن {0} الزمی است")]
        public int PublisherID { get; set; }
        //[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "نویسندگان")]
        public int[]? AuthorID { get; set; }
       
        [Display(Name = "مترجمان")]
        public int[]? TranslatorID { get; set; }
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name ="دسته بندی")]
        public int[]? CategoryID { get; set; }

        public DateTime PublisheDate { get; set; }

    }

    public class BooksIndexViewModel
    {

        public int BookID { get; set; }
        [Display(Name = "عنوان")]
        public string? Title { get; set; }
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }
        [Display(Name = "موجودی")]
        public int Stock { get; set; }
        [Display(Name = "شابک")]
        public string? ISBN { get; set; }
        [Display(Name = "ناشر")]
        public string? PublusherName { get; set; }
        [Display(Name = "تاریخ انتشار")]
        public string? PublisheDate { get; set; }
        [Display(Name = "وضعیت")]
        public string? Ispublise { get; set; }
        [Display(Name = "نویسنده")]
        public string? Author { get; set; }
        [Display(Name = "مترحمین")]
        public string? Translatorr { get; set; }
        [Display(Name = "دسته بندی")]
        public string? category { get; set; }
        [Display(Name = "زبان")]
        public string? Language { get; set; }

    }

    public class AuthorList
    {
        public int AuthorID { get; set; }
        public string? NameFamily { get; set; }
    }

    public class TranslatorList
    {
        public int TranslatorID { get; set; }
        public string? NameFamily { get; set; }
    }
    public class BooksAdvancedSearch
    {
        [Display(Name = "عنوان")]
        public string? Title { get; set; }
        [Display(Name = "شابک")]
        public string? ISBN { get; set; }
        [Display(Name = "نویسنده")]
        public string? Author { get; set; }
        [Display(Name = "مترحمین")]
        public string? Translatorr { get; set; }
        [Display(Name = "دسته بندی")]
        public string? category { get; set; }
        [Display(Name = "زبان")]
        public string? Language { get; set; }
        [Display(Name = "ناشر")]
        public string? PublusherName { get; set; }

    }
    public class ReadAllBook
    {
        public int? BookID { get; set; }
        public string? Title { get; set; }
        public string? Summery { get; set; }
        public Decimal? Price { get; set; }
        public int? Stock { get; set; }
        public int? NumOfPages { get; set; }
        public short? Weight { get; set; }
        public string? ISBN { get; set; }
        public bool? IsPublishe { get; set; }
        public int? PublisheYear { get; set; }
        public string? LanguageName { get; set; }
        public string? PublisherName { get; set; }
        public string? Authors { get; set; }
        public string? Transslators { get; set; }
        public string? Categories { get; set; }
        public DateTime PublisheDate { get; set; }
        public byte[]? Imagee { get; set; }
    }
    public class ProductsViewModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public string? TotalPrice { get; set; }

    }
}
