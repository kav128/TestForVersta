using AutoMapper;

namespace TestForVersta.Profiles;

/// <summary>
/// Provides a mapper configuration for <see cref="Models.Order"/> and connected models.
/// </summary>
public class OrderProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of <see cref="OrderProfile"/>.
    /// </summary>
    public OrderProfile()
    {
        CreateMap<BLL.Models.Order, Models.Order>();
        CreateMap<Models.OrderInsertModel, BLL.Models.OrderInsertModel>();
    }
}
