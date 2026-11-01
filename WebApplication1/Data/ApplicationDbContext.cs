using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
  public DbSet<Category> Category {  get; set; }
  public DbSet<Product> Product {  get; set; }
  public DbSet<Supplier> Supplier {  get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }
    }
}
