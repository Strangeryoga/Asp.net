using Microsoft.EntityFrameworkCore;
using StoredProceduresCoreMVCB1.Models;

namespace StoredProceduresCoreMVCB1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        { 
        }

        public DbSet<Product> products { get; set; }
    }
}
