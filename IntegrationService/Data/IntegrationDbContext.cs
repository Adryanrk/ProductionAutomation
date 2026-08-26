using IntegrationService.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegrationService.Data;

public class IntegrationDbContext : DbContext
{
    public DbSet<IntegrationOrder> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=production.db");
    }
}