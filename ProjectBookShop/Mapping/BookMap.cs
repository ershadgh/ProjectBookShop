using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Models;

namespace ProjectBookShop.Mapping
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookID);
            builder.Property(b=>b.Title).IsRequired(); 
            builder.Property(b=>b.Imagee).HasColumnType("image");
            builder.ToTable("BookInfo");

            builder
                .HasOne(d => d.Discount)
                .WithOne(b => b.Book)
                .HasForeignKey<Discount>(f=>f.BookID);

         



        }
    }
}
