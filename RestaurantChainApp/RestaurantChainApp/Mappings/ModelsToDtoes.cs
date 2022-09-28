using AutoMapper;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;
using RestaurantChainApp.Models.Order;

namespace RestaurantChainApp.Mappings
{
    public class ModelsToDtoes : Profile
    {
        public ModelsToDtoes()
        {
            CreateMap<CreateOrderModel, OrderDto>();
            CreateMap<UpdateOrderModel, OrderDto>();
        }
    }
}
