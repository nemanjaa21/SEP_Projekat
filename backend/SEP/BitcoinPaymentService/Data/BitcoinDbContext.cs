﻿using Microsoft.EntityFrameworkCore;

namespace BitcoinPaymentService.Data
{
    public class BitcoinDbContext : DbContext
    {
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
