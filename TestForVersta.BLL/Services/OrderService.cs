using AutoMapper;
using TestForVersta.BLL.Models;
using TestForVersta.DAL.Repositories;

namespace TestForVersta.BLL.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task AddOrder(OrderInsertModel orderModel, CancellationToken cancellationToken = default)
    {
        var order = _mapper.Map<DAL.Entities.Order>(orderModel);
        await _orderRepository.InsertOrder(order, cancellationToken);
    }

    public async Task<Order?> GetOrder(long id, CancellationToken cancellationToken = default)
    {
        var orderEntity = await _orderRepository.GetOrderById(id, cancellationToken);
        return orderEntity is null ? null : _mapper.Map<Order>(orderEntity);
    }

    public async Task<IList<Order>> GetOrders(CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetOrders(cancellationToken);
        return _mapper.Map<IList<Order>>(orders);
    }
}
