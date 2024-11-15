using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Infrastuctures.Database

{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public required DbSet<Category> Categories { get; set; }
    }
}