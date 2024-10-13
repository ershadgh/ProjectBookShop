using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectBookShop.Mapping
{
    // IEntityTypeConfiguration
    public class Invoices_Products : IEntityTypeConfiguration<InvoiceDetails>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetails> builder)
        {
            builder.HasKey(t => new { t.BookID, t.InvoiceID });
            builder.HasOne(b => b.Book)
                .WithMany(m => m.InvoiceDetails)
                .HasForeignKey(f => f.BookID);

            builder.HasOne(i => i.Invoice)
                .WithMany(m => m.InvoiceDetails)
                .HasForeignKey(f => f.InvoiceID);
        }
    }
}
