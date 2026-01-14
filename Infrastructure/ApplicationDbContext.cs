using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.MobilePhone)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.JobTitle)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.BirthDate)
                      .IsRequired();
            });
        }
    }
}