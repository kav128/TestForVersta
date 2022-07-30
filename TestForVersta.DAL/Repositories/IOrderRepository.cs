using TestForVersta.DAL.Entities;

namespace TestForVersta.DAL.Repositories;

public interface IOrderRepository
{
    public Task<Order?> GetOrderById(long id, CancellationToken cancellationToken = default);
    public Task<IList<Order>> GetOrders(CancellationToken cancellationToken = default);
    public Task InsertOrder(Order order, CancellationToken cancellationToken = default);
}
