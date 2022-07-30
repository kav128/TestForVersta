using TestForVersta.BLL.Models;
using TestForVersta.DAL.Repositories;

namespace TestForVersta.BLL.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository) => _orderRepository = orderRepository;

    // TODO Add cancellation tokens
    public async Task AddOrder(OrderInsertModel orderModel)
    {
        // TODO Use AutoMapper
        var order = new DAL.Entities.Order
        {
            SenderCity = orderModel.SenderCity,
            SenderAddress = orderModel.SenderAddress,
            ReceiverCity = orderModel.ReceiverCity,
            ReceiverAddress = orderModel.ReceiverAddress,
            Weight = orderModel.Weight,
            DeliveryDate = orderModel.DeliveryDate
        };
        
        await _orderRepository.InsertOrder(order);
    }

    // TODO Add cancellation tokens
    public async Task<Order?> GetOrder(long id)
    {
        var orderEntity = await _orderRepository.GetOrderById(id);
        
        if (orderEntity is null) return null;
        return new Order
        {
            Id = orderEntity.Id,
            SenderCity = orderEntity.SenderCity,
            SenderAddress = orderEntity.SenderAddress,
            ReceiverCity = orderEntity.ReceiverCity,
            ReceiverAddress = orderEntity.ReceiverAddress,
            Weight = orderEntity.Weight,
            DeliveryDate = orderEntity.DeliveryDate
        };
    }

    // TODO Add cancellation tokens
    public async Task<IList<Order>> GetOrders()
    {
        var orders = await _orderRepository.GetOrders();
        return orders.Select(order => new Order
                      {
                          Id = order.Id,
                          SenderCity = order.SenderCity,
                          SenderAddress = order.SenderAddress,
                          ReceiverCity = order.ReceiverCity,
                          ReceiverAddress = order.ReceiverAddress,
                          Weight = order.Weight,
                          DeliveryDate = order.DeliveryDate
                      })
                     .ToList();
    }
}
