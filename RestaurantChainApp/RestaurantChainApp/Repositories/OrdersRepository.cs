using RestaurantChainApp.Entities;
using System.Data;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantChainApp.Repositories
{
    public class OrdersRepository
    {
        public Order SelectOrder(IDbConnection connection, int orderid, IDbTransaction transaction = null)
        {

            return connection.Query<Order>(Sql.Queries["SelectOrder"], new { orderid }).Single();
        }

        public bool Exists(IDbConnection connection, int id, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<bool>(Sql.Queries["ExistsOrder"], new { id });
        }


        public long Insert(IDbConnection connection, Order order, IDbTransaction transaction = null) 
        {
                return connection.ExecuteScalar<long>(Sql.Queries["InsertOrder"], new
                {
                    total = order.Total,
                    status = order.Status
                });
        }

        public void Update(IDbConnection connection, Order order, IDbTransaction transaction = null)
        {
            connection.ExecuteScalar(Sql.Queries["UpdateOrder"], new
            {
                id = order.Id,
                total = order.Total,
                status = order.Status
            });
        }
    }
}
