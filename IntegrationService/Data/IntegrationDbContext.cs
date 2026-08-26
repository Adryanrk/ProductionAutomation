using IntegrationService.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegrationService.Data;

public class IntegrationDbContext : DbContext
{
    public DbSet<IntegrationOrder> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string databasePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../../../../Database/production.db"));

        optionsBuilder.UseSqlite($"Data Source={databasePath}");
    }
}