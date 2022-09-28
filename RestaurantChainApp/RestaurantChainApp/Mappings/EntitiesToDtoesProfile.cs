using AutoMapper;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;

namespace RestaurantChainApp.Mappings
{
    public class EntitiesToDtoesProfile : Profile
    {
        public EntitiesToDtoesProfile()
        {
            CreateMap<MenuItem, Dish>();
            CreateMap<MenuItem, Meal>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
