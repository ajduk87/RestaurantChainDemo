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


        public int Insert(IDbConnection connection, Order order, IDbTransaction transaction = null) 
        {
                return connection.ExecuteScalar<int>(Sql.Queries["InsertOrder"], new
                {
                    total = order.Total,
                    status = order.Status
                });
        }

        public void DeleteOrder(IDbConnection connection, int orderid, IDbTransaction transaction = null)
        {
            connection.Execute(Sql.Queries["DeleteOrder"], new { orderid });
        }
    }
}
