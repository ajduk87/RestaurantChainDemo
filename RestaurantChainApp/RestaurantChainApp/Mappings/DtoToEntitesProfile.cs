using AutoMapper;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;

namespace RestaurantChainApp.Mappings
{
    public class DtoToEntitesProfile : Profile
    {
        public DtoToEntitesProfile()
        {
            CreateMap<Dish, MenuItem>();
            CreateMap<Meal, MenuItem>();

            CreateMap<OrderDto, Order>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
