
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectBookShop.Models
{

    public class Book
    {
        private Language _language;
        private Publisher _publisher;
        private ILazyLoader _LazyLoader;
        public Book()
        {

        }
        public Book(ILazyLoader lazyLoader)
        {
            _LazyLoader = lazyLoader;
        }


        [Key]
        public int BookID { get; set; }

        public string? Title { get; set; }
        public string? Summery { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Fille { get; set; }
        public int NumOfPages { get; set; }
        public short weight { get; set; }
        public string? ISBN { get; set; }
        public bool IsPublishe { get; set; }
        public DateTime PublisheDate { get; set; }
        public int PublisheYear { get; set; }
        public int PublisherID { get; set; }
        [DefaultValue("0")]
        public bool? DeleteBook { get; set; }
        public byte[]? Imagee { get; set; }
        public int LanguageID { get; set; }
        public virtual Language? Language { get => _LazyLoader.Load(this, ref _language); set => _language = value; }
        public virtual Discount? Discount { get; set; }
        public virtual List<Author_Book>? Author_Books { get; set; }
        public virtual List<Order_Book>? order_Books { get; set; }
        public virtual List<Book_Translator>? book_Tranlators { get; set; }
        public virtual List<Book_Ctegory>? Book_Ctegories { get; set; }
        public virtual List<InvoiceDetails>? InvoiceDetails { get; set; }
        public virtual Publisher? Publisher { get => _LazyLoader.Load(this, ref _publisher); set => _publisher = value; }
 


    }
    public class Book_Ctegory
    {
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public virtual Book? Book { get; set; }
        public virtual Category? Category { get; set; }
    }
    public class Publisher
    {
        [Key]
        public int PublisherID { get; set; }
        [Display(Name = "ناشر")]
        [Required(ErrorMessage = "وارد نمودن{0} الزالمی است")]
        public string? PublisherName { get; set; }

        public virtual List<Book>? Books { get; set; }
        public virtual List<ChartData> ChartDatas { get; set; }
    }
    public class Book_Translator
    {
        public int TranslatorID { get; set; }
        public int BookID { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Translator? Translator { get; set; }
    }

    public class Translator
    {
        [Key]
        public int TranslatorID { get; set; }
        public string? Name { get; set; }
        public string? Family { get; set; }

        public virtual List<Book_Translator>? book_Tranlators { get; set; }
    }

    public class Author_Book
    {
        private Book _Book;
        private ILazyLoader LazyLoader { get; set; }
        public Author_Book()
        {

        }
        public Author_Book(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        public int BookID { get; set; }
        public int AuthorID { get; set; }
        public virtual Book? Book
        {
            get => this.LazyLoader.Load(this, ref _Book);
            set => _Book = value;
        }
        public virtual Author? Author { get; set; }
    }
    public class Author
    {
        private ILazyLoader lazyLoader;
        private List<Author_Book> _Author_Books;
        public Author()
        {

        }
        public Author(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }

        [Key]
        public int AuthorID { get; set; }
        [Display(Name = "نام نویسنده")]
        public string? FirstName { get; set; }
        [Display(Name = "نام فامیلی نویسنده")]
        public string? LastName { get; set; }
        public virtual List<Author_Book>? Author_Books
        {
            get => this.lazyLoader.Load(this, ref _Author_Books);
            set => _Author_Books = value;
        }

    }
    public class Language
    {
        [Key]
        public int LanguageID { get; set; }
        [DisplayName("زبان")]
        [Required(ErrorMessage = ("زبان  را وارد کنید"))]
        public string? LanguageName { get; set; }
        public virtual List<Book>? Books { get; set; }
    }
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string? CategoryName { get; set; }
        [ForeignKey("category")]
        public int? ParentCategoryID { get; set; }

        public virtual List<Category>? Categories { get; set; }
        public virtual Category? category { get; set; }
        public virtual List<Book_Ctegory>? Book_Ctegories { get; set; }


    }

    public class Order
    {
        public string? OrderID { get; set; }
        public long AmountPaid { get; set; }
        public string? DisPachtNumber { get; set; }
        public DateTime BuyDate { get; set; }
        public virtual OrderStatus? orderStatus { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual List<Order_Book>? order_Books { get; set; }

    }
    public class Order_Book
    {

        public int BookID { get; set; }
        public string? OrderID { get; set; }
        public virtual Book? Book { get; set; }
        public virtual Order? Order { get; set; }
    }
    public class OrderStatus
    {
        public int OrderStatusID { get; set; }
        public string? OrderStatusName { get; set; }
        public virtual List<Order>? Orders { get; set; }
    }
    public class Discount
    {
        [Key]
        public int BookID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte Percent { get; set; }
        public virtual Book? Book { get; set; }
    }
    public class Customer
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string? CustomserID { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Tel { get; set; }
        [Column(TypeName = "image")]
        public string? Image { get; set; }
        public string? PostalCode1 { get; set; }
        public string? PostalCode2 { get; set; }
        public int CityID1 { get; set; }
        public int CityID2 { get; set; }
        public virtual City? city1 { get; set; }
        public virtual City? city2 { get; set; }
        public virtual List<Order>? Orders { get; set; }
       // public virtual List<Invoice>? Invoices { get; set; }

    }
    public class Provice
    {


        [Key]
        public int ProviceID { get; set; }

        [Display(Name = "نام استان")]
        public string? ProviceName { get; set; }
        public virtual List<City>? Cities { get; set; }

    }
    public class City
    {


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int CityID { get; set; }
        [Display(Name = "نام شهر")]
        public string? CityName { get; set; }
        [ForeignKey("Provice")]
        public int ProviceID { get; set; }
        public virtual Provice? Provice { get; set; }
        public virtual List<Customer>? Customers1 { get; set; }
        public virtual List<Customer>? Customers2 { get; set; }
    }
    //فاکتورها
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int MyProperty { get; set; }
      //  public int CustomserID { get; set; }
        public virtual List<InvoiceDetails>? InvoiceDetails{get;set;}
    }
    //جدول واسط محصول(کتاب) با فاکتور
    //جدول شامل تعداد محصول و قیمت واحد هم هست که ممکن است با قیمت موجود در جدول محصولات متفاوت باشد (به دلیل تخفیف‌ها یا تغییرات قیمت در طول زمان)
    public class InvoiceDetails
    {
        public int InvoiceID { get; set; }
        public int BookID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public virtual Book? Book { get; set; }
        public virtual Invoice? Invoice { get; set; }
    }
    public class ChartData
    {
        [Key]
        public virtual int UserID { get; set; }
        public virtual int SentCount { get; set; }
        public virtual int? Number { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual int PublisherID { get; set; }

    }


}
