using InvoiceAPI.BusinessLogic.Models;
using InvoiceAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<InvoiceEntity> Invoices { get; set; }

        public DbSet<InvoiceDetailsEntity> InvoiceDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InvoiceDetailsEntity>()
                .HasKey(e => e.DetailId);

            modelBuilder.Entity<InvoiceDetailsEntity>()
                .HasOne(e => e.Invoice)
                .WithMany(i => i.InvoiceDetails)
                .HasForeignKey(e => new { e.InvoiceId, e.LocationId });

        }


    }
}
