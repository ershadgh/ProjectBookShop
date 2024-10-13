using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectBookShop.Models;

namespace ProjectBookShop.Mapping
{
    public class Book_TranslatorMap : IEntityTypeConfiguration<Book_Translator>
    {
        public void Configure(EntityTypeBuilder<Book_Translator> builder)
        {
            builder.HasKey(t => new {t.BookID,t.TranslatorID});
            builder
                .HasOne(b => b.Book)
                .WithMany(t => t.book_Tranlators)
                .HasForeignKey(f => f.BookID);
            builder
               .HasOne(l => l.Translator)
               .WithMany(t => t.book_Tranlators)
               .HasForeignKey(f => f.TranslatorID);
        }
    }
}
