using TestForVersta.DAL.Entities;

namespace TestForVersta.DAL.Repositories;

/// <summary>
/// Defines a repository that accesses a db table for <see cref="Order"/> entities.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Gets an <see cref="Order"/> with specified.
    /// </summary>
    /// <param name="id">The identifier of <see cref="Order"/> entity that we are looking for.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a nullable <see cref="Order"/> entity.</returns>
    public Task<Order?> GetOrderById(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of <see cref="Order"/> entities.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains  list of <see cref="Order"/> entities.</returns>
    public Task<IList<Order>> GetOrders(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves a passed <see cref="Order"/> entity in the database.
    /// </summary>
    /// <param name="order">The entity to add.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task InsertOrder(Order order, CancellationToken cancellationToken = default);
}
