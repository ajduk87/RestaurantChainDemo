using RestaurantChainApp.Entities;
using System.Data;
using Dapper;
using System.Collections.Generic;


namespace RestaurantChainApp.Repositories
{
    public class OrderItemsRepository
    {

        public List<OrderItem> SelectOrderItems(IDbConnection connection, int orderid, IDbTransaction transaction = null)
        {

            return connection.Query<OrderItem>(Sql.Queries["SelectOrderItems"], new { orderid }).AsList();
        }

        public long Insert(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<long>(Sql.Queries["InsertOrderItem"], new
            {
                
                orderid = orderItem.OrderId,
                menuitemid = orderItem.MenuItemId,
                amount = orderItem.Amount,
                value = orderItem.Value
            });
        }

        public void DeleteByOrderId(IDbConnection connection, int orderid, IDbTransaction transaction = null)
        {
            connection.Execute(Sql.Queries["DeleteOrderItemsForOrderId"], new { orderid });
        }
    }
}
