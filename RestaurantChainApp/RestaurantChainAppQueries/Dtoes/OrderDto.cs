using System.Collections.Generic;

namespace RestaurantChainAppQueries.Dtoes
{
    public class OrderDto : Dto
    {
        public List<OrderItemDto> orderItems { get; set; }
        public double Total { get; set; }
        public int Status { get; set; }

        public OrderDto()
        {
            orderItems = new List<OrderItemDto>();
        }
    }
}
