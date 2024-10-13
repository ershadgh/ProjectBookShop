using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Mapping;
using ProjectBookShop.Models.ViewModels;
//using System.Data.Entity.Infrastructure;


namespace ProjectBookShop.Models
{
    public class BookShopContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.;Database=MyDBooShopDB;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

        //}

        public BookShopContext(DbContextOptions<BookShopContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Author_BookMap());
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new Order_BookMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new Book_TranslatorMap());
            modelBuilder.ApplyConfiguration(new Invoices_Products());
            modelBuilder.ApplyConfiguration(new Book_CategoryMap());
            modelBuilder.Entity<ReadAllBook>().ToView("ReadAllBooks").HasNoKey();
            //modelBuilder.Entity<Book>().HasQueryFilter(a => a.DeleteBook == false);
            modelBuilder.Entity<Book>().HasQueryFilter(a => (bool)!a.DeleteBook);
            modelBuilder.Entity<Book>().Property(a => a.DeleteBook).HasDefaultValueSql("0");
            modelBuilder.Entity<Book>().Property(a => a.PublisheDate).HasDefaultValueSql("CONVERT(datetime,GETDATE())");
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author_Book> Author_Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Translator> Translator { get; set; }
        public virtual DbSet<Book_Translator> Book_Translators { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Order_Book> Order_Books { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Provice> Provices { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }
        public virtual DbSet<ChartData> ChartDatas { get; set; }
        public virtual DbSet<Book_Ctegory> Book_Ctegory { get; set; }
        public virtual DbSet<ReadAllBook> ReadAllBooks { get; set; }
        [DbFunction("GetAllAuthor", "dbo")]
        public static string GetAllAuthor(int BookId)
        {
            throw new NotImplementedException();
        }
        [DbFunction("GetAllTranstors", "dbo")]
        public static string GetAllTranstors(int BookId)
        {
            throw new NotImplementedException();
        }
        [DbFunction("GetAllCategorise", "dbo")]
        public static string GetAllCategorise(int BookId)
        {
            throw new NotImplementedException();
        }



    }
}
