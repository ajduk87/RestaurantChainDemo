using RestaurantChainApp.Entities;
using RestaurantChainApp.Enums;
using System.Collections.Generic;

namespace RestaurantChainApp.Dtoes
{
    public class OrderDto : Dto
    {
        public List<OrderItemDto> orderItems { get; set; }
        public double Total { get; set; }
        public OrderStatus Status { get; set; }

        public OrderDto()
        {
            orderItems = new List<OrderItemDto>();
        }
    }
}
