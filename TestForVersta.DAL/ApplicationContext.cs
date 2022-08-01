using Microsoft.EntityFrameworkCore;
using TestForVersta.DAL.Entities;

namespace TestForVersta.DAL;

/// <summary>
/// An application database context. <seealso cref="DbContext"/>
/// </summary>
public class ApplicationContext : DbContext
{
    /// <summary>
    /// Gets or inits a set for accessing orders. <seealso cref="DbSet{TEntity}"/>.
    /// </summary>
    public DbSet<Order> Orders { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="ApplicationContext"/>.
    /// </summary>
    public ApplicationContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ApplicationContext"/> using the specified options.
    /// </summary>
    /// <param name="options">Options for this context.</param>
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }


    /// <summary>
    /// Configures the data model.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
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
