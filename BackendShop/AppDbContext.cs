using Microsoft.EntityFrameworkCore;
using Models;

namespace ShopBackend;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Product> Products => Set<Product>();
}