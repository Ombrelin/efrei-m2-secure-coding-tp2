using Microsoft.EntityFrameworkCore;
using VulnerableWebApplication.Models;

namespace VulnerableWebApplication.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    public ApplicationDbContext(DbContextOptions options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().HasKey(person => person.Id);
        modelBuilder.Entity<ApplicationUser>().Property(person => person.Username);
        modelBuilder.Entity<ApplicationUser>().Property(person => person.Password);
    }
}