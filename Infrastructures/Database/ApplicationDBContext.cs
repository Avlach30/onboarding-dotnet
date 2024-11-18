using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Infrastuctures.Database

{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public required DbSet<Category> Categories { get; set; }

        public required DbSet<Product> Products { get; set; }

        public required DbSet<User> Users { get; set; }

        public required DbSet<Order> Orders { get; set; }

        public required DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}