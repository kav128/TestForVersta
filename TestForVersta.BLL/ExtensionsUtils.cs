using Microsoft.Extensions.DependencyInjection;
using TestForVersta.BLL.Services;

namespace TestForVersta.BLL;

public static class ExtensionsUtils
{
    public static IServiceCollection UseBusinessLogicLayer(this IServiceCollection serviceCollection) =>
        serviceCollection.AddTransient<IOrderService, OrderService>();
}
