using AgencyService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AgencyService.Data
{
    public class AgencyServiceDBContext : DbContext
    {
        public DbSet<ServiceOfferItem> OfferItems { get; set; }
        public DbSet<ServiceOffer> Offers { get; set; }
        public AgencyServiceDBContext(DbContextOptions<AgencyServiceDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgencyServiceDBContext).Assembly);
        }
    }
}
