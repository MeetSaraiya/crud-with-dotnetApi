using Microsoft.EntityFrameworkCore;

namespace crud_with_dotnetApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        }

        public DbSet<Employee> Employees { get; set; }

    }
}
