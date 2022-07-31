using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TestForVersta.BLL.Profiles;
using TestForVersta.BLL.Services;

namespace TestForVersta.BLL;

public static class ExtensionsUtils
{
    public static IServiceCollection UseBusinessLogicLayer(this IServiceCollection serviceCollection) =>
        serviceCollection.AddTransient<IOrderService, OrderService>();

    public static IMapperConfigurationExpression AddProfilesFromBLL(this IMapperConfigurationExpression expression)
    {
        expression.AddProfile<OrderProfile>();
        return expression;
    }
}
