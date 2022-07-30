using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestForVersta.DAL.Repositories;

namespace TestForVersta.DAL;

public static class ExtensionsUtils
{
    public static IServiceCollection UseDataAccessLayer(this IServiceCollection serviceCollection,
                                                        Action<DbContextOptionsBuilder>? optionsAction = null) =>
        serviceCollection.AddDbContext<ApplicationContext>(optionsAction)
                         .AddTransient<IOrderRepository, OrderRepository>();
}
