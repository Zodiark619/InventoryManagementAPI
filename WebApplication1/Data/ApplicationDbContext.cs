using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
  public DbSet<Category> Category {  get; set; }
  public DbSet<Product> Product {  get; set; }
  public DbSet<Supplier> Supplier {  get; set; }
  public DbSet<RefreshToken> RefreshToken {  get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>   options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles=new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id="11111111-1111-1111-1111-111111111111",
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole
                {
                    Id="22222222-2222-2222-2222-222222222222",
                    Name="User",
                    NormalizedName="USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

        
    }
}
