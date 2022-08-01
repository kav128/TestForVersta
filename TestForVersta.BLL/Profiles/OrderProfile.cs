using AutoMapper;

namespace TestForVersta.BLL.Profiles;

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
        CreateMap<DAL.Entities.Order, Models.Order>();
        CreateMap<Models.OrderInsertModel, DAL.Entities.Order>();
    }
}
