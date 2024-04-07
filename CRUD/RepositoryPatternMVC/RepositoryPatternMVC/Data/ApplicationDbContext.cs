using Microsoft.EntityFrameworkCore;
using RepositoryPatternMVC.Models;

namespace RepositoryPatternMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        { 
        
        }

        public DbSet<Emp> emps { get; set; } 
    }
}
