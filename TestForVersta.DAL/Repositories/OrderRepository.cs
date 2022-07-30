using Microsoft.EntityFrameworkCore;
using TestForVersta.DAL.Entities;

namespace TestForVersta.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    private ApplicationContext _context;

    public OrderRepository(ApplicationContext context) => _context = context;

    public async Task<Order?> GetOrderById(long id,
                                           CancellationToken cancellationToken = default) =>
        await _context.Orders.FirstOrDefaultAsync(order => order.Id == id,
                                                  cancellationToken);

    public async Task<IList<Order>> GetOrders(CancellationToken cancellationToken = default) =>
        await _context.Orders.ToListAsync(cancellationToken);

    public async Task InsertOrder(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
