using Microsoft.EntityFrameworkCore;
using TestForVersta.DAL.Entities;

namespace TestForVersta.DAL.Repositories;

/// <summary>
/// Represents a repository that accesses a db table for <see cref="Order"/> entities.
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly ApplicationContext _context;

    /// <summary>
    /// Initializes a new instance of <see cref="OrderRepository"/>.
    /// </summary>
    /// <param name="context"></param>
    public OrderRepository(ApplicationContext context) => _context = context;

    /// <inheritdoc />
    public async Task<Order?> GetOrderById(long id,
                                           CancellationToken cancellationToken = default) =>
        await _context.Orders.FirstOrDefaultAsync(order => order.Id == id,
                                                  cancellationToken);

    /// <inheritdoc />
    public async Task<IList<Order>> GetOrders(CancellationToken cancellationToken = default) =>
        await _context.Orders.ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task InsertOrder(Order order, CancellationToken cancellationToken = default)
    {
        if (order is null) throw new ArgumentNullException(nameof(order));

        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
