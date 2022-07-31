using TestForVersta.BLL.Models;

namespace TestForVersta.BLL.Services;

public interface IOrderService
{
    public Task AddOrder(OrderInsertModel orderModel, CancellationToken cancellationToken = default);
    public Task<Order?> GetOrder(long id, CancellationToken cancellationToken = default);
    public Task<IList<Order>> GetOrders(CancellationToken cancellationToken = default);
}
