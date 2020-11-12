using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace ProductsProject.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
