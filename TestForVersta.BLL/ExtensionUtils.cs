using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TestForVersta.BLL.Profiles;
using TestForVersta.BLL.Services;

namespace TestForVersta.BLL;

/// <summary>
/// Contains extension methods for business logic layer.
/// </summary>
public static class ExtensionUtils
{
    /// <summary>
    /// Registers the business logic layer services in the <see cref="IServiceCollection"/>. 
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection UseBusinessLogicLayer(this IServiceCollection serviceCollection) =>
        serviceCollection.AddTransient<IOrderService, OrderService>();

    /// <summary>
    /// Adds business logic layer mapping profiles to be accessible through <see cref="IMapper"/>.
    /// </summary>
    /// <param name="expression">The Automapper configuration expression that is used for configuring mapping.</param>
    /// <returns>The same mapper configuration so that multiple calls can be chained.</returns>
    public static IMapperConfigurationExpression AddProfilesFromBll(this IMapperConfigurationExpression expression)
    {
        expression.AddProfile<OrderProfile>();
        return expression;
    }
}
