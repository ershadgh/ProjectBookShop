using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Areas.Identity.Data;

namespace ProjectBookShop.Data
{
    public class IdentityDBContext : IdentityDbContext<ApplicationUser,ApplicationRole,string ,IdentityUserClaim<string>,ApplicationUserRole,IdentityUserLogin<string>,IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationRole>().ToTable("AspNetUsers").ToTable("AppRoles");

            builder.Entity<ApplicationUserRole>().ToTable("AppUserRole");

            builder.Entity<ApplicationUserRole>()
                .HasOne(userRole => userRole.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(f => f.RoleId);

            builder.Entity<ApplicationUser>().ToTable("AppUsers");

            builder.Entity<ApplicationUserRole>()
                .HasOne(UserRole => UserRole.User)
                .WithMany(User => User.Roles)
                .HasForeignKey(f => f.UserId);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
