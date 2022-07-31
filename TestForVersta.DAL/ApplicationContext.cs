using Microsoft.EntityFrameworkCore;
using TestForVersta.DAL.Entities;

namespace TestForVersta.DAL;

public class ApplicationContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasKey(order => order.Id);
        modelBuilder.Entity<Order>().Property(order => order.SenderCity).IsRequired().HasMaxLength(20);
        modelBuilder.Entity<Order>().Property(order => order.SenderAddress).IsRequired().HasMaxLength(64);
        modelBuilder.Entity<Order>().Property(order => order.ReceiverCity).IsRequired().HasMaxLength(20);
        modelBuilder.Entity<Order>().Property(order => order.ReceiverAddress).IsRequired().HasMaxLength(64);
        modelBuilder.Entity<Order>().Property(order => order.Weight).IsRequired();
        modelBuilder.Entity<Order>().HasCheckConstraint("CK_Weights",
                                                        "[Weight] > 0",
                                                        constraintBuilder =>
                                                            constraintBuilder.HasName("CK_Orders_Weights"));
        
        modelBuilder.Entity<Order>().HasCheckConstraint("CK_DeliveryDate",
                                                        "DATEDIFF(dd, '2020-01-01', [DeliveryDate]) >= 0",
                                                        constraintBuilder =>
                                                            constraintBuilder.HasName("CK_Orders_DeliveryDates"));
    }
}
