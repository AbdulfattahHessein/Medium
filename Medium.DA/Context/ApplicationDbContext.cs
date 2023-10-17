using Medium.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medium.DA.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publisher>(publisher =>
            {
                publisher.HasMany(p => p.Stories)
                .WithOne(s => s.Publisher)
                .HasForeignKey(s => s.PublisherId);
            });
        }
    }
}
