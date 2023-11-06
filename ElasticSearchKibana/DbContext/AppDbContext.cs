using ElasticSearchKibana.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElasticSearchKibana.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ): base(options)
    {
        
    }
    public DbSet<Product> Products { get; set; }
    
}