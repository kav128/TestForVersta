using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestForVersta.DAL.Repositories;

namespace TestForVersta.DAL;

/// <summary>
/// Contains extension methods for data access layer.
/// </summary>
public static class ExtensionUtils
{
    /// <summary>
    /// Registers the data access layer services in the <see cref="IServiceCollection"/>. 
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="optionsAction">An optional action to configure the <see cref="DbContextOptions"/> for the context.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection UseDataAccessLayer(this IServiceCollection serviceCollection,
                                                        Action<DbContextOptionsBuilder>? optionsAction = null) =>
        serviceCollection.AddDbContext<ApplicationContext>(optionsAction)
                         .AddTransient<IOrderRepository, OrderRepository>();
}
