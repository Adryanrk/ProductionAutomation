using Microsoft.EntityFrameworkCore;
using OrderSystem.Models;

namespace OrderSystem.Data;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string databasePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../../../../Database/production.db"));

        optionsBuilder.UseSqlite($"Data Source={databasePath}");
    }

}