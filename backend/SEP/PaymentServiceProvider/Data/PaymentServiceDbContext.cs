using Microsoft.EntityFrameworkCore;
using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Data
{
    public class PaymentServiceDbContext : DbContext
    {
        public DbSet<PaymentService>? PaymentServices { get; set; }
        public PaymentServiceDbContext(DbContextOptions<PaymentServiceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentServiceDbContext).Assembly);
        }
    }
}
