using AutoMapper;
using TestForVersta.BLL.Models;
using TestForVersta.DAL.Repositories;

namespace TestForVersta.BLL.Services;

/// <summary>
/// Represents a service that stores <see cref="Order"/> models.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="OrderService"/>.
    /// </summary>
    /// <param name="orderRepository">A repository tht is used to access the database.</param>
    /// <param name="mapper">A mapper to map BLL models to DAL entities.</param>
    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task AddOrder(OrderInsertModel orderModel, CancellationToken cancellationToken = default)
    {
        var order = _mapper.Map<DAL.Entities.Order>(orderModel);
        await _orderRepository.InsertOrder(order, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Order?> GetOrder(long id, CancellationToken cancellationToken = default)
    {
        var orderEntity = await _orderRepository.GetOrderById(id, cancellationToken);
        return orderEntity is null ? null : _mapper.Map<Order>(orderEntity);
    }

    /// <inheritdoc />
    public async Task<IList<Order>> GetOrders(CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetOrders(cancellationToken);
        return _mapper.Map<IList<Order>>(orders);
    }
}
