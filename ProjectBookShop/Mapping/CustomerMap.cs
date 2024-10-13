using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectBookShop.Models;
using System.Reflection.Emit;

namespace ProjectBookShop.Mapping
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder
                .HasOne(p => p.city1)
                .WithMany(p => p.Customers1)
                .HasForeignKey(f => f.CityID1);

            builder
                .HasOne(C => C.city2)
                .WithMany(P => P.Customers2)
                .HasForeignKey(F => F.CityID2).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
