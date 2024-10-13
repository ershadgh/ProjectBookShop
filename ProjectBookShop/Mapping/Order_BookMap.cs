using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Models;

namespace ProjectBookShop.Mapping
{
    public class Order_BookMap : IEntityTypeConfiguration<Order_Book>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order_Book> builder)
        {
            builder.HasKey(t => new { t.BookID, t.OrderID });

            builder
                .HasOne(b => b.Book)
                .WithMany(t => t.order_Books)
                .HasForeignKey(f => f.BookID);

            builder
                .HasOne(o=>o.Order)
                .WithMany(t=>t.order_Books)
                .HasForeignKey(f=>f.OrderID);
        }
    }
}
