using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectBookShop.Models;

namespace ProjectBookShop.Mapping
{
    public class Author_BookMap : IEntityTypeConfiguration<Author_Book>
    {
        public void Configure(EntityTypeBuilder<Author_Book> builder)
        {
            builder.HasKey(t=>new {t.BookID,t.AuthorID});

            builder
                .HasOne(b => b.Book)
                .WithMany(a => a.Author_Books)
                .HasForeignKey(a => a.BookID);
            builder
                .HasOne(a => a.Author)
                .WithMany(p => p.Author_Books)
                .HasForeignKey(f => f.AuthorID);

        }
    }
}
