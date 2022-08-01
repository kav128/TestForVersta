using AutoMapper;
using TestForVersta.Profiles;

namespace TestForVersta;

/// <summary>
/// Contains extension methods for presentation layer.
/// </summary>
public static class ExtensionUtils
{
    /// <summary>
    /// Adds presentation layer mapping profiles to be accessible through <see cref="IMapper"/>.
    /// </summary>
    /// <param name="expression">The Automapper configuration expression that is used for configuring mapping.</param>
    /// <returns>The same mapper configuration so that multiple calls can be chained.</returns>
    public static IMapperConfigurationExpression AddProfilesFromPresentation(
        this IMapperConfigurationExpression expression)
    {
        expression.AddProfile<OrderProfile>();
        return expression;
    }
}
