using TestForVersta.BLL.Models;

namespace TestForVersta.BLL.Services;

public interface IOrderService
{
    // TODO Add cancellation tokens
    public Task AddOrder(OrderInsertModel orderModel);
    
    // TODO Add cancellation tokens
    public Task<Order?> GetOrder(long id);
    
    // TODO Add cancellation tokens
    public Task<IList<Order>> GetOrders();
}
