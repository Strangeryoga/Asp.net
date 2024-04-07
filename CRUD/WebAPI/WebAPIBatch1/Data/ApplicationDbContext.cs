using Microsoft.EntityFrameworkCore;
using WebAPIBatch1.Models;

namespace WebAPIBatch1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        { 
        
        }

        public DbSet<Product> products { get; set; }
    }
}
