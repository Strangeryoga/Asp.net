using Microsoft.EntityFrameworkCore;
using System;
using UserRegistration.Models;
namespace UserRegistration.Data
{
    public class UserDbContext : DbContext
    {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> user { get; set; }
    }
}
