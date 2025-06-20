using Microsoft.EntityFrameworkCore;
using PcGear.Database.Entities;
using PcGear.Infrastructure.Config;

namespace PcGear.Database.Context
{
    public class PcGearDatabaseContext : DbContext
    {

        public PcGearDatabaseContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(AppConfig.ConnectionStrings?.PcGearDatabase);
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
