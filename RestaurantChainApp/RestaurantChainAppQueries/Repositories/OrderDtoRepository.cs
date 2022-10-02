using System.Data;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using RestaurantChainAppQueries.Dtoes;

namespace RestaurantChainAppQueries.Repositories
{
    public class OrderDtoRepository
    {
        public OrderDto SelectOrder(IDbConnection connection, int orderid)
        {

            return connection.Query<OrderDto>(Sql.Queries["SelectOrder"], new { orderid }).Single();
        }

        public List<OrderItemDto> SelectOrderItems(IDbConnection connection, int orderid)
        {

            return connection.Query<OrderItemDto>(Sql.Queries["SelectOrderItems"], new { orderid }).AsList();
        }
    }
}
