using App_List.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App_List.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AppListItemModel> AppListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppListItemModel>(entity =>
            {
                entity.Property(e => e.ItemName)
                .IsRequired()
                .HasMaxLength(100);
                
                entity.Property(e => e.ItemDescription)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.ItemStatus)
                .IsRequired()
                .HasMaxLength(1);
            });
            base.OnModelCreating(builder);
        }
    }
}
