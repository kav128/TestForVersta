using AutoMapper;

namespace TestForVersta.BLL.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<DAL.Entities.Order, Models.Order>();
        CreateMap<Models.OrderInsertModel, DAL.Entities.Order>();
    }
}
