using BitcoinPaymentService.Models;
using Microsoft.EntityFrameworkCore;

namespace BitcoinPaymentService.Data
{
    public class BitcoinDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Merchant>? Merchants { get; set; }
        public DbSet<Transaction>? Transactions { get; set; }
        public BitcoinDbContext(DbContextOptions<BitcoinDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BitcoinDbContext).Assembly);
        }
    }
}
