using Microsoft.EntityFrameworkCore;
using TestForVersta.DAL.Entities;

namespace TestForVersta.DAL;

public class ApplicationContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public ApplicationContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }
}
