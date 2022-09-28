using System.Collections.Generic;
using RestaurantChainApp.Dtoes;

namespace RestaurantChainApp.Models.Order
{
    public class BaseOrderModel
    {
        public List<OrderItemDto> orderItems { get; set; }
        public double Total { get; set; }
        public int Status { get; set; }
    }
}
