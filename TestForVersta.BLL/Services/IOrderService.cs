using TestForVersta.BLL.Models;

namespace TestForVersta.BLL.Services;

/// <summary>
/// Defines a service that stores <see cref="Order"/> models.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Saves a passed <see cref="Order"/> model.
    /// </summary>
    /// <param name="orderModel">The model to add.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <exception cref="ArgumentNullException">OrderModel is null.</exception>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AddOrder(OrderInsertModel orderModel, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an <see cref="Order"/> with specified id.
    /// </summary>
    /// <param name="id">The identifier of <see cref="Order"/> model that we are looking for.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a nullable <see cref="Order"/> entity.</returns>
    public Task<Order?> GetOrder(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of <see cref="Order"/> entities.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains  list of <see cref="Order"/> entities.</returns>
    public Task<IList<Order>> GetOrders(CancellationToken cancellationToken = default);
}
