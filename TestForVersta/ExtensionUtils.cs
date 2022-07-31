using AutoMapper;
using TestForVersta.Profiles;

namespace TestForVersta;

public static class ExtensionUtils
{
    public static IMapperConfigurationExpression AddProfilesFromPresentation(this IMapperConfigurationExpression expression)
    {
        expression.AddProfile<OrderProfile>();
        return expression;
    }
}
