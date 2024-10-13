using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectBookShop.Mapping
{
    public class Book_CategoryMap : IEntityTypeConfiguration<Book_Ctegory>
    {
        

        public void Configure(EntityTypeBuilder<Book_Ctegory> builder)
        {
            builder.HasKey(t => new { t.BookID, t.CategoryID });
            builder.HasOne(p => p.Book)
                .WithMany(c => c.Book_Ctegories)
                .HasForeignKey(f => f.BookID);
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Book_Ctegories)
                .HasForeignKey(f => f.CategoryID);
        }
    }

}
