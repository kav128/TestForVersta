using AutoMapper;

namespace TestForVersta.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<BLL.Models.Order, Models.Order>();
        CreateMap<Models.OrderInsertModel, BLL.Models.OrderInsertModel>();
    }
}
